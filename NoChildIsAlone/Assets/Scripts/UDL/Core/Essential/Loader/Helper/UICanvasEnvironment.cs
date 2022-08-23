#if UNITY_EDITOR

using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Experimental.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

namespace UDL.Core.Helper
{
    [InitializeOnLoad]
    public class UICanvasEnvironment
    {
		static UICanvasEnvironment()
		{
			if (EditorApplication.isPlaying)
			{
				return;
			}

			PrefabStage.prefabStageOpened += stage =>
			{
				if(stage.prefabContentsRoot.transform.parent == null)
                {
					return;
                }

				var rootEnvObject = stage.prefabContentsRoot.transform.parent.gameObject;

				var envCanvas = rootEnvObject.GetComponent<Canvas>();
				if(envCanvas == null) return;

				var prefabCanvas = AssetDatabase.LoadAssetAtPath<Canvas>(PrefabStageUtility.GetCurrentPrefabStage().prefabAssetPath);
				if (prefabCanvas == null) return;

				stage.prefabContentsRoot.transform.localScale = Vector3.one;

				envCanvas.hideFlags = HideFlags.DontSave;
				envCanvas.renderMode = prefabCanvas.renderMode;

				var prefabScalerCanvas = AssetDatabase.LoadAssetAtPath<CanvasScaler>(PrefabStageUtility.GetCurrentPrefabStage().prefabAssetPath);
				if(prefabScalerCanvas != null)
                {
					var envCanvasScaler = rootEnvObject.GetComponent<CanvasScaler>();
					if(envCanvasScaler == null)
                    {
						envCanvasScaler = rootEnvObject.AddComponent<CanvasScaler>();
						envCanvasScaler.hideFlags = HideFlags.DontSave;
					}

					envCanvasScaler.uiScaleMode = prefabScalerCanvas.uiScaleMode;
					envCanvasScaler.referenceResolution = prefabScalerCanvas.referenceResolution;
					envCanvasScaler.matchWidthOrHeight = prefabScalerCanvas.matchWidthOrHeight;
					envCanvasScaler.referencePixelsPerUnit = prefabScalerCanvas.referencePixelsPerUnit;
					envCanvasScaler.scaleFactor = prefabScalerCanvas.scaleFactor;
				}
			};
		}
	}
}

#endif