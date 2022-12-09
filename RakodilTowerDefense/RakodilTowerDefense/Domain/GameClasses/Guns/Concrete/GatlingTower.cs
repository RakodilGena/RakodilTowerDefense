using System;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using RakodilTowerDefense.Config.Configs.Global;
using RakodilTowerDefense.Config.Configs.GunConfigs;
using RakodilTowerDefense.Domain.GameClasses.Explosions.Abstract;
using RakodilTowerDefense.Domain.GameClasses.Explosions.Concrete;
using RakodilTowerDefense.Domain.GameClasses.Guns.Abstract;
using RakodilTowerDefense.Domain.GameClasses.Interfaces;

namespace RakodilTowerDefense.Domain.GameClasses.Guns.Concrete;

public class GatlingTower: TurretGun, IExplosionCreator
{
    #region Fields

    private int _currentAmmo;

    #endregion
    
    

    #region Properties

    private new GatlingConfig Config => (GatlingConfig)base.Config;
    
    public event EventHandler<Explosion>? ExplosionCreated;

    #endregion
    
    

    #region Methods
    
    public GatlingTower(Vector2 position, float defaultRotation) 
        : base(position, 
            configName: ConfigNames.Guns.Gatling,  
            defaultRotation)
    {
        SetFullAmmo();
    }

    protected override void Fire()
    {
        base.Fire();

        //getting position of spark at the end of barrel
        var sparkPosition = GetBarrelEnd();

        //create explosion and invoke it to client handler.
        var explosion = new GatlingShot(
            sparkPosition,
            explosionPosition: CurrentTarget!.Position,
            rotation: TurretRotation);
        
        ExplosionCreated?.Invoke(this, explosion);
    }

    /// <summary>
    /// Sets current ammo to maximum.
    /// </summary>
    private void SetFullAmmo()
    {
        _currentAmmo = Config.AmmoCapacity;
    }

    protected override void Unload()
    {
        Debug.Assert(Loaded);

        Loaded = false;

        //one bullet used.
        _currentAmmo--;

        //if gatling used all of its ammo and has to reload
        if (_currentAmmo <= 0)
        {
            ReloadTimeLeft = Config.ReloadTime;
            
            //after it reloaded is will have full ammo.
            SetFullAmmo();
            
            return;
        }
        
        //if any ammo left, we will "put next cartridge in the chamber".
        ReloadTimeLeft = Config.SingleBulletReloadTime;
    }

    #endregion
    

}