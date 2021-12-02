using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(SpriteRenderer))]
public class Chest : MonoBehaviour
{
    [SerializeField] private Sprite _openChestSprite;

    public event UnityAction ChestIsOpened;

    private SpriteRenderer _spriteRenderer;
    private bool _isOpened = false;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<Player>())
        {
            if(_isOpened == false)
            {
                _isOpened = true;
                _spriteRenderer.sprite = _openChestSprite;
                ChestIsOpened?.Invoke();
            }            
        }
    }
}
