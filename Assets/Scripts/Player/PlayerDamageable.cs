using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamageable : MonoBehaviour
{
    [SerializeField] private EnemyDamageWeapon enemyDamageWeapon;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "SwordEnemy")
            enemyDamageWeapon.DamageWeapon();
    }
}
