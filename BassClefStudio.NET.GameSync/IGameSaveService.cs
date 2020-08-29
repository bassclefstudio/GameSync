using System;
using System.Collections.Generic;
using System.Text;

namespace BassClefStudio.NET.GameSync
{
    public interface IGameSaveService<in T> where T : IGameState
    {
        T Copy(T state);
    }
}
