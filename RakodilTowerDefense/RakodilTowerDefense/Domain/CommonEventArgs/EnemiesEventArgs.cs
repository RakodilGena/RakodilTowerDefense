using System;
using System.Collections.Generic;
using RakodilTowerDefense.Domain.GameClasses.Enemies;

namespace RakodilTowerDefense.Domain.CommonEventArgs;

public class EnemiesEventArgs:EventArgs
{
    public IEnumerable<Enemy>? Enemies;

    public EnemiesEventArgs()
    {
    }

    public EnemiesEventArgs(IEnumerable<Enemy>enemies)
    {
        Enemies = enemies;
    }
}