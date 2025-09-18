using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerHandler : MonoBehaviour
{
    //mouse setting
    private void Start() {Cursor.lockState = CursorLockMode.Locked; Cursor.visible = false;}
    private void OnApplicationFocus(bool focus) {if (focus){Cursor.lockState = CursorLockMode.Locked; Cursor.visible = false;}}


    //Right Hand Variables
    private Vector2 velRight, targetVelRight;
    public Rigidbody2D rbRight;
    public GameObject shoulderRight;

    //Left Hand Variables
    private Vector2 velLeft, targetVelLeft;
    public Rigidbody2D rbLeft;
    public GameObject shoulderLeft;

    public float reachRange;

    //other
    private bool _grabbing;

    private void Update()
    {
        velLeft = Vector2.Lerp(velLeft, targetVelLeft, Time.deltaTime * 50);
        velRight = Vector2.Lerp(velRight, targetVelRight, Time.deltaTime * 50);
    }

    private void FixedUpdate()
    {
        //I need to add the clamp from the shoulder to the hand here (could call a function)

        //Right Hand
        if (!_grabbing)
        {
            if ((rbRight.transform.position - shoulderRight.transform.position).magnitude > reachRange)
            {
                Vector3 clampedPos = shoulderRight.transform.position + (rbRight.transform.position - shoulderRight.transform.position).normalized * reachRange;
                rbRight.MovePosition(clampedPos);

                rbRight.linearVelocity = Vector3.zero;
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
            if ((rbLeft.transform.position - shoulderLeft.transform.position).magnitude > reachRange)
            {
                Vector3 clampedPos = shoulderRight.transform.position + (rbLeft.transform.position - shoulderLeft.transform.position).normalized * reachRange;
                rbLeft.MovePosition(clampedPos);

                rbLeft.linearVelocity = Vector3.zero;
            }
            else
            {
                rbLeft.linearVelocity = velLeft;
            }

        }
        else
        {
            rbLeft.linearVelocity = velLeft * -1;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(rbRight.position, shoulderRight.transform.position);
        Gizmos.DrawLine(rbLeft.position, shoulderLeft.transform.position);

        Gizmos.DrawWireSphere(shoulderRight.transform.position, reachRange);
        Gizmos.DrawWireSphere(shoulderLeft.transform.position, reachRange);
    }

    public void MoveHands(InputAction.CallbackContext ctx)
    {
        //Calculating the velocity before applying it (so I can apply it as posative or negative later)
        targetVelRight = ctx.ReadValue<Vector2>() * 20;
        targetVelLeft = ctx.ReadValue<Vector2>() * 20;
    }

    public void GrabRight(InputAction.CallbackContext ctx)
    {

    }

    public void GrabLeft(InputAction.CallbackContext ctx)
    {

    }
}
