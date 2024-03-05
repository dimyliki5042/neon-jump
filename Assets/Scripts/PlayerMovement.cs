using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public static Transform Transform { get; private set; }

    private Vector2 _direction;
    private Rigidbody2D _rigidbody;
    [SerializeField] private float _speed;
    [SerializeField] private float _jumpForce;

    private float _minX; // Левая граница камеры

    private void Awake()
    {
        Transform = this.transform;
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Start() => _minX = CameraController.XBorder;

    private void FixedUpdate()
    {
        //if (Input.acceleration.x > 0.5f || Input.acceleration.x < 0.5f)
            Move();
    }

    // Отладочное движение
    public void OnMove(InputAction.CallbackContext context) => _direction = context.ReadValue<Vector2>() * _speed;

    private void Move()
    {
        //_direction = Input.acceleration * 15f;
        _direction *= (int)GameManager.GameDifficulty;
        _rigidbody.velocity = new Vector2(_direction.x, _rigidbody.velocity.y);
        // Перемещение персонажа с одной стороны экрана на другую
        if(transform.position.x <= _minX || transform.position.x >= Mathf.Abs(_minX))
            transform.position = new Vector2((_minX + 0.1f) * Math.Sign(transform.position.x), transform.position.y);
    }

    private void Jump() => _rigidbody.velocity = Vector2.up * _jumpForce;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (transform.position.y > collision.transform.position.y)
            Jump();
    }
}
