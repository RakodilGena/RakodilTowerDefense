using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using RakodilTowerDefense.Config.JsonConfigs.ProjectileConfigs;

namespace RakodilTowerDefense.Config.Configs.ProjectileConfigs;

public class SeekerConfig: ProjectileConfig
{
    public readonly float CollisionRange;
    
    public SeekerConfig(
        SeekerConfigJson configJson, 
        IDictionary<string, Texture2D> textures) 
        : base(configJson, textures)
    {
        CollisionRange = configJson.CollisionRange;
    }
}