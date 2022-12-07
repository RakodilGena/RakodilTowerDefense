using System;
using System.Collections.Generic;
using System.Linq;
using RakodilTowerDefense.Domain.GameClasses.Enemies;
using RakodilTowerDefense.Domain.GameClasses.Guns;
using RakodilTowerDefense.Domain.GameClasses.Guns.NonTurret;
using RakodilTowerDefense.Domain.GameClasses.Guns.Turret;

namespace RakodilTowerDefense.Domain.GameClasses.AimingStrategies;

/// <summary>
///  Strategy that aims to target enemy, which is faster to target.
/// </summary>
public class AimingStrategyClosest: AimingStrategy
{
    public override Enemy? GetTarget(Gun gun, IEnumerable<Enemy> enemies)
    {
        var inRange = TargetsInRange(gun, enemies);
        
        //if gun has no turret which means it doesn't need to rotate to aim - return first enemy.
        if (gun is NonTurretGun)
            return GetFirstTarget(inRange);

        //gun should be a turret gun unless.
        if (gun is not TurretGun turretGun)
            throw new InvalidOperationException("Gun for Aiming Strategy \"Closest\" is of invalid type");

        var target =
            inRange.MinBy(enemy => turretGun.GetRotationToTarget(enemy));

        return target;
    }
}