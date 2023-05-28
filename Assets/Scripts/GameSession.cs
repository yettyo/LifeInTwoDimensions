using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSession : MonoBehaviour
{
    [SerializeField] int playerLives = 3;
    [SerializeField] int score = 0;
    [SerializeField] TextMeshProUGUI livesText;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] string gameOverSceneName = "GameOverScene";
    void Awake()
    {
        int numGameSessions = FindObjectsOfType<GameSession>().Length;
        if (numGameSessions > 1) {
            Destroy(gameObject);
        } else {
            DontDestroyOnLoad(gameObject);
        }
    }

    public void ProcessPlayerDeath() {
        if(playerLives > 1) {
            TakeLife();
        } else {
            ResetGameSession();
        }
    }

    void Start() {
        livesText.text = playerLives.ToString();
        scoreText.text = score.ToString();
    }
    void ResetGameSession()
    {   
        ScenePersist.ResetScenePersist(); // Reset all
        // Set the isPlayerAlive flag to false for all AIChase scripts
        AIChase[] aiChases = FindObjectsOfType<AIChase>();
        foreach (AIChase aiChase in aiChases)
        {
            aiChase.isPlayerAlive = false;
        }
        SceneManager.LoadScene(0);
        Destroy(gameObject);
    }

    public void AddToScore(int pointsToAdd) {
        score += pointsToAdd;
        scoreText.text = score.ToString();
    }
    void TakeLife()
    {   
        playerLives--;
        if (SceneManager.GetActiveScene().buildIndex == 6) {
            ScenePersist.ResetScenePersist();
        }
        // Set the isPlayerAlive flag to false for all AIChase scripts
        AIChase[] aiChases = FindObjectsOfType<AIChase>();
        foreach (AIChase aiChase in aiChases)
        {
            aiChase.isPlayerAlive = false;
        }
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
        livesText.text = playerLives.ToString();
    }
}
