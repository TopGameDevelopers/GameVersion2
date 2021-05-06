using UnityEngine;
using UnityEngine.Serialization;

namespace Monster
{
    public class MonsterController : MonoBehaviour
    {
        public GameObject player;
        [FormerlySerializedAs("Speed")] public float speed;
        public Rigidbody2D rigitbody;
        public int fieldOfView;
        
        public void Start()
        {
            rigitbody = GetComponent<Rigidbody2D>();
        }

        public void FixedUpdate()
        {
            var searcher = new Searcher(player, gameObject, fieldOfView * 2);
            var path = searcher.GetPathAStar();
            if (!(path is null))
                foreach (var point in path)
                {
                    var step = speed*Time.deltaTime;
                    transform.position = Vector2.MoveTowards(transform.position, new Vector2(point.x,point.y), step);
                }
        }

        public void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Bullet"))
            {
                Destroy(gameObject);
            }
        }
    }
}