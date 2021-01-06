using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockSpawner : MonoBehaviour
{
    public GameManager gm;
    public List<Transform> listOfTransforms;

    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        //TODO: spawn random blocks at random locations every few seconds
        //TODO: also spawn those blocks at lowest point
    }
}
