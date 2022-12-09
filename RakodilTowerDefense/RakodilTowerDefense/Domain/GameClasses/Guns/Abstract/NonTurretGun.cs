using Microsoft.Xna.Framework;

namespace RakodilTowerDefense.Domain.GameClasses.Guns.Abstract;

public abstract class NonTurretGun: Gun
{

    #region Methods

    protected NonTurretGun(Vector2 position, string configName) : base(position, configName)
    {
        
    }


    public override void Update(GameTime gameTime)
    {
        //check if gun can fire.
        var (_, canFire) = CheckTarget();

        //if gun is not loaded, reload it. gun cant shoot at the same step it finishes reloading so we return.
        if (!Loaded)
        {
            Reload(gameTime);
            return;
        }

        //if gun is loaded and can fire - it fires.
        if (canFire)
            Fire();
    }

    #endregion
}