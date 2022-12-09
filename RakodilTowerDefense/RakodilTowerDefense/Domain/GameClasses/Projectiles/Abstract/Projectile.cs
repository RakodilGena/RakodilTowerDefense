using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RakodilTowerDefense.Config.Configs.Global;
using RakodilTowerDefense.Config.Configs.ProjectileConfigs;
using RakodilTowerDefense.Domain.CommonInterfaces;

namespace RakodilTowerDefense.Domain.GameClasses.Projectiles.Abstract;

/// <summary>
/// Abstract projectile.
/// </summary>
public abstract class Projectile: GameObject, IRemovable
{

    #region Fields

    #endregion

    #region Properties

    protected float Rotation { get; set; }
    private new ProjectileConfig Config => (ProjectileConfig)base.Config;
    
    public event EventHandler? Removed;

    #endregion

    #region Methods

    protected Projectile(Vector2 position, string configName, float rotation) : base(position, configName)
    {
        Rotation = rotation;
    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        //this draws projectile.
        
        ProjectileConfig cfg = Config;
        Texture2D texture = cfg.Texture;
       
        spriteBatch.Draw(
            texture: texture, 
            position: GetGamePosition(), 
            sourceRectangle: new Rectangle(0, 0, texture.Width, texture.Height), 
            color:Color.White, 
            rotation: Rotation,
            origin: new Vector2(cfg.RotationCenterX, texture.Height/2f), 
            scale: new Vector2(cfg.TextureScale) * GlobalConfig.GameFieldScale,
            effects: SpriteEffects.None,
            layerDepth: DrawLayers.Projectile);
    }

    /// <summary>
    /// Removes this projectile.
    /// </summary>
    protected void Remove()
    {
        Removed?.Invoke(this, EventArgs.Empty);
    }

    #endregion

}