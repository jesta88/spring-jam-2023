using System;
using Microsoft.Xna.Framework;

namespace QuadTree
{
    /// <summary>
    /// An interface that defines and object with a rectangle
    /// </summary>
    public interface IHasRect
    {
        Rectangle GetRect { get; }
    }
}
