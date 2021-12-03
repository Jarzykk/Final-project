using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator), typeof(Enemy))]
public class EnemyAnimations : MonoBehaviour
{
    private Enemy _enemy;

    private void OnEnable()
    {
        _enemy = GetComponent<Enemy>();

        _enemy.TakingDamage += OnTakeDamage;
        _enemy.Dying += OnDeath;
    }

    private void OnDisable()
    {
        _enemy.TakingDamage -= OnTakeDamage;
        _enemy.Dying -= OnDeath;
    }

    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void Idle()
    {
        _animator.SetTrigger("BecameIdle");
    }

    public void Move()
    {
        _animator.SetBool("IsMoving", true);
    }

    public void StopMoving()
    {
        _animator.SetBool("IsMoving", false);
    }

    public void OnAttack()
    {
        _animator.SetTrigger("IsAttaking");
    }

    public void OnTakeDamage()
    {
        _animator.SetTrigger("IsHurt");
    }

    public void OnDeath()
    {
        _animator.SetBool("IsDead", true);
    }
}
