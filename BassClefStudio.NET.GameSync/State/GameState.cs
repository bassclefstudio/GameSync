using BassClefStudio.NET.Core;
using BassClefStudio.NET.GameSync.Clients;
using BassClefStudio.NET.GameSync.Commits;
using System;
using System.Collections.Generic;
using System.Text;

namespace BassClefStudio.NET.GameSync.State
{
    /// <summary>
    /// Represents the current state of a multiplayer game.
    /// </summary>
    public abstract class GameState : Observable
    {
        /// <summary>
        /// Represents a collection of <see cref="Player"/>s who are participating in the game.
        /// </summary>
        public List<Player> Players { get; }

        private bool started;
        /// <summary>
        /// A <see cref="bool"/> value indicating whether the 
        /// </summary>
        public bool GameStarted { get => started; set => Set(ref started, value); }

        /// <summary>
        /// Creates a new empty <see cref="GameState"/>.
        /// </summary>
        public GameState()
        {
            Players = new List<Player>();
            GameStarted = false;
        }
    }
}
