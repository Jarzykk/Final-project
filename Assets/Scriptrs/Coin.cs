using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<Player>())
        {
            collision.gameObject.GetComponent<Player>().TakeCoin();
            Destroy(gameObject);
        }
    }
}
