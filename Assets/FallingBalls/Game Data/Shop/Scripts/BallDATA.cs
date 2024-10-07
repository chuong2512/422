using UnityEngine;

[CreateAssetMenu(menuName = "Create Ball DATA", fileName = "New BallDATA")]
public class BallDATA : ScriptableObject
{
    public Sprite icon;
    public Mesh mesh;
    public bool isPreunklocked = false;

    public bool IsUnlocked
    {
        get
        {
            if (isPreunklocked)
                return true;

            return PlayerPrefs.GetInt(name + "IsUnlocked", 0) == 1;
        }
    }

    public void Unlock()
    {
        PlayerPrefs.SetInt(name + "IsUnlocked", 1);
    }
}
