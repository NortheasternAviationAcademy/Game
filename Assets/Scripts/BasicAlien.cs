using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UIElements;

public class BasicAlien : MonoBehaviour
{
    bool moveUp = true;
    float lastShot = 0;
    [SerializeField] float speed;
    [SerializeField] float timeBetween;
    [SerializeField] float dangerZone;
    [SerializeField] GameObject bullet;
    // Start is called before the first frame update
    // Update is called once per frame
    private void DodgeLasers()
    {
        Vector3 bulletPosition = new Vector3(0, 0, 0);

    }

    void Start()
    {
        lastShot = Time.time;
        
    }

    void Update()
    {

        DodgeLasers();
        float direction = speed * Time.deltaTime;

        if( !moveUp)
        {
            direction = -direction;
        }
        
        if(transform.position.y > 3.5)
        {
            moveUp = false;
        }else if (transform.position.y < -3.5)
        {
            moveUp = true;
        }
        if(Time.time > lastShot + timeBetween ) 
        {
            lastShot = Time.time;
            if (!GlobalVars.aliensFlared)
            {
                GameObject currBullet = Object.Instantiate(bullet, new Vector3(transform.position.x - 2, transform.position.y, transform.position.z), Quaternion.identity);
                currBullet.gameObject.tag = "alienBull";
            }
        }
        transform.Translate(new Vector3(0, direction, 0));
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "playerBull")
        {
            Destroy(gameObject);
        }
    }

}
