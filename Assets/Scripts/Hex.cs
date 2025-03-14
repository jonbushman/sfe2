using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hex : MonoBehaviour
{
    public string ID;

    private Mesh _mesh;
    private Vector3[] _vertices;
    private int[] _triangles;

    public void DrawHex(float scale)
    {
        _mesh = GetComponent<MeshFilter>().mesh;
        MakeMeshData(scale);
        CreateMesh();
    }

    private void MakeMeshData(float scale)
    {
        _vertices = new Vector3[6];
        float trig1 = scale * Mathf.Sin(Mathf.PI / 6);
        float trig2 = scale * Mathf.Sin(Mathf.PI / 3);

        _vertices[0] = new Vector3(trig1, trig2, 0);
        _vertices[1] = new Vector3(scale, 0, 0);
        _vertices[2] = new Vector3(trig1, -1 * trig2, 0);
        _vertices[3] = new Vector3(-1 * trig1, -1 * trig2, 0);
        _vertices[4] = new Vector3(-1 * scale, 0, 0);
        _vertices[5] = new Vector3(-1 * trig1, trig2, 0);

        _triangles = new int[] { 0, 1, 2, 2, 5, 0, 2, 3, 5, 3, 4, 5 };
    }

    private void CreateMesh()
    {
        _mesh.Clear();
        _mesh.vertices = _vertices;
        _mesh.triangles = _triangles;
    }
}
