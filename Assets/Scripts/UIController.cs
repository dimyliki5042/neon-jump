using TMPro;
using UnityEngine;

public class UIController : MonoBehaviour
{
    private TextMeshProUGUI _scoreText;

    private void OnEnable()
    {
        GlobalActions.UpdatePlayerScore += DrawScore;
    }

    private void OnDisable()
    {
        GlobalActions.UpdatePlayerScore += DrawScore;
    }

    private void Awake()
    {
        _scoreText = transform.GetChild(0).GetComponent<TextMeshProUGUI>();
    }

    private void DrawScore(int score) => _scoreText.text = $"Score: {score}";
}
