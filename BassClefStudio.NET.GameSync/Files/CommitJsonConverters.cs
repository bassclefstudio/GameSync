using BassClefStudio.NET.GameSync.Commits;
using BassClefStudio.Serialization;
using BassClefStudio.Serialization.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace BassClefStudio.NET.GameSync.Files
{
    public class CommitToJsonConverter<T> : IToJsonConverter<IGameCommit<T>>
    {
    }
}
