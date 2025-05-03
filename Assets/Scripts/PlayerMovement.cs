    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public CharacterController controller;

    public float speed = 12f;

    public GameObject Panel;
    public Transform player;
    [Header("Character Layers")] 
    public LayerMask Matador;
    public LayerMask Woman;
    public LayerMask Napoleon;
    public LayerMask Chest;

    [Header("Near Booleans")] 
    public bool nearMatador = false;
    public bool nearWoman = false;
    public bool nearNapoleon = false;


    public bool isPanel = false;

    // Update is called once per frame
    void Update()
    {
        nearMatador = false;
        nearWoman = false;
        nearNapoleon = false;
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        if(isPanel)
        {
            x = 0;
            z = 0;
        }
        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * speed * Time.deltaTime);


        // Left Click
        if(Input.GetMouseButtonDown(0))
        {
            if(Physics.CheckSphere(player.position, 5f, Matador))
            { // Write code here for matador
                Debug.Log("NEAR MATADOR AND CLICKED");
                nearMatador = true;
            }


            if(Physics.CheckSphere(player.position, 5f, Woman))
            { // Write code here for Rebecca?
                Debug.Log("NEAR WOMAN AND CLICKED");
                nearWoman = true;
            }

            
            if(Physics.CheckSphere(player.position, 5f, Napoleon))
            { // Write code here for Napoleon
                Debug.Log("NEAR Napoleon AND CLICKED");
                nearNapoleon = true;
            }



            if(Physics.CheckSphere(player.position, 5f, Chest))
            {
                openPanel();
            }
        } 

        
    }


    public void openPanel()
    {
        if (Panel != null)
        {
            Panel.SetActive(true);
            isPanel = true;
        }
    }
}
