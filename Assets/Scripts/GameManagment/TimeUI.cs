using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class TimeUI : MonoBehaviour
{
    [SerializeField]
    private float _levelTime;
    private bool _isRunning = true;
    private TextMeshProUGUI _lblTime;
    public UnityAction TimeIsOver;


    public float LevelTime => _levelTime;

    // Start is called before the first frame update
    void Start()
    {
        _lblTime = GetComponent<TextMeshProUGUI>();
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
