using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject prefab;
    public float cooldown;
    float timer = 0f;

    GameObject currentObject;
    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > cooldown) {
            timer -= cooldown;
            currentObject = GameObject.Instantiate( prefab );
            currentObject.transform.position = Vector3.zero;
            currentObject.transform.parent = gameObject.transform;
        }
    }
}
