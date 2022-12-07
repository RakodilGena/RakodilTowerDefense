using System;
namespace RakodilTowerDefense.Domain.CommonInterfaces;

/// <summary>
/// Implementor of this can raise event to remove this object from some outer container.
/// </summary>
public interface IRemovable
{
    /// <summary>
    /// Raised when implementor has to be removed.
    /// </summary>
    public event EventHandler Removed;
}