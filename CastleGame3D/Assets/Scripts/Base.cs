using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Base : MonoBehaviour
{
    [SerializeField] public Player owner;
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

    /*
    public void OnTriggerEnter(Collider other)
    {
        Warrior warrior = other.GetComponentInParent<Warrior>();

        if ( warrior != null && warrior.owner != this.owner )
        {
            warrior.OnDeath();
        }
    }
    */

    protected void Update()
    {
        _slider.value = _currentHealth / _maxHealth;
        /*var fill = (slider as UnityEngine.UI.Slider).GetComponentsInChildren<UnityEngine.UI.Image>().FirstOrDefault(t => t.name == "Fill");
        if (fill != null)
        {
            fill.color = Color.Lerp(Color.red, Color.green, 0.5);
        }
        */

        UnityEngine.Color color = UnityEngine.Color.Lerp(UnityEngine.Color.red, UnityEngine.Color.green, _slider.value);
        _slider.gameObject.transform.Find("Fill Area").Find("Fill").GetComponent<Image>().color = color;
    }

    public void OnDeath()
    {
        Debug.Log("Base Destroyed");
    }
}
