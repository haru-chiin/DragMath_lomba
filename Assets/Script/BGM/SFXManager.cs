using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SFXManager : MonoBehaviour
{
    public static SFXManager Instance;

    public AudioSource sfxSource;
    public AudioClip clickSound;
    public AudioClip mergeSound;

    [Header("Battle Sounds")]
    public AudioClip attackSound;
    public AudioClip hitSound;

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

        if (sfxSource == null) sfxSource = GetComponent<AudioSource>();
    }

    public void PlayClick()
    {
        if (clickSound != null)
        {
            sfxSource.PlayOneShot(clickSound);
        }
    }
    public void PlayMerge()
    {
        if (mergeSound != null) sfxSource.PlayOneShot(mergeSound);
    }

    public void PlayAttack() { if (attackSound != null) sfxSource.PlayOneShot(attackSound); }
    public void PlayHit() { if (hitSound != null) sfxSource.PlayOneShot(hitSound); }
}