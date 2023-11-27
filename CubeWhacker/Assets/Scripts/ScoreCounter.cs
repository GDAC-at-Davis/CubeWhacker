using TMPro;
using UnityEngine;

public class ScoreCounter : MonoBehaviour
{
    public static ScoreCounter Instance { get; private set; }

    [SerializeField]
    private TMP_Text scoreText;

    private int currentScore;


    private void Awake()
    {
        Instance = this;
        currentScore = 0;
        AddScore(0);
    }

    public void AddScore(int score)
    {
        currentScore += score;
        scoreText.text = $"Cubes Whacked: {currentScore.ToString()}";
    }
}