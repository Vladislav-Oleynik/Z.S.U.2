using UnityEngine;
using UnityEngine.UI;

public class MoneyPack : MonoBehaviour
{
    public float amountOfMoney = 0f;

    public float priceOfPack = 0f;

    public Text moneyText;

    public Text priceText;

    void Start()
    {
        moneyText.text = amountOfMoney.ToString() + " gold";
        priceText.text = priceOfPack.ToString() + " $";
    }

}
