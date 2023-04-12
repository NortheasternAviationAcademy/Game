using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrollBackground : MonoBehaviour
{
    [SerializeField] private GameObject prefab;
    [SerializeField] private float speed;
    // Start is called before the first frame update
    private float length;
    private float verticalWidthSeen;
    private bool hasSpawned = false;
    void Start()
    {
        length = prefab.transform.GetChild(0).gameObject.GetComponent<Renderer>().bounds.size.x;
        float verticalHeightSeen = Camera.main.orthographicSize * 2.0f;
        float verticalWidthSeen = verticalHeightSeen * Camera.main.aspect;
        Debug.Log(verticalWidthSeen);
        //



    }

    // Update is called once per frame
    void Update()
    {
        prefab.transform.Translate( new Vector3( -speed * Time.deltaTime, 0, 0 ) );
        if( prefab.transform.position.x < -30 && !hasSpawned)
        {
            Instantiate(prefab, new Vector3(prefab.transform.position.x + length, prefab.transform.position.y, 0), Quaternion.identity);
            hasSpawned = true;
        }
        if(prefab.transform.position.x < -60)
        {
            Destroy(prefab);
        }
    }
}
