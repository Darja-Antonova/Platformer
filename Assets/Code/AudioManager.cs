using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [Header("Audio Sources")]
    public AudioSource musicSource;
    public AudioSource sfxSource;

    [Header("Audio Clips")]
    public AudioClip menuMusic;
    public AudioClip backgroundMusic;
    public AudioClip jumpSFX; //Delayed
    public AudioClip collectibleSFX;
    public AudioClip dashSFX; //Delayed
    public AudioClip runSFX; //Need to add in
    public AudioClip healingSFX; //Try stop the overlapping O.O

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Main Menu" || scene.name == "Ending")
        {
            PlayMusic(menuMusic);
        }
        else
        {
            PlayMusic(backgroundMusic);
        }
    }

    private void PlayMusic(AudioClip clip)
    {
        if (clip == null || musicSource == null) return;

        if (musicSource.clip == clip) return;

        musicSource.clip = clip;
        musicSource.loop = true;
        musicSource.Play();
    }

    public void PlaySFX(AudioClip clip)
    {
        if (clip != null && sfxSource != null)
        {
            sfxSource.PlayOneShot(clip);
        }
    }
    public void StartLoopingSFX(AudioClip clip)
    {
        sfxSource.clip = clip;
        sfxSource.loop = true;
        sfxSource.Play();
    }

    public void StopLoopingSFX()
    {
        sfxSource.loop = false;
        sfxSource.Stop();
    }
}
