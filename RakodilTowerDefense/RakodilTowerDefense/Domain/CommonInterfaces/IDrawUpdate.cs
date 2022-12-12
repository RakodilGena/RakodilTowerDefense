using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace RakodilTowerDefense.Domain.CommonInterfaces;

/// <summary>
/// Implementors of this can be either drawn or updated.
/// </summary>
public interface IDrawUpdate
{
    /// <summary>
    /// Updates implementor of this.
    /// </summary>
    /// <param name="gameTime"></param>
    public void Update(GameTime gameTime);
    
    /// <summary>
    /// Draws implementor of this.
    /// </summary>
    /// <param name="gameTime"></param>
    /// <param name="spriteBatch"></param>
    public void Draw(GameTime gameTime, SpriteBatch spriteBatch);
    
    
}