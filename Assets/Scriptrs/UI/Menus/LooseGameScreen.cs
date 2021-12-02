using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LooseGameScreen : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private PlayersControllerHandler _controllerToDisable;
    [SerializeField] private GameObject _looseScreen;
    [SerializeField] private int _sceneNumberToLoad;
    [SerializeField] private AudioSource _mainThemeToStopOnWin;
    [SerializeField] private AudioSource _looseSound;
    [SerializeField] private float _delayBeforeOpenLooseScreen;

    private float _elapsedTime = 0;

    private void OnEnable()
    {
        _player.Died += OnPlayerDeath;
    }

    private void OnDisable()
    {
        _player.Died -= OnPlayerDeath;
    }

    public void OnPlayerDeath()
    {
        _controllerToDisable.DisableController();
        _mainThemeToStopOnWin.Stop();
        _looseSound.Play();
        StartCoroutine(OpenLooseGameScreen());
    }

    private IEnumerator OpenLooseGameScreen()
    {
        while (_elapsedTime < _delayBeforeOpenLooseScreen)
        {
            _elapsedTime += Time.deltaTime;
            yield return null;
        }

        this.enabled = true;
        _looseScreen.SetActive(true);
        Time.timeScale = 0;
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void Restart()
    {
        SceneManager.LoadScene(_sceneNumberToLoad);
        Time.timeScale = 1;
    }
}
