using UnityEngine;

/// <summary>
/// Attach this to the Player GameObject.
/// Owns all interaction input so no interactable object ever reads Input directly.
/// </summary>
public class PlayerInteractor : MonoBehaviour
{
    private IInteractable currentInteractable;

    void Update()
    {
        if (currentInteractable == null) return;

        if (Input.GetKey(KeyCode.F))
            currentInteractable.OnInteractHeld(Time.deltaTime);

        if (Input.GetKeyUp(KeyCode.F))
            currentInteractable.OnInteractReleased();
    }

    /// <summary>
    /// Called by an interactable's OnTriggerEnter when the player enters its zone.
    /// </summary>
    public void SetInteractable(IInteractable interactable)
    {
        // Already tracking something — clear it first
        if (currentInteractable != null)
            currentInteractable.OnInteractExit();

        currentInteractable = interactable;
        currentInteractable.OnInteractEnter();

        // Optional: forward the prompt text to your UI system here
        // UIManager.Instance.ShowPrompt(currentInteractable.GetPromptText());
    }

    /// <summary>
    /// Called by an interactable's OnTriggerExit when the player leaves its zone.
    /// Guard check ensures a distant object can't accidentally clear a closer one.
    /// </summary>
    public void ClearInteractable(IInteractable interactable)
    {
        if (currentInteractable != interactable) return;

        currentInteractable.OnInteractExit();
        currentInteractable = null;

        // Optional: hide the UI prompt here
        // UIManager.Instance.HidePrompt();
    }
}