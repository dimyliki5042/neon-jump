using UnityEngine;

public class CameraController : MonoBehaviour
{
    private float _difference;
    [SerializeField] private float _speed;

    private void FixedUpdate()
    {
        MoveUpper();
        //if (DifferenceWithPlayer(out float difference) >= 3f)
        //{
        //    _difference = difference;
        //}
    }

    private void MoveUpper()
    {
        transform.position = Vector3.Lerp(transform.position, transform.position + Vector3.up, Time.fixedDeltaTime * _speed);
    }

    private float DifferenceWithPlayer(out float a) => a = (PlayerMovement.Transform.position - transform.position).y;
}
