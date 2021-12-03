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
        if(collision.TryGetComponent<Player>(out Player player) && collision.TryGetComponent<PlayersMovement>(out PlayersMovement playersController))
        {
            playersController.Knockback(_knockbackForce);
            player.ApplyDamage(_damage);
        }
    }
}
