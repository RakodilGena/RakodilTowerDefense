using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using RakodilTowerDefense.Config.JsonConfigs.GunConfigs;

namespace RakodilTowerDefense.Config.Configs.GunConfigs;

public class MissileConfig : CommonConfig
{
    public readonly float RotationCenterX;

    public MissileConfig(
        MissileConfigJson configJson,
        IDictionary<string, Texture2D> textures)
        : base(configJson, textures)
    {
        RotationCenterX = configJson.RotationCenterX;
    }
}