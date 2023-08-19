using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class FetchLightDirection : MonoBehaviour
{

    public GameObject dirLightRef;
    public Shader OceanShader;


    private void ApplyRotationToShader()
    {

        Vector4 mainLightDir = new Vector4(0, -1, 0, 0);
        Color sunColor = new Color(1, 1, 1);


        //mainLightDir = this.dirLightRef.transform.rotation * (Vector3.forward);
        //mainLightDir.x *= -1;
        //mainLightDir.z *= -1;

        sunColor = this.dirLightRef.GetComponent<Light>().color;


        gameObject.GetComponent<Renderer>().material.SetVector("_MainLightDirection", mainLightDir);
        gameObject.GetComponent<Renderer>().material.SetColor("_SunColor", sunColor);

    }

    // Start is called before the first frame update
    void Start()
    {
        if (this.OceanShader == null)
        {
            Debug.LogError("No Shader Set!");
            return;
        }

        this.ApplyRotationToShader();
    }

    // Update is called once per frame
    void Update()
    {
        this.ApplyRotationToShader();
    }
}
