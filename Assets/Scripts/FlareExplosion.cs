using UnityEngine;

public class FlareExplosion : MonoBehaviour
{
    // Start is called before the first frame update
    private void Start()
    {
        Destroy(gameObject, 0.25f);
    }

    // Update is called once per frame
    private void Update()
    {
    }
}