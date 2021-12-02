using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ControllsMenu : MonoBehaviour
{
    [SerializeField] private GameObject _controllsMenu;

    public void OpenControllsMenu()
    {
        _controllsMenu.SetActive(true);
    }

    public void CloseControllsMenu()
    {
        _controllsMenu.SetActive(false);
    }
}
