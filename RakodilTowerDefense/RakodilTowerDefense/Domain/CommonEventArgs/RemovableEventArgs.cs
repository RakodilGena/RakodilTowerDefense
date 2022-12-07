using System;
using RakodilTowerDefense.Domain.CommonInterfaces;

namespace RakodilTowerDefense.Domain.CommonEventArgs;

//todo remove later if not required
public class RemovableEventArgs: EventArgs
{
    public IRemovable Entity;

    public RemovableEventArgs(IRemovable entity)
    {
        Entity = entity;
    }
}