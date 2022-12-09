using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RakodilTowerDefense.Config.Configs.Global;
using RakodilTowerDefense.Config.Configs.GunConfigs;
using RakodilTowerDefense.Domain.CommonEventArgs;
using RakodilTowerDefense.Domain.CommonInterfaces;
using RakodilTowerDefense.Domain.GameClasses.Enemies;
using RakodilTowerDefense.Domain.GameClasses.Interfaces;
using RakodilTowerDefense.Extensions;

namespace RakodilTowerDefense.Domain.GameClasses.Guns.Abstract;

public abstract class Gun: GameObject, ITargeting, IRemovable
{
    #region Fields

    #endregion
    
    #region Properties

    /// <summary>
    /// How many milliseconds left until gun is loaded.
    /// </summary>
    protected float ReloadTimeLeft { get; set; }
    
    /// <summary>
    /// Gun is loaded and ready to fire.
    /// </summary>
    protected bool Loaded { get; set; }
    
    /// <summary>
    /// Current target of gun.
    /// </summary>
    protected Enemy? CurrentTarget { get; private set; }
    

    public event EventHandler<EnemiesEventArgs>? AskedForEnemies;

    public event EventHandler? Removed;
    private new GunConfig Config => (GunConfig)base.Config;

    #endregion
    
    #region Methods

    protected Gun(Vector2 position, string configName) : base(position, configName)
    {
        Loaded = true;
    }
    
    

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
       //this method draws base gun-
       //which is full non turret gun
       //or turret gun fundament

       GunConfig cfg = Config;
       Texture2D texture = cfg.Texture;
       
       spriteBatch.Draw(
           texture: texture, 
           position: GetGamePosition(), 
           sourceRectangle: new Rectangle(0, 0, texture.Width, texture.Height), 
           color:Color.White, 
           rotation: 0f,
           origin: new Vector2(texture.Width/2f, texture.Height/2f), 
           scale: new Vector2(cfg.TextureScale) * GlobalConfig.GameFieldScale,
           effects: SpriteEffects.None,
           layerDepth: DrawLayers.Gun);
    }

    /// <summary>
    /// Returns value that indicates if certain enemy is in range of gun.
    /// </summary>
    /// <param name="enemy"></param>
    /// <returns></returns>
    public bool EnemyInRange(Enemy enemy)
    {
        return Config.Range >= Position.DistanceTo(enemy.Position);
    }
    

    /// <summary>
    /// Fires at the current target.
    /// </summary>
    protected virtual void Fire()
    {
        Debug.Assert(CurrentTarget != null);
        
        CurrentTarget.ReceiveDamage(Config.Damage);
        Unload();
    }
    
    /// <summary>
    /// Tries to get new target for a gun. Returns flag that says if target found.
    /// </summary>
    private bool GetNewTarget()
    {
        var enemies = GetAliveEnemies();

        CurrentTarget = Config.AimingStrategy.GetTarget(this, enemies);
        return CurrentTarget != null;
    }

    /// <summary>
    /// Returns collection of alive enemies.
    /// </summary>
    /// <returns></returns>
    protected IEnumerable<Enemy> GetAliveEnemies()
    {
        var args = new EnemiesEventArgs();
        AskedForEnemies?.Invoke(this, args);

        Debug.Assert(args.Enemies != null);

        return args.Enemies;
    }

    /// <summary>
    /// Says if gun has valid target to fire at.
    /// </summary>
    /// <returns></returns>
    private bool HasValidTarget()
    {
        if (CurrentTarget == null)
            return false;

        if (CurrentTarget.IsAlive && EnemyInRange(CurrentTarget))
            return true;

        CurrentTarget = null;
        return false;
    }

    /// <summary>
    /// Reloads gun;
    /// </summary>
    /// <param name="gameTime"></param>
    protected virtual void Reload(GameTime gameTime)
    {
        Debug.Assert(!Loaded);

        //checking if reloaded in this step.
        double currentLoadTimeLeft = ReloadTimeLeft - gameTime.ElapsedGameTime.TotalMilliseconds;
        if (currentLoadTimeLeft <= 0)
        {
            ReloadTimeLeft = 0;
            Loaded = true;
            return;
        }

        //if not reloaded - lower remaining time;
        ReloadTimeLeft = (float)currentLoadTimeLeft;
    }

    /// <summary>
    /// Checks if gun has valid target and tries to get new one if it hasn't.
    /// Returns value that indicates if gun has target and can fire right now.
    /// </summary>
    /// <returns></returns>
    protected (bool hasTarget, bool canFire) CheckTarget()
    {
        //if gun has valid target - it can fire right now.
        if (HasValidTarget())
            return (true, true);

        //if gun has no target we try to get some. And gun cant fire at the same step it tried to get new target.
        var gotTarget = GetNewTarget();
        return (gotTarget, false);
    }

    /// <summary>
    /// Removes gun.
    /// </summary>
    protected void Remove()
    {
        //todo dont forget to use.
        Removed?.Invoke(this, EventArgs.Empty);
    }

    /// <summary>
    /// Unload gun - after shot mostly.
    /// </summary>
    protected virtual void Unload()
    {
        Debug.Assert(Loaded);

        Loaded = false;
        ReloadTimeLeft = Config.ReloadTime;
    }

    
    #endregion
}