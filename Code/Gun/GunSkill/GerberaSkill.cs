using System;
using UnityEngine;
using UnityEngine.Events;

public class GerberaSkill : GunSkill
{
    public Action OnHealth;

    [SerializeField] private int wantShootCount = 2;
    private int _shootCount = 0;
    
    protected override void AwakeSkill()
    {
        base.AwakeSkill();
        
        if(eventFeedbacks != null) OnHealth += eventFeedbacks.PlayFeedbacks;
        _gun.DamageCastCompo.OnShoot += HandleCheckShoot;
    }

    private void HandleCheckShoot(bool isTrigger)
    {
        if (isTrigger) _shootCount++;
            
        if(_shootCount >= wantShootCount)
        {
            _stat.Health++;
            OnHealth?.Invoke();
            _shootCount = 0;
        }
    }

    public override void EnterSkill()
    {
        base.EnterSkill();
        
        Shoot();
    }
}
