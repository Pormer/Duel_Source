using System;
using System.Collections;
using DataType;
using UnityEngine;
using UnityEngine.Events;

public class ItemCaster : MonoBehaviour
{
    [SerializeField] DataManagerSO dataM;
    [SerializeField] SelectDataManagerSO selectDataM;
    [SerializeField] ContactFilter2D targetFilter;
    [SerializeField] private Vector2 castSize;
    private Collider2D[] cols;
    private Player _player;

    public UnityEvent<CharacterDataSO> OnTargetCharExp;
    public UnityEvent<GunDataSO> OnTargetGunExp;

    private SelectSetting _selectSet;
    private bool _isCharSelectTime = true;

    private void Awake()
    {
        cols = new Collider2D[1];
        _selectSet = FindFirstObjectByType<SelectSetting>();
        selectDataM.OnSelect += HandleSelectChar;
        GameManager.Instance.OnGameStart += CastItemData;
    }

    public void CastItem()
    {
        var col = Physics2D.OverlapBox(transform.position, castSize, 0, targetFilter, cols);

        if (col > 0)
        {
            if (cols[0].TryGetComponent(out SelectItem item))
            {
                bool isLiftChar = selectDataM.LeftCharType == CharacterType.Default &&
                                  _player.InputReaderCompo.IsRight == false;
                bool isRightChar = selectDataM.RightCharType == CharacterType.Default &&
                                   _player.InputReaderCompo.IsRight;

                if (isRightChar)
                {
                    item.Select(true);
                    return;
                }

                if (isLiftChar)
                {
                    item.Select(false);
                    return;
                }

                if (_isCharSelectTime) return;
                bool isLiftGun = selectDataM.LeftGunType == GunType.Default &&
                                 _player.InputReaderCompo.IsRight == false;
                bool isRightGun = selectDataM.RightGunType == GunType.Default && _player.InputReaderCompo.IsRight;

                if (isRightGun)
                {
                    item.Select(true);
                    return;
                }

                if (isLiftGun)
                {
                    item.Select(false);
                    return;
                }

                //item.Select(_player.InputReaderCompo.IsRight);
            }
        }
    }

    public void CastItemData()
    {
        var col = Physics2D.OverlapBox(transform.position, castSize, 0, targetFilter, cols);

        if (col > 0)
        {
            if (cols[0].TryGetComponent(out SelectItem item))
            {
                if (item.IsChar)
                {
                    bool isLiftChar = selectDataM.LeftCharType == CharacterType.Default &&
                                      _player.InputReaderCompo.IsRight == false;
                    bool isRightChar = selectDataM.RightCharType == CharacterType.Default &&
                                       _player.InputReaderCompo.IsRight;

                    if (isLiftChar || isRightChar)
                    {
                        _selectSet.CharUiSet.UiSet(dataM.characterDatas[(int)item.CharType - 1],
                            _player.InputReaderCompo.IsRight);
                    }

                    //OnTargetCharExp?.Invoke(dataM.characterDatas[(int)item.CharType]);
                }
                else
                {
                    bool isLeftGun = selectDataM.LeftGunType == GunType.Default &&
                                     _player.InputReaderCompo.IsRight == false;
                    bool isRightGun = selectDataM.RightGunType == GunType.Default && _player.InputReaderCompo.IsRight;

                    if (isRightGun || isLeftGun)
                    {
                        _selectSet.GunUiSet.UiSet(dataM.gunDatas[(int)item.GunType - 1],
                            _player.InputReaderCompo.IsRight);
                    }
                    //OnTargetGunExp?.Invoke(dataM.gunDatas[(int)item.GunType]);
                }
            }
        }
    }

    public void Initialize(Player player)
    {
        _player = player;

        _player.InputReaderCompo.OnShootEvent += CastItem;
        _player.MovementCompo.OnEndMove += CastItemData;

        GameManager.Instance.OnFinalWin += v => _player.InputReaderCompo.OnShootEvent -= CastItem;
        GameManager.Instance.OnSettingUi += SetOnSettingUI;

        StartCoroutine(CoolTime());
    }

    private IEnumerator CoolTime()
    {
        yield return new WaitForSeconds(0.5f);
        CastItemData();
    }

    private void HandleSelectChar()
    {
        _isCharSelectTime = false;
        CastItemData();
    }

    private void SetOnSettingUI(bool b)
    {
        if (b)
        {
            _player.InputReaderCompo.OnShootEvent -= CastItem;
        }
        else
        {
            _player.InputReaderCompo.OnShootEvent += CastItem;
        }
    }

    private void OnDisable()
    {
        if (_player != null)
        {
            _player.InputReaderCompo.OnShootEvent -= CastItem;
            _player.MovementCompo.OnEndMove -= CastItemData;
        }

        selectDataM.OnSelect -= HandleSelectChar;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(transform.position, castSize);
        Gizmos.color = Color.white;
    }
}