using System.Collections.Generic;
using UnityEngine;

public class ItemsContainer : MonoBehaviour
{
    public List<CustomBodyPart> head_accs, heads, torsos, hands, legs, feet;

    public List<CustomWeapon> firstWeapons, secondWeapons;

    public static List<CustomBodyPart> head_accsList, headsList, torsosList, handsList, legsList, feetList;

    public static List<CustomWeapon> firstWeaponsList, secondWeaponsList;

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
    }

}
