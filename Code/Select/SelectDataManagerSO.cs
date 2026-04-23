using System;
using System.Collections.Generic;
using System.Linq;
using DataType;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

[CreateAssetMenu(menuName = "SO/Manager/Select")]
public class SelectDataManagerSO : ScriptableObject
{
    public event Action OnSelect;

    [field: SerializeField] public CharacterType LeftCharType { get; set; }
    [field: SerializeField] public GunType LeftGunType { get; set; }
    [field: SerializeField] public CharacterType RightCharType { get; set; }
    [field: SerializeField] public GunType RightGunType { get; set; }

    private List<SelectItem> curItemList;
    private Transform _parent;

    [SerializeField] private SelectItem selectItemObj;
    [SerializeField] private int spawnCount = 5;
    [SerializeField] private Vector2 startSpawnPos;

    private void OnEnable()
    {
        curItemList = new List<SelectItem>();

        LeftCharType = CharacterType.Default;
        LeftGunType = GunType.Default;

        RightCharType = CharacterType.Default;
        RightGunType = GunType.Default;

        SceneManager.sceneUnloaded += scene => curItemList.Clear();

#if UNITY_EDITOR
        if (!isDebug) return;
        LeftCharType = testCharType;
        LeftGunType = testGunType;

        RightCharType = testCharType;
        RightGunType = testGunType;
#endif
    }

    public void SelectCharacter(bool isRight, CharacterType type)
    {
        if (isRight)
            RightCharType = type;
        else
        {
            LeftCharType = type;
        }

        if (IsNotCharDefault())
        {
            SpawnSelectGunItem(_parent);
            OnSelect?.Invoke();
        }
    }

    public void SelectGun(bool isRight, GunType type)
    {
        if (isRight)
            RightGunType = type;
        else
        {
            LeftGunType = type;
        }

        if (IsNotGunDefault() && IsNotCharDefault())
        {
            NextStep();
        }
    }

    public void SpawnSelectCharItem(Transform parent)
    {
        _parent = parent;

        for (int i = 0; i < spawnCount; i++)
        {
            SelectItem item;
            item = Instantiate(selectItemObj, startSpawnPos + Vector2.up * i, Quaternion.identity, parent);

            item.Initialize(CheckCharDataValue((CharacterType)Random.Range(1, 13)));
            curItemList.Add(item);
        }
    }

    public void SpawnSelectGunItem(Transform parent)
    {
        _parent = parent;
        
        foreach (var item in curItemList)
        {
            item.gameObject.SetActive(false);
        }
        

        for (int i = 0; i < spawnCount; i++)
        {
            SelectItem item;
            item = Instantiate(selectItemObj, startSpawnPos + Vector2.up * i, Quaternion.identity, parent);
            item.Initialize(CheckGunDataValue((GunType)Random.Range(1, 16)));
            curItemList.Add(item);
        }
    }
    
    //중복확인
    private CharacterType CheckCharDataValue(CharacterType typeNum)
    {
        foreach (var item in curItemList)
        {
            if (item.CharType == typeNum)
            {
                return CheckCharDataValue((CharacterType)Random.Range(1, 13));
            }
        }

        return typeNum;
    }

    private GunType CheckGunDataValue(GunType typeNum)
    {
        foreach (var item in curItemList)
        {
            if (item.GunType == typeNum)
            {
                return CheckGunDataValue((GunType)Random.Range(1, 16));
            }
        }

        return typeNum;
    }

    public void NextStep()
    {
        GameManager.Instance.OnFadeIn?.Invoke((int)SceneType.InGame);
    }

    private bool IsNotCharDefault()
    {
        bool isLeftChar = LeftCharType != CharacterType.Default;
        bool isRightChar = RightCharType != CharacterType.Default;

        return isLeftChar && isRightChar;
    }

    private bool IsNotGunDefault()
    {
        bool isLeftGun = LeftGunType != GunType.Default;
        bool isRightGun = RightGunType != GunType.Default;

        return isRightGun && isLeftGun;
    }

#if UNITY_EDITOR
    [SerializeField] private CharacterType testCharType;
    [SerializeField] private GunType testGunType;
    [SerializeField] private bool isDebug;
#endif
}