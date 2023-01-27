using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopMenu : MonoBehaviour
{
    public GameObject allCategories;
    public GameObject weaponsCategory;

    private void Start()
    {
        weaponsCategory.SetActive(false);
    }

    public void WeaponsClick()
    {
        weaponsCategory.SetActive(true);
        allCategories.SetActive(false);
    }

    public void BackWeaponsClick()
    {
        weaponsCategory.SetActive(false);
        allCategories.SetActive(true);
    }
}
