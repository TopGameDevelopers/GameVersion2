using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Player
{
    public class PlayerController : MonoBehaviour
    {
        [FormerlySerializedAs("FirstCam")] public Camera firstCam;
        public float speed;
        public Rigidbody2D rigitbody;
        public Animator anim;

        private bool _facingLeft;
        private bool _damagedRecently;

        public GameObject weapon;
        public GameObject openedChest;

        public int health;
        public int heartsNumber;
        public float imperviousModeTime;
        private float _imperviousModeTimeLeft;

        
        public int level;
        //public HashSet<int> levelsCompleted;
        public int startsAmount;
        //public Dictionary<int, int> progressInformation;

        public Image[] hearts;
        public Sprite heart;
        public Sprite emptyHeart;
        
        private SpriteRenderer[] _playerBodySprites;
        private Color _defaultPlayerColor;
        private Color _recentlyDamagedColor;
    
        public Image[] stars;
        public Sprite fullStar;
        public Sprite emptyStar;

        private AudioSource _coinAudioSource;
        public AudioSource gemAudioSource;
        public AudioSource buttonAudioSource;
        public AudioSource healingAudioSource;
        public GameObject chestsCoins;
        public GameObject healing;

        [FormerlySerializedAs("Menu")] public GameObject finalMenu;
        [FormerlySerializedAs("RestartMenu")] public GameObject restartMenu;
        [FormerlySerializedAs("CCollect")] public GameObject cCollect;
        public GameObject Health;

        private float _volume;

        public void Start()
        {
            Time.timeScale = 1f;
            anim = GetComponent<Animator>();
            rigitbody = GetComponent<Rigidbody2D>();
            _coinAudioSource = GetComponent<AudioSource>();

            _playerBodySprites = GetComponentsInChildren<SpriteRenderer>();
            _defaultPlayerColor = _playerBodySprites[0].color;
            _recentlyDamagedColor = new Color(_defaultPlayerColor.r,
                _defaultPlayerColor.g, _defaultPlayerColor.b, 0.6f);
            _imperviousModeTimeLeft = imperviousModeTime;

            level = SceneManager.GetActiveScene().buildIndex - 1;

            finalMenu.SetActive(false);
            restartMenu.SetActive(false);
        }

        private void GetFinishMenu()
        {
            finalMenu.SetActive(true);
            gemAudioSource.Play();
            ShowStars();
            Time.timeScale = 0f;
            cCollect.gameObject.SetActive(false);
            Health.gameObject.SetActive(false);
            Destroy(weapon);
            
            SaveProgress();
        }

        private void SaveProgress()
        {
            var progressInformation = GetProgressInformation();
            SaveSystem.SaveProgress(progressInformation);
        }

        private Dictionary<int, int> GetProgressInformation()
        {
            var playerData = SaveSystem.LoadProgress();
            
            if (playerData is null)
            {
                var progressInfo = new Dictionary<int, int>();
                progressInfo[level] = startsAmount;
                return progressInfo;
            }
            
            if (playerData.ProgressInformation.ContainsKey(level) &&
                playerData.ProgressInformation[level] < startsAmount ||
                !playerData.ProgressInformation.ContainsKey(level))
                playerData.ProgressInformation[level] = startsAmount;
            return playerData.ProgressInformation;
            
        }

        private void ShowStars()
        {
            foreach (var star in stars)
                star.sprite = emptyStar;
            if (CoinCollect.CoinCount >= 10)
            {
                stars[0].sprite = fullStar;
                startsAmount++;
            }

            if (CoinCollect.CoinCount >= 20)
            {
                stars[1].sprite = fullStar;
                startsAmount++;
            }

            if (CoinCollect.CoinCount >= 30)
            {
                stars[2].sprite = fullStar;
                startsAmount++;
            }
        }
    
        public void FixedUpdate()
        {
            UpdateHealthSystem();
            MovePlayer();
            UpdateDamageSystem();
        }

        private void UpdateDamageSystem()
        {
            if (_damagedRecently)
                if (_imperviousModeTimeLeft <= 0)
                {
                    _damagedRecently = false;
                    ChangePlayerColor(_defaultPlayerColor);
                    _imperviousModeTimeLeft = imperviousModeTime;
                }
                else
                    _imperviousModeTimeLeft -= Time.deltaTime;
        }

        private void UpdateHealthSystem()
        {
            if (health <= 0)
            {
                restartMenu.gameObject.SetActive(true);
                Time.timeScale = 0f;
            }
            for (var i = 0; i < hearts.Length; i++)
            {
                hearts[i].sprite = i < health ? heart : emptyHeart;
                hearts[i].enabled = i < heartsNumber;
            }
        }
        
        private void MovePlayer()
        {
            var moveInputX = Input.GetAxis("Horizontal");
            var moveInputY = Input.GetAxis("Vertical");
            anim.SetBool("IsRunning", moveInputX != 0 || moveInputY != 0);
            rigitbody.velocity = new Vector2(moveInputX, moveInputY) * speed;
            if (moveInputX > 0 && !_facingLeft || moveInputX < 0 && _facingLeft)
                Flip();
        }

        private void Flip()
        {
            _facingLeft = !_facingLeft;
            var scaler = transform.localScale;
            scaler.x *= -1;
            transform.localScale = scaler;
            weapon.transform.localScale = scaler;
        }

        private void ChangePlayerColor(Color color)
        {
            foreach (var playerBodySprite in _playerBodySprites) 
                playerBodySprite.color = color;
        }
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("DamageObject") || other.CompareTag("Spikes"))
            {
                health--;
                heartsNumber--;
            }

            if (other.CompareTag("Monster"))
            {
                ProcessMonsterAttack();
            }

            if (other.CompareTag("Lava"))
            {
                health--;
                heartsNumber--;
                speed /= 2;
            }
        
            if (other.CompareTag("Coin"))
            {
                _coinAudioSource.Play();
                CoinCollect.CoinCount++;
                Destroy(other.gameObject);
            }

            if (other.CompareTag("Gem"))
            {
                GetFinishMenu();
                Destroy(other.gameObject);
            }

            if (other.CompareTag("HealChest"))
            {
                healingAudioSource.Play();
                health++;
                if (health > 5)
                    health = 5;
                heartsNumber++;
                Instantiate(healing, transform.position, Quaternion.identity);
                Destroy(other.gameObject);
            }

            if (other.CompareTag("Button"))
            {
                buttonAudioSource.Play();
            }
            
            if (other.CompareTag("Chest"))
            {
                CoinCollect.CoinCount += 5;
                Destroy(other.gameObject);
                Instantiate(openedChest, other.transform.position, Quaternion.identity);
                _coinAudioSource.Play();
                Instantiate(chestsCoins, transform.position, Quaternion.identity);
            }

            if (other.CompareTag("Train Flag"))
            {
                SceneManager.LoadScene(1);
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.CompareTag("Lava"))
            {
                speed *= 2;
            }
        }

        private void OnTriggerStay2D(Collider2D other)
        {
            if (other.CompareTag("Monster"))
            {
                ProcessMonsterAttack();
            }
        }

        private void ProcessMonsterAttack()
        {
            if (!_damagedRecently)
            {
                _damagedRecently = true;
                ChangePlayerColor(_recentlyDamagedColor);
                health--;
                heartsNumber--;
            }
        }
    }
}
