using UnityEngine;

public class BarryScript : MonoBehaviour
{
    public float jetpackStrength;
    public float flyAcceleration;

    public Rigidbody2D PlayerBody;

    public LogicScript Logic;
    public BackgroundLoop Background;

    public Animator BarryAnim;
    public AudioSource JetpackAudio;
    public GameObject Jetpack;

    private ParticleSystem jetpackBullets;
    private Animator bulletFlash;

    public bool PlayerIsAlive = true;
    // Start is called before the first frame update
    void Start()
    {
        Logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
        jetpackBullets = Jetpack.GetComponent<ParticleSystem>();
        bulletFlash = Jetpack.GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if ((Input.GetKey(KeyCode.Space) == true || Input.GetMouseButton(0) == true) && PlayerIsAlive)
        {
            Fly();
        }
        else
        {
            if (JetpackAudio.isPlaying)
            {
                JetpackAudio.Stop();
            }
            if (jetpackBullets.isPlaying)
            {
                jetpackBullets.Stop();
            }
            BarryAnim.SetBool("Fly", false);
            bulletFlash.SetBool("Fire", false);
        }
    }

    public void Fly()
    {
        if(!JetpackAudio.isPlaying)
        {
            JetpackAudio.Play();
        }
        if (!jetpackBullets.isPlaying)
        {
            jetpackBullets.Play();
        }
        bulletFlash.SetBool("Fire", true);
        BarryAnim.SetBool("Fly", true);

        if (PlayerBody.linearVelocity.y <= jetpackStrength)
        {
            PlayerBody.linearVelocity += (Vector2.up * flyAcceleration);
        }
    }

    public void PlayerDeath()
    {
        PlayerIsAlive = false;
        BarryAnim.SetTrigger("Death");
        Background.PlayerDead = true;
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (PlayerIsAlive)
        {
            if (collision.gameObject.CompareTag("Obstacle"))
            {
                Logic.GameOver();
                PlayerDeath();

            }
            else if (collision.gameObject.CompareTag("Floor"))
            {
                BarryAnim.SetBool("Grounded", true);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Coins"))
        {
            Logic.AddScore(1);
            collision.gameObject.GetComponent<CoinScript>().Collect();
        }
    }


    private void OnCollisionExit2D(Collision2D collision)
    {
        BarryAnim.SetBool("Grounded", false);
    }
}

