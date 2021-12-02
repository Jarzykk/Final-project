using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemyAnimations))]
public class PatrolState : State
{
    [SerializeField] private Transform _firstPoint;
    [SerializeField] private Transform _secondPoint;
    [SerializeField] private float _moveSpeed;

    private Transform _currentTarget;
    private EnemyAnimations _enemyAnimations;

    private void Awake()
    {
        _enemyAnimations = GetComponent<EnemyAnimations>();
    }

    private void OnEnable()
    {
        _currentTarget = _firstPoint;
        _enemyAnimations.Move();
    }

    private void OnDisable()
    {
        _enemyAnimations.StopMoving();
    }

    private void Update()
    {
        if(transform.position.x == _currentTarget.position.x)
        {
            if(_currentTarget == _firstPoint)
            {
                _currentTarget = _secondPoint;
            }
            else
            {
                _currentTarget = _firstPoint;
            }    
        }

        transform.position = Vector2.MoveTowards(transform.position, new Vector2(_currentTarget.position.x, transform.position.y), _moveSpeed * Time.deltaTime);
    }
}
