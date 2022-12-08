using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using RakodilTowerDefense.Config.JsonConfigs.GunConfigs;
using RakodilTowerDefense.Domain.GameClasses.AimingStrategies;

namespace RakodilTowerDefense.Config.Configs.GunConfigs;

public class TeslaConfig : GunConfig
{
    public readonly Texture2D IdleSparkTexture;

    public readonly float IdleSparkTextureScale, IdleSparkRedrawTime;

    public TeslaConfig(TeslaConfigJson configJson,
        IDictionary<string, Texture2D> textures,
        IDictionary<string, AimingStrategy> strategies)
        : base(configJson, textures, strategies)
    {
        IdleSparkTexture = textures[configJson.IdleSparkTexture];
        IdleSparkTextureScale = configJson.IdleSparkTextureWidth / IdleSparkTexture.Width;
        IdleSparkRedrawTime = configJson.IdleSparkRedrawTime;
    }
}