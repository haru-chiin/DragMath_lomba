using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class BGMManager : MonoBehaviour
{
    public static BGMManager Instance;

    [Header("Komponen")]
    public AudioSource bgmSource;

    [Header("Daftar Lagu")]
    public AudioClip chillBGM;
    public AudioClip battleBGM;
    public AudioClip winBGM;
    public AudioClip loseBGM;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        if (bgmSource == null) bgmSource = GetComponent<AudioSource>();
    }

    private void PlayBGM(AudioClip clip, bool loop = true)
    {
        if (clip == null) return;

        if (bgmSource.clip == clip && bgmSource.isPlaying) return;

        bgmSource.clip = clip;
        bgmSource.loop = loop;
        bgmSource.Play();
    }

    public void PlayChill() => PlayBGM(chillBGM, true);
    public void PlayBattle() => PlayBGM(battleBGM, true);
    public void PlayWin() => PlayBGM(winBGM, false);
    public void PlayLose() => PlayBGM(loseBGM, false);

    public void StopBGM() => bgmSource.Stop();
}