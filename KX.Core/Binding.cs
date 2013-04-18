using System;

namespace KX.Core
{
    public class Binding : IDisposable
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