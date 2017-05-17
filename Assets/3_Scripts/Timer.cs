using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public Text text = null;

    private float _curTime = 0.0f;
    private float _originTime = 0.0f;

    private bool _isDone = false;
    private bool _isPause = false;

    public void StartTimer(float time)
    {
        _isDone = false;
        _originTime = _curTime = time;
    }

    private void Update()
    {
        if (!_isDone && !_isPause)
        {
            _curTime -= Time.deltaTime;

            if (_curTime <= 0.0f)
            {
                _isDone = true;

                _curTime = _originTime = 0.0f;
            }
            string result = string.Format("{0:0.##}", _curTime);
            text.text = result;
        }
    }

    public void Pause()
    {
        _isPause = true;
    }

    public void Resume()
    {
        _isPause = false;
    }

    public float curTime
    {
        get
        {
            return _curTime;
        }
    }

    public float originTime
    {
        get
        {
            return _originTime;
        }
    }
      
    public bool isDone
    {
        get
        {
            return _isDone;
        }
    }

    public bool isPause
    {
        get
        {
            return _isPause;
        }
    }
}
