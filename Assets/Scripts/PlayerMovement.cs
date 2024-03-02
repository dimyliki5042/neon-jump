using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private Vector2 _direction;
    public static Transform Transform { get; private set; }
    private Rigidbody2D _rigidbody;
    [SerializeField] private float _speed;
    [SerializeField] private float _jumpFroce;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        Transform = transform;
    }

    private void Update() => Move();

    private void FixedUpdate() => Transform = transform;

    public void OnMove(InputAction.CallbackContext context) => _direction = context.ReadValue<Vector2>() * _speed;

    private void Move() => _rigidbody.velocity = new Vector2(_direction.x, _rigidbody.velocity.y);

    private void Jump() => _rigidbody.AddForce(Vector2.up * _jumpFroce, ForceMode2D.Impulse);

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.position.y < transform.position.y) // Если платформа ниже игрока, то он прыгает
            Jump();
    }
}
