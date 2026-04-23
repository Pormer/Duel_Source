using System;
using UnityEngine;
using UnityEngine.Events;

public class LaurusSkill : GunSkill
{
    public Action OnFormChanged;
    
    [SerializeField] private int curStep;
    [SerializeField] private int wantStep = 7;
    [SerializeField] private int wantDamage = 3;
    protected override void AwakeSkill()
    {
        base.AwakeSkill();
        
        if(eventFeedbacks != null) OnFormChanged += eventFeedbacks.PlayFeedbacks;
        _player.GetCompo<PlayerMovement>().OnMove += () => curStep++;
    }

    public override void EnterSkill()
    {
        base.EnterSkill();
        if (curStep >= wantStep)
        {
            OnFormChanged?.Invoke();
            _player.StatDataCompo.Damage = wantDamage;
            curStep = 0;
        }
        Shoot();
        _player.StatDataCompo.Damage = 1;
    }
}
