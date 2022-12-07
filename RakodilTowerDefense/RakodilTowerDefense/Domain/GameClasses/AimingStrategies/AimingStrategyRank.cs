using System.Collections.Generic;
using System.Linq;
using RakodilTowerDefense.Domain.GameClasses.Enemies;
using RakodilTowerDefense.Domain.GameClasses.Guns;

namespace RakodilTowerDefense.Domain.GameClasses.AimingStrategies;

/// <summary>
/// Strategy that aims to target enemy with highest rank.
/// </summary>
public class AimingStrategyRank: AimingStrategy
{
    public override Enemy? GetTarget(Gun gun, IEnumerable<Enemy> enemies)
    {
        var enemiesArray = TargetsInRange(gun, enemies).ToArray();
        if (!enemiesArray.Any())
            return null;
        
        int maxRank = enemiesArray.Max(enemy => enemy.Rank);

        var highestRankEnemies = enemiesArray.Where(enemy => enemy.Rank == maxRank);
        return GetFirstTarget(highestRankEnemies);
    }
}