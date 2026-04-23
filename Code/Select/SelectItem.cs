using System;
using DataType;
using UnityEngine;
using UnityEngine.Serialization;

public class SelectItem : MonoBehaviour
{
    [SerializeField] DataManagerSO dataM;
    [SerializeField] private SelectDataManagerSO selectDataM;
    public CharacterType CharType { get; private set; }
    public GunType GunType { get; private set; }

    private SpriteRenderer _spriter;
    public bool IsChar { get; private set; }

    public void Initialize(CharacterType cType)
    {
        _spriter = transform.Find("Visual").GetComponent<SpriteRenderer>();

        CharType = cType;
        IsChar = true;
        try
        {
            _spriter.sprite = dataM.characterDatas[(int)CharType - 1].itemSprite;
        }
        catch (Exception e)
        {
            print((int)CharType-1);
        }
    }

    public void Initialize(GunType gType)
    {
        _spriter = transform.Find("Visual").GetComponent<SpriteRenderer>();

        GunType = gType;
        IsChar = false;

        _spriter.sprite = dataM.gunDatas[(int)GunType - 1].itemSprite;
    }

    public void Select(bool isRight)
    {
        if (isRight)
        {
            if (IsChar)
                selectDataM.SelectCharacter(true, CharType);
            else
            {
                selectDataM.SelectGun(true, GunType);
            }
        }
        else
        {
            if (IsChar)
                selectDataM.SelectCharacter(false, CharType);
            else
            {
                selectDataM.SelectGun(false, GunType);
            }
        }
    }
}