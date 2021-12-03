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
            StartCoroutine(TryToHit());

            _attackTimer = 0;
        }
    }

    private IEnumerator TryToHit()
    {
        float elapsedTime = 0;

        while(elapsedTime < _dagameDelay)
        {
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(_attackPoint.position, _attackRange);

        foreach (Collider2D enemy in hitEnemies)
        {

            if(enemy.gameObject.TryGetComponent<Enemy>(out Enemy target))
            {
                HitTarget?.Invoke();
                target.TakeDamage(_damage);              
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
