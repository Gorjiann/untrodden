using UnityEngine;
using System.Collections;
using TMPro;

public class MessageController : MonoBehaviour
{
    [SerializeField] private float FadingTime;
    [SerializeField] private AudioClip MessageAudio;
    [SerializeField] private AnimationCurve FadingCurve;
    private TMP_Text selftext;
    private AudioSource selfsource;
    private float elapsedTime = 0f;

    private void Awake()
    {
        selftext = GetComponent<TMP_Text>();
        selfsource = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        elapsedTime = 0f;
        StartCoroutine(FadeOut());
    }

    private IEnumerator FadeOut()
    {
        selfsource.PlayOneShot(MessageAudio);
           Color startColor = selftext.color;
        while (elapsedTime < FadingTime)
        {
            elapsedTime += Time.deltaTime;
            float normalizedTime = elapsedTime / FadingTime;
            float alpha = FadingCurve.Evaluate(normalizedTime);
            selftext.color = new Color(startColor.r, startColor.g, startColor.b, alpha);

            yield return null;
        }
        Destroy(gameObject);
    }
}
