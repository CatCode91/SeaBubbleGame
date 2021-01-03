using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class ScoreUI : MonoBehaviour
{
    private int _score = 0;
    private TextMeshProUGUI _scoreText;

    public int Score => _score;
    public UnityAction TimeIsOver;


    // Start is called before the first frame update
    void Start()
    {
        _scoreText = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        _scoreText.text = Score.ToString();
    }

    public void AddScore(int score) 
    {
        _score += score;

        if (_score >= 100) 
        {
            _scoreText.color = new Color32(5,14,43,100);
        }

        if (_score >= 200)
        {
            _scoreText.color = new Color32(15, 46, 0, 100);
        }

        if (_score >= 300)
        {
            _scoreText.color = new Color32(84, 30, 1, 100);
        }

        if (_score >= 2000)
        {
            _scoreText.color = new Color32(100, 5, 5, 100);
        }
    }

}
