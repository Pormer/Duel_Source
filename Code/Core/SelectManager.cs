using System;
using System.Linq;
using Unity.Netcode;
using UnityEngine;

public class SelectManager : MonoBehaviour
{
    [SerializeField] private DataManagerSO dataM;
    [SerializeField] SelectDataManagerSO selectDataM;
    [field: SerializeField] public Player[] PlayerGroup { get; private set; }
    [field: SerializeField] public CharacterDataSO[] selectCharData { get; private set; }
    [field: SerializeField] public GunDataSO[] selectGunData { get; private set; }

    private InGameUI _gameUI;

    private void Awake()
    {
        _gameUI = FindFirstObjectByType<InGameUI>();
        
        PlayerGroup = new Player[2];
        selectCharData = new CharacterDataSO[2];
        selectGunData = new GunDataSO[2];
        
        if (GameManager.Instance.IsOnlinePlay)
        {
            GameManager.Instance.OnGameStart += () => PlayerGroup = FindObjectsByType<Player>(FindObjectsSortMode.None);
            GameManager.Instance.OnGameStart += StartOnlineGameClientRpc;
        }
        else
        {
            GameManager.Instance.OnGameStart += () =>
            {
                PlayerGroup = FindObjectsByType<Player>(FindObjectsSortMode.None);
                if (PlayerGroup[0].InputReaderCompo.IsRight) PlayerGroup = PlayerGroup.Reverse().ToArray();

                PlayerInitialize();
            };
        }
    }

    public void StartOnlineGameClientRpc()
    {
        if (NetworkManager.Singleton.IsHost && PlayerGroup[1].GetComponent<NetworkObject>().IsOwner)
            PlayerGroup = PlayerGroup.Reverse().ToArray();
        if (NetworkManager.Singleton.IsClient)
        {
            PlayerGroup[1].transform.position = new Vector3(4, 0);
            PlayerGroup[1].transform.eulerAngles = new Vector3(0, 180, 0);
        }

        PlayerInitialize();
    }

    private void PlayerInitialize()
    {
        try
        {
            selectCharData[0] = dataM.characterDatas[(int)selectDataM.LeftCharType -1];
            selectGunData[0] = dataM.gunDatas[(int)selectDataM.LeftGunType-1];

            selectCharData[1] = dataM.characterDatas[(int)selectDataM.RightCharType-1];
            selectGunData[1] = dataM.gunDatas[(int)selectDataM.RightGunType-1];
        }
        catch (ArgumentOutOfRangeException e)
        {
            Debug.Log(e);
        }

        for (int i = 0; i < PlayerGroup.Length; i++)
        {
            PlayerGroup[i].Initialize(selectCharData[i], selectGunData[i]);
        }
        
        _gameUI?.SetDataUI(selectCharData.ToList(), selectGunData.ToList(), PlayerGroup);
    }
}