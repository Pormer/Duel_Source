using UnityEngine;

public class SoundFeedback : Feedback
{
    [SerializeField] SoundSO soundData;
    public override void PlayFeedback()
    {
        SoundManager.Instance.PlaySFX(soundData);
    }
}
