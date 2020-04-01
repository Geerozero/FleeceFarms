using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[ExecuteInEditMode]
public class ShaderAdd : MonoBehaviour
{
    [Range(-10.0f, 10.0f)]
    public float intensity;
    [Range(0.0f, 1.0f)]
    public float red;
    [Range(0.0f, 1.0f)]
    public float green;
    [Range(0.0f, 1.0f)]
    public float blue;

    [Range(0, 3)]
    public int red_power;
    [Range(0, 3)]
    public int green_power;
    [Range(0, 3)]
    public int blue_power;

    private Material material;

    // Creates a private material used to the effect
    void Awake()
    {
        material = new Material(Shader.Find("Hidden/SimpleShader"));
    }

    // Postprocess the image
    void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        if (intensity == 0)
        {
            Graphics.Blit(source, destination);
            return;
        }

        material.SetFloat("_bwBlend", intensity);
        material.SetFloat("_red", red);
        material.SetFloat("_r_power", red_power);

        material.SetFloat("_green", green);
        material.SetFloat("_blue", blue);
        material.SetFloat("_g_power", green_power);
        material.SetFloat("_b_power", blue_power);
        Graphics.Blit(source, destination, material);
    }
}
