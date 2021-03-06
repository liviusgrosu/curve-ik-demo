using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class PathCreator : MonoBehaviour
{
    [HideInInspector]
    public Path path;

    [Header("Colours")]
    public Color anchorColour = Color.red;
    public Color controlColour = Color.white;
    public Color segmentColour = Color.green;
    public Color selectedSegmentColour = Color.yellow;

    /// <summary>
    /// Creates a new point given the objects position
    /// </summary>
    public void CreatePath()
    {
        path = new Path(transform.position);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            CreatePath();
        }
    }
}