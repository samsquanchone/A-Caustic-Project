using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public CharacterController controller;   
    public float moveSpeed = 6f;
    public Camera playerCam;
    float gravity;
    float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;
    public float distanceFromGround;

    public Interactable focus;

    public void Start()
    {
        controller = GetComponent<CharacterController>();
        distanceFromGround = GetComponent<Collider>().bounds.extents.y;
    }
    public void Update()
    {
        PlayerMove();

        LeftMouseClick();

        if(Input.GetButtonDown("Fire2"))
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

            if (Physics.Raycast(ray, out hit, 100))
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
        newFocus.onFocused(transform);
    }

    void RemoveFocus()
    {
        if(focus != null)
        {
            focus.onDeFocused();
        }
        focus = null;
    }

    void PlayerMove()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Vector3 dir = new Vector3(horizontal, 0, vertical);
        float targetAngle = Mathf.Atan2(dir.x, dir.z) * Mathf.Rad2Deg;
        float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);

        if (dir.magnitude != 0)
        {
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
            controller.Move(dir * moveSpeed * Time.deltaTime);
        }

        gravity -= (float)9.81 * Time.deltaTime;
        dir.y = gravity;
        controller.Move(dir * Time.deltaTime);
        if (controller.isGrounded) gravity = 0;
    }
}
