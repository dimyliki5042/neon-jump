using TMPro;
using UnityEngine;

public class UIController : MonoBehaviour
{
    private TextMeshProUGUI _scoreText;

    private void OnEnable()
    {
        GlobalActions.UpdatePlayerScore += DrawScore;
        GlobalActions.OnChangeDiff += ChangeTextColor;
    }

    private void OnDisable()
    {
        GlobalActions.UpdatePlayerScore -= DrawScore;
        GlobalActions.OnChangeDiff -= ChangeTextColor;
    }

    private void Awake()
    {
        _scoreText = transform.GetChild(0).GetComponent<TextMeshProUGUI>();
    }

    private void ChangeTextColor()
    {
        switch (GameManager.GameDifficulty)
        {
            case Difficulty.Normal:
                _scoreText.color = Color.yellow;
                break;
            case Difficulty.Hard:
                _scoreText.color = Color.red;
                break;
            default:
                break;
        }
    }

    private void DrawScore(int score) => _scoreText.text = $"Score: {score}";
}
