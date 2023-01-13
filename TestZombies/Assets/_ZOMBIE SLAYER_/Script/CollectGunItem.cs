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
            if (playerLoadout.firstWeapons.Contains(weapon))
            {
                playerLoadout.firstWeapons[playerLoadout.firstWeapons.IndexOf(weapon)].ResetBullet();
                onWeaponCollected.Invoke(weapon);
                Destroy(gameObject);
                return;
            }
            playerLoadout.firstWeapons.Add(weapon);
            onWeaponCollected.Invoke(weapon);
        }
        else if (weapon.weaponType == CustomWeapon.WEAPON_TYPE.second)
        {
            if (playerLoadout.secondWeapons.Contains(weapon))
            {
                playerLoadout.secondWeapons[playerLoadout.secondWeapons.IndexOf(weapon)].ResetBullet();
                onWeaponCollected.Invoke(weapon);
                Destroy(gameObject);
                return;
            }
            playerLoadout.secondWeapons.Add(weapon);
            onWeaponCollected.Invoke(weapon);
        }

        
        //GunManager.Instance.SetNewGunDuringGameplay(gunTypeID);
        Destroy(gameObject);
    }
}
