using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Camera _thisCam;
    public static float XBorder;  // Левая граница камеры
    public static float YBorder;  // Левая граница камеры

    private void Awake()
    {
        _thisCam = GetComponent<Camera>();
        CalculateXBorder();
    }

    private void FixedUpdate()
    {
        float playerPosY = PlayerMovement.Transform.position.y;
        if (playerPosY > transform.position.y)
            transform.position = new Vector3(transform.position.x, playerPosY, -10);
    }

    private void CalculateXBorder()
    {
        Vector3 camRect = _thisCam.ViewportToWorldPoint(new Vector3(0, 0, _thisCam.nearClipPlane));
        XBorder = camRect.x;
        YBorder = camRect.y;
    }
}
