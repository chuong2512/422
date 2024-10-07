using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    public int level;
    [SerializeField] int levelGoal = 20;
    [SerializeField] bool autoBalls = true;

    bool won = false;
    bool lost = false;

    public delegate void OnWinAction(int points);
    public static event OnWinAction OnWin;

    public delegate void OnloseAction();
    public static event OnloseAction OnLose;

    public delegate void OnMoneyChangeAction(int amount);
    public static event OnMoneyChangeAction OnMoneyChange;

    public int GetGoal => levelGoal;

    [System.Serializable]
    public class Wallet
    {
        [SerializeField]
        int money = 0;
        public int Money
        {
            get
            {
                return money;
            }
            set
            {
                money = value;
                OnMoneyChange?.Invoke(money);
            }
        }

        public bool CanBuy(int price)
        {
            if (price <= Money)
            {
                Money -= price;
                return true;
            }
            else
            {
                return false;
            }
        }

        public void Initialize()
        {
            Money = PlayerPrefs.GetInt("Money", 0);
        }

        public void Save()
        {
            PlayerPrefs.SetInt("Money", money);
        }
    }
    public Wallet wallet;

    int collectedAmount = 0;

    protected override void Awake()
    {
        base.Awake();

        InitializeGame();
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        CloseGame();
    }
    private void Start()
    {
        if (ServicesManager.instance != null)
        {
            ServicesManager.instance.InitializeAdmob();
            ServicesManager.instance.InitializeUnityAds();
        }
    }
    void InitializeGame()
    {
        if (level > 10)
            level = PlayerPrefs.GetInt("LastLevel", 11);

        GameObject logoController = new GameObject("Logo Controller", typeof(Logo));
        if (Loader.instance == null)
        {
            GameObject loader = new GameObject("Loader", typeof(Loader));
        }

        wallet.Initialize();

        if (autoBalls)
            levelGoal = (int)(FindObjectsOfType<Ball>().Length * 0.8f);

        Invoke("CalculatGoal", 0.5f);
        collectedAmount = 0;
    }

    void CloseGame()
    {
        wallet.Save();
    }

    public void PlayerWon(int collectedAmount)
    {
        if(ServicesManager.instance != null)
        {
            ServicesManager.instance.ShowInterstitialAdmob();
            ServicesManager.instance.ShowInterstitialUnityAds();
        }

        if (won || lost)
            return;

        won = true;

        OnWin?.Invoke(collectedAmount);
        this.collectedAmount = collectedAmount;

        PlayerPrefs.SetInt("LastLevel", level + 1);
    }

    public void PlayerLost()
    {
        if (ServicesManager.instance != null)
        {
            ServicesManager.instance.ShowInterstitialAdmob();
            ServicesManager.instance.ShowInterstitialUnityAds();
        }

        if (won || lost)
            return;

        lost = true;

        OnLose?.Invoke();
    }

    public void Reload()
    {
        Logo.instance.Remove();
        Loader.instance.Restart();
    }

    public void NextLevel()
    {
        Loader.instance.NextLevel();
    }

    public void CalculatGoal()
    {
        levelGoal = (int)(FindObjectsOfType<Ball>().Length * 0.8f);
    }

    public void GetReward(int multiplyer)
    {
        wallet.Money += collectedAmount * multiplyer;
    }
}
