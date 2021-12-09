using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

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
    public event UnityAction WasReset;

    private int _currentHealth;
    private int _coinsAmount = 0;
    private bool _isAlive = true;

    private void Start()
    {
        Reset();
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
        Died?.Invoke();
    }

    public void OnTakeCoin()
    {
        _coinsAmount++;
        TookCoin?.Invoke();
        CoinsAmountChanged?.Invoke(_coinsAmount);
    }

    private void Reset()
    {
        _currentHealth = _maxHealth;
        HealthChanged?.Invoke(_currentHealth);
        CoinsAmountChanged?.Invoke(_coinsAmount);
        WasReset?.Invoke();
    }
}
