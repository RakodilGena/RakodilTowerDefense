using System;
using RakodilTowerDefense.Domain.Enemies;

namespace RakodilTowerDefense.Domain.Guns.Turret;

public abstract class TurretGun: Gun, IRotate
{
    private float _turretRotation;

    protected float Rotation
    {
        get => _turretRotation;
        set
        {
            double buffer = value;
            buffer = buffer switch
            {
                > Math.PI => buffer - 2 * Math.PI,
                < -Math.PI => buffer + 2 * Math.PI,
                _ => buffer
            };

            _turretRotation = (float)buffer;
        }
    }

    public float GetRotationToTarget(Enemy enemy)
    {
        //get vector between enemy and gun.
        var vector = Position - enemy.Position;
        
        //get angle based on vector.
        var expectedRotation = (float)Math.Atan2(vector.Y, vector.X);

        //get angle between current rotation and expected.
        var diff = expectedRotation - _turretRotation;

        //normalizing angle so gun will always pick the shortest way to target enemy
        double finalDiff = (double)diff switch
        {
            > Math.PI => diff - Math.PI * 2,
            < -Math.PI => diff + Math.PI * 2,
            _ => diff
        };

        return (float)finalDiff;
    }
}