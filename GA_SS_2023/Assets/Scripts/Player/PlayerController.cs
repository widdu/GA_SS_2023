using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    // Serialized private values
    // [SerializeField] private Vector3 movement;    Inspectable value while uncommented
    [SerializeField] private float jumpHeight = 2;
    [SerializeField] ScoreBoard scoreBoard;
    [SerializeField] private List<BarrelSpawner> barrelSpawnerList;

    // Private values
    private PlayerCollider playerCollider;
    private IMover mover;
    private Coroutine toggleIsJumping;
    private Animator animator;
    private Vector3 playerOriginalPosition, movement, targetPosition, startPointDistanceToTrackStartPoint, distanceBetweenTracks;
    private float movementSpeed, trackAngle, movementSpeedSlopeSubtraction;
    private bool waitForRelease = false, queueJump = false, isJumping = false, switchingTrack = false;

    public bool WaitForRelease { get { return waitForRelease; } set { waitForRelease = value; } }

    public bool QueueJump { get { return queueJump; } set { queueJump = value; } }

    public bool IsJumping { get { return isJumping; } set { isJumping = value; } }

    public bool SwitchingTrack { get { return switchingTrack; } set { switchingTrack = value; } }
    public GameObject Fade;
    public FadeScript fadeScript;
    private enum Path
    {
        Start, // 0
        Track // 1
    }

    private Path path;

    private enum Track
    {
        Left = -1,
        Middle = 0, // 1
        Right = 1 // 2
    }

    private Track track;

    private void Awake()
    {
        playerCollider = GetComponent<PlayerCollider>();
        if (playerCollider == null)
        {
            Debug.LogWarning("Can't get player object's child object Collider's player collider component for player controller component!");
        }

        mover = GetComponent<IMover>();
        if (mover == null)
        {
            Debug.LogWarning("Can't find the IMover interface!");
        }

        animator = GetComponentInChildren<Animator>();
        if(animator == null)
        {
            Debug.LogWarning("Can't find player object child's animator component for player controller component!");
        }
    }

    // Start is called before the first frame update
    private void Start()
    {
        fadeScript = Fade.GetComponent<FadeScript>();
        playerOriginalPosition = transform.localPosition;

        path = Path.Start;

        track = Track.Middle;
    }

    // Update is called once per frame
    private void Update()
    {
        if (targetPosition != Vector3.zero)
        {
            MoveCharacter();
        }
        if (playerCollider.IsActive && isJumping && path == Path.Track)
        {
            isJumping = false;
            animator.Play("Idle and Move");
        }
        /*animator.SetFloat("MoveX", movement.normalized.z);
        animator.SetFloat("MoveZ", movement.normalized.x);*/
    }

    private void FixedUpdate()
    {
        if (queueJump)
        {
            if (path == Path.Track && playerCollider.IsActive && !isJumping)
            {
                mover.Jump(jumpHeight);
                toggleIsJumping = StartCoroutine(ToggleIsJumping());
                queueJump = false;
            }
        }
    }

    private IEnumerator ToggleIsJumping()
    {
        yield return !playerCollider.IsActive;
        isJumping = true;
        animator.SetTrigger("JumpTrigger");
        StopCoroutine(toggleIsJumping);
    }

    public void Setup(Vector3 startPointRightTransformPosition, float startPointMiddleTransformPositionX, Vector3 trackStartRightTargetPosition, Vector3 trackStartMiddleTargetPosition, float trackPlatformGroupLocalEulerAngleX)
    {
        // Movement speed units per second is equals to distance between points in X-axis.
        movementSpeed = startPointRightTransformPosition.x - startPointMiddleTransformPositionX;

        // Distance between start path point and respective track path start point.
        startPointDistanceToTrackStartPoint = new Vector3(0f, 0f, trackStartRightTargetPosition.z - startPointRightTransformPosition.z);

        // Distance between tracks.
        distanceBetweenTracks = new Vector3(trackStartRightTargetPosition.x - trackStartMiddleTargetPosition.x, 0f, 0f);

        // Save track platform group Z rotation for when player gets on a track platform.
        trackAngle = trackPlatformGroupLocalEulerAngleX;

        // Subtract the effect of the slope on the track from movement speed and save it to the following variable.
        movementSpeedSlopeSubtraction = movementSpeed - ((360 - Mathf.Abs(trackAngle)) * movementSpeed / 360);
    }

    private void MoveCharacter()
    {
        Vector2 playerPositionXZ = new Vector2(transform.localPosition.x, transform.localPosition.z);
        Vector2 targetPositionXZ = new Vector2(targetPosition.x, targetPosition.z);
        float distance = Vector2.Distance(playerPositionXZ, targetPositionXZ);

        if (Mathf.Max(Mathf.Abs(movement.x), Mathf.Abs(movement.z)) < distance)
        {
            transform.Translate(movement);
        }
        else
        {
            transform.position = new Vector3(targetPosition.x, transform.position.y, targetPosition.z);

            if(targetPosition.z > 0)
            {
                path = Path.Track;
                transform.localEulerAngles = new Vector3(trackAngle * (int)path, transform.rotation.y, transform.rotation.z);
            }

            targetPosition = Vector2.zero;
            animator.SetFloat("MoveZ", 0);
        }
    }

    public void MovementSwitch(Vector2 moveInput)
    {
        switch (path)
        {
            case Path.Start:
                if (targetPosition == Vector3.zero && moveInput.y != -1)
                {
                    MovementTargetStart(moveInput);
                }
                break;
            case Path.Track:
                MovementTargetTrack(moveInput);
                break;
        }
    }

    private void MovementTargetStart(Vector2 moveInput)
    {
        if (moveInput.x != 0 && moveInput.x != (int)track) // Prioritize horizontal movement.
        {
            Vector3 direction = new Vector3(moveInput.x, 0f, 0f) * movementSpeed;
            movement = direction * Time.deltaTime;
            targetPosition = direction + transform.position;
            track = track + (int)moveInput.x;
            animator.SetFloat("MoveZ", moveInput.x);
        }
        else if(moveInput.x == 0 && moveInput.y != 0)
        {
            // TODO: isJumping from start platform point to track platform point.
            Vector3 direction = new Vector3(0f, 0f, moveInput.y) * movementSpeed;
            movement = direction * Time.deltaTime;
            targetPosition = startPointDistanceToTrackStartPoint + transform.position;
            toggleIsJumping = StartCoroutine(ToggleIsJumping());
        }
    }

    private void MovementTargetTrack(Vector2 moveInput)
    {
        if (!switchingTrack)
        {
            if (moveInput.x != 0 && moveInput.x != (int)track && targetPosition == Vector3.zero && playerCollider.IsActive) // Prioritize horizontal movement.
            {
                Vector3 direction = new Vector3(moveInput.x, 0f, 0f) * movementSpeed;
                movement = direction * Time.deltaTime;
                targetPosition = moveInput.x * distanceBetweenTracks + transform.position;
                queueJump = true;
                switchingTrack = true;
                track = track + (int)moveInput.x;
            }
            else if (moveInput.x == 0 && moveInput.y != 0 && targetPosition == Vector3.zero)
            {
                Vector3 direction = new Vector3(0f, 0f, moveInput.y) * movementSpeed;
                movement = direction * Time.deltaTime;
                targetPosition = movement + transform.position;
            }
        }
    }

    public void ReachGoal(GoalScore goalScore)
    {
        fadeScript.FadeOut();
        transform.localPosition = playerOriginalPosition;
        targetPosition = Vector3.zero;
        path = Path.Start;
        track = Track.Middle;
        waitForRelease = true;
        goalScore.Score += 1;
        scoreBoard.UpdateScore();
        ResetBarrelSpawners();
        DoFade();
        fadeScript.FadeIn();
    }

    private void ResetBarrelSpawners()
    {
        foreach(BarrelSpawner barrelSpawner in barrelSpawnerList)
        {
            barrelSpawner.DestroyAllBarrels();
        }
    }

    IEnumerator DoFade(){
        yield return new WaitForSeconds(2);
    }

    public void ResetLevel()
    {
        // TODO: Record and reset score, restart run
        fadeScript.FadeOut();

        // TODO: Move the line below to scene managing script!
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        DoFade();
        fadeScript.FadeIn();
    }

    public void AnimatorSetFloat(Vector2 moveInput)
    {
        if(path == Path.Track && !isJumping && !switchingTrack)
        {
            animator.SetFloat("MoveX", moveInput.y);
        }
        else
        {
            animator.SetFloat("MoveX", 0);
        }
    }
}
