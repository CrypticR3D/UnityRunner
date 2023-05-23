using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HealthCountdown : MonoBehaviour
{
    [SerializeField] private Transform[] targets; // Reference to the characters to track
    [SerializeField] private float maxHealth = 100f; // Maximum health value for each character
    [SerializeField] private float countdownTime = 5f; // Countdown time in seconds
    [SerializeField] private TextMeshProUGUI countdownText; // Reference to the TextMeshProUGUI component

    private bool isCountdownActive; // Flag to indicate if the countdown is active
    private float currentHealth; // Current health value for each character

    private void Start()
    {
        currentHealth = maxHealth;
        countdownText.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        CharacterVisibility.OnCharacterVisibilityChanged += HandleCharacterVisibilityChanged;
    }

    private void OnDisable()
    {
        CharacterVisibility.OnCharacterVisibilityChanged -= HandleCharacterVisibilityChanged;
    }

    private void HandleCharacterVisibilityChanged(bool isAnyCharacterOffScreen)
    {
        if (isAnyCharacterOffScreen && !isCountdownActive)
        {
            StartCountdown();
        }
        else if (!isAnyCharacterOffScreen && isCountdownActive)
        {
            StopCountdown();
        }
    }

    private void Update()
    {
        if (isCountdownActive)
        {
            currentHealth -= Time.deltaTime;
            countdownText.text = currentHealth.ToString("0");

            if (currentHealth <= 0f)
            {
                CountdownTimeout();
            }
        }
    }

    private void StartCountdown()
    {
        isCountdownActive = true;
        currentHealth = maxHealth;
        countdownText.gameObject.SetActive(true);
    }

    private void StopCountdown()
    {
        isCountdownActive = false;
        countdownText.gameObject.SetActive(false);
    }

    private void CountdownTimeout()
    {
        // Reset or handle the timeout event for the characters
        // You can add your own logic here based on your game requirements
        Debug.Log("Countdown timeout!");
    }
}
