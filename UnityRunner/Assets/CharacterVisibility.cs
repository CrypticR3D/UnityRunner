using UnityEngine;

public class CharacterVisibility : MonoBehaviour
{
    public delegate void CharacterVisibilityChanged(bool isAnyCharacterOffScreen);
    public static event CharacterVisibilityChanged OnCharacterVisibilityChanged;

    private bool isAnyCharacterOffScreen;

    private void Update()
    {
        CheckCharacterVisibility();
    }

    private void CheckCharacterVisibility()
    {
        // Determine character visibility logic here
        // Set the value of isAnyCharacterOffScreen accordingly

        if (isAnyCharacterOffScreen)
        {
            OnCharacterVisibilityChanged?.Invoke(true);
        }
        else
        {
            OnCharacterVisibilityChanged?.Invoke(false);
        }
    }
}
