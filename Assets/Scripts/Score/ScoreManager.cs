using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    // Creating Instances
    public static ScoreManager instance { get; private set; }

    // Needed Variables for Scoreboard
    public bool finished = false;
    public int points = 0;
    public int multiplier = 1;
    public float timer = 0;

    // Score for Scoreboard
    private int finalScore;

    // Enemy Tracking
    private List<GameObject> zombies = new List<GameObject>();
    private List<GameObject> werewolves = new List<GameObject>();
    private int initialZombieCount = 0;
    private int initialWerewolfCount = 0;

    private int zombiesKilled = 0;
    private int werewolvesKilled = 0;

    // Grab Text for the Win Screen
    public TMP_Text amountZombieText;
    public TMP_Text amountWerewolfText;
    public TMP_Text bossText;
    public TMP_Text timerText;
    public TMP_Text scoreText;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        zombies.AddRange(GameObject.FindGameObjectsWithTag("Zombie"));
        werewolves.AddRange(GameObject.FindGameObjectsWithTag("Werewolf"));
        initialZombieCount = zombies.Count;
        initialWerewolfCount = werewolves.Count;
    }

    void Update()
    {
        if (!finished)
        {
            timer += Time.deltaTime;
        }
    }

    // Display Text
    public void DisplayText()
    {
        DeadEnemies();
        CalculateScore();

        // correct death count
        int correctZombieDeath = initialZombieCount - zombiesKilled;
        int correctWerewolfDeath = initialWerewolfCount - werewolvesKilled;

        amountZombieText.text = correctZombieDeath + " / " + initialZombieCount;
        amountWerewolfText.text = correctWerewolfDeath + " / " + initialWerewolfCount;
        bossText.text = "1 / 1";

        // Get the right time look
        int minutes = (int)timer / 60;
        int seconds = (int)timer - (minutes * 60);
        timerText.text = minutes.ToString("D2") + ":" + seconds.ToString("D2");
    }

    // Add points to the score (gets called on other objects)
    public void AddPoints(int _points)
    {
        points += _points;
    }

    // Pickups Give a Multiplier for the score
    public void AddMultiplier(int _multiplier)
    {
        multiplier += _multiplier;
    }

    // Calculates Score at the end
    public void CalculateScore()
    {
        finished = true;
        
        float timeInMinutes = timer / 60f;
        finalScore = Mathf.RoundToInt((points * multiplier) / Mathf.Max(timeInMinutes, 0.01f) * 10);

        scoreText.text = "Score: " + finalScore;
    }

    public int GetFinalScore()
    {
        return finalScore;
    }

    // Calcuates the dead (removed from list) amount of Enemies in the game
    public void DeadEnemies()
    {
        zombiesKilled = initialZombieCount - zombies.RemoveAll(z => z == null);
        werewolvesKilled = initialWerewolfCount - werewolves.RemoveAll(w => w == null);
    }
}
