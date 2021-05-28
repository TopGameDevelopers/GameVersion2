using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameThrower : MonoBehaviour
{
    public GameObject fireBall;
    public Transform shotDirection;
    private float _timeShot;
    public float startTime;

    private AudioSource _audioSource;

    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }
    
    void Update()
    {
        if (_timeShot <= 0)
        {
            Instantiate(fireBall, shotDirection.position, Quaternion.identity);
            _audioSource.Play();
            _timeShot = startTime;
        }
        else
        {
            _timeShot -= Time.deltaTime;
        }
    }
}
