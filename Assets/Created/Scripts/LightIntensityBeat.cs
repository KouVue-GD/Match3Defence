using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightIntensityBeat : MonoBehaviour
{

    public Light light;
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
        if (light.intensity < maxIntensity && dimming == false) {
            light.intensity += Time.deltaTime * speed;
        }else {
            dimming = true;
        }
        
        if ( light.intensity > minIntensity && dimming == true ) {
            light.intensity -= Time.deltaTime * speed;
        } else {
            dimming = false;
        }
    }
}
