using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private Animator _animator; // Reference to the Animator component

    private void Update()
    {
        Move();
        HandleAnimation();
    }

    protected virtual void Move()
    {
        // Get input from WASD keys
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        // Calculate the movement direction
        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical).normalized;

        // Move the player if there's any input
        if (movement != Vector3.zero)
        {
            transform.position += movement * _speed * Time.deltaTime;
        }
    }

    private void HandleAnimation()
    {
        // Check if there's any movement input
        bool isWalking = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) ||
                         Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D);

        // Set the animator parameter based on whether the player is walking or not
        _animator.SetBool("isWalking", isWalking);
    }
}
