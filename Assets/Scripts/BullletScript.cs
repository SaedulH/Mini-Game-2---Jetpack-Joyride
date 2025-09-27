using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BullletScript : MonoBehaviour
{
    public ParticleSystem bullets;

    private void Start()
    {
        //bullets = GetComponent<ParticleSystem>();
        //flash = GetComponent<Animator>();
    }

    private void OnParticleCollision(GameObject other)
    {
        


        
    }
}

