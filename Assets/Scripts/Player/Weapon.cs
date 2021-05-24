using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Weapon : MonoBehaviour
{
    [FormerlySerializedAs("Offset")] public float offset;

    public GameObject bullet;

    public Transform shotDirection;

    private float _timeShot;

    public float startTime;

    private bool facingLeft;

    public Camera playCamera;

    private AudioSource _audio;
    // Start is called before the first frame update
    void Start()
    {
        _audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        var difference = playCamera.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        var rotateZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rotateZ + offset);

        if (rotateZ + offset >= 90 && rotateZ + offset <= 180 && !facingLeft ||
            rotateZ + offset > -180 && rotateZ + offset < -90 && !facingLeft ||
            rotateZ + offset >= 0 && rotateZ + offset < 90 && facingLeft ||
            rotateZ + offset >= -90 && rotateZ + offset < 0 && facingLeft)
            Flip();

        if (_timeShot <= 0)
        {
            if (Input.GetMouseButtonDown(0))
            {
                _audio.Play();
                Instantiate(bullet, shotDirection.position, transform.rotation);
                _timeShot = startTime;
            }
        }
        else
        {
            _timeShot -= Time.deltaTime;
        }
    }
    
    private void Flip()
    {
        facingLeft = !facingLeft;
        var scaler = transform.localScale;
        scaler.y *= -1;
        transform.localScale = scaler;
    }
}
