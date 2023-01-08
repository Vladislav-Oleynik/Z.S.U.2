using UnityEngine;

[CreateAssetMenu(fileName = "Weapon", menuName = "ScriptableObjects/CustomWeapon", order = 3)]
public class CustomWeapon : ScriptableObject
{
    public enum WEAPON_TYPE {first, second}
    public WEAPON_TYPE weaponType;
    public bool unlockDefault = false;
    public string gunName;
    public Sprite icon;
    public int unlockPrice = 100;
    [Header("ANIMATION")]
    public AnimatorOverrideController animatorOverride;
    [Header("WEAPONS")]
    public int maxBullet = 99;
    public ShootingMethob shootingMethob;
    [Range(0, 100)]
    public int minPercentAffect = 90;
    public float rate = 0.2f;
    public float reloadTime = 2;
    [Range(0.5f, 1f)]
    public float accuracy = 0.9f;
    public GameObject shellFX;
    public Transform shellPoint;

    public AudioClip soundFire;
    [Range(0, 1)]
    public float soundFireVolume = 0.5f;
    public AudioClip shellSound;
    [Range(0, 1)]
    public float shellSoundVolume = 0.5f;
    public AudioClip reloadSound;
    [Range(0, 1)]
    public float reloadSoundVolume = 0.5f;
    public bool reloadPerShoot = false;
    public bool dualShot = false;
    public float fireSecondGunDelay = 0.1f;
    public bool isSpreadBullet = false;
    public int maxBulletPerShoot = 1;

    public GameObject muzzleTracerFX;
    public GameObject muzzleFX;

    [Header("UPGRADE")]
    [Space]
    public UpgradeStep[] UpgradeSteps;

    public int bullet
    {
        get { return PlayerPrefs.GetInt("gunID" + gunName, maxBullet); }
        set { PlayerPrefs.SetInt("gunID" + gunName, Mathf.Min(value, maxBullet)); }
    }

    public bool isUnlocked
    {
        get { return (PlayerPrefs.GetInt("isUnlocked" + gunName, 0) == 1) || unlockDefault; }
        set { PlayerPrefs.SetInt("isUnlocked" + gunName, value ? 1 : 0); }
    }
    public int UpgradeRangeDamage
    {
        get { return PlayerPrefs.GetInt(gunName + "UpgradeRangeDamage", UpgradeSteps[0].damage); }
        set { PlayerPrefs.SetInt(gunName + "UpgradeRangeDamage", value); }
    }

    public int CurrentUpgrade
    {
        get
        {
            int current = PlayerPrefs.GetInt(gunName + "upgrade" + "Current", 0);
            if (current >= UpgradeSteps.Length)
                current = -1;   //-1 mean overload
            return current;
        }
        set
        {
            PlayerPrefs.SetInt(gunName + "upgrade" + "Current", value);
        }
    }

    public void ResetBullet()
    {
        bullet = maxBullet;
    }
    public void UpgradeCharacter()
    {
        CurrentUpgrade++;
        UpgradeRangeDamage = UpgradeSteps[CurrentUpgrade].damage;
    }
}

[System.Serializable]
public class UpgradeStep
{
    public int price;
    public int damage;
}