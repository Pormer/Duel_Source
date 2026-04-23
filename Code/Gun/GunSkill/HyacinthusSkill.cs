using System;
using ObjectPooling;
using UnityEngine;
using UnityEngine.Events;

public class HyacinthusSkill : GunSkill
{
    public Action OnHealth;
    [SerializeField] private int wantShootCount = 5;
    private int _shootCount = 0;
    private bool _isOneCool;
    
    protected override void AwakeSkill()
    {
        base.AwakeSkill();
        
        if(eventFeedbacks != null) OnHealth += eventFeedbacks.PlayFeedbacks;
        _gun.DamageCastCompo.OnShoot += HandleCheckShoot;
    }

    private void HandleCheckShoot(bool isTrigger)
    {
        _shootCount++;
        if (_shootCount >= wantShootCount && !_isOneCool)
        {
            HyacinthusSkillChild item = PoolManager.Instance.Pop(PoolingType.HyacinthusChildItem) as HyacinthusSkillChild;
            
            if (_player.InputReaderCompo.IsRight)
            {
                item.transform.position = new Vector3(4, 0);
            }
            else
            {
                item.transform.position = new Vector3(-3, 0);
            }
            
            item.Initialize(this);
            _isOneCool = true;
            _shootCount = 0;
        }
    }

    public override void EnterSkill()
    {
        base.EnterSkill();
        
        Shoot();
    }

    public void HandleActiveStat()
    {
        _stat.Health++;
        _stat.CurBulletCount++;
        OnHealth?.Invoke();
        _isOneCool = true;
    }
}
