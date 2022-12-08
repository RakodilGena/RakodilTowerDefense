using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RakodilTowerDefense.Config.Configs.ExplosionsConfig;
using RakodilTowerDefense.Config.Configs.Global;
using RakodilTowerDefense.Domain.GameClasses.Enemies;

namespace RakodilTowerDefense.Domain.GameClasses.Explosions;

public class TeslaLightning : StaticExplosion
{
    #region Fields

    private readonly IReadOnlyList<Lightning> _lightnings;

    private int _currentSparkFrame,
        _currentLightningFrame;

    #endregion

    #region Properties

    private new TeslaLightningConfig Config => (TeslaLightningConfig)base.Config;

    #endregion

    #region Methods

    public TeslaLightning(Vector2 towerPosition, string configName, IReadOnlyList<Enemy> targets) : base(towerPosition,
        configName)
    {
        //creating a lightning for each target.
        //first lightning starts from tesla tower, others - from prev enemy to next one.
        var count = targets.Count;
        var lightnings = new List<Lightning>(count);

        _currentSparkFrame = 0;
        _currentLightningFrame = 0;
        
        var sparkRectangle = GetSparkRectangle();

        var random = new Random();
        var config = Config;

        GameObject source = this;
        for (int i = 0; i < count; i++)
        {
            var receiver = targets[i];
            lightnings.Add(
                new Lightning(
                    source,
                    receiver,
                    config,
                    sparkRectangle,
                    random));

            source = receiver;
        }

        _lightnings = lightnings;
    }



    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        //base call draws spark at tower position (static explosion is used as a shoot spark);
        base.Draw(gameTime, spriteBatch);

