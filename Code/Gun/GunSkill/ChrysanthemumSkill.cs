using System;
using UnityEngine;
using UnityEngine.Events;

public class ChrysanthemumSkill : GunSkill
{
    public Action OnHit;
    
    protected override void AwakeSkill()
    {
        base.AwakeSkill();
        
        if(eventFeedbacks != null) OnHit += eventFeedbacks.PlayFeedbacks;

        _player.GetCompo<Health>().OnHitEvent.AddListener(() =>
        {
            if(_stat.Damage <= 0) return;
            _stat.Damage--;
            OnHit?.Invoke();
        });
    }

    public override void EnterSkill()
    {
        base.EnterSkill();
        Shoot();
    }
}