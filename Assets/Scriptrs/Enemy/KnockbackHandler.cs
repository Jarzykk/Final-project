using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockbackHandler : MonoBehaviour
{
    [SerializeField] private Enemy _enemy;
    [SerializeField] private float _knockbackForce;
    [SerializeField] private int _damage;

    private void OnEnable()
    {
        _enemy.Dying += OnThisEnemyDeath;
    }

    private void OnDisable()
    {
        _enemy.Dying -= OnThisEnemyDeath;
    }

    private void OnThisEnemyDeath()
    {
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<Player>())
        {
            collision.GetComponent<PlayersMovement>().Knockback(_knockbackForce);
            collision.GetComponent<Player>().ApplyDamage(_damage);
        }
    }
}
