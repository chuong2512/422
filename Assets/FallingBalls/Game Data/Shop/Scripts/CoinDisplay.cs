using UnityEngine;
using UnityEngine.UI;

public class CoinDisplay : MonoBehaviour
{
    [SerializeField] Text moneyText;

    private void OnEnable()
    {
        GameManager.OnMoneyChange += UpdateMoney;
    }

    private void OnDisable()
    {
        GameManager.OnMoneyChange -= UpdateMoney;
    }

    private void Awake()
    {
        moneyText.text = GameManager.Instance.wallet.Money.ToString() + "$";
    }

    void UpdateMoney(int money)
    {
        moneyText.text = money.ToString() + "$";
    }
}
