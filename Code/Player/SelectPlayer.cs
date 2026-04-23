using System;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;

public class SelectPlayer : MonoBehaviour
{
    #region Compo

    [field: SerializeField] public InputReaderSO InputReaderCompo { get; private set; }
    public StatData StatDataCompo {get; private set;}
    public FeedbackPlayer EventFeedbacksCompo { get; private set; }
    public PlayerMovement MovementCompo {get; private set;}

    public SpriteRenderer PlayerSpriteRenderer { get; private set; }
    public SpriteRenderer MaskSpriteRenderer { get; private set; }
    public GameObject Barrier {  get; private set; }
    public Color BarrierColer {  get; private set; }

    #endregion

    public GunDataSO GunData { get; private set; }

    private Dictionary<Type, IPlayerComponents> _components;

    public bool IsOnBarrier { get; private set; }
    public Action OnHitBarrier;
    
    public T GetCompo<T>() where T : class
    {
        Type type = typeof(T);
        if (_components.TryGetValue(type, out IPlayerComponents compo))
        {
            return compo as T;
        }

        return default;
    }

    private void Awake()
    {
        PlayerSpriteRenderer = transform.Find("Visual")?.GetComponent<SpriteRenderer>();
        MaskSpriteRenderer = transform.Find("Visual").transform.Find("Mask").GetComponent<SpriteRenderer>();
        Barrier = transform.Find("Visual").transform.Find("Barrier")?.gameObject;


        _components = new Dictionary<Type, IPlayerComponents>();

        GetComponentsInChildren<IPlayerComponents>().ToList()
            .ForEach(x => _components.Add(x.GetType(), x));
    }

    /*public void Initialize(CharacterDataSO cdata, GunDataSO gData)
    {
        MovementCompo = GetComponent<PlayerMovement>();
        MovementCompo.Initialize(this);

        var itemCaster = GetComponentInChildren<ItemCaster>();
        if(itemCaster != null) itemCaster.Initialize(this);
        
        //????
        InputReaderCompo.OnMovementEvent += MovementCompo.SetMovement;
        
        if(cdata == null || gData ==null) return;
        
        //????
        GunData = gData;
        StatDataCompo = new StatData(cdata, gData);
        _components.Add(StatDataCompo.GetType(), StatDataCompo);
        
        _components.Values.ToList().ForEach(compo => compo.Initialize(this));
    }*/

    private void OnDestroy()
    {
        InputReaderCompo.OnMovementEvent -= MovementCompo.SetMovement;
    }
}
