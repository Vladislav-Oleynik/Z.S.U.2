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
    [SerializeField] private GameObject head_acc;
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

    [SerializeField] private PlayerLoadout playerLoadout;

    private int torsoIdx, head_accIdx, legsIdx, headIdx, feetIdx, handsIdx, firstWeaponIdx, secondWeaponIdx;

    private void Awake()
    {
        //if(PlayerController.onWeaponChanged != null)
            PlayerController.onWeaponChanged.AddListener(LoadWeapon);
    }

    private void OnDestroy()
    {
        //if (PlayerController.onWeaponChanged != null)
            PlayerController.onWeaponChanged.RemoveListener(LoadWeapon);
    }

    private void Start()
    {
        LoadPlayerLoadout();
    }

    public void ChangeTorsoItem()
    {     
        if (ItemsContainer.torsosList.Count() != 0)
        {
            torsoIdx++;
            if (torsoIdx >= ItemsContainer.torsosList.Count())
                torsoIdx = 0;
            foreach (CustomItem item in ItemsContainer.torsosList[torsoIdx].GetBodyParts())
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
        }
    }

    public void ChangeLegsItem()
    {        
        if (ItemsContainer.legsList.Count() != 0)
        {
            legsIdx++;
            if (legsIdx >= ItemsContainer.legsList.Count())
                legsIdx = 0;
            foreach (CustomItem item in ItemsContainer.legsList[legsIdx].GetBodyParts())
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
        }
    }

    public void ChangeHandsItem()
    {        
        if (ItemsContainer.handsList.Count() != 0)
        {
            handsIdx++;
            if (handsIdx >= ItemsContainer.handsList.Count())
                handsIdx = 0;
            foreach (CustomItem item in ItemsContainer.handsList[handsIdx].GetBodyParts())
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
        }
    }

    public void ChangeFeetItem()
    {        
        if (ItemsContainer.feetList.Count() != 0)
        {
            feetIdx++;
            if (feetIdx >= ItemsContainer.feetList.Count())
                feetIdx = 0;
            foreach (CustomItem item in ItemsContainer.feetList[feetIdx].GetBodyParts())
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
        }
    }

    public void ChangeHead_accItem()
    {        
        if (ItemsContainer.head_accsList.Count() != 0)
        {
            head_accIdx++;
            if (head_accIdx >= ItemsContainer.head_accsList.Count())
                head_accIdx = 0;
            foreach (CustomItem item in ItemsContainer.head_accsList[head_accIdx].GetBodyParts())
            {
                switch (item.GetBodyPart())
                {
                    case CustomItem.BodyPart.head_acc:
                        head_acc.GetComponent<SpriteRenderer>().sprite = item.GetSprite();
                        break;
                    default:
                        Debug.Log(item + "does not belong to the specified body part");
                        break;
                }
            }
        }
    }

    public void ChangeHeadItem()
    {        
        if (ItemsContainer.headsList.Count() != 0)
        {
            headIdx++;
            if (headIdx >= ItemsContainer.headsList.Count())
                headIdx = 0;
            foreach (CustomItem item in ItemsContainer.headsList[headIdx].GetBodyParts())
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
        }
    }

    public void ChangeFirstWeapon()
    {
        if (ItemsContainer.firstWeaponsList.Count() != 0)
        {
            firstWeaponIdx++;
            if (firstWeaponIdx >= ItemsContainer.firstWeaponsList.Count())
                firstWeaponIdx = 0;
            weapon.GetComponent<SpriteRenderer>().sprite = ItemsContainer.firstWeaponsList[firstWeaponIdx].icon;
        }
        else
            Debug.Log("firstWeaponsList.Count() == 0");
    }
    public void ChangeSecondWeapon()
    {
        if (ItemsContainer.secondWeaponsList.Count() != 0)
        {
            secondWeaponIdx++;
            if (secondWeaponIdx >= ItemsContainer.secondWeaponsList.Count())
                secondWeaponIdx = 0;
            weapon.GetComponent<SpriteRenderer>().sprite = ItemsContainer.secondWeaponsList[secondWeaponIdx].icon;
        }
        else
            Debug.Log("secondWeaponsList.Count() == 0");
    }

    private void LoadTorsoItem(CustomBodyPart cbp)
    {                
        foreach (CustomItem item in cbp.GetBodyParts())
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
    }

    private void LoadLegsItem(CustomBodyPart cbp)
    {
        foreach (CustomItem item in cbp.GetBodyParts())
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
    }

    private void LoadHandsItem(CustomBodyPart cbp)
    {
        foreach (CustomItem item in cbp.GetBodyParts())
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
    }

    private void LoadFeetItem(CustomBodyPart cbp)
    {
        foreach (CustomItem item in cbp.GetBodyParts())
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
    }

    private void LoadHead_accItem(CustomBodyPart cbp)
    {
        foreach (CustomItem item in cbp.GetBodyParts())
        {
            switch (item.GetBodyPart())
            {
                case CustomItem.BodyPart.head_acc:
                    head_acc.GetComponent<SpriteRenderer>().sprite = item.GetSprite();
                    break;
                default:
                    Debug.Log(item + "does not belong to the specified body part");
                    break;
            }
        }
    }

    private void LoadHeadItem(CustomBodyPart cbp)
    {
        foreach (CustomItem item in cbp.GetBodyParts())
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
    }

    private void LoadWeapon(CustomWeapon cw)
    {                           
        weapon.GetComponent<SpriteRenderer>().sprite = cw.icon;
    }

    private void LoadPlayerLoadout()
    {
        if (playerLoadout == null)
        {
            Debug.Log("PlayerLoadout == null !!!");
            return;
        }

        headIdx = playerLoadout.headIdx;
        head_accIdx = playerLoadout.head_accIdx;
        torsoIdx = playerLoadout.torsoIdx;
        handsIdx = playerLoadout.handsIdx;
        legsIdx = playerLoadout.legsIdx;
        feetIdx = playerLoadout.feetIdx;
        firstWeaponIdx = playerLoadout.firstWeaponIdx;
        secondWeaponIdx = playerLoadout.secondWeaponIdx;
        //now only one weapon of each type

        if (playerLoadout.customItems.Count != 0)
        {
            foreach (CustomBodyPart item in playerLoadout.customItems)
            {
                switch (item.GetBodyPartType())
                {
                    case CustomBodyPart.BodyPartType.head_acc:
                        LoadHead_accItem(item);
                        break;
                    case CustomBodyPart.BodyPartType.head:
                        LoadHeadItem(item);
                        break;
                    case CustomBodyPart.BodyPartType.torso:
                        LoadTorsoItem(item);
                        break;
                    case CustomBodyPart.BodyPartType.hands:
                        LoadHandsItem(item);
                        break;
                    case CustomBodyPart.BodyPartType.legs:
                        LoadLegsItem(item);
                        break;
                    case CustomBodyPart.BodyPartType.feet:
                        LoadFeetItem(item);
                        break;
                }
            }
        }

        if (playerLoadout.firstWeapons.Count() != 0 /*&& firstWeaponIdx <= playerLoadout.firstWeapons.Count()*/)
        {
            //LoadWeapon(playerLoadout.firstWeapons[firstWeaponIdx]);
            LoadWeapon(playerLoadout.firstWeapons[0]);
            return;
        }

        if (playerLoadout.secondWeapons.Count() != 0 /*&& secondWeaponIdx <= playerLoadout.secondWeapons.Count()*/)
        {
            //LoadWeapon(playerLoadout.secondWeapons[secondWeaponIdx]);
            LoadWeapon(playerLoadout.secondWeapons[0]);
        }
    }

    public void SavePlayerLoadout()
    {
        playerLoadout.customItems = new List<CustomBodyPart>();
        playerLoadout.firstWeapons = new List<CustomWeapon>();
        playerLoadout.secondWeapons = new List<CustomWeapon>();

        playerLoadout.customItems.Add(ItemsContainer.headsList[headIdx]);
        playerLoadout.customItems.Add(ItemsContainer.head_accsList[head_accIdx]);
        playerLoadout.customItems.Add(ItemsContainer.torsosList[torsoIdx]);
        playerLoadout.customItems.Add(ItemsContainer.handsList[handsIdx]);
        playerLoadout.customItems.Add(ItemsContainer.legsList[legsIdx]);
        playerLoadout.customItems.Add(ItemsContainer.feetList[feetIdx]);

        playerLoadout.firstWeapons.Add(ItemsContainer.firstWeaponsList[firstWeaponIdx]);
        playerLoadout.secondWeapons.Add(ItemsContainer.secondWeaponsList[secondWeaponIdx]);

        playerLoadout.headIdx = headIdx;
        playerLoadout.head_accIdx = head_accIdx;
        playerLoadout.torsoIdx = torsoIdx;
        playerLoadout.handsIdx = handsIdx;
        playerLoadout.legsIdx = legsIdx;
        playerLoadout.feetIdx = feetIdx;
        playerLoadout.firstWeaponIdx = firstWeaponIdx;
        playerLoadout.secondWeaponIdx = secondWeaponIdx;

        GlobalValue.SaveLoadout(playerLoadout);
        GlobalValue.LoadPlayerLoadout(ref playerLoadout);
    }

}
