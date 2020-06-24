using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightController : MonoBehaviour
{

    private Player GetPlayer;
    private Vector2 vector;

    private Quaternion rotation;
    // Start is called before the first frame update
    void Start()
    {
        GetPlayer = FindObjectOfType<Player>();
        
    }

    // Update is called once per frame
    void Update()
    {
        //vector.Set(GetPlayer.GetComponent<>)
    }
}
