using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurveTargetFinder : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float timeToComplete;
    [SerializeField] private bool onAwake;
    [SerializeField] private PathCreator pathCreator;
    private float _timeElapsed = 0f;
    private bool _startLooping;
    private bool _performingTrack;

    // Update is called once per frame
    private void Start()
    {
        if (onAwake)
        {
            PerformTracking();
            _startLooping = true;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _startLooping = !_startLooping;
        }

        if (!_performingTrack && _startLooping)
        {
            PerformTracking();
        }
    }

    private void PerformTracking()
    {
        _timeElapsed = 0f;
        StopCoroutine(StartTrackingPath());
        StartCoroutine(StartTrackingPath());
    }

    IEnumerator StartTrackingPath()
    {
        _performingTrack = true;
        while (_timeElapsed <= timeToComplete)
        {
            _timeElapsed += Time.deltaTime;
            Vector3 newPosition = Bezier.EvaluateCubic(pathCreator.path.points[0], pathCreator.path.points[1], pathCreator.path.points[2], pathCreator.path.points[3], _timeElapsed / timeToComplete);
            target.position = newPosition;
            yield return null;
        }
        _performingTrack = false;
    }
}
