using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AwakeRandomColors : MonoBehaviour
{
    private void Awake() {
        gameObject.GetComponent<MeshRenderer>().material.color = Random.ColorHSV();
    }
}
