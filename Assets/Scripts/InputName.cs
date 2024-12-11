using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InputName : MonoBehaviour
{
    public HighScore highScore;

    public Button finishedName;
    public TMP_InputField inputField;

    private string validCharacters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
    private int characterLimit = 3;
    public bool donetyping = false;
    
    void Awake()
    {
        Show();

        if(highScore == null)
        {
            highScore = HighScore.instance;
        }
        
        finishedName.onClick.AddListener(OnNameSubmitted);
    }

    void Update()
    {
        inputField.text = inputField.text.ToUpper();

        if(inputField.text.Length > 2)
        {
            donetyping = true;
        }else
        {
            donetyping = false;
        }

        if(Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter) && donetyping == true)
        {
            finishedName.onClick.Invoke();
        }
    }

    public void Hide()
    {
        if(donetyping == true)
            gameObject.SetActive(false);
    }

    public void Show()
    {
        gameObject.SetActive(true);

        inputField.characterLimit = characterLimit;
        inputField.onValidateInput = (string text, int charIndex, char addedChar) => { return ValidateChar(validCharacters, addedChar); };
    }

    private char ValidateChar(string validedCharacters, char addedChar)
    {
        if(validedCharacters.IndexOf(addedChar) != -1)
        {
            return addedChar;
        }else
        {
            return '\0';
        }

    }

    public string PlayerName()
    {
        return inputField.text;
    }

    private void OnNameSubmitted()
    {
        string playerName = inputField.text;
        int playerScore = ScoreManager.instance.GetFinalScore();

        if(!string.IsNullOrEmpty(playerName))
        {
            highScore.AddHighScoreEntry(playerScore, playerName);
            highScore.RefreshHighScoreDisplay();
        }
    }
}
