using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Flare : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] GameObject explosion;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 0.75f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector3(0, speed * Time.deltaTime, 0));
        if (math.abs(transform.position.y) > 10)
        {
            Destroy(gameObject);
        }
    }
    private void OnDestroy()
    {
        GameObject explosion = Object.Instantiate(this.explosion, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
    }
}