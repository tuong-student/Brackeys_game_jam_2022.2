using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UDL.Core.Helper
{
    public class LimitedCanvas : MonoBehaviour
    {
        float maxAspectRatio = 2960.0f / 1440.0f;

        RectTransform canvasRect;
        RectTransform containerRect;

        List<RectTransform> veilVRects;
        List<RectTransform> veilHRects;

        void Awake()
        {
            canvasRect = this.GetComponent<RectTransform>();

            veilVRects = new List<RectTransform>();
            veilVRects.Add(CreateVeil("topVeil", new Vector2(0, 0), new Vector2(0, 1), new Vector2(1, 1), new Vector2(0.5f, 1)));
            veilVRects.Add(CreateVeil("bottomVeil", new Vector2(0, 0), new Vector2(0, 0), new Vector2(1, 0), new Vector2(0.5f, 0)));
            veilHRects = new List<RectTransform>();
            veilHRects.Add(CreateVeil("leftVeil", new Vector2(0, 0), new Vector2(0, 0), new Vector2(0, 1), new Vector2(0, 1)));
            veilHRects.Add(CreateVeil("rightVeil", new Vector2(0, 0), new Vector2(1, 0), new Vector2(1, 1), new Vector2(1, 1)));            
        }

        public void SetMaxAspectRatio(float maxAspectRatio)
        {
            this.maxAspectRatio = maxAspectRatio;
        }

        RectTransform CreateVeil(string veilName, Vector2 anchoredPosition, Vector2 ancorMin, Vector2 anchorMax, Vector2 pivot)
        {
            var topVeil = new GameObject(veilName);
            topVeil.transform.SetParent(this.transform);
            RectTransform rectTransform = topVeil.AddComponent<RectTransform>();
            rectTransform.anchoredPosition = anchoredPosition;
            rectTransform.anchorMin = ancorMin;
            rectTransform.anchorMax = anchorMax;
            rectTransform.pivot = pivot;
            rectTransform.localScale = Vector3.one;
            Image image = topVeil.AddComponent<Image>();
            image.color = Color.black;
            return rectTransform;
        }

        void Update()
        {
            float width = canvasRect.sizeDelta.x;
            float height = canvasRect.sizeDelta.y;

            float maxHeight = width * maxAspectRatio;
            float maxWidth = height * maxAspectRatio;

            //containerRect.sizeDelta = new Vector2(Mathf.Min(0, maxWidth - width), Mathf.Min(0, maxHeight - height));
            // TODO: temporary, the root canvas is also the container of the screen, how should we improve this???
            canvasRect.sizeDelta = new Vector2(Mathf.Min(0, maxWidth - width), Mathf.Min(0, maxHeight - height));

            {
                float delta = (height - maxHeight) / 2;
                foreach (var veilRect in veilVRects)
                {
                    veilRect.sizeDelta = new Vector2(0, delta);
                }
            }

            {
                float delta = (width - maxWidth) / 2;
                foreach (var veilRect in veilHRects)
                {
                    veilRect.sizeDelta = new Vector2(delta, 0);
                }
            }

        }
    }
}