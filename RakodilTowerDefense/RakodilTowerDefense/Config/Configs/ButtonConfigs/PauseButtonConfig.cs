using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using RakodilTowerDefense.Config.JsonConfigs.ButtonConfigs;

namespace RakodilTowerDefense.Config.Configs.ButtonConfigs;

public class PauseButtonConfig : CommonConfig
{
    public readonly Texture2D PlayTexture;

    public readonly float PlayTextureScale;

    public PauseButtonConfig(PauseButtonConfigJson configJson, IDictionary<string, Texture2D> textures) 
        : base(configJson, textures)
    {
        PlayTexture = textures[configJson.PlayTexture];
        PlayTextureScale = configJson.PlayTextureWidth / PlayTexture.Width;
    }
}