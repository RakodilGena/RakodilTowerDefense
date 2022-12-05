﻿using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using RakodilTowerDefense.Config.JsonConfigs.EnemyConfigs;

namespace RakodilTowerDefense.Config.Configs.EnemyConfigs;

public class EnemyConfig: CommonConfig
{
    // ReSharper disable once InconsistentNaming
    public readonly float HP,
        MovingSpeed,
        RotationSpeed;

    public readonly int Bounty;

    public EnemyConfig(
        EnemyConfigJson configJson, 
        IDictionary<string, Texture2D> textures)
        : base(configJson, textures)
    {
        HP = configJson.Hp;
        MovingSpeed = configJson.MovingSpeed / 1000;
        RotationSpeed = configJson.RotationSpeed / 1000;
        Bounty = configJson.Bounty;
    }
}