using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public float radius = 3f;
    public Transform interactionTransform;

    bool isFocus = false;
    Transform player;

    bool hasInteracted = false;

    

    private void Update()
    {
        if (isFocus && !hasInteracted) // Check theres nothing focused and hasnt interacted
        {
            float distance = Vector3.Distance(player.position, interactionTransform.position); // Getting distance from player to interactable
            if (distance <= radius) // compare distance to interactable range (radius)
            { 
                // Can interact so call the objects interact
                hasInteracted = true; 
                interact();
            }
            //else Debug.Log("Out of range");
        }
    }
    public virtual void interact()
    {
        // This is meant to be overidden
        //Debug.Log("Interacted with " + transform.name);
    }

    public void onFocused(Transform playerTransform)
    {
        isFocus = true;
        player = playerTransform;
        hasInteracted = false;
    }

    public void onDeFocused()
    {
        isFocus = false;
        player = null;
        hasInteracted = false;
    }

    private void OnDrawGizmosSelected()
    {
        // to display interactable range in editor
        if(interactionTransform == null)
        {
            interactionTransform = transform;
        }
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(interactionTransform.position, radius);
    }
}
