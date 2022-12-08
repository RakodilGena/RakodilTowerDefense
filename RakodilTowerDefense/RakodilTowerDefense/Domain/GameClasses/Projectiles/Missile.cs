using System;
using System.Diagnostics;
using System.Linq;
using Microsoft.Xna.Framework;
using RakodilTowerDefense.Config.Configs.Global;
using RakodilTowerDefense.Config.Configs.ProjectileConfigs;
using RakodilTowerDefense.Domain.CommonEventArgs;
using RakodilTowerDefense.Domain.GameClasses.Enemies;
using RakodilTowerDefense.Domain.GameClasses.Explosions;
using RakodilTowerDefense.Domain.GameClasses.Interfaces;

namespace RakodilTowerDefense.Domain.GameClasses.Projectiles;

public class Missile: Seeker, IExplosionCreator, ITargeting
{
    #region Properties

    public event EventHandler<Explosion>? ExplosionCreated;
    
    public event EventHandler<EnemiesEventArgs>? AskedForEnemies;


    #endregion

    #region Methods

    private new MissileConfig Config => (MissileConfig)base.Config;

    #endregion
    
    public Missile(
        Vector2 position, 
        float rotation, 
        Enemy target) 
    
        : base(position, 
            configName:ConfigNames.Projectiles.Missile, 
            rotation, 
            target)
    {
    }


    /// <summary>
    /// Returns value that indicates if certain enemy is in range of gun.
    /// </summary>
    /// <param name="enemy"></param>
    /// <returns></returns>
    private bool EnemyInRange(Enemy enemy)
    {
        var rangeBetween = (Position - enemy.Position).Length();
        return Config.ExplosionRange >= rangeBetween;
    }
    
    protected override void OnTargetReached(object? sender, EventArgs e)
    {
        //getting alive targets.
        var args = new EnemiesEventArgs();
        AskedForEnemies?.Invoke(this, args);

        Debug.Assert(args.Enemies != null);

        //get enemies in range and deal damage to each.
        var enemiesInRange = args.Enemies.Where(EnemyInRange);
        var damage = Config.Damage;
        foreach (var enemy in enemiesInRange)
            enemy.ReceiveDamage(damage);
        
        //create explosion at enemy position for displaying.
        var explosion = new StaticExplosion(
            position: Target.Position,
            configName: ConfigNames.Explosions.Missile);
        
        ExplosionCreated?.Invoke(this, explosion);
    }

}