using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody rb;
    private float hozInput, vertInput;
    [SerializeField] private float speed = 15.0f;
    [SerializeField] private float jumpForce = 15.0f;
    private bool isJumpButtonPressed = false;
    private bool isGrounded = false;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        hozInput = Input.GetAxis("Horizontal");
        vertInput = Input.GetAxis("Vertical");


        if (Input.GetKeyDown(KeyCode.Space))
        {
            isJumpButtonPressed = true;
        }
    }

    private void FixedUpdate()
    {
        Vector3 playerMovement = new Vector3(hozInput, 0, vertInput);
        playerMovement *= speed;
        rb.AddForce(playerMovement, ForceMode.Acceleration);

        Ray ray = new Ray(transform.position, Vector3.down);

        if (Physics.Raycast(ray, transform.localScale.x / 2f + 0.01f ))
        {
            isGrounded = true;
        }
        else
        {
            isGrounded=false;
        }

        if (isJumpButtonPressed && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isJumpButtonPressed=false;
        }
    }

    //private void OnCollisionEnter(Collision collision)
    //{
    //    isGrounded = true;
    //    Debug.Log("Enter!" + Time.realtimeSinceStartup);
    //}
    //private void OnCollisionExit(Collision collision)
    //{
    //    isGrounded = false;
    //    Debug.Log("Exit!" + Time.realtimeSinceStartup);
    //}
}
