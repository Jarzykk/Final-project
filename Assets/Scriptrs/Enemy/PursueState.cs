using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(FlipEnemy), typeof(EnemyAnimations))]
public class PursueState : State
{
    [SerializeField] private float _speed;
    [SerializeField] private float _rotationDelay;

    private float _currentRotationCount = 0;
    private float _minXAxisDifferenseToUseMoveAnimation = 0.025f;
    private float _minMoveDistance = 0.001f;
    private bool _targetAtRight;
    private bool _targetWasAtRight;
    private bool _isPlayingMoveAnimation;
    private float _lastPositionX;
    private bool _needToRotate = false;
    private Rigidbody2D _rigidBody;
    private FlipEnemy _enemyFlipper;
    private EnemyAnimations _enemyAnimations;
    
    private RaycastHit2D[] _hitBuffer = new RaycastHit2D[16];
    private List<RaycastHit2D> _hitBufferList = new List<RaycastHit2D>(16);

    private void OnEnable()
    {
        _enemyAnimations = GetComponent<EnemyAnimations>();
        _rigidBody = GetComponent<Rigidbody2D>();
        _enemyFlipper = GetComponent<FlipEnemy>();

        _targetAtRight = transform.position.x < Target.transform.position.x ? true : false;
        _targetWasAtRight = _targetAtRight;

        _lastPositionX = transform.position.x;
        _isPlayingMoveAnimation = false;
    }

    private void OnDisable()
    {
        _enemyAnimations.StopMoving();
    }

    private void FixedUpdate()
    {
        if (_targetWasAtRight != _targetAtRight)
        {
            DoWhenStopedFacingTarget();
        }

        _currentRotationCount -= Time.deltaTime;

        RotateToFaceTargetOnNeed();

        if(_needToRotate == false && _currentRotationCount <= 0)
        {
            if (Mathf.Abs(transform.position.x - _lastPositionX) >= _minXAxisDifferenseToUseMoveAnimation && _isPlayingMoveAnimation == false && _currentRotationCount <= 0)
            {
                StartMoveAnimation();
            }
            else if (Mathf.Abs(transform.position.x - _lastPositionX) < _minXAxisDifferenseToUseMoveAnimation && _isPlayingMoveAnimation == true && _currentRotationCount <= 0)
            {
                StopMoveAnimatiom();
            }

            Movement();
        }

        _lastPositionX = transform.position.x;
        _targetAtRight = transform.position.x < Target.transform.position.x ? true : false;
    }

    private void Movement()
    {
        Vector2 nextStepPosition = Vector2.MoveTowards(transform.position, Target.transform.position, _speed * Time.deltaTime);
        float nextStepDistance = Vector2.Distance(transform.position, nextStepPosition);

        if (nextStepDistance > _minMoveDistance)
        {
            int count = _rigidBody.Cast(nextStepPosition, _hitBuffer, nextStepDistance);

            _hitBufferList.Clear();

            for (int i = 0; i < count; i++)
            {
                if (_hitBuffer[i].transform.GetComponent<Player>() == false)
                {
                    _hitBufferList.Add(_hitBuffer[i]);
                }
            }
        }

        Vector2 movePosition = new Vector2(Target.transform.position.x, _rigidBody.transform.position.y);

        if (_hitBufferList.Count <= 0)
            _rigidBody.position = Vector2.MoveTowards(transform.position, movePosition, _speed * Time.deltaTime);
    }

    private void DoWhenStopedFacingTarget()
    {
        StopMoveAnimatiom();
        _currentRotationCount = _rotationDelay;
        _targetWasAtRight = _targetAtRight;
        _needToRotate = true;
    }

    private void RotateToFaceTargetOnNeed()
    {
        if (_needToRotate && _currentRotationCount <= 0)
        {
            _enemyFlipper.Flip();
            _needToRotate = false;
        }
    }

    private void StartMoveAnimation()
    {
        _isPlayingMoveAnimation = true;
        _enemyAnimations.Move();
    }

    private void StopMoveAnimatiom()
    {
        _isPlayingMoveAnimation = false;
        _enemyAnimations.StopMoving();
    }
}
