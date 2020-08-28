using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace BassClefStudio.NET.GameSync
{
    /// <summary>
    /// Represents a service that manages change-based synchronization of a turn-based game.
    /// </summary>
    /// <typeparam name="T">The type of <see cref="IGameState"/> that this service synchronizes.</typeparam>
    public class GameSyncManager<T> where T : IGameState
    {
        /// <summary>
        /// The current <typeparamref name="T"/> state.
        /// </summary>
        public T GameState { get; private set; }
        
        /// <summary>
        /// The initial <typeparamref name="T"/> game state, from when the <see cref="GameSyncManager{T}"/> was created.
        /// </summary>
        public T InitialGameState { get; }

        /// <summary>
        /// Returns a read-only collection of all <see cref="IGameAction{T}"/>s applied to the <typeparamref name="T"/> game state.
        /// </summary>
        public ReadOnlyCollection<IGameAction<T>> Actions => ActionsList.AsReadOnly();
        private List<IGameAction<T>> ActionsList;

        /// <summary>
        /// Creates a new <see cref="GameSyncManager{T}"/> to handle a given <typeparamref name="T"/> game state.
        /// </summary>
        /// <param name="gameState">The initial game state of the game, from which all turns will be played.</param>
        public GameSyncManager(T gameState)
        {
            GameState = gameState;
            InitialGameState = GameState.Copy<T>();
            ActionsList = new List<IGameAction<T>>();
        }

        /// <summary>
        /// Applies a collection of <see cref="IGameAction{T}"/>s to the <see cref="IGameState"/>.
        /// </summary>
        /// <param name="newActions">The collection of <see cref="IGameAction{T}"/>s to apply.</param>
        public void AddActions(IEnumerable<IGameAction<T>> newActions)
        {
            foreach (var action in newActions)
            {
                ActionsList.Add(action);
                if(action.CanUpdate(GameState))
                {
                    action.Update(GameState);
                }
            }
        }

        /// <summary>
        /// Updates the <see cref="IGameState"/> by applying any <see cref="IGameAction{T}"/>s that are not contained in <see cref="Actions"/> from a given collection of <see cref="IGameAction{T}"/>s.
        /// </summary>
        /// <param name="updatedActions">The new full list of <see cref="IGameAction{T}"/>s.</param>
        public void UpdateActions(IEnumerable<IGameAction<T>> updatedActions)
        {
            var newActions = updatedActions.Where(n => !Actions.Any(a => a.Id == n.Id));
            AddActions(newActions);
        }

        /// <summary>
        /// Reconstructs the <see cref="IGameState"/> from the initial <see cref="IGameState"/> provided to the <see cref="GameSyncManager{T}"/> and the existing collection <see cref="Actions"/>.
        /// </summary>
        public void RemakeGameState()
        {
            GameState = InitialGameState;
            foreach (var action in Actions)
            {
                if (action.CanUpdate(GameState))
                {
                    action.Update(GameState);
                }
            }
        }
    }
}
