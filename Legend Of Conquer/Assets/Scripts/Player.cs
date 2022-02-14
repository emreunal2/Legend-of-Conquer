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
        float moveSpeed = 10;
        float horizontalMovement = Input.GetAxisRaw("Horizontal");
        float verticalMovement = Input.GetAxisRaw("Vertical");
        playerRigidbody.velocity = new Vector2(horizontalMovement*moveSpeed, verticalMovement*moveSpeed);

        playerAnimator.SetFloat("MovementX", playerRigidbody.velocity.x);
        playerAnimator.SetFloat("MovementY", playerRigidbody.velocity.y);
    }
}
