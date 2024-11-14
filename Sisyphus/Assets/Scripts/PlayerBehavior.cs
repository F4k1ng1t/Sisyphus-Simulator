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
    public Camera cam;
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
            float x = Input.GetAxis("Horizontal") * playerSpeed;
            float z = Input.GetAxis("Vertical") * playerSpeed;
            //rig.velocity = new Vector3(x, rig.velocity.y, -z);
            Vector3 localMove = new Vector3(x, 0, z);

            // Transform the local movement to world space based on the character's facing direction
            Vector3 worldMove = transform.TransformDirection(localMove);

            // Apply the velocity, keeping the current Y velocity
            rig.velocity = new Vector3(worldMove.x, rig.velocity.y, worldMove.z);
        }
    }
}
