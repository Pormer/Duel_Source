using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class PhysalisSkill : GunSkill
{
    public Action OnFormChange;
    [SerializeField] private int wantShootCount = 3;
    [SerializeField] private int wantMaxBulletCount = 6;
    private int _shootCount = 0;
    private bool _isOneCool;

    protected override void AwakeSkill()
    {
        base.AwakeSkill();
        
        if(eventFeedbacks != null) OnFormChange += eventFeedbacks.PlayFeedbacks;

        _gun.DamageCastCompo.OnShoot += HandleCheckShoot;
    }

    private void HandleCheckShoot(bool isTrigger)
    {
        if (isTrigger) _shootCount++;
            
        if (!_isOneCool && _shootCount >= wantShootCount)
        {
            _stat.maxBulletCount = wantMaxBulletCount;
            _stat.wantLoadCount = 1;
            
            OnFormChange?.Invoke();
            _isOneCool = true;
        }
    }

    public override void EnterSkill()
    {
        base.EnterSkill();

        Shoot();
    }
}