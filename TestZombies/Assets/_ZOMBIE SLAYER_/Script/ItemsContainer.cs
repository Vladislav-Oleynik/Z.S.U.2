using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemsContainer : MonoBehaviour
{
    public List<CustomBodyPart> head_accs, heads, torsos, hands, legs, feet, weapons;

    public static List<CustomBodyPart> head_accsList, headsList, torsosList, handsList, legsList, feetList, weaponsList;

    private void Start()
    {
        head_accsList = head_accs;
        headsList = heads;
        torsosList = torsos;
        handsList = hands;
        legsList = legs;
        feetList = feet;
        weaponsList = weapons;
    }

}
