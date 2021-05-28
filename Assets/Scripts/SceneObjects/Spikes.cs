using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour
{
    public GameObject[] dangerousSpikes;
    public GameObject[] safeSpikes;
    private float _timeShot;
    public float startTime;
    private bool _dangerousNow;
    private AudioSource _audio;
    void Start()
    {
        _audio = GetComponent<AudioSource>();
    }
    
    void Update()
    {
        if (_timeShot <= 0)
        {
            if (_dangerousNow)
            {
                for (var i = 0; i < dangerousSpikes.Length; i++)
                {
                    dangerousSpikes[i].SetActive(false);
                    safeSpikes[i].SetActive(true);
                }
                _dangerousNow = false;
            }
            else
            {
                for (var i = 0; i < dangerousSpikes.Length; i++)
                {
                    dangerousSpikes[i].SetActive(true);
                    safeSpikes[i].SetActive(false);
                }
                _dangerousNow = true;
                _audio.Play();
            }
            _timeShot = startTime;
        }
        else
            _timeShot -= Time.deltaTime;
    }
}
