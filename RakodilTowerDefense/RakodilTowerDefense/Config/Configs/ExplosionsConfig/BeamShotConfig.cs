using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using RakodilTowerDefense.Config.JsonConfigs.ExplosionsConfig;

namespace RakodilTowerDefense.Config.Configs.ExplosionsConfig;

public class BeamShotConfig: ExplosionWithSparkConfig
{
    public Texture2D BeamTexture;

    public float BeamTextureScale;
    
    public int BeamFrames;

    public BeamShotConfig(
        BeamShotConfigJson configJson, 
        IDictionary<string, Texture2D> textures) 
        : base(configJson, textures)
    {
        BeamTexture = textures[configJson.BeamTexture];
        BeamTextureScale = configJson.BeamTextureWidth / BeamTexture.Width;
        BeamFrames = configJson.BeamFrames;
    }
}