using System.Collections;
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
    private bool _grabbingRight;
    public HingeJoint2D hingeRight;
    private bool _callingRight;

    //Left Hand Variables
    private Vector2 velLeft, targetVelLeft;
    public Rigidbody2D rbLeft;
    public GameObject shoulderLeft;
    private bool _grabbingLeft;
    public HingeJoint2D hingeLeft;
    private bool _callingLeft;


    //other
    public float reachRange;
    public Rigidbody2D rbBody;
    public float castRadius;
    public LayerMask grabbableLayer;
    public float linearDampingUp;
    public float linearDampingDown;
    public float maxVelocity = 25f;
    private bool _bouncing;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Jumper"))
        {
            StartCoroutine(BounceTimer());
        }
    }

    private IEnumerator BounceTimer()
    {
        Debug.Log("Bounce");
        _bouncing = true;
        yield return new WaitForSeconds(5f);
        _bouncing = false;
        Debug.Log("Not Bouncing");
    }

    private void Update()
    {
        velLeft = Vector2.Lerp(velLeft, targetVelLeft, Time.deltaTime * 50);
        velRight = Vector2.Lerp(velRight, targetVelRight, Time.deltaTime * 50);


        //right
        if (_callingRight)
        {
            if (Physics2D.CircleCast(rbRight.position, castRadius, Vector2.zero, 1, grabbableLayer) && (rbLeft.transform.position - shoulderLeft.transform.position).magnitude < reachRange && (rbRight.transform.position - shoulderRight.transform.position).magnitude < reachRange)
            {
                _grabbingRight = true; hingeRight.enabled = true;
            }
            else { hingeRight.enabled = false; _grabbingRight = false; _callingRight = false; }
        }
        else
        {
            _grabbingRight = false;
            hingeRight.enabled = false;
        }
        //left
        if (_callingLeft)
        {
            if (Physics2D.CircleCast(rbLeft.position, castRadius, Vector2.zero, 1, grabbableLayer) && (rbLeft.transform.position - shoulderLeft.transform.position).magnitude < reachRange && (rbRight.transform.position - shoulderRight.transform.position).magnitude < reachRange)
            {
                _grabbingLeft = true; hingeLeft.enabled = true;
            }
            else { hingeLeft.enabled = false; _grabbingLeft = false; _callingLeft = false; }
        }
        else
        {
            _grabbingLeft = false;
            hingeLeft.enabled = false;
        }
    }

    private void FixedUpdate()
    {
        //Velocity Cap
        if (!_bouncing)
        {
            rbBody.linearVelocity = Vector2.ClampMagnitude(rbBody.linearVelocity, maxVelocity);
        }

        //Right Hand
        if (!_grabbingRight)
        {
            if ((rbRight.transform.position - shoulderRight.transform.position).magnitude > reachRange)
            {
                Vector3 clampedPos = shoulderRight.transform.position + (rbRight.transform.position - shoulderRight.transform.position).normalized * reachRange;
                rbRight.MovePosition(clampedPos);

                rbRight.linearVelocity = Vector3.zero;
            }
            rbRight.linearVelocity = velRight;
        }
        else
        {
            rbBody.linearVelocity = velRight * .2f;

            //if ((rbBody.transform.position - rbRight.transform.position).magnitude > reachRange)
            //{
            //    Vector3 clampedPos = rbRight.transform.position + (rbBody.transform.position - rbRight.transform.position).normalized * reachRange;
            //    rbBody.MovePosition(clampedPos);
            //}
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
            rbLeft.linearVelocity = velLeft;
        }
        else
        {
            rbBody.linearVelocity = velLeft * .2f;

            //if ((rbBody.transform.position - rbLeft.transform.position).magnitude > reachRange)
            //{
            //    Vector3 clampedPos = rbLeft.transform.position + (rbBody.transform.position - rbLeft.transform.position).normalized * reachRange;
            //    rbBody.MovePosition(clampedPos);
            //}
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(rbRight.position, shoulderRight.transform.position);
        Gizmos.DrawLine(rbLeft.position, shoulderLeft.transform.position);

        Gizmos.DrawWireSphere(shoulderRight.transform.position, reachRange);
        Gizmos.DrawWireSphere(shoulderLeft.transform.position, reachRange);

        Gizmos.DrawWireSphere(rbRight.position, castRadius);
        Gizmos.DrawWireSphere(rbLeft.position, castRadius);
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
            _callingRight = true;
        }else
        {
            _callingRight = false;
        }

        //if (ctx.ReadValue<float>() == 1)
        //{
        //    if (Physics2D.CircleCast(rbRight.position, castRadius, Vector2.zero, 1, grabbableLayer) && (rbLeft.transform.position - shoulderLeft.transform.position).magnitude < reachRange && (rbRight.transform.position - shoulderRight.transform.position).magnitude < reachRange)
        //    {
        //        _grabbingRight = true; hingeRight.enabled = true;
        //    }
        //    else { hingeRight.enabled = false; _grabbingRight = false; }
        //}
        //else if (ctx.ReadValue<float>() == 0)
        //{
        //    _grabbingRight = false;
        //    hingeRight.enabled = false;
        //}
    }

    public void GrabLeft(InputAction.CallbackContext ctx)
    {
        //click = 1 not clicking = 0

        if (ctx.ReadValue<float>() == 1)
        {
            _callingLeft = true;
        }
        else
        {
            _callingLeft = false;
        }

        //if (ctx.ReadValue<float>() == 1)
        //{
        //    if (Physics2D.CircleCast(rbRight.position, castRadius, Vector2.zero, 1, grabbableLayer) && (rbLeft.transform.position - shoulderLeft.transform.position).magnitude < reachRange && (rbRight.transform.position - shoulderRight.transform.position).magnitude < reachRange)
        //    {
        //        _grabbingLeft = true; hingeLeft.enabled = true;
        //    }
        //    else { hingeLeft.enabled = false; _grabbingLeft = false; }
        //}
        //else if (ctx.ReadValue<float>() == 0)
        //{
        //    _grabbingLeft = false;
        //    hingeLeft.enabled = false;
        //}
    }
}
