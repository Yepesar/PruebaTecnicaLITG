using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSPlayerController : MonoBehaviour
{
    [SerializeField] private Transform camera;
    [SerializeField] private float movementSpeed;
    [SerializeField] private float jumpForce;
    [SerializeField] private Transform gun;

    private InputManager inputManager;

    // Start is called before the first frame update
    void Start()
    {
        inputManager = InputManager.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 movement = inputManager.GetPlayerMovement();
        Debug.Log(movement);
        Move(new Vector3(movement.x, 0f, movement.y));

    }

    private void Move(Vector3 direction)
    {
        if ( direction.z != 0)
        {
            if (direction.z > 0)
            {
                transform.position = Vector3.MoveTowards(transform.position, transform.position + transform.forward, Time.deltaTime * movementSpeed);
            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, transform.position + (-transform.forward), Time.deltaTime * movementSpeed);
            }        
        }

        if (direction.x != 0)
        {
            if (direction.x > 0)
            {
                transform.position = Vector3.MoveTowards(transform.position, transform.position + transform.right, Time.deltaTime * movementSpeed);
            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, transform.position + (- transform.right), Time.deltaTime * movementSpeed);
            }
        }
    }
}
