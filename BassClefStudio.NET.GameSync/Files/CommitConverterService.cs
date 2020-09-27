using Autofac;
using BassClefStudio.NET.GameSync.Commits;
using BassClefStudio.NET.GameSync.State;
using BassClefStudio.Serialization;
using BassClefStudio.Serialization.DI;
using BassClefStudio.Serialization.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace BassClefStudio.NET.GameSync.Files
{
    /// <summary>
    /// Represents a service for converting <see cref="IGameCommit{T}"/> objects to and from JSON.
    /// </summary>
    /// <typeparam name="T">The generic parameter for the <see cref="IGameCommit{T}"/>s being converted.</typeparam>
    public class CommitConverterService<T> : ConverterService<IGameCommit<T>, JToken, JToken> where T : GameState
    {
        /// <summary>
        /// Creates a new <see cref="CommitConverterService{T}"/>.
        /// </summary>
        /// <param name="assemblies">A collection of <see cref="Assembly"/> objects used for retrieving the necessary <see cref="IConverter{TFrom, TTo}"/>s for (de)serialization.</param>
        public CommitConverterService(params Assembly[] assemblies) : base(assemblies)
        { }

        /// <inheritdoc/>
        public override IGameCommit<T> ReadItem(JToken input)
        {
            return ConverterContainer.Resolve<IFromJsonConverter<IGameCommit<T>>>().GetTo(input);
        }

        /// <inheritdoc/>
        public override JToken WriteItem(IGameCommit<T> item)
        {
            return ConverterContainer.Resolve<IToJsonConverter<IGameCommit<T>>>().GetTo(item);
        }
    }
}
