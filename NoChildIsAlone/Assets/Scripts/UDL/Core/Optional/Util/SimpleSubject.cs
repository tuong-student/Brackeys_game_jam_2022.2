using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

namespace UDL.Core
{

    public class SimpleSubject : IDisposable
    {
        List<Action> actions = new List<Action>();

        public void OnNext()
        {
            var temporaryAction = new List<Action>(actions);
            foreach (var action in temporaryAction)
            {
                action?.Invoke();
            }
        }

        public IDisposable Subscribe(Action action)
        {
            actions.Add(action);

            return this;
        }

        public void Dispose()
        {
            actions.Clear();
        }
    }
}
