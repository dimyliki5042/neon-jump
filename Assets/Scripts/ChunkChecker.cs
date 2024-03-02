using System;
using UnityEngine;

public class ChunkChecker : MonoBehaviour
{
    public static Action<int> OnOffChunk;

    private void OnTriggerExit2D(Collider2D collision)
    {
        int a = int.Parse(collision.name);
        OnOffChunk.Invoke(a);
    }
}
