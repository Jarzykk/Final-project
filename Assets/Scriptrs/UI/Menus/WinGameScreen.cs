using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WinGameScreen : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private Chest _winChest;
    [SerializeField] private GameObject _looseScreen;
    [SerializeField] private AudioSource _mainThemeToStopOnWin;
    [SerializeField] private AudioSource _winMusic;
    [SerializeField] private float _delayBeforeOpenWinScreen;
    [SerializeField] private TMP_Text _coinsAmountText;

    private float _elapsedTime = 0;

    private void OnEnable()
    {
        _winChest.ChestIsOpened += OnWinTriggered;
    }

    private void OnDisable()
    {
        _winChest.ChestIsOpened -= OnWinTriggered;
    }

    public void OnWinTriggered()
    {
        _coinsAmountText.text = _player.CoinsAmount.ToString();
        _mainThemeToStopOnWin.Stop();
        _winMusic.Play();
        StartCoroutine(OpenWinGameScreen());
    }

    private IEnumerator OpenWinGameScreen()
    {
        while (_elapsedTime < _delayBeforeOpenWinScreen)
        {
            _elapsedTime += Time.deltaTime;
            yield return null;
        }

        this.enabled = true;
        _looseScreen.SetActive(true);
        Time.timeScale = 0;
    }
}
