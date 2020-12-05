using Autofac;
using BassClefStudio.NET.GameSync.Commits;
using BassClefStudio.NET.GameSync.State;
using BassClefStudio.Serialization;
using BassClefStudio.Serialization.DI;
using BassClefStudio.Serialization.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
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
        /// <param name="assemblies">A collection of <see cref="Assembly"/> objects used for retrieving the necessary <see cref="IConverter{TFrom, TTo}"/>s for serialization and deserialization. Includes by default the core GameSync assembly and the <typeparamref name="T"/> assembly.</param>
        public CommitConverterService(Assembly[] assemblies) : base(new Assembly[] { typeof(T).GetTypeInfo().Assembly }.Concat(assemblies).ToArray())
        { }

        /// <inheritdoc/>
        protected override void ConfigureServices(ContainerBuilder builder)
        {
            builder.RegisterGeneric(typeof(CommitFromJsonConverter<>))
                .AsImplementedInterfaces();

            builder.RegisterGeneric(typeof(CommitToJsonConverter<>))
                .AsImplementedInterfaces();
        }

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
