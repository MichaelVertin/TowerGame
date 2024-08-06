using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Base : MonoBehaviour, IOwnable
{
    [SerializeField] public Player Owner { get; set; }
    [SerializeField] private Slider _slider;

    #region DamagableClass
    [SerializeField] protected float _maxHealth;
    private float _currentHealth = 0f;
    public float Health
    {
        get { return _currentHealth; }
        set
        {
            _currentHealth = value;
            if (Health <= .00001f)
            {
                OnDeath();
            }
        }
    }
    #endregion

    protected void Awake()
    {
        _currentHealth = _maxHealth;
    }

    protected void Update()
    {
        _slider.value = _currentHealth / _maxHealth;
        UnityEngine.Color color = UnityEngine.Color.Lerp(UnityEngine.Color.red, UnityEngine.Color.green, _slider.value);
        _slider.gameObject.transform.Find("Fill Area").Find("Fill").GetComponent<Image>().color = color;
    }

    public void OnDeath()
    {
        Debug.Log("Base Destroyed");
    }
}
