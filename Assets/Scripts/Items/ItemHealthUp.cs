using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemHealthUp : MonoBehaviour
{
    private float healthAdd = 10f;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            FindObjectOfType<GameManager>().HealthUp(healthAdd);
            Destroy(this.gameObject);
        }
    }
}
