using UnityEngine;
using UnityEngine.Events;

public class CollectGunItem : MonoBehaviour, ICanCollect
{
    [SerializeField] private CustomWeapon weapon;
    public AudioClip soundCollect;
    [SerializeField] private PlayerLoadout playerLoadout;
    public static UnityEvent<CustomWeapon> onWeaponCollected = new UnityEvent<CustomWeapon>();

    public void Collect()
    {
        SoundManager.PlaySfx(soundCollect);
        if (weapon.weaponType == CustomWeapon.WEAPON_TYPE.first)
        {
            playerLoadout.firstWeapons.Add(weapon);
            onWeaponCollected.Invoke(weapon);
        }
        else if (weapon.weaponType == CustomWeapon.WEAPON_TYPE.second)
        {
            playerLoadout.secondWeapons.Add(weapon);
            onWeaponCollected.Invoke(weapon);
        }
        //GunManager.Instance.SetNewGunDuringGameplay(gunTypeID);
        Destroy(gameObject);
    }
}
