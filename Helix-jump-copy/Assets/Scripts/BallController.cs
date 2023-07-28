using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class BallController : MonoBehaviour
{
    [Header("Jump Settings")]
    [SerializeField] private float jumpForce;
    private bool canJump;

    [Header("Gravity Settings")]
    [SerializeField] private float gravity;
    
    [SerializeField] private GameManager gm;
    [SerializeField] public UnityEvent newScore;
    private Rigidbody rb;
    

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
        canJump = true;
    }

    private void FixedUpdate()
    {
        Gravity();
    }
    
    void Gravity()
    {
        Vector3 gravityScale = gravity * Physics.gravity;
        rb.AddForce(gravityScale, ForceMode.Acceleration);
    }

    private void Jump()
    {
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            if(canJump)
            {
                Jump();
                canJump = false;
                StartCoroutine(WaitForTheJump());
            }
        }

        if(collision.gameObject.CompareTag("Obstacle"))
        {
            SceneManager.LoadScene(0);
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Score"))
        {
            gm.score++;
            newScore.Invoke();
            Destroy(other.gameObject);
        }
    }
    
    IEnumerator WaitForTheJump()
    {
        yield return new WaitForSeconds(0.1f);
        canJump = true;
    }
}
