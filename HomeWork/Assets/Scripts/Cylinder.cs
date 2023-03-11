
using System.Collections.Generic;
using UnityEngine;

public class Cylinder : Subject
{
    [SerializeField] private List<Mesh> allMesh = new List<Mesh>();

    private MeshFilter meshFilter;

    protected override void Start()
    {
        if (allMesh.Count == 0)
        {
            throw new UnityException("List subject is empty");
        }
        base.Start();
        meshFilter = GetComponent<MeshFilter>();
    }
    protected override void OnCollisionEnter(Collision collision)
    {
        base.OnCollisionEnter(collision);
        int index = Random.Range(0, allMesh.Count);
        meshFilter.sharedMesh = allMesh[index];
    }
}
