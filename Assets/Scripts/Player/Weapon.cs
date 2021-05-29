using UnityEngine;
using UnityEngine.Serialization;

namespace Player
{
    public class Weapon : MonoBehaviour
    {
        public float startTime;
        [FormerlySerializedAs("Offset")] public float offset;
        public GameObject bullet;
        public Transform shotDirection;
        public Camera playCamera;

        private float _timeShot;
        private bool _facingLeft;
        private AudioSource _audio;

        void Start()
        {
            _audio = GetComponent<AudioSource>();
        }
    
        void Update()
        {
            RotateWeapon();
            Shot();
        }

        private void RotateWeapon()
        {
            var difference = playCamera.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            var rotateZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, rotateZ + offset);

            if (rotateZ + offset >= 90 && rotateZ + offset <= 180 && !_facingLeft ||
                rotateZ + offset > -180 && rotateZ + offset < -90 && !_facingLeft ||
                rotateZ + offset >= 0 && rotateZ + offset < 90 && _facingLeft ||
                rotateZ + offset >= -90 && rotateZ + offset < 0 && _facingLeft)
                Flip();
        }

        private void Shot()
        {
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
                _timeShot -= Time.deltaTime;
        }
    
        private void Flip()
        {
            _facingLeft = !_facingLeft;
            var scaler = transform.localScale;
            scaler.y *= -1;
            transform.localScale = scaler;
        }
    }
}
