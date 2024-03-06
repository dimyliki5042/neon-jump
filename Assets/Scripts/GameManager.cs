using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [field:SerializeField]
    public int Level { get; private set; } // Уровень сложности
    public int LevelStep { get; private set; } // Удвоенное число для вычисления уровня

    private void Awake()
    {
        Instance = this;
        LevelStep = 150;
        Debug.Log(Application.platform);
    }

    public void SetLevel(int newLevel)
    {
        if (newLevel != Level)
        {
            LevelStep += LevelStep;
            Level = newLevel;
            UIController.Instance.ChangeTextColor();
        }
    }
}
