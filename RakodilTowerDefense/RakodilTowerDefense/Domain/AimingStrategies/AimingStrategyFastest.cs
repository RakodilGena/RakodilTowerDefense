using System.Collections.Generic;
using System.Linq;
using RakodilTowerDefense.Domain.Enemies;
using RakodilTowerDefense.Domain.Guns;

namespace RakodilTowerDefense.Domain.AimingStrategies;

/// <summary>
/// Strategy that aims to target first fastest enemy.
/// </summary>
public class AimingStrategyFastest : AimingStrategy
{
    public override Enemy? GetTarget(Gun gun, IEnumerable<Enemy> enemies)
    {
        var enemiesArray = TargetsInRange(gun, enemies).ToArray();
        if (!enemiesArray.Any())
            return null;
        
        float maxSpeed = enemiesArray.Max(enemy => enemy.MovingSpeed);

        var fastestEnemies = enemiesArray.Where(enemy => enemy.MovingSpeed == maxSpeed);
        return GetFirstTarget(fastestEnemies);
    }
}