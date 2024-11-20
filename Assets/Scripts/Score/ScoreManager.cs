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
    private int score = 0;
    private float timer = 0;

    // Enemy Tracking
    private List<GameObject> zombies = new List<GameObject>();
    private List<GameObject> werewolves = new List<GameObject>();

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
        }
    }

    void Start()
    {
        zombies.AddRange(GameObject.FindGameObjectsWithTag("Zombie"));
        werewolves.AddRange(GameObject.FindGameObjectsWithTag("Werewolf"));
    }

    void Update()
    {
        if (!finished)
        {
            timer += Time.deltaTime;
        }
    }

    // Stop Timer
    public void StopTimer()
    {
        finished = true;
    }

    // Display Text
    public void DisplayText()
    {
        RemainingEnemies();
        CalculateScore();

        amountZombieText.text = zombiesKilled + " / " + zombies.Count;
        amountWerewolfText.text = werewolvesKilled + " / " + werewolves.Count;
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
        float scoreTotal = (points * multiplier) / Mathf.Max(timer, 1);
        score = Mathf.RoundToInt(scoreTotal);
        scoreText.text = "Score: " + score;
    }

    // Calcuates the dead (removed from list) amount of Enemies in the game
    public void RemainingEnemies()
    {
        zombiesKilled = zombies.Count - zombies.RemoveAll(z => z == null);
        werewolvesKilled = werewolves.Count - werewolves.RemoveAll(w => w == null);
    }
}
