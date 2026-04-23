using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CameraZoomFeedback : Feedback
{
    private CinemachineVirtualCamera _cinemachine;
    [SerializeField] private float zoomTime = 0.6f;

    private void Awake()
    {
        _cinemachine = GameObject.Find("Virtual Camera").GetComponent<CinemachineVirtualCamera>();
    }

    public override void PlayFeedback()
    {
        _cinemachine.Priority = 8;
        StartCoroutine(ZoomOutTime());
    }

    IEnumerator ZoomOutTime()
    {
        yield return new WaitForSeconds(zoomTime);
        _cinemachine.Priority = 10;

    }
}