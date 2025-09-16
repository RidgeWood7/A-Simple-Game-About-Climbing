using UnityEngine;

public class PositioningandRotation : MonoBehaviour
{
    public GameObject part;
    public float speed;
    public float rotationModifier;

    public Transform pointA;
    public Transform pointB;

    private float _positionX;
    private float _positionY;

    private void FixedUpdate()
    {
        if (part != null)
        {
            Vector3 vectorToTarget = part.transform.position - transform.position;
            float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg - rotationModifier;
            Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * speed);
        }

    }

    void Update()
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
