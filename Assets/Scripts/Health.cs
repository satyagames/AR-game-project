using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// Handles GameObject's health
public class Health : MonoBehaviour
{
    [SerializeField] private int _maxHealth = 100;
    [SerializeField] private int _currentHealth = 100;
    [SerializeField] public UnityEvent<int> OnReceiveDamage;
    [SerializeField] public UnityEvent OnZeroHealth;
    [SerializeField] public UnityEvent<int> OnReceiveHealth;

    private void Start()
    {
        _currentHealth = _maxHealth;
    }
    public int CurrentHealth
    {
        get => _currentHealth;
        set => _currentHealth = value;
    }

    public int MaxHealth
    {
        get => _maxHealth;
        set => _maxHealth = value;
    }

    public void ReceiveDamage(int damageAmount)
    {
        CurrentHealth -= damageAmount;
        Debug.Log($"Damage received. Current Health: {CurrentHealth}");
        
        if (CurrentHealth <= 0)
        {
            OnZeroHealth?.Invoke();
        }

        OnReceiveDamage?.Invoke(CurrentHealth); // Invoke event after updating health
    }

    public void GainHealth(int gainAmount)
    {
        _currentHealth += gainAmount;
        _currentHealth = Mathf.Clamp(_currentHealth, 0, _maxHealth);
        Debug.Log($"Health gained. Current Health: {_currentHealth}");
        OnReceiveHealth?.Invoke(_currentHealth);
    }
}
