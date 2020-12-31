using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class TimeWatcher : MonoBehaviour
{
    private bool _isRunning = true;
    private float _levelTime = 60;
    private TextMeshProUGUI _lblTime;
    public UnityEvent TimeIsOver;

    public float LevelTime 
    {
        get 
        {
            return _levelTime;
        }

        set 
        {
            if (_levelTime == 0) 
            {
                _isRunning = false;
                TimeIsOver?.Invoke();
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        _lblTime = GetComponent<TextMeshProUGUI>();
        StartCoroutine("StartTimeWatcher");
    }

    // Update is called once per frame
    void Update()
    {
        if (_isRunning) 
        {
            _levelTime -= Time.deltaTime;
            _lblTime.text = _levelTime.ToString("##");
        }
    }

    public void ResetTimer() 
    {
        _levelTime = 60;
        _isRunning = true;
    }
}
