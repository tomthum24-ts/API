using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseCommon.Model
{
    public class DisposableModel : IDisposable
    {
        private bool IsDisposed { get; set; }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool isDisposing)
        {
            if (IsDisposed) return;
            if (isDisposing) DisposeUnmanagedResources();
            IsDisposed = true;
        }

        protected virtual void DisposeUnmanagedResources()
        {
        }

        ~DisposableModel()
        {
            Dispose(false);
        }
    }
}
