using System;
using UnityEngine;
using UnityEngine.Events;

public class HydrengeaSkill : GunSkill
{
    public Action OnBarrierChanged;
    protected override void AwakeSkill()
    {
        base.AwakeSkill();
        
        if(eventFeedbacks != null) OnBarrierChanged += eventFeedbacks.PlayFeedbacks;
        _gun.DamageCastCompo.OnShoot += HandleOnShoot;
    }
    
    public override void EnterSkill()
    {
        base.EnterSkill();
        Shoot();
    }

    private void HandleOnShoot(bool isTrigger)
    {
        if (isTrigger)
        {
            _stat.BarrierCount++;
            OnBarrierChanged?.Invoke();
        }
    }

    private void OnDisable()
    {
        _gun.DamageCastCompo.OnShoot -= HandleOnShoot;
    }
}
