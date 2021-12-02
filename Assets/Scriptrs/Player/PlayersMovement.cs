using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody2D), typeof(Player))]
public class PlayersMovement : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _gravityModifier = 1f;
    [SerializeField] private float _jumpForce = 5f;
    [SerializeField] private float _minGroundNormalY = .65f;
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private float _shellRadius = 0.05f;

    public event UnityAction Moved;
    public event UnityAction StopedMoving;
    public event UnityAction Jumped;
    public event UnityAction LostGround;
    public event UnityAction ReturnedOnGround;

    public bool Grounded => _grounded;
    public float Speed => _speed;
    public float InputVelocityX => _targetVelocity.x;

    private Player _player;
    private Rigidbody2D _rigidBody;
    private Vector2 _targetVelocity;
    private Vector2 _groundNormal;
    private Vector2 _velocity;
    private ContactFilter2D _contactFilter;
    private RaycastHit2D[] _hitBuffer = new RaycastHit2D[16];
    private List<RaycastHit2D> _hitBufferList = new List<RaycastHit2D>(16);
    private bool _grounded;
    private bool _wasGrounded;
    private float _minMoveDistance = 0.001f;

    private void OnEnable()
    {
        _player = GetComponent<Player>();

        _player.Died += OnPlayerDeath;
    }

    private void OnDisable()
    {
        _player.Died -= OnPlayerDeath;
    }

    private void Start()
    {
        _rigidBody = GetComponent<Rigidbody2D>();

        _contactFilter.useTriggers = false;
        _contactFilter.SetLayerMask(_layerMask);
        _contactFilter.useLayerMask = true;
    }

    private void FixedUpdate()
    {
        _velocity += _gravityModifier * Physics2D.gravity * Time.deltaTime;
        _velocity.x = _targetVelocity.x;

        _grounded = false;

        Vector2 deltaPosition = _velocity * Time.deltaTime;
        Vector2 moveAlongGround = new Vector2(_groundNormal.y, -_groundNormal.x);
        Vector2 move = moveAlongGround * deltaPosition.x * _speed;

        Movement(move, false);

        move = Vector2.up * deltaPosition.y;

        Movement(move, true);

        if(_wasGrounded == true)
        {
            if(_grounded == false)
            {
                LostGround?.Invoke();
                _wasGrounded = false;
            }
        }

        if(_grounded == true)
        {
            if(_wasGrounded == false)
            {
                ReturnedOnGround?.Invoke();
            }

            _wasGrounded = true;
        }
    }

    public void OnPlayerDeath()
    {
        this.enabled = false;
    }

    public void OnMove(float velocityX)
    {
        Moved?.Invoke();
        _targetVelocity = new Vector2(velocityX, 0);
    }

    public void OnStopMoving()
    {
        StopedMoving?.Invoke();
    }

    public void OnJump()
    {
        if(_grounded)
        {
            _velocity.y = _jumpForce;
            Jumped?.Invoke();
        }
    }

    public void Knockback(float knockbackForce)
    {
        _velocity.y += knockbackForce;
    }

    private void Movement(Vector2 move, bool yMovement)
    {
        float distance = move.magnitude;

        if (distance > _minMoveDistance)
        {
            int count = _rigidBody.Cast(move, _contactFilter, _hitBuffer, distance + _shellRadius);

            _hitBufferList.Clear();

            for (int i = 0; i < count; i++)
            {
                _hitBufferList.Add(_hitBuffer[i]);
            }

            for (int i = 0; i < _hitBufferList.Count; i++)
            {
                Vector2 currentNormal = _hitBufferList[i].normal;

                if (currentNormal.y > _minGroundNormalY)
                {
                    _grounded = true;

                    if (yMovement)
                    {
                        _groundNormal = currentNormal;                        
                        currentNormal.x = 0;
                    }
                }

                float projection = Vector2.Dot(_velocity, currentNormal);
                if (projection < 0)
                {
                    _velocity = _velocity - projection * currentNormal;
                }

                float modifiedDistance = _hitBufferList[i].distance - _shellRadius;
                distance = modifiedDistance < distance ? modifiedDistance : distance;
            }
        }

        _rigidBody.position = _rigidBody.position + move.normalized * distance;
    }
}
