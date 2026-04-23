using UnityEngine;

[CreateAssetMenu(fileName = "SoundSO", menuName = "SO/Data/SoundData")]
public class SoundSO : ScriptableObject
{
    public AudioClip clip;

    [Range(0.3f, 1f)]
    public float volume;

    [Range(1f, 3f)]
    public float pitch = 1f;
}