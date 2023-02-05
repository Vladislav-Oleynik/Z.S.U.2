using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MoneyPack : MonoBehaviour
{
    public float amountOfMoney = 0f;

    public float priceOfPack = 0f;

    public TextMeshProUGUI moneyText;

    public TextMeshProUGUI priceText;

    void Start()
    {
        moneyText.text = amountOfMoney.ToString() + " gold";
        priceText.text = priceOfPack.ToString() + " $";
    }

}
