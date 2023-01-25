using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemsContainer : MonoBehaviour
{
    public List<CustomBodyPart> head_accs, heads, torsos, hands, legs, feet;

    public List<CustomWeapon> firstWeapons, secondWeapons;

    public static List<CustomBodyPart> head_accsList, headsList, torsosList, handsList, legsList, feetList;

    public static List<CustomWeapon> firstWeaponsList, secondWeaponsList;

    public Image firstWeapon, secondWeapon;

    private int firstWeaponIdx, secondWeaponIdx;

    public PlayerLoadout loadout;

    private void Start()
    {
        head_accsList = head_accs;
        headsList = heads;
        torsosList = torsos;
        handsList = hands;
        legsList = legs;
        feetList = feet;
        firstWeaponsList = firstWeapons;
        secondWeaponsList = secondWeapons;
        firstWeaponIdx = secondWeaponIdx = 0;

        firstWeaponIdx = loadout.firstWeaponIdx;
        secondWeaponIdx = loadout.secondWeaponIdx;

        firstWeapon.sprite = firstWeapon.sprite;
        secondWeapon.sprite = secondWeapon.sprite;

        if (firstWeaponsList.Count != 0)
            firstWeapon.sprite = firstWeaponsList[firstWeaponIdx].icon;
        //firstWeapon.sprite = firstWeaponsList[0].icon;
        else
            Debug.Log("firstWeaponsList.Count == 0");

        if (secondWeaponsList.Count != 0)
            secondWeapon.sprite = secondWeaponsList[secondWeaponIdx].icon;
        //secondWeapon.sprite = secondWeaponsList[0].icon;
        else
            Debug.Log("secondWeaponsList.Count == 0");
    }

    public void ChangeWeaponImage(bool isFirstWeapon = false)
    {
        if (firstWeaponsList.Count == 0 || secondWeaponsList.Count == 0)
        {
            Debug.Log("First or second weapons list is empty!");
            return;
        }

        if (isFirstWeapon)
        {
            firstWeaponIdx++;

            if (firstWeaponIdx < firstWeapons.Count)
            {
                firstWeapon.sprite = firstWeaponsList[firstWeaponIdx].icon;
            }
            else
            {
                firstWeapon.sprite = firstWeaponsList[0].icon;
                firstWeaponIdx = 0;
            }
            
        }
        else
        {
            secondWeaponIdx++;

            if (secondWeaponIdx < secondWeapons.Count)
            {
                secondWeapon.sprite = secondWeaponsList[secondWeaponIdx].icon;
            }
            else
            {
                secondWeapon.sprite = secondWeaponsList[0].icon;
                secondWeaponIdx = 0;
            }
            
        }
    }
}
