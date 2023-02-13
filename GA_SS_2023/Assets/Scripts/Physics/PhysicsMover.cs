using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsMover : MonoBehaviour, IMover
{
    // Private variables
    private new Rigidbody rigidbody;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        if (rigidbody == null)
        {
            Debug.LogWarning("Can't find " + gameObject.name + "'s rigidbody component!");
        }
    }

    public void Jump(float height)
    {
        //Remove earlier jump forces
        /*rigidbody.velocity = new Vector2(rigidbody.velocity.x, 0f);
        rigidbody.angularVelocity = 0f;*/

        // F = m * a || F = Force, m = mass, a = acceleration
        float m = rigidbody.mass;

        // Solve the initial acceleration(a) with the formula: a = v / t. || v = speed, t = time

        // Solve the initial speed(v) and the time(t) to reach the highest point of the jump.
        // P = 1/2 * a * t^2 + v * t + P0 || P0 = initial position, P = jump height = h
        // Acceleration equals gravity || a = g || g = gravity which is the only force which affects the jump after it has started
        // h = 1/2 * g * t^2 || We can calculate time from this
        // t = sqrt(h / 0.5g)

        // Now we know how long it takes to reach the max. height
        // Next we must solve the initial velocity. Since the graph of a jump is symmetrical,
        // the initial velocity equals the final velocity.
        // vInitial = vFinal

        // a = v / t || *t => v = a * t => v = gt

        // Now we can solve the acceleration
        // a = v / t

        // And force
        // F = m * a

        float g = Mathf.Abs(Physics.gravity.y);
        float t = Mathf.Sqrt(height / (g * 0.5f));

        float F = m * (g * t);

        rigidbody.AddForce(new Vector2(0, F), ForceMode.Impulse);
    }
}
