using UnityEngine;


[CreateAssetMenu(fileName = "Item", menuName = "ScriptableObjects/CustomItem", order = 2)]
public class CustomItem : ScriptableObject
{
    [SerializeField] private string itemName;

    public enum BodyPart { 
        pelvis, torso, head, head_acc,
        shoulder_r, shoulder_l,
        elbow_r, elbow_l, wrist_r,
        wrist_l, weapon, hip_l,
        hip_r, shin_l, shin_r,
        shoe_l, shoe_r 
    }

    [SerializeField] private BodyPart bodyPart;

    [SerializeField] private Sprite sprite;

    public string GetItemName() { return itemName; }

    public BodyPart GetBodyPart() { return bodyPart; }

    public Sprite GetSprite() { return sprite; }
}                                     