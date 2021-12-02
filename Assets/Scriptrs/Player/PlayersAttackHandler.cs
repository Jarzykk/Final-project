using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayersAttackHandler : MonoBehaviour
{
    [SerializeField] private int _damage;
    [SerializeField] private float _delayBetweenAttacks;
    [SerializeField] private float _dagameDelay;
    [SerializeField] private float _attackRange;
    [SerializeField] private Transform _attackPoint;
    [SerializeField] private LayerMask _enemyLayers;
    [SerializeField] private AudioSource _attackSound;
    [SerializeField] private float _minPitchAttackSoundLevel;
    [SerializeField] private float _maxPitchAttackSoundLevel;

    public event UnityAction Attacked;
    public event UnityAction HitTarget;
    private float _attackTimer;

    private void Start()
    {
        _attackTimer = _delayBetweenAttacks;
    }

    private void Update()
    {
        _attackTimer += Time.deltaTime;
    }

    public void OnAttack()
    {
        if (_attackTimer >= _delayBetweenAttacks)
        {
            Attacked?.Invoke();
            Invoke("TryToHit", _dagameDelay);

            _attackTimer = 0;
        }
    }

    private void TryToHit()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(_attackPoint.position, _attackRange);

        foreach (Collider2D enemy in hitEnemies)
        {

            if(enemy.GetComponent<Enemy>())
            {
                HitTarget?.Invoke();
                enemy.GetComponent<Enemy>().TakeDamage(_damage);              
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (_attackPoint == null)
            return;

        Gizmos.DrawWireSphere(_attackPoint.position, _attackRange);
    }
}
