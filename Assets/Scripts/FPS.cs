using UnityEngine;

public class FPS : MonoBehaviour
{
    [SerializeField] private int _frameRate;

    [ContextMenu("Set FPS")]
    private void SetFPS()
    {
        Application.targetFrameRate = _frameRate;
    }
}
