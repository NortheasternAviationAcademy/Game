using UnityEngine;

public class PollenExplosion : MonoBehaviour
{
    [SerializeField] private float explosionSpeed;

    private float size = .2f;

    // Start is called before the first frame update
    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
        size += Time.deltaTime * explosionSpeed;
        transform.localScale = new Vector3(size, size, 1);
        if (size > 2.5) Destroy(gameObject);
    }
}