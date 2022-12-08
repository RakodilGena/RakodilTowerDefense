using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using RakodilTowerDefense.Config.JsonConfigs.ExplosionsConfig;

namespace RakodilTowerDefense.Config.Configs.ExplosionsConfig;

public class TeslaLightningConfig: BeamShotConfig
{
    public readonly int LightningFrames;
    
    public TeslaLightningConfig(TeslaLightningConfigJson configJson, IDictionary<string, Texture2D> textures) : base(configJson, textures)
    {
        LightningFrames = configJson.LightningFrames;
    }
}