using System;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using RakodilTowerDefense.Config.Configs;
using RakodilTowerDefense.Config.Configs.GunConfigs;
using RakodilTowerDefense.Domain.AimingStrategies;
using RakodilTowerDefense.Domain.CommonEventArgs;
using RakodilTowerDefense.Domain.Enemies;

namespace RakodilTowerDefense.Domain.Guns;

public abstract class Gun: GameObject, ITargeting
{
    #region Fields

    protected float _reloadTimeLeft;

    protected bool _loaded;

    protected Enemy? _currentTarget;

    #endregion
    
    #region Properties

    public event EventHandler<EnemiesEventArgs>? AskForEnemies;

    private GunConfig Config => (GunConfig)_config;

    #endregion
    
    #region Methods

    protected Gun(Vector2 position, CommonConfig cfg) : base(position, cfg)
    {
        _loaded = true;
    }

    /// <summary>
    /// Returns value that indicates if certain enemy is in range of gun.
    /// </summary>
    /// <param name="enemy"></param>
    /// <returns></returns>
    public bool EnemyInRange(Enemy enemy)
    {
        var rangeBetween = (Position - enemy.Position).Length();
        return Config.Range >= rangeBetween;
    }

    /// <summary>
    /// Tries to get new target for a gun.
    /// </summary>
    protected void GetNewTarget()
    {
        var args = new EnemiesEventArgs();
        AskForEnemies?.Invoke(this, args);

        if (args.Enemies == null)
            throw new NullReferenceException("Enemies were null for asking gun");

        _currentTarget = Config.AimingStrategy.GetTarget(this, args.Enemies);
    }

    /// <summary>
    /// Reloads gun;
    /// </summary>
    /// <param name="gameTime"></param>
    protected void Reload(GameTime gameTime)
    {
        Debug.Assert(!_loaded);

        //checking if reloaded in this step.
        double currentLoadTimeLeft = _reloadTimeLeft - gameTime.ElapsedGameTime.TotalMilliseconds;
        if (currentLoadTimeLeft <= 0)
        {
            _reloadTimeLeft = 0;
            _loaded = true;
            return;
        }

        //if not reloaded - lower remaining time;
        _reloadTimeLeft = (float)currentLoadTimeLeft;
    }

    /// <summary>
    /// Fires at the current target.
    /// </summary>
    protected abstract void Fire();

    /// <summary>
    /// Unload gun - after shot mostly.
    /// </summary>
    protected void Unload()
    {
        Debug.Assert(_loaded);

        _loaded = false;
        _reloadTimeLeft = Config.ReloadTime;
    }

    
    #endregion
}