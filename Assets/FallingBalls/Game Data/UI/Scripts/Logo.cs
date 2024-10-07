using UnityEngine;

public class Logo : MonoBehaviour
{
    public static Logo instance;

    GameObject logo;

    private void Awake()
    {
        logo = GameObject.FindWithTag("Logo");

        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            if (logo != null)
                Destroy(logo.gameObject);

            Destroy(gameObject);
        }
    }

    public void Remove()
    {
        instance = null;
        Destroy(gameObject);
    }
}
