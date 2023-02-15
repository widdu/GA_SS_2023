using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackController : MonoBehaviour
{
    [SerializeField] private float trackSpeed = 0;


    public Vector3 TrackSpeedV3
    {
        get { return new Vector3(0f, 0f, trackSpeed); }
    }

    public float TrackSpeedF
    {
        get { return trackSpeed; }
        set { trackSpeed = value; }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
