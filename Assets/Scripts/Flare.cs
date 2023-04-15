using Unity.Mathematics;
using UnityEngine;

public class Flare : MonoBehaviour
{
    [SerializeField] private float speed;

    [SerializeField] private GameObject explosion;

    // Start is called before the first frame update
    private void Start()
    {
        Destroy(gameObject, 0.75f);
    }

    // Update is called once per frame
    private void Update()
    {
        transform.Translate(new Vector3(0, speed * Time.deltaTime, 0));
        if (math.abs(transform.position.y) > 10) Destroy(gameObject);
    }

    private void OnDestroy()
    {
        var explosion = Instantiate(this.explosion,
            new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
    }
}