using System;
using UnityEngine;
using UnityEngine.Events;

public class StatData : IPlayerComponents
{
    public event Action<int> OnHealthChanged;
    public event Action<int> OnBarrierChanged;
    public event Action<int> OnBulletChanged;

    //체력
    public int maxHealth;
    private int _health;

    public int Health
    {
        get => _health;
        set
        {
            if (maxHealth < value) return;

            _health = value < 0 ? 0 : value;
            OnHealthChanged?.Invoke(_health);
        }
    }

    //배리어 수
    public int maxBarrierCount;
    private int _barrierCount;

    public int BarrierCount
    {
        get => _barrierCount;
        set
        {
            if (value < 0 || maxBarrierCount < value) return;

            OnBarrierChanged?.Invoke(value);
            _barrierCount = value;
        }
    }

    //데미지
    private int _damage;

    public int Damage
    {
        get => _damage;
        set
        {
            if (value < 0) return;
            _damage = value;
        }
    }

    //총알 수
    public int maxBulletCount;
    private int _curBulletCount;

    public int CurBulletCount
    {
        get => _curBulletCount;
        set
        {
            if (maxBulletCount < value) return;
            OnBulletChanged?.Invoke(value);
            _curBulletCount = value;
        }
    }

    //쿨타임
    public float CoolTime { get; set; }

    //장전에 필요한 움직임 수 
    public int wantLoadCount;
    private int _curLoadCount;

    public int CurLoadCount
    {
        get => _curLoadCount;
        set
        {
            if (wantLoadCount <= value)
            {
                _curLoadCount = 0;
                CurBulletCount++;
            }
            else
            {
                _curLoadCount = value;
            }
        }
    }

    //발사 가능 여부
    public bool IsNotBullet { get; set; }//총알이 없어도 발사 가능여부
    public bool IsCanShoot => _curBulletCount > 0 || IsNotBullet;

    public void Initialize(Player player)
    {
    }

    //초기화
    public StatData(CharacterDataSO cData, GunDataSO gData)
    {
        maxHealth = cData.hp;
        Health = cData.hp;

        maxBarrierCount = cData.barrierCount;
        BarrierCount = cData.barrierCount;

        _damage = gData.damage;

        maxBulletCount = gData.bulletCount;
        _curBulletCount = gData.bulletCount;

        CoolTime = gData.coolTime;

        wantLoadCount = gData.wantLoadCount;
        _curLoadCount = 0;
    }
}