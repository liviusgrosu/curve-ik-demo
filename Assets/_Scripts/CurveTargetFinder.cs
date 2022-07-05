using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurveTargetFinder : MonoBehaviour
{
    [SerializeField] private Transform target;
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
        StopCoroutine(StartTrackingPath());
        StartCoroutine(StartTrackingPath());
    }

    IEnumerator StartTrackingPath()
    {
        _performingTrack = true;

        for (int i = 0; i < pathCreator.path.Durations.Count; i++)
        {
            float segmentDuration = pathCreator.path.Durations[i];
            Vector3[] segmentPoints = pathCreator.path.GetPointsInSegement(i);
            _timeElapsed = 0f;
            while (_timeElapsed <= segmentDuration)
            {
                _timeElapsed += Time.deltaTime;
                Vector3 newPosition = Bezier.EvaluateCubic(segmentPoints[0], segmentPoints[1], segmentPoints[2], segmentPoints[3], _timeElapsed / segmentDuration);
                target.position = newPosition;
                yield return null;
            }
        }
        
        _performingTrack = false;
    }
}
