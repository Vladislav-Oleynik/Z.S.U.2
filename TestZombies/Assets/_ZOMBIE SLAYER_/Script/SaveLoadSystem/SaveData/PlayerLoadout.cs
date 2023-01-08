using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Loadout", menuName = "ScriptableObjects/PlayerLoadout", order = 2)]
public class PlayerLoadout : ScriptableObject
{
    public List<CustomBodyPart> customItems;

    public List<CustomWeapon> firstWeapons, secondWeapons;

    [Header("INDEXES")]
    public int head_accIdx;
    public int torsoIdx;
    public int legsIdx;
    public int headIdx;
    public int feetIdx;
    public int handsIdx;
    public int firstWeaponIdx;
    public int secondWeaponIdx;

}
