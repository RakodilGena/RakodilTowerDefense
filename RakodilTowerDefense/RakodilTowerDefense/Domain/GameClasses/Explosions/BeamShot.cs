using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RakodilTowerDefense.Config.Configs.ExplosionsConfig;
using RakodilTowerDefense.Config.Configs.Global;

namespace RakodilTowerDefense.Domain.GameClasses.Explosions;

public class BeamShot: ExplosionWithSpark
{
    #region Properties

    private new BeamShotConfig Config => (BeamShotConfig)base.Config;

    #endregion

    #region Methods

    
    public BeamShot(
        Vector2 position, 
        string configName, 
        float rotation, 
        Vector2 sparkPosition) 
        : base(position, configName, rotation, sparkPosition)
    {
    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        //this draws basic explosion and a spark  with base call,
        //it also draws beam
        base.Draw(gameTime, spriteBatch);
        
        BeamShotConfig cfg = Config;
        Texture2D beamTexture = cfg.BeamTexture;
        int currentFrame = GetCurrentFrameNumber(cfg.BeamFrames);
        Rectangle currentSourceRectangle = GetCurrentFrameRectangle(currentFrame, cfg.BeamFrames, beamTexture);

        //calculating beam width scale
        var widthBeamScale = (Position - SparkPosition).Length() / currentSourceRectangle.Width;
        //create scale vector considering texture and game field scales.
        var beamScale = new Vector2(widthBeamScale, 1) * cfg.BeamTextureScale * GlobalConfig.GameFieldScale;
        
        spriteBatch.Draw(
            texture: beamTexture,
            position: GetGamePosition(SparkPosition),
            sourceRectangle: currentSourceRectangle,
            color: Color.White,
            rotation: Rotation,
            origin: new Vector2(0, beamTexture.Height / 2f),//beam starts from its beginning
            scale: beamScale,
            effects: SpriteEffects.None,
            layerDepth: DrawLayers.Explosion);
    }

    #endregion
}