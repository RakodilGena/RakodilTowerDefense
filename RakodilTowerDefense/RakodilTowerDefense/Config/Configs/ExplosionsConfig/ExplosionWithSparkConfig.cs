using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using RakodilTowerDefense.Config.JsonConfigs.ExplosionsConfig;

namespace RakodilTowerDefense.Config.Configs.ExplosionsConfig;

public class ExplosionWithSparkConfig : ExplosionConfig
{
    public readonly Texture2D SparkTexture;

    public readonly float SparkTextureScale;
    public readonly float SparkRotationCenterX;

    public readonly int SparkFrames;

    public ExplosionWithSparkConfig(
        ExplosionWithSparkConfigJson configJson,
        IDictionary<string, Texture2D> textures)
        : base(configJson, textures)
    {
        SparkTexture = textures[configJson.SparkTexture];
        SparkFrames = configJson.SparkFrames;
        SparkTextureScale = configJson.SparkTextureWidth / SparkTexture.Width * SparkFrames;
        SparkRotationCenterX = configJson.SparkRotationCenterX;
    }
}