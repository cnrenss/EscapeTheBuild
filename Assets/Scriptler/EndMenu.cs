using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEditor.Build.Content;

public class EndMenu : MonoBehaviour
{
    public TextMeshProUGUI MathAbility;
    public TextMeshProUGUI MemoryAbility;
    public TextMeshProUGUI AttentionAbility;

    private void Start()
    {
        int mathScore = ScoreManager.MathScore;
        MathAbility.text = "ISLEM YETENEGINI: " + mathScore;

        int colorScore = ScoreManager.ColorScore;
        MemoryAbility.text = "HAFIZA YETENEGINI: " + colorScore;

        int puzzleScore = ScoreManager.PuzzleScore;
        AttentionAbility.text = "DIKKAT YETENEGINI: " + puzzleScore;
    }
}
