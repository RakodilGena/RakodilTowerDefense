using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using RakodilTowerDefense.Config.JsonConfigs.ProjectileConfigs;

namespace RakodilTowerDefense.Config.Configs.ProjectileConfigs;

public class MissileConfig: SeekerConfig
{
    public readonly float ExplosionRange;

    public MissileConfig(
        MissileConfigJson configJson, 
        IDictionary<string, Texture2D> textures) 
        : base(configJson, textures)
    {
        ExplosionRange = configJson.ExplosionRange;
    }
}