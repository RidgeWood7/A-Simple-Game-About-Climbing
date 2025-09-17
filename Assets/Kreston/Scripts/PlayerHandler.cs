using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerHandler : MonoBehaviour
{
    //mouse setting
    private void Start() {Cursor.lockState = CursorLockMode.Locked; Cursor.visible = false;}
    private void OnApplicationFocus(bool focus) {if (focus){Cursor.lockState = CursorLockMode.Locked; Cursor.visible = false;}}


    //Right Hand Variables
    private Vector2 velRight;
    public Rigidbody2D rbRight;
    public GameObject shoulderRight;

    //Left Hand Variables
    private Vector2 velLeft;
    public Rigidbody2D rbLeft;
    public GameObject shoulderLeft;

    //other
    private bool _grabbing;

    private void Update()
    {
        //I need to add the clamp from the shoulder to the hand here (could call a function)
    }

    public void MoveHands(InputAction.CallbackContext ctx)
    {
        //Calculating the velocity before applying it (so I can apply it as posative or negative later)
        velRight += ctx.ReadValue<Vector2>() * 0.03f;
        velLeft += ctx.ReadValue<Vector2>() * 0.03f;

        //Right Hand
        if (!_grabbing)
        {
            if((rbRight.transform.position - shoulderRight.transform.position).magnitude > 3)
            {

            }
            else
            {
                rbRight.linearVelocity = velRight;
            }

        }
        else
        {
            rbRight.linearVelocity = velRight * -1;
        }

        //Left Hand
        if (!_grabbing)
        {
            rbLeft.linearVelocity = velLeft;
        }
        else
        {
            rbLeft.linearVelocity = velLeft * -1;
        }
    }

    public void GrabRight(InputAction.CallbackContext ctx)
    {

    }

    public void GrabLeft(InputAction.CallbackContext ctx)
    {

    }
}
