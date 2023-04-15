using Unity.Mathematics;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    [SerializeField] private float speed;

    // Start is called before the first frame update
    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
        transform.Translate(new Vector3(-speed * Time.deltaTime, 0, 0));
        if (math.abs(transform.position.x) > 10) Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
    }
}