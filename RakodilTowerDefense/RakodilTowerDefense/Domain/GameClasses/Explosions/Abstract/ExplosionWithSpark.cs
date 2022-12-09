using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RakodilTowerDefense.Config.Configs.ExplosionsConfig;
using RakodilTowerDefense.Config.Configs.Global;

namespace RakodilTowerDefense.Domain.GameClasses.Explosions.Abstract;

public abstract class ExplosionWithSpark: Explosion
{
    #region Fields

    

    #endregion

    #region Properties

    private new ExplosionWithSparkConfig Config => (ExplosionWithSparkConfig)base.Config;
    
    protected float Rotation { get; }
    
    protected Vector2 SparkPosition { get; }

    #endregion

    #region Methods

    
    public ExplosionWithSpark(
        Vector2 position, 
        string configName, 
        float rotation,
        Vector2 sparkPosition) 
        : base(position, configName)
    {
        Rotation = rotation;
        SparkPosition = sparkPosition;
    }
    
    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        //this draws basic explosion with base call and a spark 
        base.Draw(gameTime, spriteBatch);
        
        ExplosionWithSparkConfig cfg = Config;
        Texture2D sparkTexture = cfg.SparkTexture;
        int currentFrame = GetCurrentFrameNumber(totalFrames: cfg.SparkFrames);
        Rectangle currentSourceRectangle = 
            GetCurrentFrameRectangle(currentFrame, 
                totalFrames: cfg.SparkFrames, 
                texture: sparkTexture);
        
        spriteBatch.Draw(
            texture: sparkTexture,
            position: GetGamePosition(SparkPosition),
            sourceRectangle: currentSourceRectangle,
            color: Color.White,
            rotation: Rotation,
            origin: new Vector2(cfg.SparkRotationCenterX, sparkTexture.Height / 2f),
            scale: new Vector2(cfg.SparkTextureScale) * GlobalConfig.GameFieldScale,
            effects: SpriteEffects.None,
            layerDepth: DrawLayers.Explosion + DrawLayers.Increment);//spark higher than beam
    }

    #endregion
}