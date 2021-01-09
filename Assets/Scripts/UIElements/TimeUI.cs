using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class TimeUI : MonoBehaviour
{
    [SerializeField]
    private float _levelTime;
    private bool _isRunning = true;
    private Text _lblTime;
   
    public float LevelTime => _levelTime;
    public UnityAction TimeIsOver;

    // Start is called before the first frame update
    void Start()
    {
        _lblTime = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_isRunning) 
        {
            _levelTime -= Time.deltaTime;
            _lblTime.text = LevelTime.ToString("##");

            if (LevelTime.ToString("##") == string.Empty) 
            {
                _isRunning = false;
                TimeIsOver?.Invoke();
            }

            if (_levelTime < 10) 
            {
                _lblTime.color = Color.red;            
            }
        }
    }
}
