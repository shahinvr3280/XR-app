using UnityEngine;

public class ExposureEngine : MonoBehaviour
{
    [Range(0,1)] public float virtualAlpha = 0f;
    public Material blendMaterial;

    void Update()
    {
        blendMaterial.SetFloat("_VirtualAlpha", virtualAlpha);
    }

    public void SetIntensity(float t)
    {
        virtualAlpha = Mathf.Clamp01(t);
    }
}
