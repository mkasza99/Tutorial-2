using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    Animator anim;
    private bool facingRight = true;
    
    private Rigidbody2D rd2d;

    public float speed;

    public Text lives;

    public Text score;

    public Text livesText;

    public Text loseText;

    public Text winText;

    private int scoreValue = 0;

    private int livesValue = 3;

    public AudioClip musicClipOne;

    public AudioClip musicClipTwo;

    public AudioSource musicSource;


    // Start is called before the first frame update
    void Start()
    {
        rd2d = GetComponent<Rigidbody2D>();
        score.text = scoreValue.ToString();
        lives.text = livesValue.ToString();
        anim = GetComponent<Animator>(); 
        musicSource.clip = musicClipOne;
          musicSource.Play();
    
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.W))
      {
          anim.SetInteger("State", 2);
      }
      
      if (Input.GetKey(KeyCode.D))
      {
          anim.SetInteger("State", 1);
      }

      if (Input.GetKey(KeyCode.A))
      {
          anim.SetInteger("State", 1);
      }
      if (Input.GetKey("escape"))
      {
        Application.Quit();
      }

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float hozMovement = Input.GetAxis("Horizontal");
        float vertMovement = Input.GetAxis("Vertical");
        rd2d.AddForce(new Vector2(hozMovement * speed, vertMovement * speed));
       
       
      if (livesValue <= 0)
      {
          loseText.text = "Game over man!";
          Destroy (gameObject);
      }

      if (scoreValue == 4)
      {
          livesValue = 3;
          lives.text = livesValue.ToString();
      }

       if (facingRight == false && hozMovement > 0)
         {
         Flip();
         }

        else if (facingRight == true && hozMovement < 0)
        {
         Flip();
        }
        
         
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
       if (collision.collider.tag == "Coin")
        {
            scoreValue += 1;
            score.text = scoreValue.ToString();
            Destroy(collision.collider.gameObject);
        }
        if (collision.collider.tag == "Enemy")
        {
            livesValue -= 1;
            lives.text = livesValue.ToString();
            Destroy(collision.collider.gameObject);
        }

        if (scoreValue == 4)
         {
          transform.position = new Vector3(69.0f, 6.0f, 0.0f);
         }
         if (scoreValue >= 8)
        {
          winText.text = "You Win! Created by Matthew Kasza";
          musicSource.clip = musicClipOne;
          musicSource.Stop();
          
          musicSource.clip = musicClipTwo;
          musicSource.Play();
        }
        
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground")
        {
          if (collision.collider.tag == "Ground")
        {
           anim.SetInteger("State", 0);
        }
            if (Input.GetKey(KeyCode.W))
            {
                rd2d.AddForce(new Vector2(0, 3), ForceMode2D.Impulse);
            }
        }

    }
    void Flip()
   {
     facingRight = !facingRight;
     Vector2 Scaler = transform.localScale;
     Scaler.x = Scaler.x * -1;
     transform.localScale = Scaler;
   }
    
}