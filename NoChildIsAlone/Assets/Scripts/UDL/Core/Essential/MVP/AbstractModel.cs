using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System;
using System.Collections.ObjectModel;

namespace UDL.Core
{
    public abstract class AbstractModel : IModel
    {
        public SimpleSubject OnDispose { get; } = new SimpleSubject();
        public List<IDisposable> disposables { get; } = new List<IDisposable>();

        bool disposed = false;

        #region IDisposable implementation
        public virtual void Dispose()
        {
            if (disposed == false)
            {
                disposed = true;
                OnDispose.OnNext();

                foreach (var disposable in disposables)
                {
                    disposable.Dispose();
                }
                disposables.Clear();
            }
        }
        #endregion


    }
}