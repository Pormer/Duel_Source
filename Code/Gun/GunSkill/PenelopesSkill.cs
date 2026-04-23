using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class PenelopesSkill : GunSkill
{
    public Action OnBulletDamageChanged;
    [SerializeField] private int wantBulletCount = 5;
    private int _curBulletShootCount;

    protected override void AwakeSkill()
    {
        base.AwakeSkill();
        
        if(eventFeedbacks != null) OnBulletDamageChanged += eventFeedbacks.PlayFeedbacks;
        _curBulletShootCount = 0;
    }

    public override void EnterSkill()
    {
        base.EnterSkill();
        
        _curBulletShootCount++;
        
        if (_curBulletShootCount >= wantBulletCount)
        {
            OnBulletDamageChanged?.Invoke();
            _stat.Damage *= 2;
            _curBulletShootCount = 0;
        }
        Shoot();
        _stat.Damage = 1;
    }
}
