using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyStateMachine : MonoBehaviour
{
    [SerializeField] private State _firstState;

    private Player _target;
    private State _current;
    private Enemy _enemy;

    public State CurrentState => _current;

    private void OnEnable()
    {
        _enemy = GetComponent<Enemy>();
        _enemy.Dying += OnEnemyDying;
        _target = GetComponent<Enemy>().Target;
        Reset(_firstState);
    }

    private void OnDisable()
    {
        _enemy.Dying -= OnEnemyDying;
    }

    private void Update()
    {
        if (_current == null)
            return;

        var nextState = _current.GetNextState();
        if (nextState != null)
            Transit(nextState);
    }

    public void OnEnemyDying()
    {
        DisableStateMachine();
    }

    private void Reset(State starttState)
    {
        _current = starttState;
        _current?.Enter(_target);
    }

    private void DisableStateMachine()
    {
        _current.Exit();
        this.enabled = false;
    }

    private void Transit(State nextState)
    {
        _current?.Exit();
        _current = nextState;        
        _current?.Enter(_target);
    }
}
