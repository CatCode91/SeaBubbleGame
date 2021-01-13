using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LevelTimer : MonoBehaviour
{
    [SerializeField]
    private float _levelTime;
    private bool _isRunning = true;
    public UnityAction TimeIsOver;
    public float LevelTime => _levelTime;

    // Update is called once per frame
    void Update()
    {
        if (_isRunning)
        {
            _levelTime -= Time.deltaTime;

            if (LevelTime <= 0)
            {
                _isRunning = false;
                TimeIsOver?.Invoke();
            }
        }
    }
}
