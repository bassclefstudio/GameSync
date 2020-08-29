using BassClefStudio.NET.GameSync.Commits;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BassClefStudio.NET.GameSync.Id
{
    /// <summary>
    /// Represents an object that can be referenced by the game engine and <see cref="IGameAction{T}"/>s by a unique ID.
    /// </summary>
    public interface IIdentifiable
    {
        /// <summary>
        /// A unique <see cref="Guid"/> id that can identify the <see cref="IIdentifiable"/> object.
        /// </summary>
        Guid Id { get; }
    }

    public static class IdentifiableExtensions
    {
        /// <summary>
        /// Returns a <typeparamref name="T"/> value from a collection of <typeparamref name="T"/> <see cref="IIdentifiable"/> objects by its unique <see cref="Guid"/> ID.
        /// </summary>
        /// <typeparam name="T">The type of <see cref="IIdentifiable"/> to look through.</typeparam>
        /// <param name="items">The collection of <typeparamref name="T"/> to search.</param>
        /// <param name="id">The unique <see cref="Guid"/> of the desired item.</param>
        public static T Get<T>(this IEnumerable<T> items, Guid id) where T : IIdentifiable
        {
            return items.First(t => t.Id == id);
        }
    }
}
