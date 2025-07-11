using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;
    public static int score;
    public static int HighScore;
    public TextMeshProUGUI text;
    public TextMeshProUGUI textHighScore;

    private void Awake()
    {
        instance = this;
        score = 0;
        LoadGameState();
    }

    public void SaveGameState()
    {
        PlayerPrefs.SetInt("HighScore", HighScore);
    }

    public void LoadGameState()
    {
        if (PlayerPrefs.HasKey("HighScore"))
        {
            HighScore = PlayerPrefs.GetInt("HighScore");
            textHighScore.text = "High Score: " + HighScore;
        }
        else
        {
            HighScore = 0;
            PlayerPrefs.SetInt("HighScore", HighScore);
        }
    }

    public void ShowScore()
    {
        text.text = "score: " + score;
        if (score > HighScore)
        {
            HighScore = score;
            textHighScore.text = "High Score: " + HighScore;
            SaveGameState();
        }
    }

    void Update()
    {
      

        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            SaveGameState();
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            LoadGameState();
        }
    }
}
