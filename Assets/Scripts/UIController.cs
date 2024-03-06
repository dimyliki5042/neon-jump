using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class UIController : MonoBehaviour
{
    public static UIController Instance {  get; private set; }
    private TextMeshProUGUI _scoreText;

    private void Awake()
    {
        Instance = this;
        _scoreText = transform.GetChild(0).GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        if (Keyboard.current.escapeKey.isPressed)
            OnMenu();

        //if (Input.GetKey(KeyCode.Home))
        //{
        //    _scoreText.color = Color.gray;
        //    OnMenu();
        //}
        //else if (Input.GetKey(KeyCode.Escape)) // Кнопка назад 
        //{
        //    _scoreText.color = Color.magenta;
        //    OnMenu();
        //}
        //else if (Input.GetKey(KeyCode.Menu))
        //{
        //    _scoreText.color = Color.cyan;
        //    OnMenu();
        //}
    }

    public void ChangeTextColor()
    {
        Color color = _scoreText.color;
        if (color.r != 1)
            color += new Color(0.5f, 0, 0, 0);
        else if(color.g != 0)
            color -= new Color(0, 0.5f, 0, 0);
        _scoreText.color = color;
    }

    public void DrawScore(int score) => _scoreText.text = $"Score: {score}";

    public void OnMenu() => Application.Quit();
}
