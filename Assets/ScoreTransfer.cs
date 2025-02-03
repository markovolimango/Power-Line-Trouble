using TMPro;
using UnityEngine;

public class ScoreTransfer : MonoBehaviour
{
    public static int Score;

    public static void SetScore(int score)
    {
        Score = score;
    }

    public void UpdateText(TMP_Text text)
    {
        text.text = Score.ToString();
    }
}