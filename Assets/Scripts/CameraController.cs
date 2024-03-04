using UnityEngine;

public class CameraController : MonoBehaviour
{
    private void LateUpdate()
    {
        float playerPosY = PlayerMovement.Transform.position.y;
        if (playerPosY > transform.position.y)
            transform.position = new Vector3(transform.position.x, playerPosY, -10);
    }
}
