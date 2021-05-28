using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Serialization;

namespace Monster
{
    public class MonsterController : MonoBehaviour
    {
        public GameObject player;
        [FormerlySerializedAs("Speed")] public float speed;
        private AudioSource _audio;
        public int fieldOfView;

        public GameObject[] obstacleObjects;
        private int2[] _obstacles;
        
         
        public void Start()
        {
            _audio = GetComponent<AudioSource>();
            GetObstaclesCoordinates();
        }

        public void FixedUpdate()
        {
            var playerPosition = player.transform.position;
            var monsterPosition = transform.position;
            var task = Task.Run(() => new Searcher(playerPosition, monsterPosition, fieldOfView * 2, _obstacles)
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
                _audio.Play();
            }
        }

        public void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Bullet"))
            {
                Destroy(gameObject);
            }
        }

        private void GetObstaclesCoordinates()
        {
            if (obstacleObjects is null) return;
            _obstacles = new int2[obstacleObjects.Length];
            for (var i = 0; i < obstacleObjects.Length; i++)
            {
                var obstacleCoordinates = obstacleObjects[i].transform.position;
                _obstacles[i] = new int2((int) obstacleCoordinates.x, (int) obstacleCoordinates.y);
            }   
        }
    }
}