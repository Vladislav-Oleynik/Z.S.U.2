using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "ScriptableObjects/CustomBodyPart", order = 1)]
public class CustomBodyPart : ScriptableObject
{
    [SerializeField] private string itemName;
    public enum BodyPartType { head, head_acc, torso, legs, hands, feet }

    [SerializeField] private BodyPartType bodyPart;

    [SerializeField] private List<CustomItem> bodyParts;

    public string GetItemName() { return itemName; }

    public BodyPartType GetBodyPartType() { return bodyPart; }

    public List<CustomItem> GetBodyParts() { return bodyParts; }
}
