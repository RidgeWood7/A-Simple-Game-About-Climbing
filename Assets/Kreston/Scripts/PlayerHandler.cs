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
    private bool _clickingRight;
    private bool _grabbingRight;
    public HingeJoint2D jointRight;

    //Left Hand Variables
    private Vector2 velLeft, targetVelLeft;
    public Rigidbody2D rbLeft;
    public GameObject shoulderLeft;
    private bool _clickingLeft;
    private bool _grabbingLeft;
    public HingeJoint2D jointLeft;

    //other
    public float reachRange;
    public Rigidbody2D rbBody;
    public float castRadius;
    public LayerMask grabbableLayer;


    private void Update()
    {
        velLeft = Vector2.Lerp(velLeft, targetVelLeft, Time.deltaTime * 50);
        velRight = Vector2.Lerp(velRight, targetVelRight, Time.deltaTime * 50);
        
        if (_clickingRight)
        {
            ClickingRight();
        }
        if (_clickingLeft)
        {
            ClickingLeft();
        }
    }

    private void FixedUpdate()
    {
        //I need to add the clamp from the shoulder to the hand here (could call a function)

        //Right Hand
        if (!_grabbingRight)
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
            if ((rbRight.transform.position - shoulderRight.transform.position).magnitude > reachRange)
            {
                Vector3 clampedPos = shoulderRight.transform.position + (rbRight.transform.position - shoulderRight.transform.position).normalized * reachRange;
                rbRight.MovePosition(clampedPos);

                rbRight.linearVelocity = Vector3.zero;
            }
            else
            {
                rbBody.linearVelocity = velRight * .2f;
            }
        }

        //Left Hand
        if (!_grabbingLeft)
        {
            if ((rbLeft.transform.position - shoulderLeft.transform.position).magnitude > reachRange)
            {
                Vector3 clampedPos = shoulderLeft.transform.position + (rbLeft.transform.position - shoulderLeft.transform.position).normalized * reachRange;
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
            if ((rbLeft.transform.position - shoulderLeft.transform.position).magnitude > reachRange)
            {
                Vector3 clampedPos = shoulderRight.transform.position + (rbLeft.transform.position - shoulderLeft.transform.position).normalized * reachRange;
                rbLeft.MovePosition(clampedPos);

                rbLeft.linearVelocity = Vector3.zero;
            }
            else
            {
                rbBody.linearVelocity = velLeft * .2f;
            }
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
        //click = 1 not clicking = 0
        if (ctx.ReadValue<float>() == 1)
        {
            _clickingRight = true;
            _grabbingRight = true;
            ClickingRight();
        }
        else if (ctx.ReadValue<float>() == 0)
        {
            _grabbingRight = false;
        }
    }

    public void GrabLeft(InputAction.CallbackContext ctx)
    {
        //click = 1 not clicking = 0
        if (ctx.ReadValue<float>() == 1)
        {
            _clickingLeft = true;
            _grabbingLeft = true;
            ClickingLeft();
        }
        else if (ctx.ReadValue<float>() == 0)
        {
            _grabbingLeft= false;
        }
    }

    private void ClickingRight()
    {
        if (Physics2D.CircleCast(rbRight.position, castRadius, Vector2.zero, 1, grabbableLayer))
        {

        }
        else {  }
    }
    private void ClickingLeft()
    {
        if (Physics2D.CircleCast(rbLeft.position, castRadius, Vector2.zero, 1, grabbableLayer))
        {

        }
        else {  }
    }
}
