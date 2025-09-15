using UnityEngine;

public class Positioning : MonoBehaviour
{
    public Transform pointA;
    public Transform pointB;

    private float _positionX;
    private float _positionY;

    private void Update()
    {
        if (pointA != null && pointB != null)
        {
            //calculate x position
            _positionX = (pointA.position.x + pointB.position.x) / 2;

            //calculate y position
            _positionY = (pointA.position.y + pointB.position.y) / 2;

            //setting the position of the part
            transform.position = new Vector2(_positionX, _positionY);
        }
    }
}
