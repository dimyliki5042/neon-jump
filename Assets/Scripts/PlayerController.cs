using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //private Rigidbody2D _rigidbody;
    private BoxCollider2D _boxCollider;
    private int _score;

    private void Awake()
    {
        //_rigidbody = GetComponent<Rigidbody2D>();
        _boxCollider = GetComponent<BoxCollider2D>();
    }

    private void LateUpdate()
    {
        SetScore();
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

    private void PlayerDie()
    {
        Debug.Log("Player died");
        _boxCollider.enabled = false;
        this.gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "DeadZone")
            PlayerDie();
    }
}
