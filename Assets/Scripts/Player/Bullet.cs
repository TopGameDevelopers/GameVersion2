using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed;
    public float destroyTime;

    void Start()
    {
        Invoke(nameof(DestroyBullet), destroyTime);
    }
    
    void Update()
    {
        transform.Translate(Vector2.right * (speed * Time.deltaTime));
    }

    private void DestroyBullet()
    {
        Destroy(gameObject);
    }

    /*private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }
    }*/
}
