using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using RakodilTowerDefense.Config.JsonConfigs.ProjectilesConfig;

namespace RakodilTowerDefense.Config.Configs.ProjectilesConfig;

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