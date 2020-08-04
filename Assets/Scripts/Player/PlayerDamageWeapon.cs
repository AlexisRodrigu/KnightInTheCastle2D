using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamageWeapon : MonoBehaviour
{
    [SerializeField] private EnemyController enemyControllerScript;
    [SerializeField] private float damageOfWeaponEnemy;
    
   public void DamageWeapon()
    {
        if (enemyControllerScript.LifeEnemy > 0)
            enemyControllerScript.LifeEnemy -= damageOfWeaponEnemy;
    }
}
