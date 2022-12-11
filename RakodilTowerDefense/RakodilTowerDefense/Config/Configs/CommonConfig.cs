using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using RakodilTowerDefense.Config.JsonConfigs;

namespace RakodilTowerDefense.Config.Configs;

/// <summary>
/// Common config class. Superclass for any other configs.
/// </summary>
public class CommonConfig
{
    public float TextureScale { get; protected set; }
    
    public readonly Texture2D Texture;

    public CommonConfig(CommonConfigJson configJson, IDictionary<string, Texture2D> textures)
    {
        Texture = textures[configJson.Texture];
        TextureScale = configJson.TextureWidth / Texture.Width;
    } 
}

