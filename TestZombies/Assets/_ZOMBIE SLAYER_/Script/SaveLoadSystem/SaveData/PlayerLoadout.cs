using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Loadout", menuName = "ScriptableObjects/PlayerLoadout", order = 2)]
public class PlayerLoadout : ScriptableObject
{
    public List<CustomBodyPart> customItems;

    public int torsoIdx, head_accIdx, legsIdx, headIdx, feetIdx, handsIdx;

}
