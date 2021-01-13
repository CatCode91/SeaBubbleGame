using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class TimeUI : MonoBehaviour
{
    private Text _lblTime;
    private LevelTimer _timer;
   
   
    // Start is called before the first frame update
    void Start()
    {
        _lblTime = GetComponent<Text>();
        _timer = GetComponentInParent<LevelTimer>();
    }

    // Update is called once per frame
    void Update()
    {
        _lblTime.text = _timer.LevelTime.ToString("##");

        if (_timer.LevelTime < 10)
        {
            _lblTime.color = Color.red;
        }
    }
}
