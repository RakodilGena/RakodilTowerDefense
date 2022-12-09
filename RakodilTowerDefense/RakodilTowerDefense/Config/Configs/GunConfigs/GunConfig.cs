using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using RakodilTowerDefense.Config.JsonConfigs.GunConfigs;
using RakodilTowerDefense.Domain.GameClasses.AimingStrategies;

namespace RakodilTowerDefense.Config.Configs.GunConfigs;

public class GunConfig: CommonConfig
{
    public readonly float Damage, ReloadTime, Range;

    public readonly int Cost;

    public readonly AimingStrategy AimingStrategy;

    public GunConfig(GunConfigJson configJson,
        IDictionary<string, Texture2D> textures,
        IDictionary<string, AimingStrategy> strategies)
        : base(configJson, textures)
    {
        Damage = configJson.Damage;
        ReloadTime = configJson.ReloadTime * 1000;
        Range = configJson.Range;
        Cost = configJson.Cost;
        
        AimingStrategy = strategies[configJson.AimingStrategy];
    }
}