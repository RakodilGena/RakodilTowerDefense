using System;
using Microsoft.Xna.Framework;
using RakodilTowerDefense.Config.Configs.Global;
using RakodilTowerDefense.Domain.GameClasses.Explosions.Abstract;
using RakodilTowerDefense.Domain.GameClasses.Explosions.Concrete;
using RakodilTowerDefense.Domain.GameClasses.Guns.Abstract;
using RakodilTowerDefense.Domain.GameClasses.Interfaces;

namespace RakodilTowerDefense.Domain.GameClasses.Guns.Concrete;

public class LaserGun : TurretGun, IExplosionCreator
{
    #region Properties

    public event EventHandler<Explosion>? ExplosionCreated;

    #endregion

    #region Methods

    public LaserGun(
        Vector2 position,
        float defaultRotation)
        : base(position,
            configName: ConfigNames.Guns.Laser,
            defaultRotation)
    {
    }

    protected override void Fire()
    {
        base.Fire();

        //getting position of spark at the end of barrel
        var sparkPosition = GetBarrelEnd();

        //create explosion and invoke it to client handler.
        var explosion = new LaserShot(sparkPosition,
            explosionPosition: CurrentTarget!.Position,
            rotation: TurretRotation);
        
        ExplosionCreated?.Invoke(this, explosion);
    }


    #endregion

}