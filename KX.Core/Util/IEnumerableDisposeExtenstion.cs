using System;
using System.Collections;
using System.Linq;

namespace KX.Core.Util
{
    public static class EnumerableDisposeExtenstion
    {
        public static void DisposeAll(this IEnumerable items)
        {
            foreach (var disposable in items.OfType<IDisposable>())
            {
                disposable.Dispose();
            }
        }
    }
}
