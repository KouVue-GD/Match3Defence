using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightIntensityBeat : MonoBehaviour
{

    public Light light1;
    public float maxIntensity;
    public float minIntensity;
    public float speed;
    bool dimming;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (light1.intensity < maxIntensity && dimming == false) {
            light1.intensity += Time.deltaTime * speed;
        }else {
            dimming = true;
        }
        
        if ( light1.intensity > minIntensity && dimming == true ) {
            light1.intensity -= Time.deltaTime * speed;
        } else {
            dimming = false;
        }
    }
}
