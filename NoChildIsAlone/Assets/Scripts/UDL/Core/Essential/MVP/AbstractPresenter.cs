using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UDL.Core
{
	public abstract class AbstractPresenter : IDisposable 
	{
        AbstractModel model;
		List<MonoBehaviour> views;

		IDisposable viewSubscription;
		IDisposable modelSubscription;
		
		public AbstractPresenter (AbstractModel model, MonoBehaviour view, MonoBehaviour view2 = null, MonoBehaviour view3 = null, MonoBehaviour view4 = null, MonoBehaviour view5 = null)
		{
            var list = new List<MonoBehaviour>() { view };

            if (view2 != null)
                list.Add(view2);
            if (view3 != null)
                list.Add(view3);
            if (view4 != null)
                list.Add(view4);
            if (view5 != null)
                list.Add(view5);

            HiddenConstructor (model, list);
		}

		public AbstractPresenter (AbstractModel model, List<MonoBehaviour> views)
		{
			HiddenConstructor (model, views);
		}

		void HiddenConstructor(AbstractModel model, List<MonoBehaviour> views)
		{
			this.model = model;
			this.views = views;

			foreach (var view in views) {
				AbstractView abstractView = view.GetComponent<AbstractView> ();
				if (abstractView != null) {
					if (abstractView is CommunicableAbstractView && model is CommunicableAbstractModel)
					{
						((CommunicableAbstractView)abstractView).SetCommunicable((CommunicableAbstractModel)model);
					}
					viewSubscription = abstractView.OnDispose.Subscribe(Dispose);
				}
			}

			modelSubscription = model.OnDispose.Subscribe(Dispose);
		}

		#region IDisposable implementation

		public void Dispose ()
		{
			model.Dispose();

			viewSubscription.Dispose();
			modelSubscription.Dispose();

			foreach (var view in views) {
                if (view == null) continue;
                var abstractView = view.GetComponent<AbstractView>();
                if (abstractView != null)
                {
                    abstractView.Dispose();
                }
                else
                {
                    UnityEngine.Object.Destroy(view.gameObject);
                }
			}
		}

		#endregion

	}
}