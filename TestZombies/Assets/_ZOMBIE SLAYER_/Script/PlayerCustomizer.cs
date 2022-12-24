using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class PlayerCustomizer : MonoBehaviour
{
    #region Our customizable GameObjects
    [Header("Our customizable parts")]

    [SerializeField] private GameObject pelvis;
    [SerializeField] private GameObject torso;
    [SerializeField] private GameObject head;
    [SerializeField] private GameObject hat;
    [SerializeField] private GameObject shoulder_r;
    [SerializeField] private GameObject shoulder_l;
    [SerializeField] private GameObject elbow_r;
    [SerializeField] private GameObject elbow_l;
    [SerializeField] private GameObject wrist_r;
    [SerializeField] private GameObject wrist_l;
    [SerializeField] private GameObject weapon;
    [SerializeField] private GameObject hip_l;
    [SerializeField] private GameObject hip_r;
    [SerializeField] private GameObject shin_l;
    [SerializeField] private GameObject shin_r;
    [SerializeField] private GameObject shoe_l;
    [SerializeField] private GameObject shoe_r;
    #endregion


    [Header("List of items for customize")]

    [SerializeField] private List<CustomBodyPart> torsoItems, headItems, legsItems, faceItems, feetItems, handsItems;

    private int torsoIdx, headIdx, legsIdx, faceIdx, feetIdx, handsIdx;

    private enum BodyPart { head, face, torso, legs, hands, feet }

    private void Start()
    {
        torsoIdx = headIdx = legsIdx = faceIdx = feetIdx = handsIdx = 0;

        LoadCustomItems();
    }

    //public void ChangeImage(CustomBodyPart cbp) 
    //{
    //    switch (cbp.GetBodyPartType())
    //    {
    //        case CustomBodyPart.BodyPartType.torso:
    //            ChangeTorsoItem(cbp);
    //            break;
    //        case CustomBodyPart.BodyPartType.legs:
    //            ChangeLegsItem(cbp);
    //            break;
    //        case CustomBodyPart.BodyPartType.head:
    //            ChangeHeadItem(cbp);
    //            break;
    //        case CustomBodyPart.BodyPartType.face:
    //            ChangeFaceItem(cbp);
    //            break;
    //        case CustomBodyPart.BodyPartType.feet:
    //            ChangeFeetItem(cbp);
    //            break;
    //        case CustomBodyPart.BodyPartType.hands:
    //            ChangeHandsItem(cbp);
    //            break;
    //    }
        
    //}

    public void ChangeTorsoItem()
    {
        if (torsoIdx >= torsoItems.Count())
            torsoIdx = 0;

        if (torsoItems.Count() != 0)
        {
            foreach (CustomItem item in torsoItems[torsoIdx].GetBodyParts())
            {
                switch (item.GetBodyPart())
                {
                    case CustomItem.BodyPart.torso:
                        torso.GetComponent<SpriteRenderer>().sprite = item.GetSprite();
                        break;
                    case CustomItem.BodyPart.shoulder_r:
                        shoulder_r.GetComponent<SpriteRenderer>().sprite = item.GetSprite();
                        break;
                    case CustomItem.BodyPart.shoulder_l:
                        shoulder_l.GetComponent<SpriteRenderer>().sprite = item.GetSprite();
                        break;
                    case CustomItem.BodyPart.elbow_r:
                        elbow_r.GetComponent<SpriteRenderer>().sprite = item.GetSprite();
                        break;
                    case CustomItem.BodyPart.elbow_l:
                        elbow_l.GetComponent<SpriteRenderer>().sprite = item.GetSprite();
                        break;
                    default:
                        Debug.Log(item + "does not belong to the specified body part");
                        break;
                }
            }

            torsoIdx++;
        }
    }

    public void ChangeLegsItem()
    {
        if (legsIdx >= legsItems.Count())
            legsIdx = 0;

        if (legsItems.Count() != 0)
        {
            foreach (CustomItem item in legsItems[legsIdx].GetBodyParts())
            {
                switch (item.GetBodyPart())
                {
                    case CustomItem.BodyPart.pelvis:
                        pelvis.GetComponent<SpriteRenderer>().sprite = item.GetSprite();
                        break;
                    case CustomItem.BodyPart.shin_l:
                        shin_l.GetComponent<SpriteRenderer>().sprite = item.GetSprite();
                        break;
                    case CustomItem.BodyPart.shin_r:
                        shin_r.GetComponent<SpriteRenderer>().sprite = item.GetSprite();
                        break;
                    case CustomItem.BodyPart.hip_l:
                        hip_l.GetComponent<SpriteRenderer>().sprite = item.GetSprite();
                        break;
                    case CustomItem.BodyPart.hip_r:
                        hip_r.GetComponent<SpriteRenderer>().sprite = item.GetSprite();
                        break;
                    default:
                        Debug.Log(item + "does not belong to the specified body part");
                        break;
                }
            }

            legsIdx++;
        }
    }

    public void ChangeHandsItem()
    {
        if (handsIdx >= handsItems.Count())
            handsIdx = 0;

        if (handsItems.Count() != 0)
        {
            foreach (CustomItem item in handsItems[handsIdx].GetBodyParts())
            {
                switch (item.GetBodyPart())
                {
                    case CustomItem.BodyPart.wrist_l:
                        wrist_l.GetComponent<SpriteRenderer>().sprite = item.GetSprite();
                        break;
                    case CustomItem.BodyPart.wrist_r:
                        wrist_r.GetComponent<SpriteRenderer>().sprite = item.GetSprite();
                        break;
                    default:
                        Debug.Log(item + "does not belong to the specified body part");
                        break;
                }
            }

            handsIdx++;
        }
    }

    public void ChangeFeetItem()
    {
        if (feetIdx >= feetItems.Count())
            feetIdx = 0;

        if (feetItems.Count() != 0)
        {
            foreach (CustomItem item in feetItems[feetIdx].GetBodyParts())
            {
                switch (item.GetBodyPart())
                {
                    case CustomItem.BodyPart.shoe_l:
                        shoe_l.GetComponent<SpriteRenderer>().sprite = item.GetSprite();
                        break;
                    case CustomItem.BodyPart.shoe_r:
                        shoe_r.GetComponent<SpriteRenderer>().sprite = item.GetSprite();
                        break;
                    default:
                        Debug.Log(item + "does not belong to the specified body part");
                        break;
                }
            }

            feetIdx++;
        }
    }

    public void ChangeHeadItem()
    {
        if (headIdx >= headItems.Count())
            headIdx = 0;

        if (headItems.Count() != 0)
        {
            foreach (CustomItem item in headItems[headIdx].GetBodyParts())
            {
                switch (item.GetBodyPart())
                {
                    case CustomItem.BodyPart.hat:
                        hat.GetComponent<SpriteRenderer>().sprite = item.GetSprite();
                        break;
                    default:
                        Debug.Log(item + "does not belong to the specified body part");
                        break;
                }
            }

            headIdx++;
        }
    }

    public void ChangeFaceItem()
    {
        if (faceIdx >= faceItems.Count())
            faceIdx = 0;

        if (faceItems.Count() != 0)
        {
            foreach (CustomItem item in faceItems[faceIdx].GetBodyParts())
            {
                switch (item.GetBodyPart())
                {
                    case CustomItem.BodyPart.head:
                        head.GetComponent<SpriteRenderer>().sprite = item.GetSprite();
                        break;
                    default:
                        Debug.Log(item + "does not belong to the specified body part");
                        break;
                }
            }

            faceIdx++;
        }
    }

    private void LoadCustomItems()
    {
        List<CustomBodyPart> customItems = Resources.LoadAll<CustomBodyPart>("").ToList();
        foreach (var item in customItems)
        {
            Debug.Log(item.GetItemName());
            switch (item.GetBodyPartType())
            {
                case CustomBodyPart.BodyPartType.face:
                    faceItems.Add(item);
                    break;
                case CustomBodyPart.BodyPartType.head:
                    headItems.Add(item);
                    break;
                case CustomBodyPart.BodyPartType.torso:
                    torsoItems.Add(item);
                    break;
                case CustomBodyPart.BodyPartType.hands:
                    handsItems.Add(item);
                    break;
                case CustomBodyPart.BodyPartType.legs:
                    legsItems.Add(item);
                    break;
                case CustomBodyPart.BodyPartType.feet:
                    feetItems.Add(item);
                    break;
            }
        }

    }
}
