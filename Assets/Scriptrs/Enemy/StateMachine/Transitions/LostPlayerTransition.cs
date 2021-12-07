using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LostPlayerTransition : Transition
{
    [SerializeField] private float _pursueDistance;
    [SerializeField] private float _delayBeforTransit;
    [SerializeField] private State _playerIsFound;

    private bool _playerIsLost = false;
    private float _transitDelayCount;

    private void Update()
    {
        if (Vector2.Distance(transform.position, Target.transform.position) > _pursueDistance)
            OnTargetLost();
        else
            OnTargetFound();

        if (_playerIsLost == true)
            _transitDelayCount -= Time.deltaTime;

        if(_transitDelayCount <= 0 && _playerIsLost)
            OnTransitToTargetState();
    }

    private void OnTransitToTargetState()
    {
        _playerIsFound.enabled = NeedTransit = true;
    }

    private void OnTargetLost()
    {
        if (_playerIsLost == false)
        {
            _playerIsLost = true;
            _playerIsFound.enabled = false;
            _transitDelayCount = _delayBeforTransit;
        }
    }

    private void OnTargetFound()
    {
        _playerIsLost = false;
        _playerIsFound.enabled = true;
    }
}
