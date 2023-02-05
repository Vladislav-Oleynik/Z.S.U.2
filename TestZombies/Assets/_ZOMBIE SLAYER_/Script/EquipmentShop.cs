using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EquipmentShop : MonoBehaviour
{
    public float price = 0f;
    public TextMeshProUGUI priceText;

    void Start()
    {
        priceText.text = price.ToString() + " $";
    }

}
