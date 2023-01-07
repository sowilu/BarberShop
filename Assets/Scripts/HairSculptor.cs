using System.Collections.Generic;
using UnityEngine;

public class HairSculptor : MonoBehaviour
{
    public float maxLength;
    private MeshFilter _mf;
    private MeshCollider _mc;
    private Mesh _mesh;
    private List<Vector3> _normals = new List<Vector3>();
    private List<Vector3> _vertices = new List<Vector3>();
    private Vector3[] _verticesMin;
    private float[] _lengths;
    private List<Color> _colors = new List<Color>();
    
    void Start()
    {
        _mc = GetComponent<MeshCollider>();
        _mf = GetComponent<MeshFilter>();
        _mesh = _mf.mesh;
        
        _mesh.GetNormals(_normals);
        
        _mesh.GetVertices(_vertices);
        _verticesMin = new Vector3[_vertices.Count];
        _vertices.CopyTo(_verticesMin);
        _lengths = new float[_vertices.Count];
        
        _vertices.CopyTo(_verticesMin);
        
        _mesh.GetColors(_colors);

        GenerateRandomHair();
    }

    private void GenerateRandomHair()
    {
        for (int i = 0; i < _vertices.Count; i++)
        {
            if (_colors[i].a != 0)
            {
                _lengths[i] = Random.Range(0.1f, 0.2f);
                _vertices[i] = _verticesMin[i] + _normals[i] * _lengths[i] * maxLength;
                _colors[i] = Color.black;
            } 
            else
            {
                var color = Color.black;
                color.a = 0;
                _colors[i] = color;
            }
                
        }
        _mesh.SetColors(_colors);
        _mesh.SetVertices(_vertices);
        _mc.sharedMesh = _mesh;
    }

    public void Extrude(Vector3 pos, float radius, float strength = 0.3f)
    {
        for (int i = 0; i < _vertices.Count; i++)
        {
            var worldPos = transform.TransformPoint(_vertices[i]);

            if (_colors[i].a != 0 && Vector3.Distance(pos, worldPos) <= radius)
            {
                _lengths[i] += strength;
                _lengths[i] = Mathf.Clamp(_lengths[i], 0, 1);
                _vertices[i] = _verticesMin[i] + _lengths[i] * maxLength * _normals[i];
            }
        }
        
        _mesh.SetVertices(_vertices);
        _mc.sharedMesh = _mesh;
    }
    
    public void SmoothExtrude(Vector3 pos, float radius, float strength = 0.3f)
    {
        var sum = 0f;
        var count = 0;
        for (int i = 0; i < _vertices.Count; i++)
        {
            var worldPos = transform.TransformPoint(_vertices[i]);

            if (_colors[i].a != 0 && Vector3.Distance(pos, worldPos) <= radius)
            {
                sum += _lengths[i];
                count++;
            }
        }

        var average = sum / count;
        
        
        for (int i = 0; i < _vertices.Count; i++)
        {
            var worldPos = transform.TransformPoint(_vertices[i]);

            if (_colors[i].a != 0 && Vector3.Distance(pos, worldPos) <= radius)
            {
                _lengths[i] = Mathf.MoveTowards(_lengths[i], average, strength);
                //_lengths[i] = Mathf.Clamp(_lengths[i], 0, 1);
                _vertices[i] = _verticesMin[i] + _lengths[i] * maxLength * _normals[i];
            }
        }
        
        _mesh.SetVertices(_vertices);
        _mc.sharedMesh = _mesh;
    }
    
    

    public void Dye(Color color, Vector3 pos, float radius, float strength = 0.3f)
    {
        for (int i = 0; i < _vertices.Count; i++)
        {
            var worldPos = transform.TransformPoint(_vertices[i]);

            if (_colors[i].a != 0 && Vector3.Distance(pos, worldPos) <= radius)
            {
                _colors[i] = color;
            }
        }
        _mesh.SetColors(_colors);
    }
}
