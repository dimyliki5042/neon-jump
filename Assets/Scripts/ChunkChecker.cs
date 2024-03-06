using UnityEngine;

public class ChunkChecker : MonoBehaviour
{
    private void OnTriggerExit2D(Collider2D collision)
    {
        int chunkIndex = int.Parse(collision.name);
        PlatformGenerator.Instance.ResortPlatforms(collision.transform, chunkIndex);
    }
}
