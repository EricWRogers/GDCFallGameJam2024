using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HighScore : MonoBehaviour
{
    public static HighScore instance { get; private set; }

    public Transform parentObject;
    public Transform entryContainer;
    public Transform entryTemplate;
    private List<HighScoreEntry> highScoreEntryList;
    private List<Transform> highScoreEntryTransformList;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject); // Prevent duplicate instances
        }
        
        parentObject = transform.Find("HoldsAllObjects");
        entryContainer = parentObject.Find("HighScoreEntryContainer");
        entryTemplate = entryContainer.Find("HighScoreEntryTemplate");

        entryTemplate.gameObject.SetActive(false);

        //AddHighScoreEntry(10000, "DDD");
        //ClearHighScoreEntry();


        if (PlayerPrefs.HasKey("HighScore"))
        {
            try
            {
                string jsonString = PlayerPrefs.GetString("HighScore");
                HighScoresJson highscores = JsonUtility.FromJson<HighScoresJson>(jsonString);
                highScoreEntryList = highscores?.highScoreEntryList ?? new List<HighScoreEntry>();
                Debug.Log($"Loaded HighScore JSON: {jsonString}");
            }
            catch
            {
                Debug.Log("Failed to load HighScore data. Resetting to default.");
                highScoreEntryList = new List<HighScoreEntry>();
                PlayerPrefs.DeleteKey("HighScore");
            }
        }
        else
        {
            highScoreEntryList = new List<HighScoreEntry>();
        }

        if (highScoreEntryList != null)
        {
            // Sort the high score list by score in descending order
            highScoreEntryList.Sort((entry1, entry2) => entry2.score.CompareTo(entry1.score));
            
            highScoreEntryTransformList = new List<Transform>();
            foreach (HighScoreEntry highScoreEntry in highScoreEntryList)
            {
                CreateHighScoreEntryTransform(highScoreEntry, entryContainer, highScoreEntryTransformList);
            }
        }
        else
        {
            Debug.LogError("HighScoreEntryList is null.");
        }

        Debug.Log(PlayerPrefs.GetString("HighScore"));
    }

    public void AddHighScoreEntry(int score, string name)
    {
        HighScoreEntry highScoreEntry = new HighScoreEntry {score = score, name = name};

        HighScoresJson highscores;

        if (PlayerPrefs.HasKey("HighScore"))
        {
            string jsonString = PlayerPrefs.GetString("HighScore");
            highscores = JsonUtility.FromJson<HighScoresJson>(jsonString);
        }
        else
        {
            highscores = new HighScoresJson { highScoreEntryList = new List<HighScoreEntry>() };
        }

        if(highscores.highScoreEntryList == null)
        {
            highscores.highScoreEntryList = new List<HighScoreEntry>();
        }

        highscores.highScoreEntryList.Add(highScoreEntry);

        string json = JsonUtility.ToJson(highscores);
        PlayerPrefs.SetString("HighScore", json);
        PlayerPrefs.Save();
    }

    public void RefreshHighScoreDisplay()
    {
        // Clear existing high score entries
        foreach (Transform child in entryContainer)
        {
            if (child != entryTemplate)
            {
                Destroy(child.gameObject);
            }
        }

        // Reload high scores
        string jsonString = PlayerPrefs.GetString("HighScore", "{}");
        HighScoresJson highscores = JsonUtility.FromJson<HighScoresJson>(jsonString);

        if (highscores != null && highscores.highScoreEntryList != null)
        {
            highscores.highScoreEntryList.Sort((entry1, entry2) => entry2.score.CompareTo(entry1.score));
            highScoreEntryTransformList = new List<Transform>();

            foreach (HighScoreEntry highScoreEntry in highscores.highScoreEntryList)
            {
                CreateHighScoreEntryTransform(highScoreEntry, entryContainer, highScoreEntryTransformList);
            }
        }
    }

    public void ClearHighScoreEntry()
    {
        PlayerPrefs.DeleteKey("HighScore");
        PlayerPrefs.Save();
        Debug.Log("Cleared the Temp HighScore Data");
    }

    private void CreateHighScoreEntryTransform(HighScoreEntry highScoreEntry, Transform container, List<Transform> transformList)
    {
        float templateHeight = 25f;
        Transform entryTransform = Instantiate(entryTemplate, container);
        RectTransform entryRectTransform = entryTransform.GetComponent<RectTransform>();
        entryRectTransform.anchoredPosition = new Vector2(0, -templateHeight * transformList.Count);
        entryTransform.gameObject.SetActive(true);
        
        int rank = transformList.Count + 1;
        string rankStr;
        switch(rank)
        {
            default:
                rankStr = rank + "TH"; break;
            case 1:
                rankStr = "1ST"; break;
            case 2:
                rankStr = "2ND"; break;
            case 3:
                rankStr = "3RD"; break;
        }

        entryTransform.Find("posText").GetComponent<TextMeshProUGUI>().text = rankStr;

        int score = highScoreEntry.score;
        entryTransform.Find("scoreText").GetComponent<TextMeshProUGUI>().text = score.ToString();

        string name = highScoreEntry.name;
        entryTransform.Find("nameText").GetComponent<TextMeshProUGUI>().text = name;

        transformList.Add(entryTransform);
    }

    private class HighScoresJson
    {
        public List<HighScoreEntry> highScoreEntryList;
    }

    [System.Serializable]
    private class HighScoreEntry
    {
        public int score;
        public string name;
    }
    
}
