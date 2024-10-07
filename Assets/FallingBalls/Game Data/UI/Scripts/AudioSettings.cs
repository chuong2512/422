using UnityEngine;

public class AudioSettings : MonoBehaviour
{
    [SerializeField] GameObject on, off;

    private void Awake()
    {
        on.SetActive(SoundManager.Instance.IsOn);
        off.SetActive(!SoundManager.Instance.IsOn);
    }

    public void ChangeSettings()
    {
        SoundManager.Instance.IsOn = !SoundManager.Instance.IsOn;

        on.SetActive(SoundManager.Instance.IsOn);
        off.SetActive(!SoundManager.Instance.IsOn);
    }
}
