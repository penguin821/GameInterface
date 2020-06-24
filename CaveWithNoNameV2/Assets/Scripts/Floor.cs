using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floor : MonoBehaviour
{
    private BoxCollider2D collider;
    private int check = 0;

    // Start is called before the first frame update
    void Start()
    {
        collider = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        if (true)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, new Vector2(0.0001f, 0.0f), 0.0001f);
            if (hit)
            {
                if (hit.collider.gameObject.tag == "Soda")
                {
                    collider.enabled = false;
                }
                else if (hit.collider.gameObject.tag == "Exit")
                {
                    collider.enabled = false;
                }
                else if (hit.collider.gameObject.tag == "Food")
                {
                    collider.enabled = false;
                }
                else if (hit.collider.gameObject.tag == "Wall")
                {
                    collider.enabled = false;
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Exit")
        {
            collider.enabled = true;
        }
        if (other.tag == "Soda")
        {
            collider.enabled = true;
        }
        if (other.tag == "Food")
        {
            collider.enabled = true;
        }
        if (other.tag == "Wall")
        {
            collider.enabled = true;
        }
    }
}