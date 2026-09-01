using OnlineStoreOrder;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineStoreOrder
{
    public class ManifestExporter : IDisposable
    {
        private readonly string _filePath;

        private bool _disposed;

        public bool IsDisposed => _disposed;

        public ManifestExporter(string filePath)
        {
            _filePath = filePath;
            File.WriteAllText(_filePath, string.Empty);
        }

        public void WriteOrder(Order order, string handlingNotes)
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(
                    nameof(ManifestExporter));
            }

            string line =
                $"OrderId={order.OrderId} | " +
                $"CustomerId={order.CustomerId} | " +
                $"Amount={order.TotalAmount} | " +
                $"Express={order.IsExpress} | " +
                $"Handling={handlingNotes}";

            File.AppendAllText(_filePath,line);
        }

        public void Dispose()
        {
            if (_disposed) return;
            _disposed = true;


        }

        ~ManifestExporter()
        {
            Dispose();
        }
    }
}
