using UnityEngine;
using System.Collections;
using System;

public class PlayerController : MonoBehaviour
{
    public bool canMove = true;
    private bool jumpPowerCooldown = true;

    [Range(1, 10)]
    public float moveSpeed = 3f;
    [Range(1, 10)]
    public float jumpVelocity = 6f;
    [Range(1, 10)]
    public float fallMultiplier = 3f;
    [Range(1, 10)]
    public float lowJumpMultiplier = 2f;
    [Header("Weapon")]
    public int currWeap = 0;
    public LayerMask groundLayer;

    Rigidbody2D rb;
    Vector3 movement;
    [SerializeField]
    bool isGrounded = false;
    private bool isFacingRight = true;
    
    private PlayerAnimatorController playerAnimator;

    public void TakeDamage(float meleeDamage)
    {
        throw new NotImplementedException();
    }

    private Vector3 mousePos;

    //0 Jump, 1 Time, 2 Shield.
    public int activePortal = 0;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<PlayerAnimatorController>();
    }
    

    void Update()
    {
        movement.x = Input.GetAxis("Horizontal");
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (Input.GetMouseButtonDown(1))
        {
            currWeap = 1;
        }
        else if (Input.GetMouseButtonUp(1))
        {
            currWeap = 0;
        }

        if (currWeap != 0)
        {
            if (transform.position.x > mousePos.x && isFacingRight || transform.position.x < mousePos.x && !isFacingRight)
            {
                Flip();
            }
        }
        else
        {
            if (movement.x > 0 && !isFacingRight || movement.x < 0 && isFacingRight)
            {
                Flip();
            }
        }

        if (canMove == true)
        {
            if (Input.GetButtonDown("Jump") && isGrounded == true)
            {
                GetComponent<Rigidbody2D>().velocity = Vector2.up * jumpVelocity;
                playerAnimator.Jump(true);
            }
        }
        
        //Just for test, when we have a proper death mechanic change it. Right now it is just to show player death.
        if (Input.GetKeyDown(KeyCode.H))
        {
            playerAnimator.Death();
        }

        if (Input.GetKeyDown(KeyCode.E) && jumpPowerCooldown == true)
        {
            jumpVelocity = 10.0f;
            jumpPowerCooldown = false;
            playerAnimator.JumpPortal();
            FMODUnity.RuntimeManager.PlayOneShot("event:/FX/Portal");
            StartCoroutine(ResetJumpAtribute());
            StartCoroutine(JumpPortalRoutine());
        }


        if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.Tab))
        {
            activePortal++;
            if (activePortal > 2)
            {
                activePortal = 0;
            }
            Debug.Log(activePortal);
        }
    }

    private void FixedUpdate()
    {
        if (canMove == true)
        {
            transform.position += movement * moveSpeed * Time.fixedDeltaTime;

            if (rb.velocity.y < 0)
            {
                rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
            }
            else if (rb.velocity.y > 0 && !Input.GetButton("Jump"))
            {
                rb.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
            }

            CheckGround();

            playerAnimator.Move(movement.x != 0);
            int m = 1;
            if (isFacingRight && movement.x < 0 || !isFacingRight && movement.x > 0)
            {
                m = -1;
            }
            playerAnimator.Flip(m);
        }
    }

    private void CheckGround()
    {
        if ((Physics2D.OverlapCircle(transform.GetChild(0).position, 0.2f, groundLayer) != null ))
        {
            if(isGrounded == false)
            {
                playerAnimator.Jump(false);
                FMODUnity.RuntimeManager.PlayOneShot("event:/FX/Land");
            }
            isGrounded =  true;
        }
        else if(isGrounded == true)
        {
            isGrounded =  false;
        }
    }

    private void Flip()
    {
        isFacingRight = !isFacingRight;
        transform.Rotate(0f, 180f, 0f);
    }

    private IEnumerator ResetJumpAtribute()
    {
        yield return new WaitForSeconds(10.0f);
        jumpVelocity = 6.0f;
    }

    private IEnumerator JumpPortalRoutine()
    {
        yield return new WaitForSeconds(20.0f);
        jumpPowerCooldown = true;
    }
}
