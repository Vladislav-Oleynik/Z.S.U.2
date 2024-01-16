using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeGun : MonoBehaviour
{
    private PlayerController player;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.GetComponent<PlayerController>();
    }

    public void NextGun()
    {
        if (player)
        {
            player.NextWeapon();
        }
    }
}
