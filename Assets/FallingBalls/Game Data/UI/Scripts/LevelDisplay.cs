using UnityEngine;
using UnityEngine.UI;

public class LevelDisplay : MonoBehaviour
{
   Text levelText;

    private void Awake()
    {
        levelText = GetComponent<Text>();

        levelText.text = "Level " + GameManager.Instance.level.ToString();
    }
}
