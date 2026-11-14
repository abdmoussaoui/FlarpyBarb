using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LogicScript : MonoBehaviour
{
    public int playerScore;
    public Text scoreText;
    public GameObject gameOverScreen;
    public bool IsGameOver { get; private set; }

    private GameAudio gameAudio;

    public void PlayFlapSound() => gameAudio.Flap();

    private void Awake()
    {
        gameAudio = GetComponent<GameAudio>();
        if (gameAudio == null) gameAudio = gameObject.AddComponent<GameAudio>();
        playerScore = 0;
        if (scoreText != null) scoreText.text = "0";
        if (gameOverScreen != null) gameOverScreen.SetActive(false);
    }

    [ContextMenu("Increase Score")]
    private void IncreaseScore() => addScore(1);

    // Preserve existing scene button connections.
    public void addScore(int scoreToAdd)
    {
        if (IsGameOver || scoreToAdd <= 0) return;
        playerScore += scoreToAdd;
        gameAudio.Score();
        if (scoreText != null) scoreText.text = playerScore.ToString();
    }

    public void restartGame() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    public void gameOver()
    {
        if (IsGameOver) return;
        IsGameOver = true;
        gameAudio.Crash();
        if (gameOverScreen != null) gameOverScreen.SetActive(true);
    }
}
