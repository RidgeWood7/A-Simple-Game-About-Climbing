using UnityEngine;
using UnityEngine.InputSystem;

public class HandPlacement : MonoBehaviour
{
    //Target for the hand when the hand is not gripping something
    public Transform pointAOpen;
    public Transform pointBOpen;

    //Target for the hand when the hand is gripping something
    public Transform pointAClose;
    public bool gripping;

    //positions
    private float _positionX;
    private float _positionY;

    //mouse movement components
    Vector2 pos = new Vector2(0f, 3f);
    public Transform targetsEmpty;
    public float sensitivity = .5f;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
    private void Update()
    {
        if (pointAOpen != null && pointBOpen != null && !gripping)
        {
            //calculate x position
            _positionX = pointAOpen.position.x;

            //calculate y position
            _positionY = pointAOpen.position.y;

            //setting the position of the part
            transform.position = new Vector2(_positionX, _positionY);
        }
        else if (pointAClose != null && pointBOpen != null && gripping)
        {
            //calculate x position
            _positionX = pointAClose.position.x;

            //calculate y position
            _positionY = pointAClose.position.y;

            //setting the position of the part
            transform.position = new Vector2(_positionX, _positionY);
        }

        //Move the position of the targets
        if (targetsEmpty != null)
        {
            pos.x += Input.GetAxis("Mouse X") * sensitivity;
            pos.y += Input.GetAxis("Mouse Y") * sensitivity;
            targetsEmpty.position = Vector2.ClampMagnitude(pos, 4f);
            pos = Vector2.ClampMagnitude(pos, 4f);
            targetsEmpty.position = Vector2.ClampMagnitude(pos, 4f);
            Debug.Log(pos);
            Debug.Log(targetsEmpty);
            Mouse.current.WarpCursorPosition(targetsEmpty.position);
        }
    }
}
