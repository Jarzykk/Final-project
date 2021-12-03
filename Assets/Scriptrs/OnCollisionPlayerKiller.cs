using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnCollisionPlayerKiller : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent<Player>(out Player player))
        {
            player.ApplyDamage(player.CurrentHealth);
        }
    }
}
