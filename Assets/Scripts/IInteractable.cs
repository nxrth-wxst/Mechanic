using UnityEngine;

public interface IInteractable
{
    void OnInteractEnter();   
    void OnInteractExit();    
    void OnInteractHeld(float deltaTime);  
    void OnInteractReleased();             
    string GetPromptText();   
}
