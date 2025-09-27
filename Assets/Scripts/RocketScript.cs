using System.Collections;
using UnityEngine;

public class RocketScript : MonoBehaviour
{
    public Animator Warning;
    public Animator Rocket;
    public GameObject Barry;

    public AudioSource WarningAudio;
    public AudioSource RocketAudio;

    public float Deadzone = -45;

    public float RocketSpeed = 50;
    public float LockOnSpeed = 20;
    public float LockOnTime = 2;
    private float timer = 0;
    private bool gaveWarning = false;


    // Start is called before the first frame update
    void Awake()
    {
        Barry = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {

        timer += Time.deltaTime;
        if (timer < LockOnTime)
        {
            LockOn();
        }
        else
        {
            if (!gaveWarning)
            {
                StartCoroutine(WarningAlert());
            }
            else
            {
                SendRocket();
            }
        }
    }

    IEnumerator WarningAlert()
    {
        Warning.SetTrigger("Warn");
        WarningAudio.Play();
        yield return new WaitForSeconds(0.75F);
        Warning.SetTrigger("WarnOff");
        yield return new WaitForSeconds(0.25F);
        WarningAudio.Stop();
        gaveWarning = true;
    }

    void LockOn()
    {
        float barryLocation = Barry.transform.position.y;
        if (barryLocation > transform.position.y)
        {
            transform.position = transform.position + (Vector3.up * LockOnSpeed) * Time.deltaTime;
        }
        else
        {
            transform.position = transform.position + (Vector3.down * LockOnSpeed) * Time.deltaTime;
        }
    }

    void SendRocket()
    {
        RocketAudio.Play();
        transform.position = transform.position + (Vector3.left * RocketSpeed) * Time.deltaTime;
        if (transform.position.x < Deadzone)
        {
            Debug.Log("Rocket Deleted");
            RocketAudio.Stop();
            Destroy(gameObject);
        }
    }
}
