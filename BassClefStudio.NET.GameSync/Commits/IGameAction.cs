using BassClefStudio.NET.GameSync.State;
using System;
using System.Collections.Generic;
using System.Text;

namespace BassClefStudio.NET.GameSync.Commits
{
    /// <summary>
    /// Represents an action that changes the state of an <see cref="IGameState"/>.
    /// </summary>
    public interface IGameAction<in T> where T : IGameState
    {
        /// <summary>
        /// The <see cref="Player"/> that has signed and performed this <see cref="IGameAction{T}"/>.
        /// </summary>
        Player SigningPlayer { get; }

        /// <summary>
        /// Returns a <see cref="bool"/> indicating whether the changes defined in the <see cref="IGameAction{T}"/> can be made to the given <typeparamref name="T"/>.
        /// </summary>
        /// <param name="gameState">The <typeparamref name="T"/> state to be updated.</param>
        bool CanUpdate(T gameState);

        /// <summary>
        /// Applies the <see cref="IGameAction{T}"/> to the given <typeparamref name="T"/> state.
        /// </summary>
        /// <param name="gameState">The <typeparamref name="T"/> state to update.</param>
        void Update(T gameState);
    }
}
