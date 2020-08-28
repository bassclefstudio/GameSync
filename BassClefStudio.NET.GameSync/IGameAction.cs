using System;
using System.Collections.Generic;
using System.Text;

namespace BassClefStudio.NET.GameSync
{
    /// <summary>
    /// Represents an action that changes the state of an <see cref="IGameState"/>.
    /// </summary>
    public interface IGameAction<in T> where T : IGameState
    {
        Guid Id { get; }
        bool CanUpdate(T gameState);
        void Update(T gameState);
    }
}
