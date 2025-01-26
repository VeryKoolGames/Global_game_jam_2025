using UnityEngine;

public class SkyRotationScript : MonoBehaviour
{
    public Material material; // Le matériau utilisant ton Shader Graph
    private float rotation = 0;
    public float speed = 0.005f;


    void Update()
    {
        rotation += speed;
        material.SetFloat("_Rotation", rotation);
    }
}
