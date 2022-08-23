using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using UDL.Core.Helper;
using UnityEngine.EventSystems;
using UnityEditor;

namespace UDL.Core
{
	public class ScreenLoader
	{
		static bool eventSystemInited;
		static bool newInputSystem = true;

		static Dictionary<int, List<GameObject>> orders = new Dictionary<int, List<GameObject>>();

		public static T Load<T>(string path, int sortOrder = 3)
		{
			if(eventSystemInited == false)
            {
				eventSystemInited = true;

				if (newInputSystem == false)
				{
					var eventSystem = GameObject.FindObjectOfType<StandaloneInputModule>();
					if (eventSystem == null)
					{
						new GameObject("EvenSystem", typeof(StandaloneInputModule));
					}
				}
			}

			if(!orders.ContainsKey(sortOrder))
            {
				orders[sortOrder] = new List<GameObject>();
			}

			orders[sortOrder].RemoveAll(item => item == null);

			var prefab = Resources.Load<GameObject>(path);

			if (prefab == null)
			{
				throw new System.Exception(path + " doesn't exist");
			}

			var go = Object.Instantiate(prefab);
			orders[sortOrder].Add(go);

			var canvas = go.GetComponent<Canvas>();

			if(canvas == null)
            {
				throw new System.Exception("Canvas component is missing on the screen :" + path);
			}

			canvas.sortingOrder = sortOrder * 10 + orders[sortOrder].Count;

			//go.AddComponent<LimitedCanvas>(); TODO: Do we really need LimitedCanvas?

			T view = go.GetComponent<T>();

			return view;
		}
	}
}