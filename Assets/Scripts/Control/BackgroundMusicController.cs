using System.Collections;
using UnityEngine;

public class BackgroundMusicController : MonoBehaviour
{
    public float fadeDuration = 2f; // 渐入时间
    public float waitDuration = 2f; // 等待时间

    private AudioSource audioSource;
    [SerializeField] private float targetVolume = 1f;
    [SerializeField] private float initialVolume = 0f;
    private float fadeSpeed;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        initialVolume = audioSource.volume;
        fadeSpeed = (targetVolume - initialVolume) / fadeDuration;
        PlayMusic();
    }

    private void Update()
    {
        if (!audioSource.isPlaying)
        {
            StartCoroutine(PlayDelayed(waitDuration));
        }
    }

    // 播放音乐
    private void PlayMusic()
    {
        audioSource.volume = initialVolume;
        audioSource.Play();
    }

    // 延迟播放音乐
    private IEnumerator PlayDelayed(float delay)
    {
        yield return new WaitForSeconds(delay);
        while (audioSource.volume < targetVolume)
        {
            audioSource.volume += fadeSpeed * Time.deltaTime;
            yield return null;
        }
    }
}
