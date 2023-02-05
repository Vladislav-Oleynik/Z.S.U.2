using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopCharacterUpgrade : MonoBehaviour
{
    //public GunTypeID weapon;
    //public GameObject lockedObj;
    //public Text unlockPriceTxt;
    //public GameObject dot;
    //public GameObject dotHoder;
    //List<Image> upgradeDots;
    //public Sprite dotImageOn, dotImageOff;
    //[Space]
    //public Text
    //    currentRangeDamage, upgradeRangeDamageStep;

    public CustomWeapon weapon;

    public TextMeshProUGUI price;

    bool isMax = false;

    public TextMeshProUGUI currentWeaponLvl;


    // Start is called before the first frame update
    void Start()
    {
        //upgradeDots = new List<Image>();
        //upgradeDots.Add(dot.GetComponent<Image>());
        //for (int i = 1; i < weapon.UpgradeSteps.Length; i++)
        //{
        //    upgradeDots.Add(Instantiate(dot, dotHoder.transform).GetComponent<Image>());
        //}

        currentWeaponLvl.text = weapon.CurrentUpgrade.ToString() + " lvl";

        if (weapon.CurrentUpgrade + 1 >= weapon.UpgradeSteps.Length)
            isMax = true;

        UpdateParameter();
    }

    void UpdateParameter()
    {
        //lockedObj.SetActive(!weapon.isUnlocked);
        //unlockPriceTxt.text = "$" + weapon.unlockPrice;

        //currentRangeDamage.text = weapon.UpgradeRangeDamage + "";
        if (isMax)
        {
            //upgradeRangeDamageStep.enabled = false;
            price.text = "";
            currentWeaponLvl.text = "Max lvl";
        }

        else
        {
            price.text = weapon.UpgradeSteps[weapon.CurrentUpgrade + 1].price + "";
            //upgradeRangeDamageStep.text = "-> " + weapon.UpgradeSteps[weapon.CurrentUpgrade + 1].damage;
        }
       
        //SetDots(weapon.CurrentUpgrade + 1);
    }

    //void SetDots(int number)
    //{
    //    for (int i = 0; i < upgradeDots.Count; i++)
    //    {
    //        if (i < number)
    //            upgradeDots[i].sprite = dotImageOn;
    //        else
    //            upgradeDots[i].sprite = dotImageOff;
    //    }
    //}

    public void Upgrade()
    {
        if (isMax)
            return;

        if (GlobalValue.SavedCoins >= weapon.UpgradeSteps[weapon.CurrentUpgrade + 1].price)
        {
            GlobalValue.SavedCoins -= weapon.UpgradeSteps[weapon.CurrentUpgrade + 1].price;
            SoundManager.PlaySfx(SoundManager.Instance.soundUpgrade);

            weapon.UpgradeCharacter();


            if (weapon.CurrentUpgrade + 1 >= weapon.UpgradeSteps.Length)
                isMax = true;

            UpdateParameter();
        }
        else
            SoundManager.PlaySfx(SoundManager.Instance.soundNotEnoughCoin);
    }

    //public void UnlockPrice()
    //{
    //    if(GlobalValue.SavedCoins >= weapon.unlockPrice)
    //    {
    //        SoundManager.PlaySfx(SoundManager.Instance.soundUnlockGun);
    //        GlobalValue.SavedCoins -= weapon.unlockPrice;
    //        weapon.isUnlocked = true;
    //        UpdateParameter();
    //    }
    //}
}
