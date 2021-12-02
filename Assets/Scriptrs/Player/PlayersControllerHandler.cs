using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayersControllerHandler : MonoBehaviour
{
    [SerializeField] private PlayersAttackHandler _attackHandler;
    [SerializeField] private PlayersMovement _movementHandler;

    private PlayersInput _playerInput;

    private void Awake()
    {
        _playerInput = new PlayersInput();
    }

    private void OnEnable()
    {
        _playerInput.Enable();
        _playerInput.Player.Attack.performed += ctx => _attackHandler.OnAttack();
        _playerInput.Player.Move.performed += ctx => _movementHandler.OnMove(_playerInput.Player.Move.ReadValue<float>());
        _playerInput.Player.Jump.performed += ctx => _movementHandler.OnJump();
        _playerInput.Player.Move.canceled += ctx => _movementHandler.OnStopMoving();
    }

    private void OnDisable()
    {
        DisableController();
    }

    public void EnableController()
    {
        _playerInput.Enable();
    }

    public void DisableController()
    {
        _playerInput.Disable();
    }
}
