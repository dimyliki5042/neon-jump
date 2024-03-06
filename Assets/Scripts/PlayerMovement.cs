using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public static Transform Transform { get; private set; }

    private Vector2 _direction;
    private Rigidbody2D _rigidbody;
    [SerializeField] private float _speed; // Скорость персонажа (15 для акселерометра)
    [SerializeField] private float _jumpForce; // Сила прыжка персонажа
    [SerializeField, Range(0, 1)] private float _accelerationDeadZone; // Мертвая зона для акселерометра

    private float _minX; // Левая граница камеры

    private void Awake()
    {
        Transform = this.transform;
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void OnEnable() => InputSystem.EnableDevice(Accelerometer.current.device);

    private void OnDisable() => InputSystem.DisableDevice(Accelerometer.current.device);

    private void Start() => _minX = CameraController.XBorder; // Получение левой границы камеры по оси-Х

    private void FixedUpdate()
    {
        _direction = Accelerometer.current.acceleration.ReadValue();
        if (MathF.Abs(_direction.x) > _accelerationDeadZone)
            Move();
    }

    // Отладочное движение
    //public void OnMove(InputAction.CallbackContext context) => 
    //    _direction = context.ReadValue<Vector2>() * (_speed + GameManager.Instance.Level);

    private void Move()
    {
        _direction *= 5f * (_speed + GameManager.Instance.Level);
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
