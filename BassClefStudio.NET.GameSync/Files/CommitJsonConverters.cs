using BassClefStudio.NET.GameSync.Commits;
using BassClefStudio.NET.GameSync.State;
using BassClefStudio.Serialization;
using BassClefStudio.Serialization.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace BassClefStudio.NET.GameSync.Files
{
    /// <summary>
    /// An <see cref="IToJsonConverter{T}"/> for seserializing <see cref="IGameCommit{T}"/>s.
    /// </summary>
    public class CommitToJsonConverter<T> : IToJsonConverter<IGameCommit<T>> where T : GameState
    {
        public IEnumerable<IToJsonConverter<IGameAction<T>>> ActionConverters { get; set; }

        /// <inheritdoc/>
        public bool CanConvert(IGameCommit<T> item) => true;

        /// <inheritdoc/>
        public JToken Convert(IGameCommit<T> item)
        {
            return new JObject(
                new JProperty("type", "commit"),
                new JProperty("actions", item.GetActions().Select(a => ActionConverters.GetTo(a))));
        }
    }

    /// <summary>
    /// An <see cref="IFromJsonConverter{T}"/> for deserializing <see cref="IGameCommit{T}"/>s.
    /// </summary>
    public class CommitFromJsonConverter<T> : IFromJsonConverter<IGameCommit<T>> where T : GameState
    {
        public IEnumerable<IFromJsonConverter<IGameAction<T>>> ActionConverters { get; set; }

        /// <inheritdoc/>
        public bool CanConvert(JToken item) => item.IsJsonType("commit");

        /// <inheritdoc/>
        public IGameCommit<T> Convert(JToken item)
        {
            IEnumerable<IGameAction<T>> actions = item["actions"].Select(a => ActionConverters.GetTo(a));
            return new GameCommit<T>(actions);
        }
    }
}
