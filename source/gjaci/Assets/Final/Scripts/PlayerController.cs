using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    // Public references.
    public float baseSpeed = 3.0f;
    public float rotateSpeed = 3.0f;
    public float momentum = 0.0f;

    // Private references
    private CharacterController _controller;

    // Update the player.
    void Update()
    {
        HandleInput();
        HandleMomentum(momentum);
    }


    // Handle the player input.
    void HandleInput()
    {
        // We only need to check movement if input has been affected.
        if (Input.anyKey)
        {
            // Determine speed for this.
            float modifier = 1.0f;

            if (Input.GetAxis("Sprint") != 0.0f)
            {
                modifier = 1.5f;
            }

            // Handle movement based on initial input.
            HandleMovement(modifier);
        }
    }

    // Move the player.
    void HandleMovement(float modifier = 0.0f)
    {
        // Check if they have a character controller.
        _controller = _controller == null ? GetComponent<CharacterController>() : _controller;

        if (_controller != null)
        {
            // Rotate around y-axis.
            transform.Rotate(0, Input.GetAxis("Horizontal") * rotateSpeed, 0);

            // Movement.
            Vector3 forward = transform.TransformDirection(Vector3.forward);
            float speed = baseSpeed * modifier * Input.GetAxis("Vertical");
            momentum = speed;
            _controller.SimpleMove(forward * speed);
        }
    }

    void HandleMomentum(float _momentum = 0.0f)
    {
        // Check if they have a character controller.
        _controller = _controller == null ? GetComponent<CharacterController>() : _controller;

        if (_momentum > 0.1f)
        {
            // Movement.
            Vector3 forward = transform.TransformDirection(Vector3.forward);
            _controller.SimpleMove(forward * _momentum);
            momentum = _momentum * 0.85f;
        }
    }

}
