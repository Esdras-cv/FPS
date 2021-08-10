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
    public float deathDistance = 3;

    bool isGrounded;
    Vector3 velocity;
    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if(isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float X = Input.GetAxis("Horizontal");
        float Z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * X + transform.forward * Z;

        controller.Move(move * speed * Time.deltaTime);

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);

        if(Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpForce * -2f * gravity);
        }

        if(!isGrounded)
        {
            RaycastHit hit;
            Physics.Raycast(groundCheck.position, -transform.up, out hit, 100f);

            if(hit.distance > deathDistance)
            {
                FindObjectOfType<GameController>().OnFall();
            }
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
