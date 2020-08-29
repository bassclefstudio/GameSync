﻿using System;
using System.Collections.Generic;
using System.Text;

namespace BassClefStudio.NET.GameSync.Commits
{
    /// <summary>
    /// Represents a client or individual on a shared client (for example, an AI on a device that also has a human player) that can create and sign <see cref="IGameCommit{T}"/>.
    /// </summary>
    public class Player
    {
        /// <summary>
        /// A unique <see cref="Guid"/> id that can identify the <see cref="Player"/> to other clients.
        /// </summary>
        public Guid Id { get; }

        /// <summary>
        /// The <see cref="Player"/>'s display name.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Creates a new <see cref="Player"/> from their unique <see cref="Guid"/> and display name.
        /// </summary>
        /// <param name="id">The id of the <see cref="Player"/>.</param>
        /// <param name="name">The <see cref="Player"/>'s display name.</param>
        public Player(Guid id, string name)
        {
            Id = id;
            Name = name;
        }
    }

    /// <summary>
    /// An enum indicating the features that a <see cref="Player"/> supports.
    /// </summary>
    [Flags]
    public enum PlayerType
    { 
        /// <summary>
        /// Indicates a <see cref="Player"/> can take turns as a human player.
        /// </summary>
        Human = 1,

        /// <summary>
        /// Indicates a <see cref="Player"/> can take turns as a computer-controlled player.
        /// </summary>
        AI = 2,

        /// <summary>
        /// Indicates the <see cref="Player"/> is responsible for processing turns from other clients.
        /// </summary>
        Host = 4
    }
}