using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathCopyRotation : MonoBehaviour
{
    public Transform FakeParent;

    private Path _path;
    private Vector3[] _positionOffset;

    private void Awake()
    {
        _path = GetComponent<PathCreator>().path;
        _positionOffset = new Vector3[_path.points.Count];
    }

    private void Start()
    {
        if(FakeParent != null)
        {
            SetFakeParent(FakeParent);
        }
    }
 
    private void Update()
    {
        if (FakeParent == null)
        {
            return;
        }
 
        for (int pointIndex = 0; pointIndex < _path.points.Count; pointIndex++)
        {
            Vector3 targetPos = FakeParent.position - _positionOffset[pointIndex];
            _path.points[pointIndex] = RotatePointAroundPivot(targetPos, FakeParent.position, FakeParent.localRotation);
        }

        GetComponent<PathCreator>().path = _path;
    }
 
    public void SetFakeParent(Transform parent)
    {
        for (int pointIndex = 0; pointIndex < _path.points.Count; pointIndex++)
        {
            _positionOffset[pointIndex] = parent.position - _path.points[pointIndex];
        }
        
        FakeParent = parent;
    }
 
    public Vector3 RotatePointAroundPivot(Vector3 point, Vector3 pivot, Quaternion rotation)
    {
        //Get a direction from the pivot to the point
        Vector3 dir = point - pivot;
        //Rotate vector around pivot
        dir = rotation * dir; 
        //Calc the rotated vector
        point = dir + pivot; 
        //Return calculated vector
        return point; 
    }
}
