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
        private IGameSaveService<T> SaveService;

        /// <summary>
        /// Returns a read-only collection of all <see cref="IGameAction{T}"/>s currently applied to the <typeparamref name="T"/> game state.
        /// </summary>
        public ReadOnlyCollection<IGameAction<T>> Actions => ActionsList.AsReadOnly();
        private List<IGameAction<T>> ActionsList;

        /// <summary>
        /// Returns a read-only collection of all mew <see cref="IGameAction{T}"/>s applied to the <typeparamref name="T"/> game state since the last commit was created.
        /// </summary>
        public ReadOnlyCollection<IGameAction<T>> NewActions => NewActionsList.AsReadOnly();
        private List<IGameAction<T>> NewActionsList;

        /// <summary>
        /// Creates a new <see cref="GameSyncManager{T}"/> to handle a given <typeparamref name="T"/> game state.
        /// </summary>
        /// <param name="gameState">The initial game state of the game, from which all turns will be played.</param>
        /// <param name="saveService">The <see cref="IGameSaveService{T}"/> which will be used to create save copies of the <typeparamref name="T"/> game state.</param>
        public GameSyncManager(T gameState, IGameSaveService<T> saveService)
        {
            GameState = gameState;
            SaveService = saveService;
            InitialGameState = SaveService.Copy(GameState);
            ActionsList = new List<IGameAction<T>>();
            NewActionsList = new List<IGameAction<T>>();
        }

        /// <summary>
        /// Commits all of <see cref="NewActions"/> to a new <see cref="IGameCommit{T}"/> and then migrates them into the <see cref="Actions"/> collection.
        /// </summary>
        public IGameCommit<T> Commit()
        {
            IGameCommit<T> commit = new GameCommit<T>(NewActions);
            ActionsList.AddRange(NewActionsList);
            NewActionsList.Clear();
            return commit;
        }

        /// <summary>
        /// Adds a new action to the <see cref="GameSyncManager{T}"/>, applies it to the <typeparamref name="T"/> state, and adds it to the <see cref="NewActions"/> collection.
        /// </summary>
        /// <param name="newAction">The new <see cref="IGameAction{T}"/> to apply.</param>
        public void AddAction(IGameAction<T> newAction)
        {
            NewActionsList.Add(newAction);
        }

        /// <summary>
        /// Applies an <see cref="IGameCommit{T}"/> to the current <see cref="IGameState"/>.
        /// </summary>
        /// <param name="commit">The received <see cref="IGameCommit{T}"/> containing <see cref="IGameAction{T}"/>s to apply.</param>
        public void ApplyCommit(IGameCommit<T> commit)
        {
            foreach (var action in commit.GetActions())
            {
                ActionsList.Add(action);
                if(action.CanUpdate(GameState))
                {
                    action.Update(GameState);
                }
            }
        }

        /// <summary>
        /// Resets all <see cref="IGameAction{T}"/>s with a new collection of <see cref="IGameState"/>s and rebuilds the game state (equivalent to calling <see cref="RebuildGameState"/>).
        /// </summary>
        /// <param name="updatedActions">The new full list of <see cref="IGameAction{T}"/>s.</param>
        public void ReplaceActions(IEnumerable<IGameAction<T>> updatedActions)
        {
            NewActionsList.Clear();
            ActionsList.Clear();
            ActionsList.AddRange(updatedActions);
            RebuildGameState();
        }

        /// <summary>
        /// Reconstructs the <see cref="IGameState"/> from the initial <see cref="IGameState"/> provided to the <see cref="GameSyncManager{T}"/> and the existing collection <see cref="Actions"/>.
        /// </summary>
        public void RebuildGameState()
        {
            GameState = SaveService.Copy(InitialGameState);
            foreach (var action in Actions.Concat(NewActionsList))
            {
                if (action.CanUpdate(GameState))
                {
                    action.Update(GameState);
                }
            }
        }
    }
}
