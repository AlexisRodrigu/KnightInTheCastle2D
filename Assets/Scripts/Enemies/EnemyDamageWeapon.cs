using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamageWeapon : MonoBehaviour
{

    [SerializeField] private PlayerController playerControllerScript;
    [SerializeField] private float damageOfWeaponEnemy;
    
   public void DamageWeapon()
    {
        if (playerControllerScript.Health > 0)
            playerControllerScript.Health -= damageOfWeaponEnemy;
    }
}
