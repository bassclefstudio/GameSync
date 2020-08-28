using System;
using System.Collections.Generic;
using System.Text;

namespace BassClefStudio.NET.GameSync
{
    /// <summary>
    /// Represents the current state of a multiplayer game.
    /// </summary>
    public interface IGameState
    {
        /// <summary>
        /// Creates a shallow copy of the <see cref="IGameState"/>, which can be used to save the current game.
        /// </summary>
        IGameState Copy();
    }

    public static class GameStateExtensions
    {
        /// <summary>
        /// Creates a shallow copy of a <typeparamref name="T"/> game state, which can be used to save the current game.
        /// </summary>
        public static T Copy<T>(this T state) where T : IGameState
        {
            var copy = state.Copy();
            if(copy is T copyAs)
            {
                return copyAs;
            }
            else
            {
                throw new InvalidOperationException("The copying action of the IGameState produced an object of a differing type to itself.");
            }
        }
    }
}
