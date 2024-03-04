using System;
using UnityEngine;

public class ChunkChecker : MonoBehaviour
{
    private void OnTriggerExit2D(Collider2D collision)
    {
        int a = int.Parse(collision.name);
        GlobalActions.OnOffChunk.Invoke(a);
    }
}
