using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    public Slider healthBar;
    private int currentHealth = 100;

    private void Start()
    {
        UpdateHealthBar();
    }

    public void UpdateHealth(int changeAmount)
    {
        currentHealth -= changeAmount;
        if (currentHealth < 0)
        {
            currentHealth = 0;
        }

        UpdateHealthBar();
    }

    private void UpdateHealthBar()
    {
        healthBar.value = currentHealth;
    }
}