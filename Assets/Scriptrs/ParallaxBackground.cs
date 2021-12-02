using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class ParallaxBackground : MonoBehaviour
{
    [SerializeField] private Transform _cameraTransform;
    [SerializeField] private Vector2 _parallaxEffectMultiplier;
    [SerializeField] private bool _infinitHorizontalParallax;
    [SerializeField] private bool _infinitVerticalParallax;

    private Vector3 _lastCameraPosition;
    private Sprite _sprite;
    Texture2D _texture;
    private float _textureUnitSizeX;
    private float _textureUnitSizeY;

    private void Start()
    {
        _lastCameraPosition = _cameraTransform.position;
        _sprite = GetComponent<SpriteRenderer>().sprite;
        _texture = _sprite.texture;
        _textureUnitSizeX = _texture.width / _sprite.pixelsPerUnit;
        _textureUnitSizeY = _texture.height / _sprite.pixelsPerUnit;
    }

    private void LateUpdate()
    {
        Vector3 deltaMovement = _cameraTransform.position - _lastCameraPosition;
        transform.position += new Vector3(deltaMovement.x * _parallaxEffectMultiplier.x, deltaMovement.y * _parallaxEffectMultiplier.y, 0);
        _lastCameraPosition = _cameraTransform.position;

        if(_infinitVerticalParallax)
        {
            if (Mathf.Abs(_cameraTransform.position.x - transform.position.x) >= _textureUnitSizeX)
            {
                float offsetPositionX = GetOffsetAxisPosition(_cameraTransform.position.x, transform.position.x, _textureUnitSizeX);
                transform.position = new Vector3(_cameraTransform.position.x + offsetPositionX, transform.position.y);
            }
        }

        if(_infinitVerticalParallax)
        {
            if (Mathf.Abs(_cameraTransform.position.y - transform.position.y) >= _textureUnitSizeY)
            {
                float offsetPositionY = GetOffsetAxisPosition(_cameraTransform.position.y, transform.position.y, _textureUnitSizeY);
                transform.position = new Vector3(transform.position.x, _cameraTransform.position.y + offsetPositionY, transform.position.y);
            }
        }
    }

    private float GetOffsetAxisPosition(float cameraPositionByAxis, float currentPositionByAxis, float textureUnitSizeByAxis)
    {
        return (cameraPositionByAxis - currentPositionByAxis) % textureUnitSizeByAxis;
    }
}
