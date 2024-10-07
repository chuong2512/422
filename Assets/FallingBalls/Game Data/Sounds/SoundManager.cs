using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] AudioSource sfxSource, musicSource;
    [SerializeField] AudioClip winSound;
    [SerializeField] AudioClip ballSound;
    [SerializeField] AudioClip hitSound;

    public static SoundManager Instance;
    public bool IsOn
    {
        get
        {
            return isOn;
        }
        set
        {
            isOn = value;
            musicSource.enabled = isOn;
            sfxSource.enabled = isOn;

            PlayerPrefs.GetInt("Sound", IsOn ? 1 : 0);
        }
    }

    bool isOn = true;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else if (Instance != this)
            Destroy(gameObject);

        IsOn = PlayerPrefs.GetInt("Sound", 0) == 1;

        DontDestroyOnLoad(gameObject);
    }

    public void Win()
    {
        sfxSource.volume = 1;
        sfxSource.PlayOneShot(winSound);
    }

    public void Ball()
    {
        sfxSource.volume = 1;
        sfxSource.PlayOneShot(ballSound);
    }

    public void Hit(float power)
    {
        sfxSource.volume = power / 100;
        sfxSource.PlayOneShot(hitSound);
    }
}
