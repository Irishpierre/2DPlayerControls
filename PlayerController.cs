using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    //Var
    [SerializeField] private float speed = 10.0f;
    [SerializeField] float jumpSpeed = 5.0f;
    [SerializeField] private LayerMask ground;

    //Gain access to user controllers
    private PlayerActionControls playerActionControls;
    private Rigidbody2D playerRb;
    private Collider2D playerColl;


    //Run before Start
    private void Awake()
    {
        playerActionControls = new PlayerActionControls();
        playerRb = GetComponent<Rigidbody2D>();
        playerColl = GetComponent<Collider2D>();
    }

    private void OnEnable()
    {
        playerActionControls.Enable();
    }

    private void OnDisable()
    {
        playerActionControls.Disable();
    }

    // Start is called before the first frame update
    void Start()
    {
        playerActionControls.Land.Jump.performed += _ => Jump();
    }

    private void Jump()
    {
        if (IsGrounded())
        {
            playerRb.AddForce(new Vector2(0, jumpSpeed), ForceMode2D.Impulse);
        }
    }

    private bool IsGrounded()
    {
        Vector2 topLeftPoint = transform.position;
        topLeftPoint.x -= playerColl.bounds.extents.x;
        topLeftPoint.y += playerColl.bounds.extents.y;

        Vector2 bottomRightPoint = transform.position;
        bottomRightPoint.x += playerColl.bounds.extents.x;
        bottomRightPoint.y -= playerColl.bounds.extents.y;
        return Physics2D.OverlapArea(topLeftPoint, bottomRightPoint, ground);
    }


    // Update is called once per frame
    void Update()
    {
        // Read the movement value
        float movementInput = playerActionControls.Land.Move.ReadValue<float>();
        // Move the player
        Vector3 currentPos = transform.position;
        currentPos.x += movementInput * speed * Time.deltaTime;
        transform.position = currentPos;

    }
}
