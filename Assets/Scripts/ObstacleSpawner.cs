using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public ObstacleType[] Obstacles;
    public BackgroundLoop Background;

    public float SpawnRate = 3;
    private float timer = 0;
    public float HeightOffset = 10;
    private float stepSize = 4;

    public int MinTime;
    public int MaxTime;

    public bool hazardEventActive = false;

    void Start()
    {
        Background = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<BackgroundLoop>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!hazardEventActive && !Background.PlayerDead)
        {
            if (timer < SpawnRate)
            {
                timer += Time.deltaTime;
            }
            else
            {
                SpawnObstacle();
                SpawnRate = Random.Range(MinTime, MaxTime);
                timer = 0;
            }
        }
    }

    void SpawnObstacle()
    {
        float lowestPoint = transform.position.y - HeightOffset;
        float highestPoint = transform.position.y + HeightOffset;
        float point = Random.Range(lowestPoint, highestPoint);
        float pointStep = Mathf.Floor(point / stepSize);

        int index;
        while (true)
        {
            index = Random.Range(0, Obstacles.Length);
            float chance = Random.Range(0f, 100f);
            if (chance <= Obstacles[index].ChanceToSpawn)
            {
                break;
            }
        }

        Instantiate(Obstacles[index].ObstacleObject, new Vector3(transform.position.x, pointStep * stepSize), Obstacles[index].ObstacleObject.transform.rotation, transform);
    }

}

[System.Serializable]
public class ObstacleType
{
    public GameObject ObstacleObject;
    [field:SerializeField, Range(0, 100)] public float ChanceToSpawn { get; set; }
}

