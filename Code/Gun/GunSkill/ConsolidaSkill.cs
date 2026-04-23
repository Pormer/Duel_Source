using System;
using UnityEngine;

public class ConsolidaSkill : GunSkill
{
    public Action OnFormChange;
    private int _hitCount = 0;
    protected override void AwakeSkill()
    {
        base.AwakeSkill();
        _gun.DamageCastCompo.OnShoot += HandleOnShoot;
        if(eventFeedbacks != null) OnFormChange += eventFeedbacks.PlayFeedbacks;
    }

    private void HandleOnShoot(bool isTrigger)
    {
        if (isTrigger)
        {
            if (_hitCount >= 3 && !_isFormChange)
            {
                _stat.CoolTime = 0.3f;
                _stat.wantLoadCount = 1;
                OnFormChange?.Invoke();
                _isFormChange = true;
            }
            _hitCount++;
        }
    }

    public override void EnterSkill()
    {
        base.EnterSkill();
        Shoot();
    }
}