using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

public class TimeScaleFeedback : Feedback
{
    [SerializeField] float timeScaleValue = 0.5f;
    [SerializeField] private float changeTime = 0.5f;
    public override void PlayFeedback()
    {
        Time.timeScale = timeScaleValue;
        StartCoroutine(TimeChange());
    }

    private IEnumerator TimeChange()
    {
        yield return new WaitForSeconds(changeTime);
        Time.timeScale = 1;
    }
}
