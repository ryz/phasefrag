using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SimplePlatformController : MonoBehaviour {

    [HideInInspector] public bool facingRight = true;
    [HideInInspector] public bool jump = true;

    public float moveForce = 365f;
    public float maxSpeed = 5f;
    public float jumpForce = 700f;

    public Transform groundCheck;

    public GameObject body; // To hide the body when in ghost form
    public GameObject other;
    public GameObject enemy2.renderer.bounds;

    public Slider phaseBarSlider;
    public Text phaseBarNum;

    public GameSystem gameSystem;

    public bool overlap;

    // Phase Bar regeneration delay
    private float phaseRegenDelay = 1;
    private float phaseRegenStart;

    private float playerRespawnDelay = 3;
    private float playerRespawnStart;


    private bool grounded = false;
    private Animator anim;
    private Rigidbody2D rb2d;

    public string playerIdentifier = "P1";
    public string jumpButton = "Jump";
    public string horizontalCtrl = "Horizontal";
    public string phaseShiftButton = "PhaseShift";

    // Use this for initialization
    void Awake() {
        anim = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();

        phaseBarNum.text = "Phase Bar: " + Mathf.Round(phaseBarSlider.value * 100) + "%";

    }
   
    // Update is called once per frame
    void Update() {
        grounded = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));

        if (Input.GetButtonDown(jumpButton) && grounded)
        {
            jump = true;
        }

        if (Input.GetButtonDown(phaseShiftButton))
        {
            PhaseShiftStart();
        }

        if (Input.GetButton(phaseShiftButton))
        {

                if (phaseBarSlider.value > 0)
                {
                    phaseBarSlider.value -= .01f;
                    phaseBarNum.text = "Phase Bar: " + Mathf.Round(phaseBarSlider.value * 100) + "%";
                }

                if (phaseBarSlider.value <= 0)
                {
                    PhaseShiftEnd();
                }
          

        }

        if (!Input.GetButton(phaseShiftButton))
        {
            if (phaseBarSlider.value < 100)
            {
                if (Time.time > phaseRegenStart)
                {
                    phaseBarSlider.value += .005f;
                    phaseBarNum.text = "Phase Bar: " + Mathf.Round(phaseBarSlider.value * 100) + "%";
                }
                
            }
        }

        if (Input.GetButtonUp(phaseShiftButton))
        {
            PhaseShiftEnd();
        }

        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            Application.LoadLevel(0);
        }
    }

    void FixedUpdate()
    {
        float h = Input.GetAxis(horizontalCtrl);
        anim.SetFloat("Speed", Mathf.Abs(h));

        if (h * rb2d.velocity.x < maxSpeed)
            rb2d.AddForce(Vector2.right * h * moveForce);

        if (Mathf.Abs(rb2d.velocity.x) > maxSpeed)
            rb2d.velocity = new Vector2(Mathf.Sign(rb2d.velocity.x) * maxSpeed, rb2d.velocity.y);

        if (h > 0 && !facingRight)
            Flip();
        else if (h < 0 && facingRight)
            Flip();

        if (jump)
        {
            anim.SetTrigger("Jump");
            rb2d.AddForce(new Vector2(0f, jumpForce));
            jump = false;
        }
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    void PhaseShiftStart()
    {
        body.GetComponent<Renderer>().enabled = false;
        Physics2D.IgnoreLayerCollision(9, 10, true);
        Physics2D.IgnoreLayerCollision(9, 13, true);
        Physics2D.IgnoreLayerCollision(13, 10, true);
    }

    void PhaseShiftEnd()
    {
        body.GetComponent<Renderer>().enabled = true;
        Physics2D.IgnoreLayerCollision(9, 10, false);
        Physics2D.IgnoreLayerCollision(9, 13, false);
        Physics2D.IgnoreLayerCollision(13, 10, false);
        phaseRegenStart = Time.time + phaseRegenDelay;
    }

    void OnCollisionEnter2D(Collision2D collisionTarget) {
        if (collisionTarget.gameObject.tag == "Enemy")
        {
            Debug.Log("bump into " + collisionTarget.gameObject.name);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log(">>>>> TRIGGER MAH BOI");
            //Destroy(gameObject);
            PlayerDeath();
            gameSystem.AddScore(5);
        }

    }

    void PlayerDeath()
    {
        playerRespawnStart = Time.time + playerRespawnDelay;
        Debug.Log("Player died");
        GetComponent<Renderer>().enabled = false;
        if (Time.time > playerRespawnStart)
        {
        GetComponent<Renderer>().enabled = true;
        }
    }
}