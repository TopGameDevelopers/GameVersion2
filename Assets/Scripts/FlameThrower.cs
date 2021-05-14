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
    // Start is called before the first frame update
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
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
