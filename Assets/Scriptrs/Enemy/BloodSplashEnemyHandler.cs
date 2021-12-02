using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class BloodSplashEnemyHandler : MonoBehaviour
{
    [SerializeField] private ParticleSystem _bloodSplash;

    private Enemy _enemy;

    private void OnEnable()
    {
        _enemy = GetComponent<Enemy>();

        _enemy.TakingDamage += OnTakeDamage;
    }

    private void OnDisable()
    {
        _enemy.TakingDamage -= OnTakeDamage;
    }

    private void OnTakeDamage()
    {
        _bloodSplash.Play();
    }
}
