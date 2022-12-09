using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using RakodilTowerDefense.Config.JsonConfigs.GunConfigs;
using RakodilTowerDefense.Domain.GameClasses.AimingStrategies;

namespace RakodilTowerDefense.Config.Configs.GunConfigs;

public class TeslaConfig : GunConfig
{
    public readonly Texture2D IdleSparkTexture;

    public readonly float
        IdleSparkTextureScale,
        IdleSparkDrawTime,
        IdleSparkReloadTime,
        LightningJumpRange;

    public readonly int MaximumTargets;
        

    public TeslaConfig(TeslaConfigJson configJson,
        IDictionary<string, Texture2D> textures,
        IDictionary<string, AimingStrategy> strategies)
        : base(configJson, textures, strategies)
    {
        IdleSparkTexture = textures[configJson.IdleSparkTexture];
        IdleSparkTextureScale = configJson.IdleSparkTextureWidth / IdleSparkTexture.Width;
        IdleSparkDrawTime = configJson.IdleSparkDrawTime * 1000;
        IdleSparkReloadTime = configJson.IdleSparkReloadTime * 1000;
        MaximumTargets = configJson.MaximumTargets;
        LightningJumpRange = configJson.LightningJumpRange;
    }
}