using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BulletPack : MonoBehaviour, ICanCollect
{
    //public int amount = 30;
    public AudioClip sound;
    public static UnityEvent onAmmoCollected = new UnityEvent();

    public void Collect()
    {
        onAmmoCollected.Invoke();
        SoundManager.PlaySfx(sound);
        Destroy(gameObject);
    }
}
