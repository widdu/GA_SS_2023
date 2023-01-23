using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathController : MonoBehaviour
{
    // Serialized private variables
    //[SerializeField] private Transform myPathTransform; // obsolete?

    // Private variables
    private GameObject trackPathGroup;
    private Transform myStartPointTransform, myEndPointTransform, trackPathGroupTransform;
    private TrackPropertyDistribution trackPropertyDistribution;
    private float originalStartPointPositionZ, originalEndPointPositionZ, originalTrackPlatformGroupScaleZ;

    private void Awake()
    {
        myStartPointTransform = transform.Find("Start").GetComponent<Transform>();
        if(myStartPointTransform == null)
        {
            Debug.LogWarning("Can't find start point's transform component for " + gameObject.name + "'s path controller component!");
        }

        myEndPointTransform = transform.Find("End").GetComponent<Transform>();
        if (myEndPointTransform == null)
        {
            Debug.LogWarning("Can't find end point's transform component for " + gameObject.name + "'s path controller component!");
        }

        trackPathGroup = transform.parent.gameObject;
        if(trackPathGroup == null)
        {
            Debug.LogWarning("Can't find Track path group game object for " + gameObject.name + "'s path controller component!");
        }

        trackPathGroupTransform = trackPathGroup.GetComponent<Transform>();
        if (trackPathGroupTransform == null)
        {
            Debug.LogWarning("Can't find Track path group's transform component for " + gameObject.name + "'s path controller component!");
        }

        trackPropertyDistribution = trackPathGroup.GetComponent<TrackPropertyDistribution>();
        if(trackPropertyDistribution == null)
        {
            Debug.LogWarning("Can't find Track path group's track property distribution component for " + gameObject.name + "'s path controller component!");
        }
    }

    // Start is called before the first frame update
    private void Start()
    {
        // Get original values.
        // Start and end points' position Z values.
        originalStartPointPositionZ = myStartPointTransform.localPosition.z;
        originalEndPointPositionZ = myEndPointTransform.localPosition.z;
        originalTrackPlatformGroupScaleZ = trackPropertyDistribution.OriginalTrackPlatformGroupScaleZ;
    }

    // Update is called once per frame
    private void Update()
    {
        // Update start and end points in proportion to track platform group scale.

        // X / trackPathGroupTransform.localScale.z = originalStartPointPositionZ / originalTrackPlatformGroupScaleZ.
        myStartPointTransform.localPosition = new Vector3(myStartPointTransform.localPosition.x, myStartPointTransform.localPosition.y, UpdateTrackPaths(originalStartPointPositionZ));

        // X / trackPathGroupTransform.localScale.z = originalEndPointPositionZ / originalTrackPlatformGroupScaleZ.
        myEndPointTransform.localPosition = new Vector3(myEndPointTransform.localPosition.x, myEndPointTransform.localPosition.y, UpdateTrackPaths(originalEndPointPositionZ));
    }

    private float UpdateTrackPaths(float original)
    {
        float nominator1 = trackPathGroupTransform.localScale.z * original / originalTrackPlatformGroupScaleZ;
        float nominator2 = 1 * nominator1 / trackPathGroupTransform.localScale.z; // Integer value one(1) represents path scale.

        return nominator2;
    }
}
