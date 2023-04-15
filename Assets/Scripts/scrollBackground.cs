using UnityEngine;

public class scrollBackground : MonoBehaviour
{
    [SerializeField] private GameObject prefab;
    [SerializeField] private float speed;

    private bool hasSpawned;

    // Start is called before the first frame update
    private float length;
    private float verticalWidthSeen;

    private void Start()
    {
        length = prefab.transform.GetChild(0).gameObject.GetComponent<Renderer>().bounds.size.x;
        var verticalHeightSeen = Camera.main.orthographicSize * 2.0f;
        var verticalWidthSeen = verticalHeightSeen * Camera.main.aspect;
        Debug.Log(verticalWidthSeen);
        //
    }

    // Update is called once per frame
    private void Update()
    {
        prefab.transform.Translate(new Vector3(-speed * Time.deltaTime, 0, 0));
        if (prefab.transform.position.x < -30 && !hasSpawned)
        {
            Instantiate(prefab, new Vector3(prefab.transform.position.x + length, prefab.transform.position.y, 0),
                Quaternion.identity);
            hasSpawned = true;
        }

        if (prefab.transform.position.x < -60) Destroy(prefab);
    }
}