using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public List<GameObject> targets;
    public float spawnRate = 1;
    public bool isGameActive;

    private int score = 0;
    private int lives = 0;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI livesText;
    public TextMeshProUGUI gameoverText;
    public Button RestartButton;
    public GameObject titleScreen;

    public GameObject pauseScreen;
    private int paused = 0;

    // Start is called before the first frame update
    void Start()
    {

    }

    public void StartGame(int difficulty)
    {
        spawnRate /= difficulty;
        titleScreen.gameObject.SetActive(false);
        isGameActive = true;
        StartCoroutine(SpawnTarget());
        UpdateScore(0);
        UpdateLives(3);
    }

    IEnumerator SpawnTarget()
    {
        while (isGameActive)
        {
            yield return new WaitForSeconds(spawnRate);
            int randomIndex = Random.Range(0, targets.Count);
            Instantiate(targets[randomIndex]);
        }
    }

    public void UpdateScore(int value)
    {
        score += value;
        scoreText.text = "Score: " + score;
    }

    void ChangePause()
    {
        if(paused == 0)
        {
            Time.timeScale = 0;
            pauseScreen.gameObject.SetActive(true);
        }
        else
        {
            Time.timeScale = 1;
            pauseScreen.gameObject.SetActive(false);
        }
        paused = 1 - paused;
    }

    public void UpdateLives(int value)
    {
        lives += value;
        livesText.text = "Lives: " + Mathf.Max(lives, 0);
        if (lives <= 0)
            GameOver();
    }

    public void GameOver()
    {
        isGameActive = false;
        gameoverText.gameObject.SetActive(true);
        RestartButton.gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            ChangePause();
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
