using System.Collections;
using System.Collections.Generic;
using ObjectPooling;
using UnityEngine;

public class EffectFeedback : Feedback
{
    [SerializeField] private PoolingType effectType;
    [SerializeField] private float effectScale = 1;
    [SerializeField] private float lifeTime = 2;
    [SerializeField] private Transform effectPos;
    private EffectPlayer effect;

    public override void PlayFeedback()
    {
        effect = PoolManager.Instance.Pop(effectType) as EffectPlayer;
        effect.transform.localScale *= effectScale;

        if (effectPos == null)
        {
            effect.SetPositionAndPlay(new Vector3(0.5f, transform.position.y, 0f));
            return;
        }

        effect.SetPositionAndPlay(effectPos.position);
        //StartCoroutine(DelayAndGotoPoolCoroutine());
    }

    /*private IEnumerator DelayAndGotoPoolCoroutine()
    {
        yield return new WaitForSeconds(lifeTime);
        PoolManager.Instance.Push(effect);
    }*/
}