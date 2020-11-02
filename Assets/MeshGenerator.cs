using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class MeshGenerator : MonoBehaviour
{
    private Mesh _mesh;

    private Vector3[] _vertices;

    private int[] _triangles;

    public int xSize = 20;
    public int zSize = 20;

    public bool drawGizmos = false;
    
    // Start is called before the first frame update
    void Start()
    {
        _mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = _mesh;

        CreateShape();
        UpdateMesh();
    }

    void CreateShape()
    {
        _vertices = new Vector3[(xSize + 1) * (zSize + 1)];

        for (int i = 0, z = 0; z <= zSize; z++)
        {
            for (int x = 0; x <= xSize; x++)
            {
                float y = Mathf.PerlinNoise(x * .3f, z * .3f) * 2f;
                _vertices[i] = new Vector3(x, y, z);
                i++;
            }
        }
        
        _triangles = new int[xSize * zSize * 6];
        int tris = 0;
        int vert = 0;

        for (int z = 0; z < zSize; z++)
        {

            for (int x = 0; x < xSize; x++)
            {
                _triangles[tris] = vert;
                _triangles[tris + 1] = vert + xSize + 1;
                _triangles[tris + 2] = vert + 1;
                _triangles[tris + 3] = vert + 1;
                _triangles[tris + 4] = vert + xSize + 1;
                _triangles[tris + 5] = vert + xSize + 2;

                vert++;
                tris += 6;
            }

            vert++;
        }
        
    }

    void UpdateMesh()
    {
        _mesh.Clear();

        _mesh.vertices = _vertices;
        _mesh.triangles = _triangles;
        
        _mesh.RecalculateNormals();
    }

    private void OnDrawGizmos()
    {
        if (drawGizmos)
        {


            if (_vertices == null)
            {
                return;
            }

            for (int i = 0; i < _vertices.Length; i++)
            {
                Gizmos.DrawSphere(_vertices[i], .1f);
            }
        }
    }
}
