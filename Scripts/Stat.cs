using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stat : MonoBehaviour
{
    public float initialXVelocity;
    public float initialYVelocity;

    public float coefficientFriction;
    public float mass;

    public float FrictionForce { set { }  get { return mass * (float)9.81 * coefficientFriction; } }
    public float FrictionAcceleration { set { } get { return FrictionForce / mass; } }


}
