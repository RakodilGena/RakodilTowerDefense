using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using RakodilTowerDefense.Config.JsonConfigs.ProjectileConfigs;

namespace RakodilTowerDefense.Config.Configs.ProjectileConfigs;

public class ProjectileConfig : CommonConfig
{
    public readonly float RotationCenterX, Speed, Damage;

    public ProjectileConfig(
        ProjectileConfigJson configJson,
        IDictionary<string, Texture2D> textures)
        : base(configJson, textures)
    {
        RotationCenterX = configJson.RotationCenterX;
        Speed = configJson.Speed / 1000;
        Damage = configJson.Damage;
    }
}