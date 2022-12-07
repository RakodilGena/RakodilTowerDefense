using System;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RakodilTowerDefense.Config.Configs.ExplosionsConfig;
using RakodilTowerDefense.Domain.CommonInterfaces;

namespace RakodilTowerDefense.Domain.GameClasses.Explosions;

public abstract class Explosion : GameObject, IRemovable
{
    #region Fields

    private float _currentDrawTime;

    #endregion


    #region Properties

    protected new ExplosionConfig Config => (ExplosionConfig)base.Config;

    public event EventHandler? Removed;

    #endregion


    #region Methods

    protected Explosion(
        Vector2 position,
        string configName)
        : base(position, configName)
    {
        _currentDrawTime = 0;
    }

    

    /// <summary>
    /// Returns current frame of texture, based on ratio between currentDrawTime and totalDrawTime.
    /// </summary>
    /// <param name="totalFrames"></param>
    /// <returns></returns>
    protected int GetCurrentFrameNumber(int totalFrames)
    {
        if (totalFrames == 1)
            return 0;

        var totalDrawTime = Config.DrawTime;
        Debug.Assert(_currentDrawTime < totalDrawTime);

        int frame = (int)Math.Truncate(_currentDrawTime / totalDrawTime * totalFrames);
        return frame;
    }

    /// <summary>
    /// Returns current rectangle of texture to draw.
    /// </summary>
    /// <param name="frameNumber"></param>
    /// <param name="totalFrames"></param>
    /// <param name="texture"></param>
    /// <returns></returns>
    protected static Rectangle GetCurrentFrameRectangle(int frameNumber, int totalFrames, Texture2D texture)
    {
        Debug.Assert(totalFrames > frameNumber);

        var width = texture.Width / totalFrames;

        return new Rectangle(width * frameNumber, 0, width, texture.Height);
    }
    

    public override void Update(GameTime gameTime)
    {
        //calculating draw time at this step.
        var newDrawTime = _currentDrawTime + (float)gameTime.ElapsedGameTime.TotalMilliseconds;

        //if threshold of drawing explosion not reached - keep ticking and drawing.
        if (newDrawTime < Config.DrawTime)
        {
            _currentDrawTime = newDrawTime;
            return;
        }

        //if threshold reached this explosion no longer drawn.
        Removed?.Invoke(this, EventArgs.Empty);
    }

    #endregion
}