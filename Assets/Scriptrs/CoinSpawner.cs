using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    [SerializeField] private Coin _coinTemplate;
    [SerializeField] private int _cointAmountToSpawn;
    [SerializeField] private float _distanceBetweenCoins;
    [SerializeField] private Enemy _enemy;

    private void OnEnable()
    {
        _enemy.Dying += SpawnCoins;
    }

    private void OnDisable()
    {
        _enemy.Dying -= SpawnCoins;
    }

    public void SpawnCoins()
    {
        Vector3 spawnCoinPosition = transform.position;

        for (int i = 0; i < _cointAmountToSpawn; i++)
        {
            Instantiate(_coinTemplate, spawnCoinPosition, Quaternion.identity);
            spawnCoinPosition.x += _distanceBetweenCoins;
        }
    }
}
