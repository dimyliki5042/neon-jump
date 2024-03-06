using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D _rigidbody;
    private BoxCollider2D _boxCollider;
    private int _score;
    [SerializeField] private float _jumpBoostForce;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _boxCollider = GetComponent<BoxCollider2D>();
    }

    private void FixedUpdate()
    {
        // ¬ычисление уровн€ сложности -> (очки игрока дел€тс€ без остатка на шаг и прибавл€етс€ текущий уровень игрока)
        int level = (_score / GameManager.Instance.LevelStep) + GameManager.Instance.Level;
        GameManager.Instance.SetLevel(level);
        SetScore();
    }

    private void SetScore()
    {
        int y = (int)transform.position.y;
        if(_score < y)
        {
            _score = y;
            UIController.Instance.DrawScore(_score);
        }
    }

    private void PlayerDie()
    {
        _boxCollider.enabled = false;
        gameObject.SetActive(false);
        SceneManager.LoadScene(0); // ѕерезагрузка сцены пока что
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "DeadZone")
            PlayerDie();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "BoostPlatform":
                _rigidbody.AddForce(Vector2.up * _jumpBoostForce, ForceMode2D.Impulse);
                break;
            case "DestroyPlatform":
                Destroy(collision.gameObject);
                break;
            default: break;
        }
    }
}
