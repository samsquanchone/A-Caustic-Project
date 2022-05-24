using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public Camera playerCam;
    public Interactable focus;
    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        LeftMouseClick();

        if (Input.GetButtonDown("Fire2"))
        {
            RemoveFocus();
        }
    }

    public void LeftMouseClick()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Ray ray = playerCam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 1000))
            {
                Interactable interactable = hit.collider.GetComponent<Interactable>();
                if (interactable != null)
                {
                    Debug.Log("Clicked on " + interactable.name);
                    SetFocus(hit.collider.GetComponent<Interactable>());
                }
            }
        }
    }
    void SetFocus(Interactable newFocus)
    {
        if (newFocus != focus)
        {
            if (focus != null)
            {
                focus.onDeFocused();
            }
            focus = newFocus;
        }
        newFocus.onFocused(gameObject);
    }

    void RemoveFocus()
    {
        if (focus != null)
        {
            focus.onDeFocused();
        }
        focus = null;
    }
}
