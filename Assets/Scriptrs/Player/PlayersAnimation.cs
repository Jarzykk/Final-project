using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator), typeof(Player))]
public class PlayersAnimation : MonoBehaviour
{
    [SerializeField] private PlayersMovement _playrsMovement;
    [SerializeField] private PlayersAttackHandler _playersAttackHandler;

    private Player _player;
    private Animator _animator;

    private bool _isMoving = false;
    private bool _isFacingLeft;
    private bool _grounded;

    private void OnEnable()
    {
        _player = GetComponent<Player>();
        _animator = GetComponent<Animator>();

        _player.TookDamage += OnTakeDamage;
        _playrsMovement.Moved += OnMove;
        _playrsMovement.StopedMoving += OnStopMoving;
        _playrsMovement.Jumped += OnJump;
        _playrsMovement.LostGround += OnLostGround;
        _playrsMovement.ReturnedOnGround += OnLandAfterFall;
        _playersAttackHandler.Attacked += OnAttack;
        _player.Died += OnDeath;

        _grounded = _playrsMovement.Grounded;
    }

    private void OnDisable()
    {
        _player.TookDamage -= OnTakeDamage;
        _playrsMovement.Moved -= OnMove;
        _playrsMovement.StopedMoving -= OnStopMoving;
        _playrsMovement.Jumped -= OnJump;
        _playrsMovement.LostGround -= OnLostGround;
        _playrsMovement.ReturnedOnGround -= OnLandAfterFall;
        _playersAttackHandler.Attacked -= OnAttack;
        _player.Died -= OnDeath;
    }

    private void FixedUpdate()
    {
        if(_isMoving)
        {
            if(_playrsMovement.InputVelocityX > 0 && _isFacingLeft == true)
            {
                _isFacingLeft = false;
                Flip();
            }
            else if(_playrsMovement.InputVelocityX < 0 && _isFacingLeft == false)
            {
                _isFacingLeft = true;
                Flip();
            }
        }

        if(_grounded != _playrsMovement.Grounded)
        {
            ChangeGroundedAnimatorParameter();
            _grounded = _playrsMovement.Grounded;
        }
    }

    private void OnAttack()
    {
        _animator.SetTrigger(AnimatorPlayerController.States.Triggers.IsAttaking);
    }

    private void OnJump()
    {
        _animator.SetTrigger(AnimatorPlayerController.States.Triggers.Jump);
    }

    private void OnMove()
    {
        _animator.SetFloat(AnimatorPlayerController.Params.Speed, _playrsMovement.Speed);
        _isMoving = true;
    }

    private void OnStopMoving()
    {
        _animator.SetFloat(AnimatorPlayerController.Params.Speed, 0);
        _isMoving = false;
    }

    private void OnLostGround()
    {
        _animator.SetTrigger(AnimatorPlayerController.States.Triggers.Fall);
    }

    private void OnLandAfterFall()
    {
        _animator.SetTrigger(AnimatorPlayerController.States.Triggers.LandedAfterFall);
    }

    private void OnTakeDamage()
    {
        _animator.SetTrigger(AnimatorPlayerController.States.Triggers.Hurt);
    }

    private void OnDeath()
    {
        _animator.SetBool(AnimatorPlayerController.States.Bools.IsDead, true);
    }

    private void ChangeGroundedAnimatorParameter()
    {
        _animator.SetBool(AnimatorPlayerController.States.Bools.IsGrounded, _playrsMovement.Grounded);
    }

    private void Flip()
    {
        transform.Rotate(0, 180, 0);
    }
}
