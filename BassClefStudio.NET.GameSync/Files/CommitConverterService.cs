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
    public class CommitConverterService<T> : ConverterService<IGameCommit<T>, JToken, JToken> where T : GameState
    {
        public CommitConverterService(params Assembly[] assemblies) : base(assemblies)
        { }

        public override IGameCommit<T> ReadItem(JToken input)
        {
            return ConverterContainer.Resolve<IFromJsonConverter<IGameCommit<T>>>().GetTo(input);
        }

        public override JToken WriteItem(IGameCommit<T> item)
        {
            return ConverterContainer.Resolve<IToJsonConverter<IGameCommit<T>>>().GetTo(item);
        }
    }
}
