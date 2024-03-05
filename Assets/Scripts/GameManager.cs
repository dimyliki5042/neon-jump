using UnityEngine;

public class GameManager : MonoBehaviour
{
    //public static GameManager Instance { get; private set; }
    [SerializeField] public static Difficulty GameDifficulty {  get; private set; }

    private void OnEnable() => GlobalActions.OnChangeDiff += ChangeDifficulty;

    private void OnDisable() => GlobalActions.OnChangeDiff -= ChangeDifficulty;

    private void Awake()
    {
        GameDifficulty = Difficulty.Easy;
    }

    public void ChangeDifficulty() => GameDifficulty++;
}

public enum Difficulty
{
    Easy = 1,
    Normal,
    Hard
}
