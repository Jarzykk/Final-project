using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnCollisionPlayerKiller : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<Player>())
        {
            Player player = collision.GetComponent<Player>();

            player.ApplyDamage(player.CurrentHealth);
        }
    }
}
