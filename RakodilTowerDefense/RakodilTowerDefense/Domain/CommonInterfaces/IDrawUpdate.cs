using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace RakodilTowerDefense.Domain.CommonInterfaces;

public interface IDrawUpdate
{
    /// <summary>
    /// Updates game object.
    /// </summary>
    /// <param name="gameTime"></param>
    public void Update(GameTime gameTime);
    
    /// <summary>
    /// Draws game object.
    /// </summary>
    /// <param name="gameTime"></param>
    /// <param name="spriteBatch"></param>
    public void Draw(GameTime gameTime, SpriteBatch spriteBatch);
    
    
}