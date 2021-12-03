using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy), typeof(EnemyAnimations))]
public class AttackState : State
{
    [SerializeField] private int _damage;
    [SerializeField] private float _delayBeforeFirstAttack;
    [SerializeField] private float _delayBetweenAttacks;
    [SerializeField] private float _dagameDelay;
    [SerializeField] private float _attackRange;
    [SerializeField] private Transform _attackPoint;
    [SerializeField] private AudioSource _attackSound;

    public float AttackRange => _attackRange;

    private float _attackDelayCount;
    private Enemy _enemy;
    private EnemyAnimations _enemyAnimations;


    private void OnEnable()
    {
        _enemy = GetComponent<Enemy>();
        _enemyAnimations = GetComponent<EnemyAnimations>();
        _attackDelayCount = _delayBeforeFirstAttack;

        _enemy.TakingDamage += OnTakeDamage;
    }

    private void OnDisable()
    {
        _enemy.TakingDamage += OnTakeDamage;
    }

    private void Update()
    {
        if(_attackDelayCount <= 0)
        {
            Attack(Target);
            _attackDelayCount = _delayBetweenAttacks; ;
        }

        _attackDelayCount -= Time.deltaTime;
    }

    public void OnTakeDamage()
    {
        _attackDelayCount = _delayBetweenAttacks; ;
    }

    private void Attack(Player target)
    {
        _attackSound.Play();
        _enemyAnimations.OnAttack();
        Invoke("TryToHit", _dagameDelay);
    }

    private void TryToHit()
    {
        Collider2D[] hitTargets = Physics2D.OverlapCircleAll(_attackPoint.position, _attackRange);

        foreach (Collider2D target in hitTargets)
        {
            if (target.TryGetComponent<Player>(out Player player))
                player.ApplyDamage(_damage);
        }
    }
}
