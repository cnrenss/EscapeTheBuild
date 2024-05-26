using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SonOyun : MonoBehaviour
{
    public GameObject Panel;
    public GameObject charPrefab;
    public Transform gridParent;
    public int gridSize = 60; 
    private string targetString;
    private string currentString = "";
    public TextMeshProUGUI targetText;
    public TextMeshProUGUI scoreText;
    private float timer = 0f;
    private bool gameStarted = false;
    public Button finishgame;
    public Button startgame;

    void Start()
    {
        Panel.SetActive(false);
        
        finishgame.onClick.AddListener(EndGame);
        startgame.onClick.AddListener(StartGame);
        gridParent.gameObject.SetActive(false);
        targetText.gameObject.SetActive(false);
        scoreText.gameObject.SetActive(false);
        charPrefab.gameObject.SetActive(false);
    }

    void StartNewRound()
    {
        
        currentString = "";
        targetString = GenerateRandomTargetString(3);
        ClearGrid();
        GenerateRandomGridWithTarget();
        ShowTargetString();

    }
    void StartGame()
    {
        gameStarted = true;
        timer = 30f;
        startgame.gameObject.SetActive(false);
        gridParent.gameObject.SetActive(true);
        targetText.gameObject.SetActive(true);
        scoreText.gameObject.SetActive(true);
        charPrefab.gameObject.SetActive(true);
        StartNewRound();
    }

    void ClearGrid()
    {
        foreach (Transform child in gridParent)
        {
            Destroy(child.gameObject);
        }
    }

    void GenerateRandomGridWithTarget()
    {
        string chars = "abcdefghijklmnopqrstuvwxyz0123456789";
        int targetPosition = Random.Range(0, gridSize - targetString.Length + 1);

        for (int i = 0; i < gridSize; i++)
        {
            GameObject newChar = Instantiate(charPrefab, gridParent);
            char randomChar;
            if (i >= targetPosition && i < targetPosition + targetString.Length)
            {
                randomChar = targetString[i - targetPosition];
            }
            else
            {
                randomChar = chars[Random.Range(0, chars.Length)];
            }
            newChar.GetComponentInChildren<TextMeshProUGUI>().text = randomChar.ToString();
            newChar.GetComponent<Button>().onClick.AddListener(() => OnCharClicked(randomChar.ToString()));
            Debug.Log("Buttonlar Olustu: " + randomChar); 
        }
        Debug.Log("Toplam Button Sayýsý: " + gridSize); 
    }

    string GenerateRandomTargetString(int length)
    {
        string chars = "abcdefghijklmnopqrstuvwxyz0123456789";
        string result = "";
        for (int i = 0; i < length; i++)
        {
            result += chars[Random.Range(0, chars.Length)];
        }
        return result;
    }

    void ShowTargetString()
    {
        targetText.text = "Sýrasýyla Bu Deðerleri Bulunuz: " + targetString;
    }

    void OnCharClicked(string clickedChar)
    {
        currentString += clickedChar;
        if (currentString.Length > targetString.Length)
        {
            currentString = "";
        }
        else if (currentString == targetString)
        {
            Debug.Log("Dogru deger girildi");
            ScoreManager.PuzzleScore += 10;
            UpdateScoreText();
            StartNewRound();
        }
    }

    void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
            UpdateTimerText();
        }
        else
        {
            GameOver();
        }
    }

    void UpdateScoreText()
    {
        scoreText.text = "Puan: " + ScoreManager.PuzzleScore;
    }

    void UpdateTimerText()
    {
        scoreText.text = "Kalan Zaman: " + Mathf.Ceil(timer).ToString();
    }

    void GameOver()
    {
        foreach (Transform child in gridParent)
        {
            child.GetComponent<Button>().interactable = false;
        }
        targetText.text = "Süreniz Doldu. Dikkat Yeteneði Puanýnýz: " + ScoreManager.PuzzleScore;
    }
    public void EndGame()
    {
        SceneManager.LoadScene("EndGame");
    }
}
