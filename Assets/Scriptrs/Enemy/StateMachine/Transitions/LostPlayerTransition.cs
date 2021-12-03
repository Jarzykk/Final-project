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
        CheckIfTargetIsLost();

        if (_playerIsLost == true)
            _transitDelayCount -= Time.deltaTime;

        CheckNeedOfTransitionForTargetState();
    }

    private void CheckNeedOfTransitionForTargetState()
    {
        if (_transitDelayCount <= 0 && _playerIsLost)
        {
            _playerIsFound.enabled = true;
            NeedTransit = true;
        }
    }

    private void CheckIfTargetIsLost()
    {
        if (Vector2.Distance(transform.position, Target.transform.position) > _pursueDistance)
        {
            if (_playerIsLost == false)
            {
                _playerIsLost = true;
                _playerIsFound.enabled = false;
                _transitDelayCount = _delayBeforTransit;
            }
        }
        else
        {
            _playerIsLost = false;
            _playerIsFound.enabled = true;
        }
    }
}
