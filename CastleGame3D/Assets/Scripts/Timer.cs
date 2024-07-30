using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] public bool active;

    [SerializeField] private float _time;
    private TextMeshProUGUI _text;

    public float Time
    {
        get
        {
            return _time;
        }
        set
        {
            _time = value;
            _text.text = _time.ToString("0.0");
        }
    }

    private void Awake()
    {
        _text = GetComponent<TextMeshProUGUI>();
        if (_text == null)
        {
            Debug.LogError("Timer must have a TextMeshProUGUI component");
        }
        active = true;
    }

    // Update is called once per frame
    void Update()
    {
        if( active )
        {
            Time -= UnityEngine.Time.deltaTime;
            if( Time < 0.0f )
            {
                Time = 0.0f;
            }
        }
    }
}
