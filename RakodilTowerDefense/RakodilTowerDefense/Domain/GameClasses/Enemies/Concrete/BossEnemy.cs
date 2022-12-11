using Microsoft.Xna.Framework;
using RakodilTowerDefense.Config.Configs.Global;

namespace RakodilTowerDefense.Domain.GameClasses.Enemies.Concrete;

public class BossEnemy: Enemy
{
    public BossEnemy(Vector2 position, float initialRotation) 
        : base(
            position, 
            initialRotation,
            configName: ConfigNames.Enemies.Boss)
    {
    }
}