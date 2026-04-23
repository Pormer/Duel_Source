using System;
using UnityEngine;
using UnityEngine.Events;

public class BloodrootSkill : GunSkill
{
    public Action OnMinusHealth;
    private Health _health;

    protected override void AwakeSkill()
    {
        base.AwakeSkill();

        _health = _player.GetComponent<Health>();
        _stat.wantLoadCount = 0;
        
        _stat.IsNotBullet = true;
        
        if(eventFeedbacks != null) OnMinusHealth += eventFeedbacks.PlayFeedbacks;
    }

    public override void EnterSkill()
    {
        base.EnterSkill();

        if (_stat.Health <= 1)
        {
            _health.OnDeadEvent?.Invoke();
            GameManager.Instance.OnGameWin?.Invoke(!_player.InputReaderCompo.IsRight);
        }
        OnMinusHealth?.Invoke();
        _stat.Health--;

        Shoot();
    }
}
