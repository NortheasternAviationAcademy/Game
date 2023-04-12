using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Unity.Mathematics;
using UnityEngine;

public class Pollenbomb : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] GameObject explosion;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector3(-speed * Time.deltaTime, 0, 0));
        if (math.abs(transform.position.x) > 10)
        {
            Destroy(gameObject);
        }
    }
    private void OnCollisionEnter2D(Collision2D col)
    {
        if( col.gameObject.tag != "alienBull")
        {
            GameObject currBomb = Object.Instantiate(explosion, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
            currBomb.gameObject.tag = "playerBull";
            Destroy(gameObject);
        }
    }
}
