using UnityEngine;
using UnityEngine.UI;

public class BallHolder : MonoBehaviour
{
    public BallDATA data;
    [SerializeField] GameObject unlocked, locked, selected;
    [SerializeField] Image icon;

    int price = 500;

    void UpdateHolder()
    {
        if (data.icon != null)
            icon.sprite = data.icon;

        unlocked.SetActive(data.IsUnlocked);
        locked.SetActive(!data.IsUnlocked);
    }

    void Buy()
    {
        GameManager.Instance.wallet.Money -= price;
        data.Unlock();
    }

    public void Set(BallDATA data)
    {
        this.data = data;
        UpdateHolder();
    }

    public void Selected(bool value)
    {
        selected.SetActive(value);
    }

    public void HandlePressButton()
    {
        if (data.IsUnlocked)
        {
            ShopManager.Instance.SelectBall(data);
        }
        else
        {
            if (GameManager.Instance.wallet.Money >= price)
            {
                Buy();
                ShopManager.Instance.SelectBall(data);
                UpdateHolder();
            }
        }
    }
}
