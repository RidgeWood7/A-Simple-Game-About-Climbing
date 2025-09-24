using UnityEngine;

public class TeleporterThing : MonoBehaviour
{
    public GameObject otherTp;
    TeleporterThing otherTpComp;
    public bool canTeleport;
    SpriteRenderer spriteRenderer;

    void Start()
    {
        canTeleport = true;
        otherTpComp = otherTp.GetComponent<TeleporterThing>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (canTeleport == true)
        {
            spriteRenderer.color = Color.green;
        }
        else
        {
            spriteRenderer.color = Color.red;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && canTeleport)
        {
            otherTpComp.canTeleport = false;
            collision.transform.position = otherTp.transform.position;
            canTeleport = false;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            canTeleport = true;
        }
    }
}
