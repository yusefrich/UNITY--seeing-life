using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogController : MonoBehaviour
{
    private bool interactable;

    public AutoTypeTextEffect[] texts;
    public GameObject initialBaloon;
    public GameObject secondBaloon;
    private int currentInteractingText = 0;
    
    
    
    
    
    public void InteractWith()
    {
        //next dialog in conversation
        if (texts[currentInteractingText].IsTextComplete())
        {
            texts[currentInteractingText].gameObject.SetActive(false);
            texts[currentInteractingText].HideBalloon();
            secondBaloon.SetActive(true);
            currentInteractingText++;
            
            if (currentInteractingText >= texts.Length)
            {
                currentInteractingText = 0;
            }
            
            texts[currentInteractingText].gameObject.SetActive(true);
            texts[currentInteractingText].StartText();
        }
        texts[currentInteractingText].EndText();
        print("interacted");
    }

    public void EndInteraction()
    {
        //endConversation
        print("skiped");
    }

    public void SetInteractable(bool status)
    {
        interactable = status;
        if (interactable)
        {
            StartDialogInteractions();
        }
        else
        {
            EndDialogInteractions();
        }
    }

    public bool IsInteractable()
    {
        return interactable;
    }

    void StartDialogInteractions()
    {
        foreach (var text in texts)
        {
            text.gameObject.SetActive(false);
        }
            
        texts[0].gameObject.SetActive(true);
        texts[0].StartText();

    }
    void EndDialogInteractions()
    {
        foreach (var text in texts)
        {
            if (text.gameObject.activeInHierarchy)
            {
                text.EndText();
                text.gameObject.SetActive(false);
            }
        }
        currentInteractingText = 0;

    }
    
    
    // for debug porpuse 

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            initialBaloon.SetActive(true);
            StartDialogInteractions();
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            InteractWith();
        }
    }
}
