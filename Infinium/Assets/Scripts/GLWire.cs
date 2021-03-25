using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GLWire : MonoBehaviour
{
    public bool wire;
    public bool wire2;
    // Attach this script to a camera, this will make it render in wireframe
    void OnPreRender()
    {
        GL.wireframe = wire;
    }

    void OnPostRender()
    {
        GL.wireframe = wire2;
    }
}
