using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCoins : MonoBehaviour
{
    private int coinsAdd = 1;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            FindObjectOfType<GameManager>().AddScore(coinsAdd);
            Destroy(this.gameObject);
        }
    }
}
