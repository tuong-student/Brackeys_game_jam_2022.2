using System;

namespace UDL.Core
{
    public static class CoreExtentions
    {
        public static T AddToDisposableEntity<T>(this T disposable, IModel entity) where T : IDisposable
        {
            entity.OnDispose.Subscribe(() =>
            {
                disposable.Dispose();
            });

            return disposable;
        }
    }
}