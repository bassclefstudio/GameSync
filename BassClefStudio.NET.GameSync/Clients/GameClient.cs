using System;
using System.Collections.Generic;
using System.Text;

namespace BassClefStudio.NET.GameSync.Clients
{
    /// <summary>
    /// Represents a client - a physical computer, bot, or service - that can perform actions and store state information as one or more <see cref="Player"/>s.
    /// </summary>
    public class GameClient
    {
        /// <summary>
        /// The collection of <see cref="Player"/>s that this <see cref="GameClient"/> manages.
        /// </summary>
        public IEnumerable<Player> Players { get; }

        /// <summary>
        /// Creates a new <see cref="GameClient"/> from a collection of <see cref="Player"/>s.
        /// </summary>
        /// <param name="players">The collection of <see cref="Player"/>s that this <see cref="GameClient"/> manages.</param>
        public GameClient(IEnumerable<Player> players)
        {
            Players = players;
        }
    }
}
