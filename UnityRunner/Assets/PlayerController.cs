using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    //Start() variables
    private Rigidbody2D rb;
    //private Animator anim;
    private Collider2D coll;

    //FSM
    private enum State { idle, running, jumping, falling, hurt, climb }
    private State state = State.idle;

    //Ladder Variables
    //[HideInInspector] public bool canClimb = false;
    //[HideInInspector] public bool bottomLadder = false;
    //[HideInInspector] public bool topLadder = false;
    //public Ladder ladder;
    //[SerializeField] float climbSpeed = 5f;

    //Inspector variables
    [SerializeField] private LayerMask ground;
    [SerializeField] private float speed = 5f;
    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private float hurtForce = 10f;
    //[SerializeField] private AudioSource cherry;
    //[SerializeField] private AudioSource footstep;
    //[SerializeField] private AudioSource hurt;
    //[SerializeField] private AudioSource jump;

    //public float timeLeft = 3.0f;
    //public TextMeshProUGUI timertext;

    public bool HasKey = false;
    public GameObject ShowKey;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        //anim = GetComponent<Animator>();
        coll = GetComponent<Collider2D>();
        //PermanentUI.perm.healthAmount.text = PermanentUI.perm.health.ToString();
    }

    private void Update()
    {
        if(state == State.climb)
        {
            Climb();
        }
        else if (state != State.hurt)
        {
            Movement();
        }

        //AnimationState();
        //anim.SetInteger("state", (int)state); //Sets animation based on Enumerator state

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Key"))
        {
            HasKey = true;
            ShowKey.SetActive(true);
            Destroy(collision.gameObject);
            //cherry.Play();
            //PermanentUI.perm.cherries += 1;
            //PermanentUI.perm.cherryText.text = PermanentUI.perm.cherries.ToString();
        }

        if (collision.CompareTag("Respawn"))
        {
            //footstep.Stop();
            //jump.Stop();
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
        }

        if (collision.CompareTag("PowerUp"))
        {
            Destroy(collision.gameObject);
            speed = 10f;
            jumpForce = 25f;
            GetComponent<SpriteRenderer>().color = Color.yellow;
            StartCoroutine(ResetPower());
        }
        if (collision.CompareTag("Health"))
        {
            Destroy(collision.gameObject);
            if (PermanentUI.perm.health <= 2)
            {
                PermanentUI.perm.health += 1;
            }
            
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Enemy"))
        {
            Enemy enemy = other.gameObject.GetComponent<Enemy>();
           
           if(state == State.falling)
           {
                enemy.JumpedOn();
                Jump();
           }
           else
            {
                //state = State.hurt;
                HandleHealth(); //Deals with health, updating UI, and will reset level if health <= 0

                if (other.gameObject.transform.position.x > transform.position.x)
                {
                    //Enemy is to my right therefor I should be damaged and move left
                    rb.velocity = new Vector2(-hurtForce, rb.velocity.y);
                }
                else
                {
                    //Enemy is to my left therefor I should be damaged and move right
                    rb.velocity = new Vector2(hurtForce, rb.velocity.y);

                }
            }
        }

        if (other.gameObject.CompareTag("MovingPlatform"))
        {
            this.transform.parent = coll.transform;
        }

        if (other.gameObject.CompareTag("Door"))
        {
            if (HasKey)
            {
                HasKey = false;
                ShowKey.SetActive(false);
                Destroy(other.gameObject);
            }
        }
    }
    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("MovingPlatform"))
        {
            this.transform.parent = null;
        }
    }
    private void HandleHealth()
    {
        PermanentUI.perm.health -= 1;
        //PermanentUI.perm.healthAmount.text = PermanentUI.perm.health.ToString();
        if (PermanentUI.perm.health <= 0)
        {
            PermanentUI.perm.health = 3;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    private void Movement()
    {
        float hdirection = Input.GetAxis("Horizontal");

        //if(canClimb && Mathf.Abs(Input.GetAxis("Vertical")) > .1f)
        //{
        //    state = State.climb;
        //    rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
        //    transform.position = new Vector3(ladder.transform.position.x, rb.position.y);
        //    rb.gravityScale = 0f;
        //}
        //Moving left
        if (hdirection < 0)
        {
            rb.velocity = new Vector2(-speed, rb.velocity.y);
            //transform.localScale = new Vector2(-1, 1);
        }

        //Moving right
        else if (hdirection > 0)
        {
            rb.velocity = new Vector2(speed, rb.velocity.y);
            //transform.localScale = new Vector2(1, 1);
        }

        //Jumping
        if (Input.GetButtonDown("Jump") && coll.IsTouchingLayers(ground))
        {
            Jump();
        }

    }

    private void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        //jump.Play();
        state = State.jumping;
    }

    private void AnimationState()
    {
        if(state == State.climb)
        {

        }
        else if(state == State.jumping)
        {
            if(rb.velocity.y < .1f)
            {
                state = State.falling;
            }
        }
        else if(state == State.falling)
        {
            if(coll.IsTouchingLayers(ground))
            {
                state = State.idle;
            }
        }
        else if (state == State.hurt)
        {
            if(Mathf.Abs(rb.velocity.x) < .1f)
            {
                state = State.idle;
            }
        }
        else if(Mathf.Abs(rb.velocity.x) > 2f)
        {
            //Moving
            state = State.running;
        }
        else
        {
            state = State.idle;
        }
    }

    private void Footstep() 
    {
        //footstep.Play();
    }

    private void Hurt()
    {
        //hurt.Play();
    }

    private void Climb()
    {
        //if (Input.GetButtonDown("Jump"))
        //{
        //    rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        //    canClimb = false;
        //    rb.gravityScale = 3;
        //    anim.speed = 1f;
        //    Jump();
        //    return;
        //}
        //float vDirection = Input.GetAxis("Vertical");   
        ////Climbing Up
        //if(vDirection > .1f && !topLadder)
        //{
        //    rb.velocity = new Vector2(0f, vDirection * climbSpeed);
        //    anim.speed = 1f;
        //}
        ////Climbing Down
        //else if (vDirection < -.1f && !bottomLadder)
        //{
        //    rb.velocity = new Vector2(0f, vDirection * climbSpeed);
        //    anim.speed = 1f;
        //}
        ////Still
        //else
        //{
        //    anim.speed = 0f;
        //    rb.velocity = Vector2.zero;
        //}
    }

    private IEnumerator ResetPower()
    {
        yield return new WaitForSeconds(5);
        speed = 7f;
        jumpForce = 15f;
        GetComponent<SpriteRenderer>().color = Color.white;
    }
}
