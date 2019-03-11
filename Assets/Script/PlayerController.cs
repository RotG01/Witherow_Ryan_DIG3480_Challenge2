using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{

    public float speed;
    public float jumpForce;
    public Text countText;
    public Text livesText;
    public Text scoreText;
    public Text winText;
    public Text loseText;
    public AudioClip musicClipOne;
    public AudioSource musicSource;

    Animator anim;

    private Rigidbody2D rb2d;
    private int count;
    private int score;
    private int lives;
    private int InAir;
    private bool facingRight;
    private MusicManager winMusic;

    void Start()
    {
        winMusic = FindObjectOfType<MusicManager>();

        rb2d = GetComponent<Rigidbody2D>();
        count = 0;
        score = 0;
        lives = 3;
        facingRight = true;
        winText.text = "";
        loseText.text = "";
        SetAllText();

        anim = GetComponent<Animator>();

    }

    void Update()
    {
        if (Input.GetKey("escape"))
            Application.Quit();

        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.Space))
        {
            anim.SetInteger("State", 1);
        }
        if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.Space) || Input.GetKeyUp(KeyCode.UpArrow))
        {
            anim.SetInteger("State", 0);
        }
        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            anim.SetInteger("State", 2);
        }
        if (Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.LeftArrow))
        {
            anim.SetInteger("State", 0);
        }
    }

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");

        Vector2 movement = new Vector2(moveHorizontal, 0);

        rb2d.AddForce(movement * speed);

        if (facingRight == false && moveHorizontal > 0)
        {
            Flip();
        }
        else if (facingRight == true && moveHorizontal < 0)
        {
            Flip();
        }
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector2 Scaler = transform.localScale;
        Scaler.x = Scaler.x * -1;
        transform.localScale = Scaler;
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground")
        {
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.Space))
            {
                rb2d.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (lives > 0)
        {
            if (other.gameObject.CompareTag("PickUp"))
            {

                other.gameObject.SetActive(false);
                count = count + 1;
                score = score + 1;
                SetAllText();

                if (score == 4)
                {
                    transform.position = new Vector2(84.51f, 4.45f);
                    lives = 3;
                }
            }
            else if (other.gameObject.CompareTag("Enemy"))
            {
                other.gameObject.SetActive(false);

                if (loseText.text != "You Lose!" && winText.text != "You Win!")
                {
                    lives = lives - 1;
                }
                SetAllText();
            }
        }
    }

    void SetAllText()
    {
        countText.text = "Count: " + count.ToString();
        scoreText.text = "Score: " + score.ToString();
        livesText.text = "Lives: " + lives.ToString();

        if (score >= 8 && loseText.text != "You Lose!" && lives > 0)
        {
            winText.text = "You Win!";

            if (musicClipOne != null)
            {
                winMusic.ChangeBM(musicClipOne);
            }
        }

        else if (lives == 0)
        {
            loseText.text = "You Lose!";
        }
    }
}