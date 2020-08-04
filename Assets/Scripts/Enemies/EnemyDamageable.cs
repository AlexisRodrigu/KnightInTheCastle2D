using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamageable : MonoBehaviour
{
    [SerializeField] private PlayerDamageWeapon playerDamageWeaponScript;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "SwordPlayer")
            playerDamageWeaponScript.DamageWeapon();

    }
}
