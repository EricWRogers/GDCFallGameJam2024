using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HighScore : MonoBehaviour
{
    private Transform entryContainer;
    private Transform entryTemplate;
    private List<HighScoreEntry> highScoreEntryList;
    private List<Transform> highScoreEntryTransformList;

    void Awake()
    {
        entryContainer = transform.Find("HighScoreEntryContainer");
        entryTemplate = entryContainer.Find("HighScoreEntryTemplate");

        entryTemplate.gameObject.SetActive(false);

        // Check if "HighScore" exists in PlayerPrefs
        if (PlayerPrefs.HasKey("HighScore"))
        {
            string jsonString = PlayerPrefs.GetString("HighScore");
            HighScoresJson highscores = JsonUtility.FromJson<HighScoresJson>(jsonString);

            if (highscores != null && highscores.highScoreEntryList != null)
            {
                highScoreEntryList = highscores.highScoreEntryList;
            }
            else
            {
                highScoreEntryList = new List<HighScoreEntry>();
            }
        }
        else
        {
            // Create default high scores
            highScoreEntryList = new List<HighScoreEntry>
            {
                new HighScoreEntry { score = 5000, name = "AAA" },
                new HighScoreEntry { score = 4000, name = "BBB" },
                new HighScoreEntry { score = 3000, name = "CCC" },
                new HighScoreEntry { score = 2000, name = "DDD" },
                new HighScoreEntry { score = 1000, name = "EEE" },
            };

            // Save default high scores to PlayerPrefs
            HighScoresJson defaultHighScores = new HighScoresJson { highScoreEntryList = highScoreEntryList };
            string json = JsonUtility.ToJson(defaultHighScores);
            PlayerPrefs.SetString("HighScore", json);
            PlayerPrefs.Save();
        }

        // Sort the high score list
        highScoreEntryList.Sort((entry1, entry2) => entry2.score.CompareTo(entry1.score));

        highScoreEntryTransformList = new List<Transform>();
        foreach (HighScoreEntry highScoreEntry in highScoreEntryList)
        {
            CreateHighScoreEntryTransform(highScoreEntry, entryContainer, highScoreEntryTransformList);
        }

        Debug.Log(PlayerPrefs.GetString("HighScore"));
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
