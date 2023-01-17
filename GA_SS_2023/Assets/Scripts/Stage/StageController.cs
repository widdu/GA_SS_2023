using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageController : MonoBehaviour
{
    // Serialized private variables.
    [SerializeField] private Transform playerTransform;
    [SerializeField] private Transform trackPlatformGroupTransform;
    [SerializeField] private Transform ballPlatformTransform;
    [SerializeField] private float platformDistanceAddition;

    // Private variables.
    // Player transform values.
    private Vector3 playerOriginalPosition;

    // Track platform group transform values.
    private float trackOriginalPositionY;
    private float trackOriginalPositionZ;
    private float trackOriginalScaleZ;

    // Ball platform values.
    private float ballPlatformOriginalPositionZ;
    private float ballPlatformOriginalPositionY;
    private float ballPlatformOriginalScaleY;

    private void Awake()
    {
        
    }

    // Start is called before the first frame update.
    private void Start()
    {
        // Get original values.
        // Player values.
        playerOriginalPosition = playerTransform.position;

        // Track platform group values.
        trackOriginalScaleZ = trackPlatformGroupTransform.localScale.z;
        trackOriginalPositionY = trackPlatformGroupTransform.position.y;
        trackOriginalPositionZ = trackPlatformGroupTransform.position.z;

        // Ball platform values.
        ballPlatformOriginalScaleY = ballPlatformTransform.transform.localScale.y;
        ballPlatformOriginalPositionY = ballPlatformTransform.transform.position.y;
        ballPlatformOriginalPositionZ = ballPlatformTransform.transform.position.z;

        // Reset platform distance addition upon starting the game.
        platformDistanceAddition = 0f;
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
            ballPlatformTransform.position = new Vector3(ballPlatformTransform.position.x, ballPlatformTransform.position.y, ballPlatformOriginalPositionZ + platformDistanceAddition);

            // Update track platform group's and ball platform's transform values proportionally. Some yet unknown variables are missing from the formulas,
            // because the objects shouldn't grow more detached from each other as the platformDistanceAddition increases.

            // X / ballPlatformTransform.position.z = trackOriginalScaleZ / ballPlatformOriginalPositionZ.
            //float trackNominatorScaleZ = ballPlatformTransform.position.z * trackOriginalScaleZ / ballPlatformOriginalPositionZ; // WIP
            trackPlatformGroupTransform.localScale = new Vector3(trackPlatformGroupTransform.localScale.x, trackPlatformGroupTransform.localScale.y, UpdateStageObjects(trackOriginalScaleZ));

            // X / ballPlatformTransform.position.z = trackOriginalPositionY / ballPlatformOriginalPositionZ.
            //float trackNominatorPositionY = ballPlatformTransform.position.z * trackOriginalPositionY / ballPlatformOriginalPositionZ; // WIP
            trackPlatformGroupTransform.position = new Vector3(trackPlatformGroupTransform.position.x, UpdateStageObjects(trackOriginalPositionY), trackPlatformGroupTransform.position.z);

            // X / ballPlatformTransform.position.z = trackOriginalPositionZ / ballPlatformOriginalPositionZ.
            //float trackNominatorPositionZ = ballPlatformTransform.position.z * trackOriginalPositionZ / ballPlatformOriginalPositionZ; // WIP
            trackPlatformGroupTransform.position = new Vector3(trackPlatformGroupTransform.position.x, trackPlatformGroupTransform.position.y, UpdateStageObjects(trackOriginalPositionZ));

            // X / ballPlatformTransform.position.z = ballPlatformOriginalScaleY / ballPlatformOriginalPositionZ.
            //float ballPlatformNominatorScaleY = ballPlatformTransform.position.z * ballPlatformOriginalScaleY / ballPlatformOriginalPositionZ; // WIP
            ballPlatformTransform.localScale = new Vector3(ballPlatformTransform.localScale.x, UpdateStageObjects(ballPlatformOriginalScaleY), ballPlatformTransform.localScale.z);

            // X / ballPlatformTransform.position.z = ballPlatformOriginalPositionY / ballPlatformOriginalPositionZ.
            //float ballPlatformNominatorPositionY = ballPlatformTransform.position.z * ballPlatformOriginalPositionY / ballPlatformOriginalPositionZ; // WIP
            ballPlatformTransform.position = new Vector3(ballPlatformTransform.position.x, UpdateStageObjects(ballPlatformOriginalPositionY), ballPlatformTransform.position.z);
    }

    private float UpdateStageObjects(float original)
    {
        float nominator = ballPlatformTransform.position.z * original / ballPlatformOriginalPositionZ;
        return nominator;
    }
}
