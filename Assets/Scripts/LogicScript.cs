using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LogicScript : MonoBehaviour
{
    public int PlayerScore;
    public int Countdown;
    private float highscore;
    public TMP_Text CountdownText;
    public TMP_Text CurrentScoreValue;
    public TMP_Text FinalScoreText;
    public TMP_Text HighScoreValue;

    public float BarrySpeed = 1 / 9F;
    private float timer = 0;
    private float hazardTimer = 0;
    public float HazardInterval = 10;

    public GameObject GameOverScreen;
    public AudioSource DeathAudio;

    public GameObject BarryObject;
    public ObstacleSpawner ObstacleSpawner;
    public GameObject Rocket;
    private bool isGameStart = false;
    private bool isGameOver = false;

    private void Awake()
    {
        StartCoroutine(CountdownStart(Countdown));
        highscore = PlayerPrefs.GetFloat("JoyRide");
        HighScoreValue.text = highscore.ToString();
        isGameStart = false;
        isGameOver = false;
    }

    [ContextMenu("Increase Score")]
    public void AddScore(int scoreToAdd)
    {
        if (GameOverScreen.activeSelf == false)
        {
            PlayerScore = PlayerScore + scoreToAdd;
            CurrentScoreValue.text = PlayerScore.ToString();
        }
    }

    [ContextMenu("Restart Game")]
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    [ContextMenu("Return to Menu")]
    public void ReturnToMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    [ContextMenu("Quit Game")]
    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }

    public void GameOver()
    {
        string finalScoreString;
        if (PlayerScore > highscore)
        {
            finalScoreString = "New High Score! : ";
            PlayerPrefs.SetFloat("JoyRide", PlayerScore);
        }
        else
        {
            finalScoreString = "Score : ";
        }
        GameOverScreen.SetActive(true);

        FinalScoreText.text = finalScoreString + PlayerScore.ToString();
        CurrentScoreValue.text = "";

        isGameOver = true;
        DeathAudio.Play();
    }

    void Update()
    {
        if(!isGameStart || isGameOver)
        {
            return;
        }

        timer += Time.deltaTime;
        hazardTimer += Time.deltaTime;
        if (timer >= BarrySpeed)
        {
            AddScore(1);
            timer = 0;
        }
        if (hazardTimer >= HazardInterval)
        {
            SendMissile();
            hazardTimer = 0;
        }
    }

    IEnumerator CountdownStart(int seconds)
    {
        int counter = seconds;
        while (counter > 0)
        {
            yield return new WaitForSeconds(1);
            counter--;
            CountdownText.text = counter.ToString();
            if (counter == 0)
            {
                CountdownText.text = "";
            }
        }
        StartGame();
    }

    void StartGame()
    {
        isGameStart = true;
        BarryObject.GetComponent<BarryScript>().enabled = true;
        BarryObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        ObstacleSpawner.enabled = true;
    }

    void SendMissile()
    {
        Instantiate(Rocket, Rocket.transform.position, Quaternion.identity);
    }
}
