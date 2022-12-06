using System.Collections.Generic;
using RakodilTowerDefense.Domain.Enemies;
using RakodilTowerDefense.Domain.Guns;

namespace RakodilTowerDefense.Domain.AimingStrategies;

/// <summary>
/// Strategy that aims to target very first enemy.
/// </summary>
public class AimingStrategyFirst : AimingStrategy
{
    public override Enemy? GetTarget(Gun gun, IEnumerable<Enemy> enemies)
    {
        var inRange = TargetsInRange(gun, enemies);
        return GetFirstTarget(inRange);
    }
}