using UnityEngine;

public class BasicAlien : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float timeBetween;
    [SerializeField] private float dangerZone;
    [SerializeField] private GameObject bullet;
    private float lastShot;
    private bool moveUp = true;

    private void Start()
    {
        lastShot = Time.time;
    }

    private void Update()
    {
        DodgeLasers();
        var direction = speed * Time.deltaTime;

        if (!moveUp) direction = -direction;

        if (transform.position.y > 3.5)
            moveUp = false;
        else if (transform.position.y < -3.5) moveUp = true;
        if (Time.time > lastShot + timeBetween)
        {
            lastShot = Time.time;
            if (!GlobalVars.aliensFlared)
            {
                var currBullet = Instantiate(bullet,
                    new Vector3(transform.position.x - 2, transform.position.y, transform.position.z),
                    Quaternion.identity);
                currBullet.gameObject.tag = "alienBull";
            }
        }

        transform.Translate(new Vector3(0, direction, 0));
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "playerBull") Destroy(gameObject);
    }

    // Start is called before the first frame update
    // Update is called once per frame
    private void DodgeLasers()
    {
        var bulletPosition = new Vector3(0, 0, 0);
    }
}