using BassClefStudio.NET.GameSync.State;
using System;
using System.Collections.Generic;
using System.Text;

namespace BassClefStudio.NET.GameSync.Commits
{
    /// <summary>
    /// Represents a group of <see cref="IGameAction{T}"/>s sent between game clients.
    /// </summary>
    /// <typeparam name="T">The type of <see cref="IGameState"/> that this collection of <see cref="IGameAction{T}"/>s acts on.</typeparam>
    public interface IGameCommit<in T> where T : IGameState
    {
        /// <summary>
        /// Returns the transported <see cref="IEnumerable{T}"/> of <see cref="IGameAction{T}"/>.
        /// </summary>
        IEnumerable<IGameAction<T>> GetActions();
    }

    /// <summary>
    /// Represents a basic <see cref="IGameCommit{T}"/> created by this client from a collection of <see cref="IGameAction{T}"/>s.
    /// </summary>
    /// <typeparam name="T">The type of <see cref="IGameState"/> that this collection of <see cref="IGameAction{T}"/>s acts on.</typeparam>
    public class GameCommit<T> : IGameCommit<T> where T : IGameState
    {
        private IEnumerable<IGameAction<T>> Actions { get; }

        /// <summary>
        /// Creates a <see cref="GameCommit{T}"/> from a collection of <see cref="IGameAction{T}"/>s.
        /// </summary>
        /// <param name="actions">The <see cref="IEnumerable{T}"/> of <see cref="IGameAction{T}"/>s that this <see cref="GameCommit{T}"/> will represent.</param>
        public GameCommit(IEnumerable<IGameAction<T>> actions)
        {
            Actions = actions;
        }

        /// <inheritdoc/>
        public IEnumerable<IGameAction<T>> GetActions() => Actions;
    }
}
