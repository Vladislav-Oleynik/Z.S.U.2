using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipmentShop : MonoBehaviour
{
    public float price = 0f;
    public Text priceText;

    void Start()
    {
        priceText.text = price.ToString() + " $";
    }

}
