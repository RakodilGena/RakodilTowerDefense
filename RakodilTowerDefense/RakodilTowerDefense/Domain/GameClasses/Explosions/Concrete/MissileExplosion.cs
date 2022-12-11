using Microsoft.Xna.Framework;
using RakodilTowerDefense.Config.Configs.Global;
using RakodilTowerDefense.Domain.GameClasses.Explosions.Abstract;

namespace RakodilTowerDefense.Domain.GameClasses.Explosions.Concrete;

public class MissileExplosion: Explosion
{
    public MissileExplosion(Vector2 explosionPosition) 
        : base(explosionPosition, 
            rotation: 0,
            configName: ConfigNames.Explosions.Missile)
    {
        
    }
}