using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using RakodilTowerDefense.Config.JsonConfigs.ExplosionsConfig;

namespace RakodilTowerDefense.Config.Configs.ExplosionsConfig;

public class BeamShotConfig: ExplosionWithSparkConfig
{
    public readonly Texture2D BeamTexture;

    public readonly float BeamTextureScale;
    
    public readonly int BeamFrames;

    public BeamShotConfig(
        BeamShotConfigJson configJson, 
        IDictionary<string, Texture2D> textures) 
        : base(configJson, textures)
    {
        BeamTexture = textures[configJson.BeamTexture];
        BeamFrames = configJson.BeamFrames;
        BeamTextureScale = configJson.BeamTextureWidth  / BeamTexture.Width * BeamFrames;
    }
}