using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    [HideInInspector] public bool reward1;
    [HideInInspector] public bool reward2;

    [SerializeField] Animator gameOverAnim;
    [SerializeField] Text collectedPointsText;
    [SerializeField] Text levelCompletdText;

    [Space]
    [SerializeField] Animator menuAnim;

    private void Start()
    {
        instance = this;

        if (ServicesManager.instance != null && PlayerPrefs.GetInt("noAds") == 0)
        {
            ServicesManager.instance.InitializeAdmob();
            ServicesManager.instance.InitializeUnityAds();
        }
    }
    private void OnEnable()
    {
        GameManager.OnWin += ShowWinPanel;
        GameManager.OnLose += ShowLostPanel;
    }

    private void OnDisable()
    {
        GameManager.OnWin -= ShowWinPanel;
        GameManager.OnLose -= ShowLostPanel;
    }

    void ShowWinPanel(int points)
    {
        gameOverAnim.SetBool("Win", true);
        gameOverAnim.SetBool("Lose", false);

        collectedPointsText.text = "+" + points;
        levelCompletdText.text = "Level " + GameManager.Instance.level + " completed!";
    }

    void ShowLostPanel()
    {
        gameOverAnim.SetBool("Win", false);
        gameOverAnim.SetBool("Lose", true);
    }

    public void HideMenu()
    {
        menuAnim.SetBool("Show", false);
    }

    public void Next()
    {
        GameManager.Instance.GetReward(1);
        GameManager.Instance.NextLevel();
    }

    public void NextRewarded()
    {
        if(ServicesManager.instance != null)
        {
            ServicesManager.instance.ShowRewardedVideoAdAdmob();
            ServicesManager.instance.ShowRewardedVideoAdAdmob();

            reward1 = true;
            reward2 = false;
        }
    }
    public void GetReward(bool get)
    {
        if(get)
        {
            GameManager.Instance.GetReward(5);
            GameManager.Instance.NextLevel();
        }
    }
    public void MoneyForVideoAds()
    {
        if (ServicesManager.instance != null)
        {
            ServicesManager.instance.ShowRewardedVideoAdAdmob();
            ServicesManager.instance.ShowRewardedVideoAdAdmob();

            reward1 = false;
            reward2 = true;
        }
    }
    public void GetReward2(bool get)
    {
        if (get)
        {
            GameManager.Instance.wallet.Money += 100;
        }
    }
}
