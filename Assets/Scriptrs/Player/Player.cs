using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider2D), typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{
    [SerializeField] private int _maxHealth;

    public int CurrentHealth => _currentHealth;
    public bool IsAlive => _isAlive;
    public int CoinsAmount => _coinsAmount;

    public event UnityAction<int> HealthChanged;
    public event UnityAction<int> CoinsAmountChanged;
    public event UnityAction TookCoin;
    public event UnityAction TookDamage;
    public event UnityAction Died;

    private int _currentHealth;
    private int _coinsAmount = 0;
    private bool _isAlive = true;
    private Collider2D _collider;
    private Rigidbody2D _rigidbody;

    private void Start()
    {
        _collider = GetComponent<Collider2D>();
        _rigidbody = GetComponent<Rigidbody2D>();

        ResetPlayer();
    }

    public void ApplyDamage(int damage)
    {
        TookDamage?.Invoke();

        if(damage > 0)
        {
            _currentHealth -= damage;
            HealthChanged?.Invoke(_currentHealth);

            if(_currentHealth <= 0)
            {
                Die();
            }
        }
    }

    private void Die()
    {
        _isAlive = false;
        _collider.enabled = false;
        _rigidbody.isKinematic = true;
        Died?.Invoke();
    }

    public void TakeCoin()
    {
        _coinsAmount++;
        TookCoin?.Invoke();
        CoinsAmountChanged?.Invoke(_coinsAmount);
    }

    private void ResetPlayer()
    {
        _currentHealth = _maxHealth;
        HealthChanged?.Invoke(_currentHealth);
        CoinsAmountChanged?.Invoke(_coinsAmount);
    }
}
