using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Fireball : MonoBehaviour
{
    [SerializeField] private int _damage;
    [SerializeField] private float _speed;
    [SerializeField] private float _lifeTime;
    
    private Vector2 _targetDirection;
    private Rigidbody2D _rigidBody;
    private float _lifeTimeCount;
    private bool _startedMoving = false;

    private void Start()
    {
        _rigidBody = GetComponent<Rigidbody2D>();

        _lifeTimeCount = 0;
    }

    private void Update()
    {
        if (_lifeTimeCount >= _lifeTime)
            Destroy(gameObject);

        if (_startedMoving == false)
        {
            _rigidBody.velocity = new Vector2(_targetDirection.x, _targetDirection.y);
            _startedMoving = true;
        }            

        _lifeTimeCount += Time.deltaTime;
    }

    public void SetTargetDirection(Vector2 targetPosition)
    {
        _targetDirection = (targetPosition - new Vector2(transform.position.x, transform.position.y)).normalized * _speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Player>())
            collision.gameObject.GetComponent<Player>().ApplyDamage(_damage);

        if (collision.gameObject.GetComponent<Enemy>())
            collision.gameObject.GetComponent<Enemy>().TakeDamage(_damage);

        Destroy(gameObject);
    }
}
