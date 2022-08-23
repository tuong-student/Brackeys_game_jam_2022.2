using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public static class ComponentExtensions
{
    public static GameObject FindDeep(
        this Component self,
        string name,
        bool includeInactive = false)
    {
        var children = self.GetComponentsInChildren<Transform>(includeInactive);
        foreach (var transform in children)
        {
            if (transform.name == name)
            {
                return transform.gameObject;
            }
        }
        return null;
    }


    public static List<Material> FindMaterials(
        this Component self,
        bool includeInactive = false,
        bool sharedMaterial = false)
    {
        var materials = new List<Material>();

        var children = self.GetComponentsInChildren<Transform>(includeInactive);
        foreach (var transform in children)
        {
            var renderer = transform.GetComponent<Renderer>();
            if (renderer == null)
                continue;

            Material[] mats;
            if (sharedMaterial)
            {
                mats = renderer.sharedMaterials;
            }
            else
            {
                mats = renderer.materials;
            }

            foreach (var material in mats)
            {
                if (material != null)
                {
                    materials.Add(material);
                }
            }
        }
        return materials;
    }

    public static void SetLayer(this Component self, string layerName, bool includeInactive = true, List<string> ignoreLayerName = null)
    {
        var children = self.GetComponentsInChildren<Transform>(includeInactive);
        foreach (var transform in children)
        {
            if (ignoreLayerName != null)
            {
                if (!ignoreLayerName.Contains(LayerMask.LayerToName(transform.gameObject.layer)))
                {
                    transform.gameObject.layer = LayerMask.NameToLayer(layerName);
                }
            }
            else
            {
                transform.gameObject.layer = LayerMask.NameToLayer(layerName);
            }
        }
    }

    public static void SetTag(this Component self, string tagName, bool includeInactive = true, List<string> ignoreTagName = null)
    {
        var children = self.GetComponentsInChildren<Transform>(includeInactive);
        foreach (var transform in children)
        {
            if (ignoreTagName != null)
            {
                if (!ignoreTagName.Contains(transform.gameObject.tag))
                {
                    transform.gameObject.tag = tagName;
                }
            }
            else
            {
                transform.gameObject.tag = tagName;
            }
        }
    }

    #region animator
    public static List<Animator> SetBool(this List<Animator> source, string key, bool result)
    {
        for (int i = 0; i < source.Count; i++)
        {
            var animator = source[i];
            if (animator != null && animator.isActiveAndEnabled && animator.runtimeAnimatorController != null)
            {
                if (animator.HasParameter(key))
                {
                    animator.SetBool(key, result);
                }
            }
        }
        return source;
    }

    public static bool HasParameter(this Animator animator, string paramName)
    {
        foreach (AnimatorControllerParameter param in animator.parameters)
        {
            if (param.name == paramName)
                return true;
        }
        return false;
    }

    public static List<Animator> SetTrigger(this List<Animator> source, string key)
    {
        for (int i = 0; i < source.Count; i++)
        {
            var animator = source[i];
            if (animator != null && animator.isActiveAndEnabled && animator.runtimeAnimatorController != null)
            {
                if (animator.HasParameter(key))
                {
                    animator.SetTrigger(key);
                }
            }
        }
        return source;
    }

    public static List<Animator> ResetTrigger(this List<Animator> source, string key)
    {
        for (int i = 0; i < source.Count; i++)
        {
            var animator = source[i];
            if (animator != null && animator.isActiveAndEnabled && animator.runtimeAnimatorController != null)
            {
                if (animator.HasParameter(key))
                {
                    animator.ResetTrigger(key);
                }
            }
        }
        return source;
    }

    public static List<Animator> SetFloat(this List<Animator> source, string key, float value)
    {
        for (int i = 0; i < source.Count; i++)
        {
            var animator = source[i];
            if (animator != null && animator.isActiveAndEnabled && animator.runtimeAnimatorController != null)
            {
                if (animator.HasParameter(key))
                {
                    animator.SetFloat(key, value);
                }
            }
        }
        return source;
    }

    public static List<Animator> SetSpeed(this List<Animator> source, float speed)
    {
        for (int i = 0; i < source.Count; i++)
        {
            var animator = source[i];
            if (animator != null && animator.isActiveAndEnabled)
            {
                animator.speed = speed;
            }
        }
        return source;
    }

    public static List<Animator> PlayBackTime(this List<Animator> source, float time)
    {
        for (int i = 0; i < source.Count; i++)
        {
            var animator = source[i];
            if (animator != null && animator.isActiveAndEnabled)
            {
                animator.playbackTime = time;
            }
        }
        return source;
    }

    public static List<Animator> PlayAtTime(this List<Animator> source, string stateName, float time)
    {
        for (int i = 0; i < source.Count; i++)
        {
            var animator = source[i];
            if (animator != null && animator.runtimeAnimatorController != null)
            {
                animator.Play(stateName, 0, time);
            }
        }
        return source;
    }

    #endregion

    #region partical system
    public static List<ParticleSystem> SetPause(this List<ParticleSystem> source, bool isPause)
    {
        for (int i = 0; i < source.Count; i++)
        {
            var particle = source[i];
            if (isPause)
            {
                particle.Pause(true);
            }
            else
            {
                particle.Play(true);
            }
        }
        return source;
    }
    #endregion
}