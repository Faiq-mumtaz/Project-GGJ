using UnityEngine;

public class MusicLoop : MonoBehaviour
{
    public AudioSource audioSource;

    void Awake()
    {
        if (audioSource == null)
            audioSource = GetComponent<AudioSource>();

        audioSource.loop = true;
        audioSource.Play();
    }
}
