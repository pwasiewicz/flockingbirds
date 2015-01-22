namespace FlockingSimulation.Extensions
{
    using System;
    using System.Collections.Generic;

    public static class EnumerableExtension
    {
        public static IEnumerable<T> GenerateSequence<T>(
            int count,
            Func<T> elementInitalizer,
            Type collectionType = null)
        {
            if (elementInitalizer == null)
            {
                throw new ArgumentNullException("elementInitalizer");
            }

            if (collectionType == null)
            {
                return GenerateYieldSequence(count, elementInitalizer);
            }

            return GenerateSequenceWithCustomType(
                count,
                elementInitalizer,
                collectionType);
        }

        private static IEnumerable<T> GenerateSequenceWithCustomType<T>(
            int count,
            Func<T> elementInitalizer,
            Type collectionType)
        {
            var enumerableType = typeof(ICollection<T>);

            if (!enumerableType.IsAssignableFrom(collectionType))
            {
                throw new InvalidOperationException(
                    "Collection type must implement ICollection interface.");
            }

            var collection = Activator.CreateInstance(collectionType) as ICollection<T>;
            if (collection == null)
            {
                throw new InvalidOperationException(
                    "Collection type must implement ICollection interface.");
            }

            for (var i = 0; i < count; i++)
            {
                collection.Add(elementInitalizer());
            }

            return collection;
        }

        private static IEnumerable<T> GenerateYieldSequence<T>(
            int count,
            Func<T> elementInitalizer)
        {
            for (var i = 0; i < count; i++)
            {
                yield return elementInitalizer();
            }
        }

    }
}
