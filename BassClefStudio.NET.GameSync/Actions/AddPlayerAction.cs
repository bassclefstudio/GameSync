using BassClefStudio.NET.GameSync.Clients;
using BassClefStudio.NET.GameSync.Commits;
using BassClefStudio.NET.GameSync.State;
using System;
using System.Collections.Generic;
using System.Text;

namespace BassClefStudio.NET.GameSync.Actions
{
    /// <summary>
    /// Represents an <see cref="IGameAction{T}"/> that adds the given <see cref="SigningPlayer"/> to the <see cref="GameState.Players"/> of any <see cref="GameState"/>.
    /// </summary>
    public class AddPlayerAction : IGameAction<GameState>
    {
        /// <inheritdoc/>
        public Guid SigningPlayerId => SigningPlayer.Id;

        /// <summary>
        /// The <see cref="Player"/> to add to the game.
        /// </summary>
        public Player SigningPlayer { get; }

        /// <summary>
        /// Creates a new <see cref="AddPlayerAction"/>.
        /// </summary>
        /// <param name="signingPlayer">The <see cref="Player"/> to add to the game.</param>
        public AddPlayerAction(Player signingPlayer)
        {
            SigningPlayer = signingPlayer;
        }

        /// <inheritdoc/>
        public bool CanUpdate(GameState gameState)
        {
            return !gameState.GameStarted;
        }

        /// <inheritdoc/>
        public void Update(GameState gameState)
        {
            gameState.Players.Add(SigningPlayer);
        }
    }
}
