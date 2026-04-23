using System;
using UnityEngine;
using UnityEngine.Events;

public class Gun : MonoBehaviour
{
    public UnityEvent OnShoot;
    
    #region Compo
    
    public GunSkill SkillCompo { get; private set; }
    public DamageCaster DamageCastCompo {get; private set;}
    public SpriteRenderer SpriterCompo {get; private set;}
    public Animator AnimCompo {get; private set;}

    #endregion
    
    private GunDataSO _gunData;
    protected Player agent;


    public void Initialize(Player player)
    {
        agent = player;
        _gunData = player.GunData;

        //Get Compo
        DamageCastCompo = transform.Find("DamageCaster").GetComponent<DamageCaster>();
        DamageCastCompo.Initialize(_gunData.range);
        
        AnimCompo = transform.Find("Visual").GetComponent<Animator>();
        
        //리플렉션
        string skillStr = $"{_gunData.gunType.ToString()}Skill";

        var type = Type.GetType(skillStr);

        SkillCompo = gameObject.AddComponent(type) as GunSkill;
        SkillCompo?.Initialize(this, agent);
    }
}
