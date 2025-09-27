using UnityEngine;

public class CoinScript : MonoBehaviour
{
    public Animator CoinAnim;
    public AudioSource CollectAudio;
 
    public void Collect()
    {
        CollectAudio.Play();
        CoinAnim.SetTrigger("Collect");
    }
}
