using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyFliper : MonoBehaviour
{
    private bool _isFacingRight;
    private float _previousPositionX;
    private Transform _attackPoint;

    private void Awake()
    {
        _attackPoint = GetComponent<Enemy>().AttackPoint;
    }

    private void Start()
    {
        _previousPositionX = transform.position.x;

        _isFacingRight = CheckIfFacingRight();
    }

    private void Update()
    {
        if (_previousPositionX != transform.position.x)
        {
            if (_previousPositionX > transform.position.x)
            {
                if (_isFacingRight == true)
                {
                    Flip();
                }
            }

            if (_previousPositionX < transform.position.x)
            {
                if (_isFacingRight == false)
                {
                    Flip();
                }
            }
        }

        _previousPositionX = transform.position.x;
    }

    public void Flip()
    {
        transform.Rotate(0, 180, 0);
        _isFacingRight = !_isFacingRight;
    }

    private bool CheckIfFacingRight()
    {
        float attackPointXPosition = _attackPoint.position.x;
        float currentXPosition = transform.position.x;

        return currentXPosition < attackPointXPosition ? true : false;
    }
}
