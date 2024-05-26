using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class renkgame : MonoBehaviour
{
    public GameObject SonOyun;
    public GameObject IkinciPanel;
    public TextMeshProUGUI sonuc;
    public Image[] images; 
    public TMP_InputField inputField; 
    public Button checkButton; 
    public Button startButton;
    public Button NextGame; 
    private int questionCount = 0;
    private List<string> correctColors = new List<string>();


    void Start()
    {
        checkButton.onClick.AddListener(CheckAnswer);
        startButton.onClick.AddListener(StartGame);
        checkButton.gameObject.SetActive(false);
        NextGame.onClick.AddListener(ClearPanel);
        foreach (Image img in images)
        {
            img.gameObject.SetActive(false); 
        }

    }
    void StartGame()
    {
        startButton.gameObject.SetActive(false);
        checkButton.gameObject.SetActive(true);

        StartCoroutine(DisplayColors());
    }

    IEnumerator DisplayColors()
    {
        foreach (Image img in images)
        {
            img.gameObject.SetActive(false);
        }
        

        correctColors = new List<string>();
        int colorLength = 0;

       

        while (colorLength < 3)
        {
            int index = Random.Range(0, images.Length);
            if (correctColors.Contains(images[index].name))
            {
                continue;
            }
            images[index].gameObject.SetActive(true);
            correctColors.Add(images[index].name);
            ++colorLength;
            yield return new WaitForSeconds(1);
            images[index].gameObject.SetActive(false);
        }



    yield return new WaitForSeconds(3);

        foreach (Image img in images)
        {
            img.gameObject.SetActive(false);
        }
    }

    void CheckAnswer()
    {
        string[] userAnswers = inputField.text.Split(' ');
        bool isCorrect = true;

        if (userAnswers.Length != correctColors.Count)
        {
            isCorrect = false;
        }
        else
        {
            for (int i = 0; i < correctColors.Count; i++)
            {
                if (!correctColors[i].Equals(userAnswers[i], System.StringComparison.OrdinalIgnoreCase))
                {
                    isCorrect = false;
                    break;
                }
            }
        }

        if (isCorrect)
        {
            ScoreManager.ColorScore += 25;
            Debug.Log("Doðru! Skor: " + ScoreManager.ColorScore);
        }
        questionCount++;
        if (questionCount >= 4)
        {
            ShowScore();
        }
        else
        {
            inputField.text = "";
            StartCoroutine(DisplayColors());
        }

        void ShowScore()
        {
            sonuc.text = ("Hafýza Yeteneði Puanýn: " + ScoreManager.ColorScore);
        }     
    }
    void ClearPanel()
    {
        IkinciPanel.SetActive(false);
        SonOyun.SetActive(true);
        
    }
}

