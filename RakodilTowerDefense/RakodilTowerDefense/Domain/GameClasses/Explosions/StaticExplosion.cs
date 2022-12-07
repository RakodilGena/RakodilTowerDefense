using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RakodilTowerDefense.Config.Configs.ExplosionsConfig;
using RakodilTowerDefense.Config.Configs.Global;

namespace RakodilTowerDefense.Domain.GameClasses.Explosions;

public class StaticExplosion : Explosion
{

    public StaticExplosion(Vector2 position, string configName) : base(position, configName)
    {
    }
    
    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        //this method draws explosion at current frame

        ExplosionConfig cfg = Config;
        Texture2D texture = cfg.Texture;
        int currentFrame = GetCurrentFrameNumber(cfg.ExplosionFrames);
        Rectangle currentSourceRectangle = GetCurrentFrameRectangle(currentFrame, cfg.ExplosionFrames, texture);

        spriteBatch.Draw(
            texture: texture,
            position: GetGamePosition(),
            sourceRectangle: currentSourceRectangle,
            color: Color.White,
            rotation: 0f,
            origin: new Vector2(currentSourceRectangle.Width / 2f, texture.Height / 2f),
            scale: new Vector2(cfg.TextureScale) * GlobalConfig.GameFieldScale,
            effects: SpriteEffects.None,
            layerDepth: DrawLayers.Explosion + DrawLayers.Increment);//explosion higher than beam
    }
}