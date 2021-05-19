using System.Threading.Tasks;
using Unity.Jobs;
using Unity.Mathematics;
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

        private int2[] obstacles = new int2[]
        {
            new int2(33, 30),
            new int2(36, 34),
            new int2(23, 18),
            new int2(7, 28)
        };
         
        public void Start()
        {
            rigitbody = GetComponent<Rigidbody2D>();
        }

        public void FixedUpdate()
        {
            var playerPosition = player.transform.position;
            var monsterPosition = transform.position;
            var task = Task.Run(() => new Searcher(playerPosition, monsterPosition, fieldOfView * 2, obstacles)
                .GetPathAStar());
            var path = task.Result;
            if (!(path is null))
            {
                var step = speed * Time.deltaTime;
                foreach (var point in path)
                    transform.position = Vector2.MoveTowards(transform.position, 
                        new Vector2(point.x, point.y), step);
                transform.position = Vector2.MoveTowards(transform.position, 
                    player.transform.position, step);
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