using UnityEngine;

public class PlaySound : MonoBehaviour
{
    [Header("References")]
    [SerializeField]
    private AudioSource source = null;
    [SerializeField]
    private AudioClip[] randomizedSounds1;
    [SerializeField]
    private AudioClip[] randomizedSounds2;
    [SerializeField]
    private AudioClip[] multipleSounds;
    [SerializeField]
    private AudioClip[] soundsList;

    public void PlaySpecificSound(int soundIndex)
    {
        source.clip = soundsList[soundIndex];
        source.Play();
    }
    public void PlayRandomizedSound1()
    {
        source.clip = randomizedSounds1[Random.Range(0, randomizedSounds1.Length)];
        source.Play();
    }
    public void PlayRandomizedSound2()
    {
        source.clip = randomizedSounds2[Random.Range(0, randomizedSounds2.Length)];
        source.Play();
    }
    public void PlayMultipleSounds()
    {
        for (int i = 0; i < multipleSounds.Length; i++)
        {
            source.PlayOneShot(multipleSounds[i]);
        }
    }
}