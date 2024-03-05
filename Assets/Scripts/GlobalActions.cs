using System;
using UnityEngine;

public class GlobalActions : MonoBehaviour
{
    public static Action<int> UpdatePlayerScore;

    public static Action<Transform, int> OnOffChunk;

    public static Action OnChangeDiff;
}
