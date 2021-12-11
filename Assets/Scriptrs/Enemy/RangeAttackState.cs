using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator), typeof(Enemy), typeof(EnemyFliper))]
public class RangeAttackState : State
{
    [SerializeField] private Fireball _fireballTemplate;
    [SerializeField] private float _attackRange;
    [SerializeField] private float _delayBetweenAttacks;
    [SerializeField] private float _rotationDelay;
    [SerializeField] private AudioSource _fireballCastSound;

    private float _currentRotationCount = 0;
    private bool _needToRotate = false;
    private bool _facingTarget;

    private float _attackTimeCount;
    private Animator _animator;
    private Transform _attackPoint;
    private EnemyFliper _enemyFlipper;

    private void OnEnable()
    {
        _attackTimeCount = 0;
        _animator = GetComponent<Animator>();
        _attackPoint = GetComponent<Enemy>().AttackPoint;
        _enemyFlipper = GetComponent<EnemyFliper>();

        _facingTarget = CheckIfFacingTarget();

        if (_facingTarget == false)
            _enemyFlipper.Flip();
    }

    private void Update()
    {
        _facingTarget = CheckIfFacingTarget();

        if(_facingTarget == false && _needToRotate == false)
        {
            _currentRotationCount = _rotationDelay;
            _needToRotate = true;
        }

        _currentRotationCount -= Time.deltaTime;

        if (_currentRotationCount > 0)
        {
            return;
        }

        if (_needToRotate && _currentRotationCount <= 0)
        {
            _enemyFlipper.Flip();
            _needToRotate = false;
        }

        if(Vector2.Distance(transform.position, Target.transform.position) <= _attackRange)
        {
            if(_attackTimeCount <= 0 && _facingTarget == true)
            {
                _fireballCastSound.Play();
                Fireball fireball = Instantiate(_fireballTemplate, _attackPoint.position, Quaternion.identity);
                fireball.SetTargetDirection(Target.transform.position);

                _animator.SetTrigger("IsAttaking");
                _attackTimeCount = _delayBetweenAttacks;
            }
        }

        _attackTimeCount -= Time.deltaTime;
    }

    private bool CheckIfFacingTarget()
    {
        return Vector2.Distance(transform.position, Target.transform.position) > Vector2.Distance(_attackPoint.position, Target.transform.position) ? true : false;
    }
}
