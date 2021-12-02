using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(EnemyAnimations), typeof(Collider2D), typeof(Rigidbody2D))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private int _maxHealth = 50;
    [SerializeField] private Player _target;
    [SerializeField] private Transform _attackPoint;
    [SerializeField] private AudioSource _deathSound;

    public event UnityAction Dying;
    public event UnityAction TakingDamage;
    public Player Target => _target;
    public Transform AttackPoint => _attackPoint;

    private int _currentHealth;
    private EnemyAnimations _enemyAnimations;
    private Collider2D _collider;
    private Rigidbody2D _rigidBody;

    private void Start()
    {
        _enemyAnimations = GetComponent<EnemyAnimations>();
        _collider = GetComponent<Collider2D>();
        _rigidBody = GetComponent<Rigidbody2D>();
        _currentHealth = _maxHealth;
    }

    public void TakeDamage(int damage)
    {
        if(damage > 0 && _currentHealth > 0)
        {
            _currentHealth -= damage;
            _enemyAnimations.OnTakeDamage();
            TakingDamage?.Invoke();

            if (_currentHealth <= 0)
                Die();
        }
    }

    private void Die()
    {
        _deathSound.Play();
        _enemyAnimations.OnDeath();
        _collider.enabled = false;
        _rigidBody.isKinematic = true;
        Dying?.Invoke();
        this.enabled = false;
    }
}
