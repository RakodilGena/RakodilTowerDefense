using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RakodilTowerDefense.Config.JsonConfigs.GunConfigs;
using RakodilTowerDefense.Domain.GameClasses.AimingStrategies;

namespace RakodilTowerDefense.Config.Configs.GunConfigs;

public class GunTurretConfig: GunConfig
{
    public readonly Texture2D TurretTexture;
   
    public readonly float 
        TurretTextureScale, 
        RotationSpeed, 
        RotationCenterX, 
        BarrelLength;

    public GunTurretConfig(
        GunTurretConfigJson configJson,
        IDictionary<string, Texture2D> textures,
        IDictionary<string, AimingStrategy> strategies)
    :base(configJson, textures, strategies)
    {
        TurretTexture = textures[configJson.TurretTexture];
        TurretTextureScale = configJson.TurretTextureWidth / TurretTexture.Width;
        
        RotationSpeed = MathHelper.ToRadians(configJson.RotationSpeed / 1000);
        
        RotationCenterX = configJson.RotationCenterX;
        BarrelLength = configJson.BarrelLength;
    }
}