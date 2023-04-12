using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienSpawner : MonoBehaviour
{
    [SerializeField] float timeBetween;
    [SerializeField] GameObject alien;
    float lastSpawn = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Time.time > lastSpawn + timeBetween)
        {
            lastSpawn = Time.time;
            GameObject currBullet = Object.Instantiate(alien, new Vector3(transform.position.x + 6 + Random.Range(-3f, 3f), transform.position.y, transform.position.z), Quaternion.identity);
        }
    }
}
