    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public CharacterController controller;

    public float speed = 12f;


    public Transform player;
    [Header("Character Layers")] 
    public LayerMask Matador;
    public LayerMask Woman;
    public LayerMask Napoleon;


    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * speed * Time.deltaTime);


        // Left Click
        if(Input.GetMouseButtonDown(0))
        {
            if(Physics.CheckSphere(player.position, 5f, Matador))
            { // Write code here for matador
                Debug.Log("NEAR MATADOR AND CLICKED");
            }

            if(Physics.CheckSphere(player.position, 5f, Woman))
            { // Write code here for Rebecca?
                Debug.Log("NEAR WOMAN AND CLICKED");
            }
            
            if(Physics.CheckSphere(player.position, 5f, Napoleon))
            { // Write code here for Napoleon
                Debug.Log("NEAR Napoleon AND CLICKED");
            }
        } 

        
    }
}
