using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageController : MonoBehaviour
{
    // Serialized private variables.
    [SerializeField] private GameObject player;
    [SerializeField] private Transform trackPlatformGroupTransform, ballPlatformTransform, trackPathGroupTransform;

    // Start platform points.
    [SerializeField] private Transform startPointLeftTransform, startPointMiddleTransform, startPointRightTransform;

    // Track start points.
    [SerializeField] private Transform trackStartLeftTransform, trackStartMiddleTransform, trackStartRightTransform;

    // Track end points.
    [SerializeField] private Transform trackEndLeftTransform, trackEndMiddleTransform, trackEndRightTransform;

    [SerializeField] private TrackPropertyDistribution trackPropertyDistribution;
    [SerializeField] private float platformDistanceAddition;

    // Private variables.
    private PlayerController playerController;

    private Transform playerTransform;

    // Start platform point transforms.
    private Vector3 startLeftTargetPosition, startMiddleTargetPosition, startRightTargetPosition;

    // Track start point transforms.
    private Vector3 trackStartLeftTargetPosition, trackStartMiddleTargetPosition, trackStartRightTargetPosition;

    // Track end point transforms.
    private Vector3 trackEndLeftTargetPosition, trackEndMiddleTargetPosition, trackEndRightTargetPosition;

    // Player transform values.
    private Vector3 playerOriginalPosition;

    // Track platform group transform values.
    private float trackOriginalPositionY, trackOriginalPositionZ, trackOriginalScaleZ;

    // Ball platform values.
    private float ballPlatformOriginalPositionZ, ballPlatformOriginalPositionY, ballPlatformOriginalScaleY;

    private void Awake()
    {
        playerController = player.GetComponent<PlayerController>();
        if(playerController == null)
        {
            Debug.LogWarning("Can't find player controller component for stage controller component!");
        }

        playerTransform = player.GetComponent<Transform>();
        if (playerTransform == null)
        {
            Debug.LogWarning("Can't find player object's transform component for stage controller component!");
        }
    }

    // Start is called before the first frame update.
    private void Start()
    {
        // Get start point positions.
        startLeftTargetPosition = startPointLeftTransform.position;
        startMiddleTargetPosition = startPointMiddleTransform.position;
        startRightTargetPosition = startPointRightTransform.position;

        // Get track start point positions.
        trackStartLeftTargetPosition = trackStartLeftTransform.position;
        trackStartMiddleTargetPosition = trackStartMiddleTransform.position;
        trackStartRightTargetPosition = trackStartRightTransform.position;

        // Get track end point positions.
        trackEndLeftTargetPosition = trackEndLeftTransform.position;
        trackEndMiddleTargetPosition = trackEndMiddleTransform.position;
        trackEndRightTargetPosition = trackEndRightTransform.position;

        // Get original values.
        // Player values.
        playerOriginalPosition = playerTransform.localPosition;

        // Track platform group values.
        trackOriginalScaleZ = trackPlatformGroupTransform.localScale.z;
        trackOriginalPositionY = trackPlatformGroupTransform.localPosition.y;
        trackOriginalPositionZ = trackPlatformGroupTransform.localPosition.z;

        // Get the platforms' Z scale value for distributing it for paths.
        trackPropertyDistribution.OriginalTrackPlatformGroupScaleZ = trackOriginalScaleZ;

        // Ball platform values.
        ballPlatformOriginalScaleY = ballPlatformTransform.transform.localScale.y;
        ballPlatformOriginalPositionY = ballPlatformTransform.transform.localPosition.y;
        ballPlatformOriginalPositionZ = ballPlatformTransform.transform.localPosition.z;

        // Reset platform distance addition upon starting the game.
        platformDistanceAddition = 0f;

        playerController.Setup(startRightTargetPosition, startMiddleTargetPosition.x, trackStartRightTargetPosition.z, trackPlatformGroupTransform.localEulerAngles.x);
    }

    // Update is called once per frame.
    private void Update()
    {
        // Indented lines are supposed to go under "StageUpdate" method in the future, when there are interactable objects implemented!

        // When player interacts with the interactable object which is supposed to be placed on the top of every track platform, player's score increases by 1 and their position is reseted to
        // playerOriginalPosition!

        // The object rescaling and repositioning is supposed to happen when every row of interactable objects have been interacted ATLEAST for the same amount of times, for example:
        // 1-1-1: rescaling and repositioning has happened ONCE.
        // 2-3-1: rescaling and repositioning has happened ONCE.
        // 2-3-2: rescaling and repositioning has happened TWICE.
        // 2-3-3: rescaling and repositioning has happened TWICE.
        // 3-3-3: rescaling and repositioning has happened THRICE.
        // 5-4-3: rescaling and repositioning has happened THRICE.
        // Rescaling and repositioning can be demonstrated by increasing platfromDistanceAddition value, the component is attached to Stage object!

            // Add platformDistanceAddition value to Ball platform transform position z value.
            ballPlatformTransform.localPosition = new Vector3(ballPlatformTransform.localPosition.x, ballPlatformTransform.localPosition.y, ballPlatformOriginalPositionZ + platformDistanceAddition);

            // Update track platform group's and ball platform's transform values proportionally. Some yet unknown variables are missing from the formulas,
            // because the objects shouldn't grow more detached from each other as the platformDistanceAddition increases.

            // X / ballPlatformTransform.localPosition.z = trackOriginalScaleZ / ballPlatformOriginalPositionZ.
            trackPlatformGroupTransform.localScale = new Vector3(trackPlatformGroupTransform.localScale.x, trackPlatformGroupTransform.localScale.y, UpdateStageObjects(trackOriginalScaleZ));

            // Update Track path group local scale according to Track platform group local scale.
            trackPathGroupTransform.localScale = trackPlatformGroupTransform.localScale;

            // X / ballPlatformTransform.localPosition.z = trackOriginalPositionY / ballPlatformOriginalPositionZ.
            trackPlatformGroupTransform.localPosition = new Vector3(trackPlatformGroupTransform.localPosition.x, UpdateStageObjects(trackOriginalPositionY), trackPlatformGroupTransform.localPosition.z);

            // X / ballPlatformTransform.localPosition.z = trackOriginalPositionZ / ballPlatformOriginalPositionZ.
            trackPlatformGroupTransform.localPosition = new Vector3(trackPlatformGroupTransform.localPosition.x, trackPlatformGroupTransform.localPosition.y, UpdateStageObjects(trackOriginalPositionZ));

            // Update Track path group position according to Track platform group position.
            trackPathGroupTransform.localPosition = trackPlatformGroupTransform.localPosition;

            // X / ballPlatformTransform.localPosition.z = ballPlatformOriginalScaleY / ballPlatformOriginalPositionZ.
            ballPlatformTransform.localScale = new Vector3(ballPlatformTransform.localScale.x, UpdateStageObjects(ballPlatformOriginalScaleY), ballPlatformTransform.localScale.z);

            // X / ballPlatformTransform.localPosition.z = ballPlatformOriginalPositionY / ballPlatformOriginalPositionZ.
            ballPlatformTransform.localPosition = new Vector3(ballPlatformTransform.localPosition.x, UpdateStageObjects(ballPlatformOriginalPositionY), ballPlatformTransform.localPosition.z);
    }

    private float UpdateStageObjects(float original)
    {
        float nominator = ballPlatformTransform.localPosition.z * original / ballPlatformOriginalPositionZ; // WIP, some yet unknown factor(s) missing.
        return nominator;
    }
}
