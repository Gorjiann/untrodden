using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
     private AudioSource audioSource;
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        EventManager.OnDeath.AddListener(Playe);
    }
    public void Playe(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }
}
