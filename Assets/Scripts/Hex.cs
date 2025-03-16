using System.Collections.Generic;
using UnityEngine;

public class Hex : MonoBehaviour
{
    public string ID;

    [SerializeField] private List<Texture> _randomSprites;
    [SerializeField] Material _originalMaterial;
    private Mesh _mesh;
    private Vector3[] _vertices;
    private Vector2[] _uvs;
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
        float trig1 = Mathf.Sin(Mathf.PI / 6);
        float trig2 = Mathf.Sin(Mathf.PI / 3);

        _vertices[0] = scale * new Vector3(trig1, trig2, 0);
        _vertices[1] = scale * new Vector3(1, 0, 0);
        _vertices[2] = scale * new Vector3(trig1, -1 * trig2, 0);
        _vertices[3] = scale * new Vector3(-1 * trig1, -1 * trig2, 0);
        _vertices[4] = scale * new Vector3(-1, 0, 0);
        _vertices[5] = scale * new Vector3(-1 * trig1, trig2, 0);

        _triangles = new int[] { 0, 1, 2, 2, 5, 0, 2, 3, 5, 3, 4, 5 };

        _uvs = new Vector2[6];
        _uvs[0] = new Vector2(0.75f, 1f);
        _uvs[1] = new Vector2(1f, 0.5f);
        _uvs[2] = new Vector2(0.75f, 0f);
        _uvs[3] = new Vector2(0.25f, 0f);
        _uvs[4] = new Vector2(0f, 0.5f);
        _uvs[5] = new Vector2(0.25f, 1f);
    }

    private void CreateMesh()
    {
        _mesh.Clear();
        _mesh.vertices = _vertices;
        _mesh.triangles = _triangles;
        _mesh.uv = _uvs;

    }

    public void UpdateTexture(Texture tex)
    {
        var mat = GetComponent<Renderer>().material = new Material(_originalMaterial);

        mat.SetTexture("_MainTex", tex);

        //var rand = Random.Range(0.0f, 1.0f);
        //if (rand < 1.9f)
        //{
        //    mat.SetTexture("_MainTex", _randomSprites[0]);
        //}
        //else
        //{
        //    var r = Random.Range(1, _randomSprites.Count);
        //    mat.SetTexture("_MainTex", _randomSprites[r]);
        //}
    }
}
