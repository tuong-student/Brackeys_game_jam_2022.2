using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomCamera : MonoBehaviour
{
    #region Components
    [SerializeField] GameObject target;
    [SerializeField] Vector3 offset;
    [SerializeField] float smoothTime;
    #endregion

    #region Stats
    [SerializeField] float duration, magnitude;
    [SerializeField] float explodeMagnitude;
    #endregion

    public static CustomCamera InsCustomCamera;

    void Awake()
    {
        if (InsCustomCamera == null) InsCustomCamera = this;
    }

    void Update()
    {
        FollowTarget();
    }

    public void FollowTarget()
    {
        NOOD.NoodyCustomCode.SmoothCameraFollow(this.gameObject, smoothTime, target.transform, offset);
    }

    public void Shake()
    {
        StartCoroutine(NOOD.NoodyCustomCode.ObjectShake(this.gameObject, duration, magnitude));
    }

    public void ExplodeShake()
    {
        StartCoroutine(NOOD.NoodyCustomCode.ObjectShake(this.gameObject, duration, explodeMagnitude));
    }
}
