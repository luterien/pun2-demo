using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthUI : MonoBehaviour
{
    public PlayerController playerController;
    public Slider healthSlider;

    private void Update()
    {
        healthSlider.value = playerController.PlayerHealth.HealthPercentage;
    }
}