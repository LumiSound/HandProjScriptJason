using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class PostProcessShaderManager : MonoBehaviour
{

    public Shader postProcessShaderClass = null;
    private Material _renderMaterial;


    // Start is called before the first frame update
    void Start()
    {
        if (this.postProcessShaderClass == null)
        {
            Debug.LogError("No post process shader set");
            this._renderMaterial = null;
            return;
        }
        else
            this._renderMaterial = new Material(this.postProcessShaderClass);
    }

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        if (this._renderMaterial != null)
            Graphics.Blit(source, destination, this._renderMaterial);
        else
            destination = source;

    }
}
