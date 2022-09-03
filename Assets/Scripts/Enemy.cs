using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Enemy : collisionChecks
{
    [Header("Enemy settings")]
    public int health;
    public float Speed;

    [Header("Raycast settings")]
    public float hitRayLength;
    public LayerMask rayCollisionLayers;
    [HideInInspector]
    public Collider2D boundingBox;

    void Awake()
    {
        boundingBox = gameObject.GetComponent<BoxCollider2D>();
    }
}
