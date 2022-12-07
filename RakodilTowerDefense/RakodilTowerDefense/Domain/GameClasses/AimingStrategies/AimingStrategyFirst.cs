using System.Collections.Generic;
using RakodilTowerDefense.Domain.GameClasses.Enemies;
using RakodilTowerDefense.Domain.GameClasses.Guns;

namespace RakodilTowerDefense.Domain.GameClasses.AimingStrategies;

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