using UnityEngine;

public class AlienSpawner : MonoBehaviour
{
    [SerializeField] private float timeBetween;
    [SerializeField] private GameObject alien;
    private float lastSpawn;

    // Start is called before the first frame update
    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
        if (Time.time > lastSpawn + timeBetween)
        {
            lastSpawn = Time.time;
            var currBullet = Instantiate(alien,
                new Vector3(transform.position.x + 6 + Random.Range(-3f, 3f), transform.position.y,
                    transform.position.z), Quaternion.identity);
        }
    }
}