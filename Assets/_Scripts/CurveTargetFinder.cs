using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurveTargetFinder : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float timeToComplete;
    [SerializeField] private PathCreator pathCreator;
    private float _timeElapsed = 0f;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _timeElapsed = 0f;
            StopCoroutine(StartTrackingPath());
            StartCoroutine(StartTrackingPath());
        }
    }

    IEnumerator StartTrackingPath()
    {
        while (_timeElapsed <= timeToComplete)
        {
            // do a mini while loop here 
            
            _timeElapsed += Time.deltaTime;
            Vector3 newPosition = Bezier.EvaluateCubic(pathCreator.path.points[0], pathCreator.path.points[1], pathCreator.path.points[2], pathCreator.path.points[3], _timeElapsed / timeToComplete);
            target.position = newPosition;
            yield return null;
        }
    }
}
