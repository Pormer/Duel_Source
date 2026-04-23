using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlinkFeedback : Feedback
{
    [SerializeField] private SpriteRenderer _targetRenderer;
    [SerializeField] private float flashTime = 0.1f;

    private Material _targetMat;

    private readonly int _isHitHash = Shader.PropertyToID("_IsHit");

    private void Awake()
    {
        _targetMat = _targetRenderer.material; //스프라이트 랜더러에 있는 매티리얼을 가져온다.
    }

    public override void PlayFeedback()
    {
        _targetMat.SetInt(_isHitHash, 1);
        StartCoroutine(DelayBlink());
    }

    private IEnumerator DelayBlink()
    {
        yield return new WaitForSeconds(flashTime);
        _targetMat.SetInt(_isHitHash, 0);
    }
}
