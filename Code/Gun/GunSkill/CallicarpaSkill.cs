using System;
using UnityEngine;
using UnityEngine.Events;

public class CallicarpaSkill : GunSkill
{
    public Action OnFormChange;
    [SerializeField] private int wantMaxBulletCount = 4;
    private int curShootBulletCount;
    
    protected override void AwakeSkill()
    {
        base.AwakeSkill();
        curShootBulletCount = 0;
        
        if(eventFeedbacks == null) return;
        if(eventFeedbacks != null) OnFormChange += eventFeedbacks.PlayFeedbacks;
    }
    
    

    public override void EnterSkill()
    {
        base.EnterSkill();
        curShootBulletCount++;
        Shoot();
        
        if (!_isFormChange && curShootBulletCount >= wantMaxBulletCount)
        {
            print("전"+_stat.CurBulletCount);
            _stat.CurBulletCount = _stat.maxBulletCount;
            print("후"+_stat.CurBulletCount);
            _stat.Damage = 2;
            _isFormChange = true;
            OnFormChange?.Invoke();
        }
    }
}
