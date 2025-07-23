using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Outline_TG : MonoBehaviour
{
    [SerializeField] GameObject rectPrefab;

    private Transform Top;
    private Transform Right;
    private Transform Left;
    private Transform Bottom;

    [SerializeField] private float _width = 1f;
    [SerializeField] private float _height = 1f;
    [SerializeField] private float _thickness = .1f;

    public float Width
    {
        get
        {
            return _width;
        }
        set
        {
            _width = value;
            UpdateFrame();
        }
    }

    public float Height
    {
        get
        {
            return _height;
        }
        set
        {
            _height = value;
            UpdateFrame();
        }
    }
    
    public float Thickness
    {
        get
        {
            return _thickness;
        }
        set
        {
            _thickness = value;
            UpdateFrame();
        }
    }



    private void Awake()
    {
        Top = Instantiate<GameObject>(rectPrefab, this.transform).transform;
        Bottom = Instantiate<GameObject>(rectPrefab, this.transform).transform;
        Left = Instantiate<GameObject>(rectPrefab, this.transform).transform;
        Right = Instantiate<GameObject>(rectPrefab, this.transform).transform;

        UpdateFrame();
    }

    private void Update()
    {
        //UpdateFrame();
    }

    private void UpdateFrame()
    {
        float frameVerticalDistance = (_height - _thickness) / 2f;
        float frameHorizontalDistance = (_width - _thickness) / 2f;

        Top.transform.localScale = new Vector2(_width, _thickness);
        Top.transform.localPosition = new Vector2(0, frameVerticalDistance);

        Bottom.transform.localScale = new Vector2(_width, _thickness);
        Bottom.transform.localPosition = new Vector2(0, -frameVerticalDistance);

        Right.transform.localScale = new Vector2(_thickness, _height);
        Right.transform.localPosition = new Vector2(frameHorizontalDistance, 0);

        Left.transform.localScale = new Vector2(_thickness, _height);
        Left.transform.localPosition = new Vector2(-frameHorizontalDistance, 0);
    }
}
