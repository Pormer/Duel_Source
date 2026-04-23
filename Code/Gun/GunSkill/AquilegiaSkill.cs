using System;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class AquilegiaSkill : GunSkill
{
    public Action OnOneCombo;
    
    [SerializeField] private int wantHitCount = 2;

    private bool _isFormChanged;
    private bool[] hitCombos;
    private int currentComboNum;

    private int CurrentComboNum
    {
        get => currentComboNum;
        set
        {
            if(value >= wantHitCount) return;
            
            currentComboNum = value;
        }
    }

    protected override void AwakeSkill()
    {
        base.AwakeSkill();
        hitCombos = new bool[wantHitCount];
        if(eventFeedbacks != null) OnOneCombo += eventFeedbacks.PlayFeedbacks;
        _gun.DamageCastCompo.OnShoot += HandleHitEvent;
    }

    private void HandleHitEvent(bool isTrigger)
    {
        hitCombos[CurrentComboNum] = isTrigger;
        
        if(!isTrigger)
        {
            hitCombos.ToList().ForEach(i => i = false);
            CurrentComboNum = 0;
        }
        else
        {
            CurrentComboNum++;
            OnOneCombo?.Invoke();
        }
    }

    public override void EnterSkill()
    {
        base.EnterSkill();

        if (hitCombos.All(item => item) && !_isFormChanged) _isFormChanged = true;
        
        if (_isFormChanged)
        {
            _stat.Damage = 4;
            if (_stat.BarrierCount <= 0) _stat.Damage = 1;
            _stat.BarrierCount--;
            _isFormChanged = false;
        }
        else
        {
            _stat.Damage = 0;
        }
        
        Shoot();
    }
}
