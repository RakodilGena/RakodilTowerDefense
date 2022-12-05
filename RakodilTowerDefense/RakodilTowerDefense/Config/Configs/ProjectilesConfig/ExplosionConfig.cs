using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using RakodilTowerDefense.Config.JsonConfigs.ProjectilesConfig;

namespace RakodilTowerDefense.Config.Configs.ProjectilesConfig;

public class ExplosionConfig : CommonConfig
{
    public readonly float DrawTime;

    public readonly int ExplosionFrames;

    public ExplosionConfig(
        ExplosionConfigJson configJson,
        IDictionary<string, Texture2D> textures)
        : base(configJson, textures)
    {
        DrawTime = configJson.DrawTime;
        ExplosionFrames = configJson.ExplosionFrames;
    }
}