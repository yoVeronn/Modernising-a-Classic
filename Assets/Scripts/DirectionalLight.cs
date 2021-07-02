using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionalLight : MonoBehaviour
{
    public Light light;
    private float r =1;
    private float g =1;
    private float b =1;

    private float smooth = 100;
    // Start is called before the first frame update
    void Start()
    {
        light.color = new Color(0.509434f, 0.3471181f, 0.1701317f);
    }

    // Update is called once per frame
    void Update()
    {
        //every 15s, change colour
        StartCoroutine(WaveCountdown(2));
        StartCoroutine(WaveCountdown(3));
        StartCoroutine(WaveCountdown(4));
        StartCoroutine(WaveCountdown(5));
        
    }

    IEnumerator WaveCountdown(int waveNumber)
    {
        yield return new WaitForSeconds(15);
        if (waveNumber == 2)
        {
            light.color = new Color(0.5283019f, 0.1315771f, 0.1809263f);
        }

        else if (waveNumber == 3)
        {
            light.color = new Color(0.1485582f, 0.2504252f, 0.735849f);
        }

        else if (waveNumber == 4)
        {
            light.color = new Color(0.2187255f, 0.6037736f, 0.5168301f);
        }

        else if (waveNumber == 5)
        {
            r -= Time.deltaTime / smooth;
            g -= Time.deltaTime / smooth;
            light.color = new Color(r, g, b);
            if (r <= 0) { r = 0; }
            if (g <= 0) { g = 0; }
        }
       
    }
}
