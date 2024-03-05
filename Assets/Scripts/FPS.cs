using UnityEngine;

public class FPS : MonoBehaviour
{
    private void Awake()
    {
        Application.targetFrameRate = (int)Screen.currentResolution.refreshRateRatio.value;
    }
}
