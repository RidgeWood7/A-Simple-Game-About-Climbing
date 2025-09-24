using UnityEngine;

public class TeleportControl : MonoBehaviour
{
    public Transform destination;
    GameObject player;

    private void Awake()
    {
        player = GameObject.Find("Body");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Body"))
        {
            if(Vector2.Distance(player.transform.position,transform.position) > 0.3f)
            {
                player.transform.position = destination.transform.position;
            }
                
        }
    }
}
