using UnityEngine;

public class CharacterSwitcher : MonoBehaviour
{
    [SerializeField] private GameObject[] characters; // Reference to the characters to switch between
    private int activeCharacterIndex = 0; // Index of the currently active character

    private void Start()
    {
        SetActiveCharacter(activeCharacterIndex);
    }

    private void Update()
    {
        // Check for character switch input
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SwitchCharacter();
        }
    }

    private void SwitchCharacter()
    {
        // Disable control for the currently active character
        characters[activeCharacterIndex].GetComponent<MovementController>().enabled = false;

        // Switch to the next character
        activeCharacterIndex++;
        if (activeCharacterIndex >= characters.Length)
        {
            activeCharacterIndex = 0;
        }

        // Enable control for the new active character
        characters[activeCharacterIndex].GetComponent<MovementController>().enabled = true;
    }

    private void SetActiveCharacter(int index)
    {
        // Enable control for the specified character
        characters[index].GetComponent<MovementController>().enabled = true;

        // Set all characters visible
        for (int i = 0; i < characters.Length; i++)
        {
            characters[i].SetActive(true);
        }

        // Disable control for all characters except the specified character
        for (int i = 0; i < characters.Length; i++)
        {
            if (i != index)
            {
                characters[i].GetComponent<MovementController>().enabled = false;
            }
        }
    }
}
