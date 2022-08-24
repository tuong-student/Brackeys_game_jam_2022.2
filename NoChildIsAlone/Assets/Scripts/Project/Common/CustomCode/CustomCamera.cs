using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CustomCamera : MonoBehaviour
{
    #region Components
    [SerializeField] GameObject target;
    [SerializeField] Transform FPSCamTransform;
    [SerializeField] Vector3 offset;
    [SerializeField] float smoothTime;
    #endregion

    #region Stats
    [SerializeField] float duration, magnitude;
    [SerializeField] float explodeMagnitude;
    public bool isCamHead;
    Vector3 originalRotation;
    Vector3 currentVelocity = Vector3.zero;
    #endregion

    public static CustomCamera InsCustomCamera;

    void Awake()
    {
        if (InsCustomCamera == null) InsCustomCamera = this;
    }

    private void Start()
    {
        originalRotation = this.transform.eulerAngles;
    }

    void Update()
    {
        if(!isCamHead)
            FollowTarget();
    }

    public void FollowTarget()
    {
        NOOD.NoodyCustomCode.SmoothCameraFollow(this.gameObject, smoothTime, target.transform, offset);
    }

    public void SetCamToHead()
    {
        this.transform.DOMove(target.transform.position, 1f);
        this.transform.DORotate(FPSCamTransform.eulerAngles, 1f);
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
