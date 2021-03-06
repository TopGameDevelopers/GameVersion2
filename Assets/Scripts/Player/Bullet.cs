using UnityEngine;

namespace Player
{
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
    }
}
