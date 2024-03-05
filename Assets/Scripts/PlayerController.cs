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
        SetScore();
        CheckScore();
    }

    private void SetScore()
    {
        int y = (int)transform.position.y;
        if(_score < y)
        {
            _score = y;
            GlobalActions.UpdatePlayerScore.Invoke(_score);
        }
    }

    private void CheckScore()
    {
        //if (_score == 150 || _score == 300 || _score == 500)
        //    GlobalActions.OnChangeDiff.Invoke();
    }

    private void PlayerDie()
    {
        Debug.Log("Player died");
        _boxCollider.enabled = false;
        this.gameObject.SetActive(false);
        SceneManager.LoadScene(0); // Перезагрузка сцены пока что
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
