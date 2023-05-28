using UnityEngine;

public class CharacterSwitcher : MonoBehaviour
{
    [SerializeField] private GameObject[] characters; // Reference to the characters to switch between
    private int activeCharacterIndex = 0; // Index of the currently active character
    public ParticleSystem P1P;
    public ParticleSystem P2P;

    bool P1Pool;


    private void Start()
    {
        SetActiveCharacter(activeCharacterIndex);
        P1P.enableEmission = true;
        P2P.enableEmission = false;
    }

    private void Update()
    {
        // Check for character switch input
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SwitchCharacter();
        }

        if (P1Pool)
        {
            P1P.enableEmission = false;
            P2P.enableEmission = true;
        }

        else if (!P1Pool)
        {
            P1P.enableEmission = true;
            P2P.enableEmission = false;
        }

    }

    private void SwitchCharacter()
    {
        P1Pool = !P1Pool;

        // Disable control for the currently active character
        characters[activeCharacterIndex].GetComponent<PlayerController>().enabled = false;


        // Switch to the next character
        activeCharacterIndex++;
        if (activeCharacterIndex >= characters.Length)
        {
            activeCharacterIndex = 0;
        }

        // Enable control for the new active character
        characters[activeCharacterIndex].GetComponent<PlayerController>().enabled = true;
    }

    private void SetActiveCharacter(int index)
    {
        // Enable control for the specified character
        characters[index].GetComponent<PlayerController>().enabled = true;

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
                characters[i].GetComponent<PlayerController>().enabled = false;
            }
        }
    }
}
