using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float xSpeed;
    [SerializeField] private float ySpeed;
    [SerializeField] private GameObject bullet;
    [SerializeField] private GameObject fireball;
    [SerializeField] private GameObject meteor;
    [SerializeField] private GameObject pollen;
    [SerializeField] private GameObject flare;
    [SerializeField] private Slider Slider;
    public int health = 4;
    public List<GameObject> playerLasers = new();
    private bool canFireball = true;
    private bool canFlare = true;
    private bool canMeteor = true;
    private bool canPollen = true;
    private bool canShoot = true;
    private float windX = 1;

    private float windY = 1;

    // Start is called before the first frame update
    private void Start()
    {
        GameObject globalref = GameObject.Find("GlobalVars");
        GlobalVars GlobalVars = globalref.GetComponent<GlobalVars>();
        if (GlobalVars.scaly) health += 2;
        if (GlobalVars.boosted)
        {
            xSpeed += 2;
            ySpeed += 2;
        }

        Slider.maxValue = health;
        Slider.value = health;
        gameObject.transform.GetChild(0).gameObject.SetActive(GlobalVars.boosted);
        gameObject.transform.GetChild(1).gameObject.SetActive(GlobalVars.scaly);
        gameObject.transform.GetChild(2).gameObject.SetActive(GlobalVars.stabilized);
        gameObject.transform.GetChild(3).gameObject.SetActive(GlobalVars.shielded);

        StartCoroutine(changeWind());
    }

    // Update is called once per frame
    private void Update()
    {
        GameObject globalref = GameObject.Find("GlobalVars");
        GlobalVars GlobalVars = globalref.GetComponent<GlobalVars>();
        GlobalVars.packageplayerdata();

        var xVal = ((Input.GetKey("d") ? xSpeed : 0) - (Input.GetKey("a") ? xSpeed : 0)) * Time.deltaTime;
        var yVal = ((Input.GetKey("w") ? ySpeed : 0) - (Input.GetKey("s") ? ySpeed : 0)) * Time.deltaTime;

        if (!GlobalVars.stabilized)
        {
            yVal -= windX * Time.deltaTime;
            xVal -= windY * Time.deltaTime;
        }

        if (transform.position.x > -1 && xVal > 0) xVal = 0;
        transform.Translate(new Vector3(xVal, yVal, 0));
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        GameObject globalref = GameObject.Find("GlobalVars");
        GlobalVars GlobalVars = globalref.GetComponent<GlobalVars>();
        if (col.gameObject.tag == "alienBull")
        {
            if (Random.Range(-0f, 3f) > 1 && GlobalVars.shielded)
            {
                var currBullet = Instantiate(bullet,
                    new Vector3(col.transform.position.x, col.transform.position.y, transform.position.z),
                    Quaternion.identity);
                currBullet.gameObject.tag = "playerBull";
            }
            else
            {
                Slider.value -= 1;
                if (Slider.value == 0) Destroy(gameObject);
            }
        }
    }


    public void shoot()
    {
        if (canShoot) StartCoroutine(shootCooldown());
    }

    public void shootFireball()
    {
        if (canFireball) StartCoroutine(fireballCooldown());
    }

    public void shootMeteor()
    {
        if (canMeteor) StartCoroutine(meteorCooldown());
    }

    public void shootPollen()
    {
        if (canPollen) StartCoroutine(pollenCooldown());
    }

    public void shootFlare()
    {
        if (canFlare) StartCoroutine(flareCooldown());
    }

    private IEnumerator changeWind()
    {
        while (true)
        {
            windX = Random.Range(2f, -2f);
            windY = Random.Range(2f, -2f);
            yield return new WaitForSeconds(2);
        }
    }

    private IEnumerator shootCooldown()
    {
        canShoot = false;
        var currBullet = Instantiate(bullet,
            new Vector3(transform.position.x + 3f, transform.position.y, transform.position.z), Quaternion.identity);
        playerLasers.Add(currBullet);
        currBullet.gameObject.tag = "playerBull";
        yield return new WaitForSeconds(1);
        canShoot = true;
    }

    private IEnumerator fireballCooldown()
    {
        canFireball = false;
        var fireBall = Instantiate(fireball,
            new Vector3(transform.position.x + 2.5f, transform.position.y, transform.position.z), Quaternion.identity);
        fireBall.gameObject.tag = "playerBull";
        yield return new WaitForSeconds(5);
        canFireball = true;
    }

    private IEnumerator meteorCooldown()
    {
        canMeteor = false;
        var currMeteor = Instantiate(meteor, new Vector3(-1, 7, transform.position.z), Quaternion.identity);
        currMeteor.gameObject.tag = "playerBull";
        yield return new WaitForSeconds(7);
        canMeteor = true;
    }

    private IEnumerator pollenCooldown()
    {
        canPollen = false;
        var pollenBomb = Instantiate(pollen,
            new Vector3(transform.position.x + 3f, transform.position.y, transform.position.z), Quaternion.identity);
        pollenBomb.gameObject.tag = "playerBull";
        yield return new WaitForSeconds(1);
        canPollen = true;
    }

    private IEnumerator flareCooldown()
    {
        canFlare = false;
        var pollenBomb = Instantiate(flare, new Vector3(4.67f, -6.6f, transform.position.z), Quaternion.identity);
        GlobalVars.aliensFlared = true;
        yield return new WaitForSeconds(2);
        GlobalVars.aliensFlared = false;
        yield return new WaitForSeconds(3);
        canFlare = true;
    }
}