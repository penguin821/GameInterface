using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckTiles : MonoBehaviour
{
    public SpriteRenderer checkTile;

    private bool exit = false;
    private Transform target;

    float time = 0;
    float blinktime = 0.2f;
    float xtime = 0;
    float waittime = 0.2f;

    float r, g, b, z;

    // Start is called before the first frame update
    void Start()
    {
        checkTile = GetComponent<SpriteRenderer>();

    }

    // Update is called once per frame
    void Update()
    {
        if (time < 0.5f) // 버프 지속시간 -3초
        {
            checkTile.color = new Color(r, g, b, z); // 처음엔 켜져있고
        }
        else // 약 3초
        {
            if (xtime < blinktime) // 깜빡
            {
                checkTile.color = new Color(r, g, b, z - xtime * 2.5f); //꺼졌다가
            }
            else if (xtime < waittime + blinktime)
            {

            }
            else
            {
                checkTile.color = new Color(r, g, b, (xtime - (waittime + blinktime)) * 2.5f);
                //켜졌다가
                if (xtime > waittime + blinktime * 2)
                {
                    xtime = 0;
                }
            }
            xtime += Time.deltaTime;
        }
        time += Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Exit")
        {
            r = 0f;
            g = 1f;
            b = 1f;
            z = 0.2f;
            checkTile.color = new Color(r, g, b, z);
        }
        else if (other.tag == "Food")
        {
            r = 0f;
            g = 1f;
            b = 1f;
            z = 0.2f;
            checkTile.color = new Color(r, g, b, z);
        }
        else if (other.tag == "Battery")
        {
            r = 0f;
            g = 1f;
            b = 1f;
            z = 0.2f;
            checkTile.color = new Color(r, g, b, z);
        }
        else if (other.tag == "Soda")
        {
            r = 0f;
            g = 1f;
            b = 1f;
            z = 0.2f;
            checkTile.color = new Color(r, g, b, z);
        }
        else if (other.tag == "Wall")
        {
            r = 1f;
            g = 0.5f;
            b = 0f;
            z = 0.2f;
            checkTile.color = new Color(r, g, b, z);
        }
        else if (other.tag == "OuterWall")
        {
            r = 1f;
            g = 0f;
            b = 0f;
            z = 0.5f;
            checkTile.color = new Color(r, g, b, z);
        }
        else if (other.tag == "Enemy")
        {
            r = 1f;
            g = 0f;
            b = 0f;
            z = 0.2f;
            checkTile.color = new Color(r, g, b, z);
        }
        else if (other.tag == "Floor")
        {
            r = 1f;
            g = 1f;
            b = 1f;
            z = 0.2f;
            checkTile.color = new Color(r, g, b, z);
        }
    }
}
