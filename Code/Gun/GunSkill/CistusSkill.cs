using ObjectPooling;
using UnityEngine;
using UnityEngine.Serialization;

public class CistusSkill : GunSkill
{
    public bool IsFire { get; set; }
    private int _hitStack = 0;
    private CistusSkillChild _childItem;
    
    
    protected override void AwakeSkill()
    {
        base.AwakeSkill();
        _player.GetCompo<Health>().OnHitEvent.AddListener(() =>
        {
            _hitStack++;
            if (_hitStack >= 2)
            {
                if(_childItem != null) PoolManager.Instance.Push(_childItem);
                _childItem = PoolManager.Instance.Pop(PoolingType.CistusChildItem) as CistusSkillChild;
                
                if (_player.InputReaderCompo.IsRight)
                {
                    _childItem.transform.position = new Vector3(4, 0);
                }
                else
                {
                    _childItem.transform.position = new Vector3(-3, 0);
                }
                
                _childItem.Initialize(this);
                _hitStack = 0;
            }
        });
    }

    public override void EnterSkill()
    {
        base.EnterSkill();
        
        if (IsFire)
        {
            _stat.Damage += _stat.Damage;
        }
        Shoot();
        _stat.Damage = 1;
    }
}
