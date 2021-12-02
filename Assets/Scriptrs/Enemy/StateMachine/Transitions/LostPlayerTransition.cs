using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LostPlayerTransition : Transition
{
    [SerializeField] private float _pursueDistance;
    [SerializeField] private float _delayBeforTransit;
    [SerializeField] private State _stateAfterPlayerIsFound;

    private bool _playerIsLost = false;
    private float _transitDelayCount;

    private void Update()
    {
        if (Vector2.Distance(transform.position, Target.transform.position) > _pursueDistance)
        {
            if(_playerIsLost == false)
            {
                _playerIsLost = true;
                _stateAfterPlayerIsFound.enabled = false;
                _transitDelayCount = _delayBeforTransit;
            }            
        }
        else
        {
            _playerIsLost = false;
            _stateAfterPlayerIsFound.enabled = true;
        }

        if (_playerIsLost == true)
            _transitDelayCount -= Time.deltaTime;

        if (_transitDelayCount <= 0 && _playerIsLost)
        {
            _stateAfterPlayerIsFound.enabled = true;
            NeedTransit = true;
        }
    }
}
