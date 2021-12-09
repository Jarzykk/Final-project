using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D), typeof(Rigidbody2D), typeof(Player))]
public class PlayerPhysicBodyEnabler : MonoBehaviour
{
    private Collider2D _collider;
    private Rigidbody2D _rigidbody;
    private Player _player;

    private void OnEnable()
    {
        _player = GetComponent<Player>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _collider = GetComponent<Collider2D>();

        _player.WasReset += OnResetPlayer;
        _player.Died += OnPlayerDeath;
    }

    private void OnDisable()
    {
        _player.WasReset -= OnResetPlayer;
        _player.Died -= OnPlayerDeath;
    }

    private void OnResetPlayer()
    {
        _collider.enabled = true;
        _rigidbody.isKinematic = false;
    }

    private void OnPlayerDeath()
    {
        _collider.enabled = false;
        _rigidbody.isKinematic = true;
    }
}
