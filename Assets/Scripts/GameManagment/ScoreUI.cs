using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ScoreUI : MonoBehaviour
{
    private Text _scoreText;

    public int Score { get; private set; }
    public UnityAction TimeIsOver;
    public Color[] colors;


    // Start is called before the first frame update
    void Start()
    {
        _scoreText = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        _scoreText.text = Score.ToString();
    }

    public void AddScore(int score) 
    {
        Score += score;

        if (Score >= 100) 
        {
            _scoreText.color = colors[0];
        }

        if (Score >= 200)
        {
            _scoreText.color = colors[1];
        }

        if (Score >= 300)
        {
            _scoreText.color = colors[2];
        }

        if (Score >= 400)
        {
            _scoreText.color = colors[3];
        }
    }

}
