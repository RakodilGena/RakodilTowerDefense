using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace RakodilTowerDefense.Domain;

public interface IObject
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