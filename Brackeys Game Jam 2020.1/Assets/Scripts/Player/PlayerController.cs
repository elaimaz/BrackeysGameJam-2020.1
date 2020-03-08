using UnityEngine;
using System.Collections;
using System;

public class PlayerController : MonoBehaviour
{
    public Collider2D coll;
    public bool canMove = true;
    
    [Range(1, 10)]
    public float defaultMoveSpeed = 3f;
    [HideInInspector]
    public float moveSpeed = 3f;
    [Range(1, 10)]
    public float defaultJumpVelocity = 6f;
    [HideInInspector]
    public float jumpVelocity;
    [Range(1, 10)]
    public float fallMultiplier = 3f;
    [Range(1, 10)]
    public float lowJumpMultiplier = 2f;
    [Header("Weapon")]
    public int currWeap = 0;
    public LayerMask groundLayer;

    Rigidbody2D rb;
    Vector3 movement;
    
//    [HideInInspector]
    public bool isGrounded = false;
    private bool isFacingRight = true;
    
    public PlayerAnimatorController playerAnimator;
    
    private Vector3 mousePos;
    
    //0 Jump, 1 Time, 2 Shield.
    public int activePortal = 0;

//    [SerializeField]
    public ChangeColor changeColor;
    public ChangeColor changeColorSecondary;

    private bool isAboveStari = false;
    private bool isFalling = false;

    public string BossMusicEvent = "event:/Music/BossMusic";
    FMOD.Studio.EventInstance bossMusic;

    public string LevelMusicEvent = "event:/Music/Area1Music";
    FMOD.Studio.EventInstance levelMusic;
    
    public GameObject GameOverPanel;
    
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<PlayerAnimatorController>();
        moveSpeed = defaultMoveSpeed;
        jumpVelocity = defaultJumpVelocity;
        levelMusic = FMODUnity.RuntimeManager.CreateInstance(LevelMusicEvent);
        bossMusic = FMODUnity.RuntimeManager.CreateInstance(BossMusicEvent);
        levelMusic.start();
    }
    

    void Update()
    {
        movement.x = Input.GetAxis("Horizontal") * (isFalling?0:1);
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

        if(Input.GetAxis("Vertical") < 0)
        {
            if(isAboveStari && isFalling == false)
            {
                coll.isTrigger = true;
                isFalling = true;
            }
        }
        
        //Just for test, when we have a proper death mechanic change it. Right now it is just to show player death.
        if (Input.GetKeyDown(KeyCode.H))
        {
            playerAnimator.Death();
        }
    }
    
    public void OnPortalJumpPowerUPActivated(){
        //When jump powerup is activated.
        changeColor.ChangePortalColor(0);
        changeColorSecondary.ChangePortalColor(0);
        playerAnimator.JumpPortal();
        FMODUnity.RuntimeManager.PlayOneShot("event:/FX/Portal");
        FMODUnity.RuntimeManager.PlayOneShot("event:/FX/PortalSwitch");
        
    }
    public void OnShieldPowerUPActivated(){
        //When shield powerup is activated.
        changeColor.ChangePortalColor(2);
        changeColorSecondary.ChangePortalColor(2);
        //Insert Shield Hability Method
        playerAnimator.JumpPortal();
        FMODUnity.RuntimeManager.PlayOneShot("event:/FX/Portal");
        //Inset coroutine of when shield will fade.... i recomend 3s
    }
    public void OnSpeedPowerUPActivated(){
        //When speed powerup is activated.
        changeColor.ChangePortalColor(1);
        changeColorSecondary.ChangePortalColor(1);
        playerAnimator.JumpPortal();
        FMODUnity.RuntimeManager.PlayOneShot("event:/FX/Portal");
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
        }
        
        //Check Ground should always run independant on whether canMove is TRUE or FALSE.
        //THis is bacause PowerUpBar is dependant on isGrounded.
        CheckGround();
        
        if (canMove == true)
        {
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

    public void PlayerDeath()
    {
        canMove = false;
        playerAnimator.Death();
        GameOverPanel.SetActive(true);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Stair")
        {
            isAboveStari = true;
        }
        else if (collision.tag == "BossRoom")
        {
            levelMusic.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
            bossMusic.start();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "Stair")
        {
            isAboveStari = false;
            coll.isTrigger = false;
            isFalling = false;
        }
        else if (collision.tag == "BossRoom")
        {
            bossMusic.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
            levelMusic.start();
        }
    }
    void OnDestroy()
    {
        levelMusic.release();
        bossMusic.release();
    }
}
