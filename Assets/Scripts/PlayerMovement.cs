using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public static Transform Transform { get; private set; }

    private Vector2 _direction;
    private Rigidbody2D _rigidbody;
    [SerializeField] private float _speed;
    [SerializeField] private float _jumpFroce;

    private void Awake()
    {
        Transform = this.transform;
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        Move();
    }

    public void OnMove(InputAction.CallbackContext context) => _direction = context.ReadValue<Vector2>() * _speed;

    private void Move()
    {
        _rigidbody.velocity = new Vector2(_direction.x, _rigidbody.velocity.y);
        if(transform.position.x <= -2.5f || transform.position.x >= 2.5f)
            transform.position = new Vector2(-2.4f * Math.Sign(transform.position.x), transform.position.y);
    }

    private void Jump() => _rigidbody.velocity = Vector2.up * _jumpFroce;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (transform.position.y > collision.transform.position.y)
        {
            Jump();
        }
    }
}
