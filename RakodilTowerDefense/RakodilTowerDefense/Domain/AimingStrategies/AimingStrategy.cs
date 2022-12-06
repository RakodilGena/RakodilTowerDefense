
using System.Collections.Generic;
using System.Linq;
using RakodilTowerDefense.Domain.Enemies;
using RakodilTowerDefense.Domain.Guns;

namespace RakodilTowerDefense.Domain.AimingStrategies;

public abstract class AimingStrategy
{
    /// <summary>
    /// Returns most suitable enemy for targeting. 
    /// </summary>
    /// <param name="gun"></param>
    /// <param name="enemies"></param>
    /// <returns></returns>
    public abstract Enemy? GetTarget(Gun gun, IEnumerable<Enemy> enemies);

    /// <summary>
    /// Returns enemy with longest traveled distance.
    /// </summary>
    /// <param name="enemies"></param>
    /// <returns></returns>
    protected Enemy? GetFirstTarget(IEnumerable<Enemy> enemies)
    {
        return enemies.MinBy(enemy => enemy.TraveledDistance);
    }

    /// <summary>
    /// Return enemies that are in range of tower.
    /// </summary>
    /// <param name="gun"></param>
    /// <param name="enemies"></param>
    /// <returns></returns>
    protected IEnumerable<Enemy> TargetsInRange(Gun gun, IEnumerable<Enemy> enemies)
    {
        return from enemy in enemies
            where gun.EnemyInRange(enemy)
            select enemy;
    }
}