using System;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using RakodilTowerDefense.Config.Configs.Global;
using RakodilTowerDefense.Domain.GameClasses.Guns.Abstract;
using RakodilTowerDefense.Domain.GameClasses.Interfaces;
using RakodilTowerDefense.Domain.GameClasses.Projectiles.Abstract;
using RakodilTowerDefense.Domain.GameClasses.Projectiles.Concrete;

namespace RakodilTowerDefense.Domain.GameClasses.Guns.Concrete;

public class MissileTower: TurretLoadableGun, IProjectileCreator
{
    #region Properties
    
    public event EventHandler<Projectile>? ProjectileCreated;

    #endregion


    #region Methods
    
    public MissileTower(Vector2 position, float defaultRotation) 
        : base(position, 
            configName: ConfigNames.Guns.Missile, 
            defaultRotation) { }

    protected override void Fire()
    {
        Debug.Assert(CurrentTarget != null);

        //creating missile that starts from center of tower.
        var missile = new Missile(Position, TurretRotation, CurrentTarget);
        ProjectileCreated?.Invoke(this, missile);
        
        Unload();
    }

    #endregion

}