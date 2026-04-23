using System;
using System.Collections;
using DataType;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class GameManager : MonoSingleton<GameManager>
{
    [field: SerializeField] public bool IsOnlinePlay { get; private set; }

    public UnityEvent<bool> OnGameWin;
    public Action<bool> OnFinalWin;

    public Action<int> OnFadeIn;

    public Action OnGameStart;
    public Action<bool> OnSettingUi;

    [SerializeField] private int leftScore;

    public int LeftScore
    {
        get => scoreData.leftScore;
        set
        {
            if (value > 5) return;
            leftScore = value;
            if (scoreData != null) scoreData.leftScore = value;
        }
    }

    [SerializeField] private int rightScore;

    public int RightScore
    {
        get => scoreData.rightScore;
        set
        {
            if (value > 5) return;
            rightScore = value;
            if (scoreData != null) scoreData.rightScore = value;
        }
    }

    [SerializeField] private ScoreDataSO scoreData;
    [SerializeField] SelectDataManagerSO selectDataM;

    private void Awake()
    {
        OnGameWin.AddListener(HandleScoreChange);
    }

    private void Start()
    {
        var item = FindAnyObjectByType<ScoreUI>();
        if(item == null)
            StartCoroutine(StartInitGame());
    }

    IEnumerator StartInitGame()
    {
        yield return new WaitForSeconds(0.3f);
        OnGameStart?.Invoke();
    }

    private void HandleScoreChange(bool isRight)
    {
        if (isRight) RightScore++;
        else LeftScore++;

        selectDataM.LeftCharType = CharacterType.Default;
        selectDataM.RightCharType = CharacterType.Default;
        selectDataM.LeftGunType = GunType.Default;
        selectDataM.RightGunType = GunType.Default;
        StartCoroutine(WaitFade());
    }

    IEnumerator WaitFade()
    {
        yield return new WaitForSecondsRealtime(1f);
        OnFadeIn?.Invoke(2);
    }

    public void ResetGame()
    {
        RightScore = 0;
        LeftScore = 0;
        
        selectDataM.LeftCharType = CharacterType.Default;
        selectDataM.RightCharType = CharacterType.Default;
        selectDataM.LeftGunType = GunType.Default;
        selectDataM.RightGunType = GunType.Default;
    }
}