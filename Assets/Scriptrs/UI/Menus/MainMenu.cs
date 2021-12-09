using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private int _sceneNumberToLoad;

    public void LoadSceneByNumber()
    {
        SceneManager.LoadScene(_sceneNumberToLoad);
    }

    public void ExitApplication()
    {
        Application.Quit();
    }
}
