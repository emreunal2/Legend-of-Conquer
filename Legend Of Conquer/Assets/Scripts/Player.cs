using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] Rigidbody2D playerRigidbody;
    [SerializeField] Animator playerAnimator;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        float moveSpeed = 5;
        // Getting inputs
        float horizontalMovement = Input.GetAxisRaw("Horizontal");
        float verticalMovement = Input.GetAxisRaw("Vertical");
        
        // Remove dioganal move
        if (horizontalMovement != 0) verticalMovement = 0;

        playerRigidbody.velocity = new Vector2(horizontalMovement*moveSpeed, verticalMovement*moveSpeed);

        // Send input infos to animation
        playerAnimator.SetFloat("MovementX", playerRigidbody.velocity.x);
        playerAnimator.SetFloat("MovementY", playerRigidbody.velocity.y);

        // Setting walking animation 
        if (horizontalMovement == 1 || horizontalMovement == -1 || verticalMovement == 1 || verticalMovement == -1)
        {
            playerAnimator.SetFloat("lastX", horizontalMovement);
            playerAnimator.SetFloat("lastY", verticalMovement);
        }

    }
}
