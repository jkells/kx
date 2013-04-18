using System;

namespace KX.Core
{
    public class KXBinding : IDisposable
    {
        public virtual void Dispose(bool disposing)
        {            
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}