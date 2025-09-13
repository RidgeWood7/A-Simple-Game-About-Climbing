using UnityEngine;

public class HandPlacement : MonoBehaviour
{
    //Target for the hand when the hand is not gripping something
    public Transform pointAOpen;
    public Transform pointBOpen;

    //Target for the hand when the hand is gripping something
    public Transform pointAClose;
    public bool gripping;

    private float _positionX;
    private float _positionY;



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
    }
}
