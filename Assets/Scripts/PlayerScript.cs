using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{   
    private Rigidbody2D rd2d;
    public float speed;
    public Text score;
    public Text win;
    public Text lives;
    private int scoreValue = 0;
    private int livesValue = 3;
    public AudioSource musicSource;
    public AudioClip musicClipOne;
    public AudioClip musicClipTwo;

    // Start is called before the first frame update
    void Start()
    {
        rd2d = GetComponent<Rigidbody2D>();
        score.text = scoreValue.ToString();
        win.text = "";
        lives.text = livesValue.ToString();
        musicSource.clip = musicClipOne;
        musicSource.Play();
        musicSource.loop = true;
    }

    
    // Update is called once per frame
    void FixedUpdate()     
    {
        float hozMovement = Input.GetAxis("Horizontal");
        float vertMovement = Input.GetAxis("Vertical");
        rd2d.AddForce(new Vector2(hozMovement * speed, vertMovement * speed));
        
        if (Input.GetKey("escape"))
            {
                Application.Quit();
            }
    }

private void OnCollisionEnter2D(Collision2D collision)
    {
       if (collision.collider.tag == "Coin")
        {    
                scoreValue += 1;
                score.text = scoreValue.ToString();
                Destroy(collision.collider.gameObject);
            
            if (scoreValue == 4)
            {
                transform.position= new Vector2(55.0f, 0);
                livesValue =3;
                lives.text = livesValue.ToString();
            }
            if (scoreValue == 8)
            {
                win.text = "You Win! Game created by Jessica Wise.";
                {
                    musicSource.clip = musicClipTwo;
                    musicSource.Play();
                }
            }
        }
        if (collision.collider.tag == "Enemy")
        {   
                livesValue -= 1;
                lives.text = livesValue.ToString();
                Destroy(collision.collider.gameObject);

            if (livesValue == 0)
            {
                win.text = "You Lose! Game created by Jessica Wise";
                Destroy(this);
            }
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground")
        {
            if (Input.GetKey(KeyCode.W))
            {
                rd2d.AddForce(new Vector2(0, 3), ForceMode2D.Impulse);
            }
        }
    }
}
