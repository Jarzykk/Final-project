using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Player), typeof(PlayersAttackHandler))]
public class PlayerSoundEffectsHandler : MonoBehaviour
{
    [SerializeField] private AudioSource _attackSound;
    [SerializeField] private float _minPitchAttackSoundLevel;
    [SerializeField] private float _maxPitchAttackSoundLevel;

    [SerializeField] private AudioSource _hitSound;
    [SerializeField] private float _minPitchHitSoundLevel;
    [SerializeField] private float _maxPitchHitSoundLevel;

    [SerializeField] private AudioSource _hurtSound;
    [SerializeField] private float _minPitchHurtSoundLevel;
    [SerializeField] private float _maxPitchHurtSoundLevel;

    [SerializeField] private AudioSource _takeCoinSound;
    [SerializeField] private float _minPitchtakeCoinSoundLevel;
    [SerializeField] private float _maxPitchtakeCoinSoundLevel;

    private Player _player;
    private PlayersAttackHandler _playersAttackHandler;

    private void OnEnable()
    {
        _player = GetComponent<Player>();
        _playersAttackHandler = GetComponent<PlayersAttackHandler>();

        _player.TookDamage += OnTakeDamage;
        _player.TookCoin += OnTakeCoin;
        _playersAttackHandler.Attacked += OnAttack;
        _playersAttackHandler.HitTarget += OnTargetHit;
    }

    private void OnDisable()
    {
        _player.TookDamage -= OnTakeDamage;
        _playersAttackHandler.Attacked -= OnAttack;
        _player.TookCoin -= OnTakeCoin;
        _playersAttackHandler.HitTarget -= OnTargetHit;
    }

    private void OnAttack()
    {
        _attackSound.pitch = Random.Range(_minPitchAttackSoundLevel, _maxPitchAttackSoundLevel);
        _attackSound.Play();
    }

    private void OnTargetHit()
    {
        _hitSound.pitch = Random.Range(_minPitchHitSoundLevel, _maxPitchHitSoundLevel);
        _hitSound.Play();
    }

    private void OnTakeDamage()
    {
        _hurtSound.pitch = Random.Range(_minPitchHurtSoundLevel, _maxPitchHurtSoundLevel);
        _hurtSound.Play();
    }

    private void OnTakeCoin()
    {
        _takeCoinSound.pitch = Random.Range(_minPitchtakeCoinSoundLevel, _maxPitchtakeCoinSoundLevel);
        _takeCoinSound.Play();
    }
}
