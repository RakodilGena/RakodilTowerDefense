using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using RakodilTowerDefense.Config.JsonConfigs.GunConfigs;
using RakodilTowerDefense.Domain.GameClasses.AimingStrategies;

namespace RakodilTowerDefense.Config.Configs.GunConfigs;

public class GatlingConfig: GunTurretConfig
{
    public readonly int AmmoCapacity;

    public readonly float SingleBulletReloadTime;

    public GatlingConfig(
        GatlingConfigJson configJson,
        IDictionary<string, Texture2D> textures,
        IDictionary<string, AimingStrategy> strategies)
        :base(configJson, textures, strategies)
    {
        AmmoCapacity = configJson.AmmoCapacity;
        SingleBulletReloadTime = configJson.SingleBulletReloadTime * 1000;
    }
}