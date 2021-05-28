using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    public float speed;
    public Vector2 direction;

    void Update() => transform.Translate(direction * (speed * Time.deltaTime));

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Wall") || other.CompareTag("Player") || other.CompareTag("Monster"))
        {
            Destroy(gameObject);
        }
    }
}
