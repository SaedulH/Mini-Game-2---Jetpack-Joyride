using System.Collections;
using UnityEngine;

public class BackgroundLoop : MonoBehaviour
{

    public GameObject[] Levels;
    private Camera mainCamera;
    private Vector2 screenBounds;
    public float Choke;
    public float DestroyThreshold = 10f;
    public float BackGroundSpeed = 2.5F;
    public float ForeGroundSpeed = 5;

    public bool PlayerDead = false;
    public bool KeepGoing = true;

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = gameObject.GetComponent<Camera>();
        screenBounds = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height));

        foreach (GameObject obj in Levels)
        {
            LoadChildObjects(obj);
        }

    }

    void LoadChildObjects(GameObject obj)
    {
        float objectWidth = obj.GetComponent<SpriteRenderer>().bounds.size.x - Choke;
        int childsNeeded = (int)Mathf.Ceil(screenBounds.x * 2 / objectWidth);

        GameObject clone = Instantiate(obj);
        for (int i = 0; i <= childsNeeded; i++)
        {
            GameObject c = Instantiate(clone);
            c.transform.SetParent(obj.transform);
            c.transform.position = new Vector3(objectWidth * i, obj.transform.position.y, obj.transform.position.z);
            c.name = obj.name + i;
        }
        Destroy(clone);
        Destroy(obj.GetComponent<SpriteRenderer>());
    }


    void RepositionBackground(GameObject obj)
    {
        Transform[] children = obj.GetComponentsInChildren<Transform>();
        if (children.Length > 1)
        {
            GameObject firstChild = children[1].gameObject;
            GameObject lastChild = children[children.Length - 1].gameObject;
            float halfObjectWidth = lastChild.GetComponent<SpriteRenderer>().bounds.extents.x - Choke;

            if (transform.position.x + screenBounds.x > lastChild.transform.position.x + halfObjectWidth)
            {
                firstChild.transform.SetAsLastSibling();
                firstChild.transform.position = new Vector3(lastChild.transform.position.x + halfObjectWidth * 2, lastChild.transform.position.y, lastChild.transform.position.z);
            }
            else if (transform.position.x - screenBounds.x < firstChild.transform.position.x - halfObjectWidth)
            {
                lastChild.transform.SetAsFirstSibling();
                lastChild.transform.position = new Vector3(firstChild.transform.position.x - halfObjectWidth * 2, firstChild.transform.position.y, firstChild.transform.position.z);
            }
        }
    }

    void MoveBackground(GameObject[] levels)
    {
        foreach (GameObject obj in levels)
        {
            obj.transform.position = obj.transform.position + (Vector3.left * ForeGroundSpeed) * Time.deltaTime;
            RepositionBackground(obj);
        }
    }

    // Update is called once per frame
    void LateUpdate()
    {

        if (KeepGoing)
        {
            MoveBackground(Levels);
        }

        if (PlayerDead && KeepGoing)
        {
            StartCoroutine(SlowDown(2));
        }
    }

    IEnumerator SlowDown(int seconds)
    {
        int counter = seconds;
        while (counter > 0)
        {
            yield return new WaitForSeconds(1);
            ForeGroundSpeed = ForeGroundSpeed / 2;

            counter--;
        }
        KeepGoing = false;
    }
}

