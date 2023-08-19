using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class ShaderManager : MonoBehaviour
{
    // Start is called before the first frame update
    private Vector2 _screenSize;
    private Material _myUnlitShader;

    void Start()
    {
        this._screenSize = new Vector2(Screen.width, Screen.height);
        this._myUnlitShader = GetComponent<MeshRenderer>().material;
        this._myUnlitShader.SetVector("_ScreenResolution", new Vector4(this._screenSize.x, this._screenSize.y, 0.0f, 0.0f));

    }

    // Update is called once per frame
    void Update()
    {

    }
}
