using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningScript : MonoBehaviour
{

    public Animator lightning;
    public float interval = 5;
    private float timer = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (timer < interval)
        {
            timer = timer + Time.deltaTime;
        }
        else
        {
            lightning.SetTrigger("Boom");
            timer = 0;
            interval = Random.Range(10, 20);
        }
    }
}
