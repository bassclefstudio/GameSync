using System;
using System.Collections.Generic;
using System.Text;

namespace BassClefStudio.NET.GameSync.State
{
    /// <summary>
    /// Represents a service that can make save copies of a <typeparamref name="T"/> game state.
    /// </summary>
    /// <typeparam name="T">The type of <see cref="GameState"/> this service saves.</typeparam>
    public interface IGameSaveService<T> where T : GameState
    {
        /// <summary>
        /// Create a by-value save copy of the <typeparamref name="T"/> state.
        /// </summary>
        /// <param name="state">The existing <typeparamref name="T"/> state to copy.</param>
        T Copy(T state);
    }
}
