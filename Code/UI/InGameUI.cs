using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UIElements;

public class InGameUI : MonoBehaviour
{
    private UIDocument _uiDocument;
    [SerializeField] private VisualTreeAsset healthItemTreeAsset = null;
    [SerializeField] private VisualTreeAsset etcItemTreeAsset = null;
    private VisualElement _root;
    private VisualElement rightGroupElem;
    private VisualElement leftGroupElem;

    private VisualElement[] healthGroup;
    private VisualElement[] barrierGroup;
    private VisualElement[] bulletGroup;


    [SerializeField] private Color[] colors;

    #region StatItemGroup

    private StatItem[][] _itemHearts;
    private StatItem[][] _itemBarriers;
    private StatItem[][] _itemBullets;

    [SerializeField] private Sprite healthSprite;
    [SerializeField] private Sprite barrierSprite;
    [SerializeField] private Sprite bulletSprite;

    #endregion

    private void Awake()
    {
        _uiDocument = GetComponent<UIDocument>();
        _root = _uiDocument.rootVisualElement;
        
        _itemHearts = new StatItem[2][];
        _itemBarriers = new StatItem[2][];
        _itemBullets = new StatItem[2][];

        healthGroup = new VisualElement[2];
        barrierGroup = new VisualElement[2];
        bulletGroup = new VisualElement[2];

        rightGroupElem = _root.Q<VisualElement>("RightStatElem");
        leftGroupElem = _root.Q<VisualElement>("LeftStatElem");

        healthGroup[0] = leftGroupElem.Q<VisualElement>("HeartGroup");
        healthGroup[1] = rightGroupElem.Q<VisualElement>("HeartGroup");
        barrierGroup[0] = leftGroupElem.Q<VisualElement>("BarrierGroup");
        barrierGroup[1] = rightGroupElem.Q<VisualElement>("BarrierGroup");
        bulletGroup[0] = leftGroupElem.Q<VisualElement>("BulletGroup");
        bulletGroup[1] = rightGroupElem.Q<VisualElement>("BulletGroup");
        
        healthGroup[0].Clear();
        healthGroup[1].Clear();
        barrierGroup[0].Clear();
        barrierGroup[1].Clear();
        bulletGroup[0].Clear();
        bulletGroup[1].Clear();
    }

    public void SetDataUI(List<CharacterDataSO> cDataList, List<GunDataSO> gDataList, Player[] players)
    {
        players[0].StatDataCompo.OnHealthChanged += i => HealthChange(i, _itemHearts[0]);
        players[1].StatDataCompo.OnHealthChanged += i => HealthChange(i, _itemHearts[1]);
        players[0].StatDataCompo.OnBarrierChanged += i => HealthChange(i, _itemBarriers[0]);
        players[1].StatDataCompo.OnBarrierChanged += i => HealthChange(i, _itemBarriers[1]);
        players[0].StatDataCompo.OnBulletChanged += i => HealthChange(i, _itemBullets[0]);
        players[1].StatDataCompo.OnBulletChanged += i => HealthChange(i, _itemBullets[1]);
        
        for (int i = 0; i < 2; i++)
        {
            _itemHearts[i] = new StatItem[cDataList[i].hp];
            _itemBarriers[i] = new StatItem[cDataList[i].barrierCount];
            _itemBullets[i] = new StatItem[gDataList[i].bulletCount];
        }

        for (int i = 0; i < 2; i++)
        {
            for (int x = 0; x < cDataList[i].hp; x++)
            {
                var temp = healthItemTreeAsset.CloneTree();
                var item = temp.Q<VisualElement>("BackgroundIcon");
                
                _itemHearts[i][x] = new StatItem(item, healthSprite, colors[i]);
                healthGroup[i].Add(temp);
            }

            for (int x = 0; x < cDataList[i].barrierCount; x++)
            {
                var temp = etcItemTreeAsset.CloneTree();
                var item = temp.Q<VisualElement>("BackgroundIcon");

                _itemBarriers[i][x] = new StatItem(item, barrierSprite, colors[i]);
                barrierGroup[i].Add(temp);
            }

            for (int x = 0; x < gDataList[i].bulletCount; x++)
            {
                var temp = etcItemTreeAsset.CloneTree();
                var item = temp.Q<VisualElement>("BackgroundIcon");
                var item2 = temp.Q<VisualElement>("ActiveItem");

                item.style.width = 90;
                item.style.height = 90;
                item2.style.width = 100;
                item2.style.height = 100;

                _itemBullets[i][x] = new StatItem(item, bulletSprite, colors[i]);
                bulletGroup[i].Add(temp);
            }
        }
    }

    private void HealthChange(int curValue, StatItem[] statItems)
    {
        for (int i = 0; i < statItems.Length ; i++)
        {
            statItems[i].IsActive = curValue <= i;
        }
    }
}