using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class PrunusSkill : GunSkill
{
    public Action OnOneCombo;
    [SerializeField] private int wantHitCount = 3;

    private bool[] hitCombos;
    private int currentComboNum;
    private int CurrentComboNum
    {
        get => currentComboNum;
        set
        {
            if (value >= wantHitCount)
            {
                currentComboNum = 0;
                return;
            }
            
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

        if (!isTrigger)
        {
            for (int i = 0; i < hitCombos.Length; i++) hitCombos[i] = false;
            
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

        if (hitCombos.All(item => item))
        {
            _stat.Damage = 1000000000;
        }

        Shoot();
        _stat.Damage = 0;
    }
}