using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CoinsCount : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private TMP_Text _coinsCount;

    private void OnEnable()
    {
        _player.CoinsAmountChanged += ChangeCoinsAmount;
    }

    private void OnDisable()
    {
        _player.CoinsAmountChanged -= ChangeCoinsAmount;
    }

    private void ChangeCoinsAmount(int coinsAmount)
    {
        _coinsCount.text = coinsAmount.ToString();
    }
}
