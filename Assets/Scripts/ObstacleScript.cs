using UnityEngine;

public class ObstacleScript : MonoBehaviour
{
    public float Deadzone = -45;
    public float ObstacleSpeed = 50;
    public BackgroundLoop Background;

    private void Start()
    {
        Background = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<BackgroundLoop>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Background.KeepGoing)
        {
            transform.position = transform.position + (Vector3.left * ObstacleSpeed) * Time.deltaTime;

            if (transform.position.x < Deadzone)
            {
                Debug.Log("Obstacle Deleted");
                Destroy(gameObject);
            }
        }
        else if (Background.PlayerDead)
        {
            gameObject.GetComponent<PolygonCollider2D>().enabled = false;
        }
    }
}

