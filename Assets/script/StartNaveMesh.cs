using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class StartNaveMesh : MonoBehaviour
{
    public NavMeshSurface2d Surface2D;
    void Start()
    {
        Physics2D.SyncTransforms();
        Surface2D.BuildNavMeshAsync();
    }
}
