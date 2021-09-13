using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayer : MonoBehaviour
{
    public CharacterController controller;
    public float speed = 12f;
    public float gravity = -9.81f;
    public float jumpForce = 3f;
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    public float distMaxQueda = 3f;

    Vector3 velocity;
    private float ultimaPosY, distQueda;
    void Start()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        ultimaPosY = controller.transform.position.y;

        if(ultimaPosY > controller.transform.position.y && controller.velocity.y < 0)
        {
            distQueda += ultimaPosY - controller.transform.position.y;
        }

        if(distQueda >= distMaxQueda && controller.isGrounded)
        {
            FindObjectOfType<GameController>().OnFall();
        }

        if(controller.isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float X = Input.GetAxis("Horizontal");
        float Z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * X + transform.forward * Z;

        controller.Move(move * speed * Time.deltaTime);

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);

        if(Input.GetButtonDown("Jump") && controller.isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpForce * -2f * gravity);
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Portal"))
        {
            FindObjectOfType<GameController>().NextFase();
        }
        else if(other.gameObject.CompareTag("PlatMovel"))
        {
            transform.SetParent(other.transform);
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if(other.gameObject.CompareTag("PlatMovel"))
        {
            transform.SetParent(null);
        }
    }
}
