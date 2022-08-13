namespace Domain.Core.Extensions
{
    public static class LinqExtentions
    {
        public static IEnumerable<T> SelectRecursive<T>(this T source, Func<T, T?> navigationFunc) where T : class
        {
            if (source != null)
            {
                yield return source;

                foreach (var recursiveItem in navigationFunc(source)?.SelectRecursive(navigationFunc) ?? new T[] { })
                {
                    yield return recursiveItem;
                }
            }
        }

        public static IEnumerable<T> SelectManyRecursive<T>(this IEnumerable<T> source, Func<T, IEnumerable<T>> recursiveFunc)
        {
            if (source != null)
            {
                foreach (var mainItem in source)
                {
                    yield return mainItem;

                    IEnumerable<T> recursiveSequence = (recursiveFunc(mainItem) ?? new T[] { }).SelectManyRecursive(recursiveFunc);

                    if (recursiveSequence != null)
                    {
                        foreach (var recursiveItem in recursiveSequence)
                        {
                            yield return recursiveItem;
                        }
                    }
                }
            }
        }
       
        public static IEnumerable<F> SelectManyRecursive<T,F>(this IEnumerable<T> source,
            Func<T, IEnumerable<T>> recursiveFunc,
            Func<T, IEnumerable<F>> dataFunc)
        {
            if (source != null)
            {
                foreach (var mainItem in source)
                {
                    IEnumerable<F> dataSequence = dataFunc(mainItem) ?? new F[] { };

                    foreach (var item in dataSequence)
                        yield return item;

                    IEnumerable<T> recursiveSequence = (recursiveFunc(mainItem) ?? new T[] { }).SelectManyRecursive(recursiveFunc);

                    foreach (var recursiveItem in recursiveSequence)
                    {
                        IEnumerable<F> recursiveItemDataSequence = dataFunc(mainItem) ?? new F[] { };
                        foreach (var item in recursiveItemDataSequence)
                            yield return item;
                    }
                }
            }
        }


        public static List<TSource> ReverseToList<TSource>(this IEnumerable<TSource> source) where TSource : class
            => source.Reverse().ToList();

        public static List<TResult> SelectToList<TSource, TResult>(this IEnumerable<TSource> source, Func<TSource, TResult> selectFunc)
            where TSource : class
            where TResult : class
         => source.Select(selectFunc).ToList();
    }
}
