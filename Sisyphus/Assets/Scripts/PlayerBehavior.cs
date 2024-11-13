using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField]
    int playerSpeed;

    [Header("Components")]
    public Rigidbody rig;
    private void Start()
    {
        
    }
    private void Update()
    {
        Move();
    }
    void Move()
    {
        if (rig != null) 
        {
            float z = Input.GetAxis("Horizontal") * playerSpeed;
            float x = Input.GetAxis("Vertical") * playerSpeed;
            rig.velocity = new Vector3(-x, rig.velocity.y, z);
        }
    }
}