        //this draws all lightnings.
        var count = _lightnings.Count;
        for (int i = 0; i < count; i++)
            _lightnings[i].Draw(spriteBatch);
    }

    /// <summary>
    /// Returns current spark rectangle based on current spark frame.
    /// </summary>
    /// <returns></returns>
    private Rectangle GetSparkRectangle()
    {
        var totalFrames = Config.SparkFrames;

        return GetCurrentFrameRectangle(_currentSparkFrame,
            totalFrames: totalFrames,
            texture: Config.SparkTexture);
    }

    public override void Update(GameTime gameTime)
    {
        //this updates total draw time.
        base.Update(gameTime);

        UpdateSparkFrame();
        UpdateBeamFrame();
    }


    /// <summary>
    /// This decides if its time to change current spark frame of each lightning.
    /// </summary>
    private void UpdateSparkFrame()
    {
        //gets spark frame number based on draw time.
        var currentSparkFrameNumber = GetCurrentFrameNumber(Config.SparkFrames);

        if (currentSparkFrameNumber == _currentSparkFrame)
            return;

        //if number changed - we have to tell all the single lightnings that it has to draw other peace of its spark texture.
        _currentSparkFrame = currentSparkFrameNumber;

        //getting spark rectangle for all the lightning sparks.
        var sparkRect = GetSparkRectangle();

        var count = _lightnings.Count;
        for (int i = 0; i < count; i++)
            _lightnings[i].SetSparkFrame(sparkRect);
        
    }

    /// <summary>
    /// This decides if its time to change current beam frame of each lightning.
    /// </summary>
    private void UpdateBeamFrame()
    {
        //gets lightning frame number based on draw time.
        var currentLightningFrameNumber = GetCurrentFrameNumber(Config.LightningFrames);

        if (currentLightningFrameNumber == _currentLightningFrame)
            return;

        //if number changed - we have to tell all the single lightnings that it has to draw other peace of its beam texture.
        _currentLightningFrame = currentLightningFrameNumber;

        var random = new Random();
        
        var count = _lightnings.Count;
        for (int i = 0; i < count; i++)
            _lightnings[i].SetNewBeamFrame(random);
    }
    

    #endregion


    /// <summary>
    /// Inside class that represents single lightning from one target to another with spark of hitting last target.
    /// Can itself draw lightning and spark.
    /// </summary>
    private class Lightning
    {
        private readonly GameObject _source;
        private readonly GameObject _receiver;

        private readonly TeslaLightningConfig _config;

        private Rectangle
            _beamRectangle,
            _sparkRectangle;

        private SpriteEffects _beamEffect;


        internal Lightning(
            GameObject source,
            GameObject receiver,
            TeslaLightningConfig config,
            Rectangle sparkFrame,
            Random random)
        {
            _source = source;
            _receiver = receiver;
            _config = config;

            SetSparkFrame(sparkFrame);
            SetNewBeamFrame(random);
        }

        /// <summary>
        /// Sets spar frame to draw.
        /// </summary>
        /// <param name="sparkFrame"></param>
        internal void SetSparkFrame(Rectangle sparkFrame)
        {
            _sparkRectangle = sparkFrame;
        }

        /// <summary>
        /// Randomly sets frame of lightning texture to use in draw method and also sets flipEffect.
        /// </summary>
        /// <param name="random"></param>
        internal void SetNewBeamFrame(Random random)
        {
            var beamFrames = _config.BeamFrames;

            var lightningFrame = beamFrames == 1 ? 0 : random.Next(0, beamFrames);

            _beamRectangle =
                GetCurrentFrameRectangle(lightningFrame,
                    totalFrames: beamFrames,
                    texture: _config.BeamTexture);

            //this will decide to flip beam (1) or not(0).
            _beamEffect = (SpriteEffects)random.Next(0, 2);

            // var beamFlip = random.Next(0, 2);
            // _beamEffect = beamFlip switch
            // {
            //     0 => SpriteEffects.None,
            //     _ => SpriteEffects.FlipHorizontally
            // }
        }

        /// <summary>
        /// Draws lightning and spark of hit.
        /// </summary>
        /// <param name="spriteBatch"></param>
        internal void Draw(SpriteBatch spriteBatch)
        {
            DrawLightning(spriteBatch);
            DrawSpark(spriteBatch);
        }


        /// <summary>
        /// Draws lightning.
        /// </summary>
        /// <param name="spriteBatch"></param>
        private void DrawLightning(SpriteBatch spriteBatch)
        {
            Vector2 beamVector = _source.Position - _receiver.Position;

            //calculating beam width scale
            var widthBeamScale = beamVector.Length() / _beamRectangle.Width;

            //create scale vector considering texture and game field scales.
            var beamScale = new Vector2(widthBeamScale, 1) * _config.BeamTextureScale * GlobalConfig.GameFieldScale;

            //also calculate beam rotation
            var beamRotation = (float)Math.Atan2(beamVector.Y, beamVector.X);

            spriteBatch.Draw(
                texture: _config.BeamTexture,
                position: _source.GetGamePosition(),
                sourceRectangle: _beamRectangle,
                color: Color.White,
                rotation: beamRotation,
                origin: new Vector2(0, _beamRectangle.Height / 2f), //beam starts from its beginning
                scale: beamScale,
                effects: _beamEffect,
                layerDepth: DrawLayers.Explosion);
        }

        /// <summary>
        /// Draws spark of hit.
        /// </summary>
        /// <param name="spriteBatch"></param>
        private void DrawSpark(SpriteBatch spriteBatch)
        {
            //this method draws spark at current frame
            spriteBatch.Draw(
                texture: _config.SparkTexture,
                position: _receiver.GetGamePosition(),
                sourceRectangle: _sparkRectangle,
                color: Color.White,
                rotation: 0f,
                origin: new Vector2(_sparkRectangle.Width / 2f, _sparkRectangle.Height / 2f),
                scale: new Vector2(_config.SparkTextureScale) * GlobalConfig.GameFieldScale,
                effects: SpriteEffects.None,
                layerDepth: DrawLayers.Explosion + DrawLayers.Increment); //spark higher than lightning
        }
    }
}