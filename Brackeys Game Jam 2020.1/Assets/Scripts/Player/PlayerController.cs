using UnityEngine;
using System.Collections;
using System;

public class PlayerController : MonoBehaviour
{
    public bool canMove = true;
    
    private bool jumpPowerCooldown = true;
    private bool shieldPowerCooldown = true;
    private bool speedPowerCooldown = true;
    [Range(1, 10)]
    public float jumpPowerUpTime;
    [Range(5, 30)]
    public float jumpPowerUpResetTime;
    [Range(1, 10)]
    public float shieldPowerUpTime;
    [Range(5, 30)]
    public float shieldPowerUpResetTime;
    [Range(1, 10)]
    public float speedPowerUpTime;
    [Range(5, 30)]
    public float speedPowerUpResetTime;

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

    private Vector3 mousePos;

    //0 Jump, 1 Time, 2 Shield.
    public int activePortal = 0;

    [SerializeField]
    private ChangeColor changeColor;

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


        if (Input.GetKeyDown(KeyCode.E))
        {
            //Remember the jump into portal animation takes about 1.5s
            if (activePortal == 0 && jumpPowerCooldown == true)
            {
                jumpVelocity = 10.0f;
                jumpPowerCooldown = false;
                playerAnimator.JumpPortal();
                FMODUnity.RuntimeManager.PlayOneShot("event:/FX/Portal");
                StartCoroutine(ResetJumpAtribute());
                StartCoroutine(JumpPortalRoutine());
            }else if (activePortal == 1 && shieldPowerCooldown == true)
            {
                //Insert Shield Hability Method
                shieldPowerCooldown = false;
                playerAnimator.JumpPortal();
                FMODUnity.RuntimeManager.PlayOneShot("event:/FX/Portal");
                //Inset coroutine of when shield will fade.... i recomend 3s
                StartCoroutine(ShieldPortalRoutine());
            }else if (activePortal == 2 && speedPowerCooldown == true)
            {
                moveSpeed = 6.0f;
                speedPowerCooldown = false;
                playerAnimator.JumpPortal();
                FMODUnity.RuntimeManager.PlayOneShot("event:/FX/Portal");
                StartCoroutine(ResetSpeedAtribute());
                StartCoroutine(SpeedPortalRoutine());
            }

        }


        if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.Tab))
        {
            activePortal++;
            if (activePortal > 2)
            {
                activePortal = 0;
            }
            changeColor.ChangePortalColor(activePortal);
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            activePortal = 0;
            changeColor.ChangePortalColor(activePortal);
        }else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            activePortal = 1;
            changeColor.ChangePortalColor(activePortal);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            activePortal = 2;
            changeColor.ChangePortalColor(activePortal);
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
        yield return new WaitForSeconds(jumpPowerUpTime + 1.5f);
        jumpVelocity = 6.0f;
    }

    private IEnumerator JumpPortalRoutine()
    {
        yield return new WaitForSeconds(jumpPowerUpResetTime);
        jumpPowerCooldown = true;
    }

    private IEnumerator ResetShieldAtribute()
    {
        yield return new WaitForSeconds(shieldPowerUpTime + 1.5f);
        //Reset Shield.
    }
    
    private IEnumerator ShieldPortalRoutine()
    {
        yield return new WaitForSeconds(shieldPowerUpResetTime);
        shieldPowerCooldown = true;
    }

    private IEnumerator ResetSpeedAtribute()
    {
        yield return new WaitForSeconds(speedPowerUpTime + 1.5f);
        moveSpeed = 3.0f;
    }

    private IEnumerator SpeedPortalRoutine()
    {
        yield return new WaitForSeconds(speedPowerUpResetTime);
        speedPowerCooldown = true;
    }


}
