using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;

public class MathProblemScript : MonoBehaviour
{
    public GameObject FirstGamePanel;
    public GameObject NextGamePanel;
    public TextMeshProUGUI ProblemText;
    public TextMeshProUGUI Time;
    public TMP_InputField AnswerInput;
    public Button CheckButton;
    public Button NextGame;

    public int MathScore = 0;

    private Coroutine _timerCoroutine;

    private readonly string[] _problems = {
        "(4 + 3) x 2^2 - 10 / 5 = ?",
        "8 + 4 x 2^2 / 2 - 3 = ?",
        "3x + 120 = 0 x?",
        "5!+4!+3!+2! = ?" };

    private readonly int[] _answers = { 26, 13, -40, 152 };

    private int _currentProblemIndex = 0;
    private float _timeLeft = 30f;
    private bool _isRunning = false;
   
    void Question()
    {
        if (_currentProblemIndex < _problems.Length)
        {
            ProblemText.text = _problems[_currentProblemIndex];
            AnswerInput.text = "";
        }
        else
        {
            ProblemText.text = "Sýradaki oyuna geciniz! Hesap Yetenegi Puanýnýz: " + MathScore;
            CheckButton.enabled = false;
            StopTimer();
        }

    }

    void CheckAnswer()
    {
        int cevap;
        if (int.TryParse(AnswerInput.text, out cevap))
        {
            // Cevap doðruysa bunu çalýþtýr
            if (cevap == _answers[_currentProblemIndex])
            {
                MathScore += 25;
            }
        }
        ScoreManager.MathScore = MathScore;
        _currentProblemIndex++;
        Question();
    }

    IEnumerator Timer()
    {
        _isRunning = true;
        while (_isRunning && _currentProblemIndex < _problems.Length)
        {
            yield return new WaitForSeconds(1f);
            _timeLeft -= 1f;
            Time.text = "Kalan Zaman: " + Math.Round(_timeLeft);
        }
    }

    void StopTimer()
    {
        if (_timerCoroutine != null)
        {
            _isRunning = false;
            StopCoroutine(_timerCoroutine);
            _timerCoroutine = null;
            Debug.Log("StopTimer metodu çalýþtýrýldý.");
        }
    }

    void Update()
    {
        Time.text = "Kalan Zaman: " + Math.Round(_timeLeft);
        if(_timeLeft <= 0 )
        {
            ProblemText.text = " Süre Bitti Sonraki Oyuna Geciniz Hesap Yetenegi Puanýnýz " + MathScore;
            StopTimer();
        }           
    }

    void Start()
    {
        
        NextGamePanel.SetActive(false);
        Question();       
        CheckButton.onClick.AddListener(CheckAnswer);    
        _timerCoroutine = StartCoroutine(Timer());
        NextGame.onClick.AddListener(ClearPanel);
    }
    void ClearPanel()
    {
        FirstGamePanel.SetActive(false);
        NextGamePanel.SetActive(true);
    }
     
}
