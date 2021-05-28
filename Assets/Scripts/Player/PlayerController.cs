using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public Camera FirstCam;
    public Camera RestartCam;
    public Camera BeginCam;

    public float speed;
    public Rigidbody2D rigitbody;
    public Animator anim;

    private bool facingLeft;

    public GameObject weapon;
    public GameObject openedChest;

    private int coinCount;

    public int health;
    public int heartsNumber;

    public Image[] hearts;
    public Sprite heart;
    public Sprite emptyHeart;


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
    public GameObject RestartMenu;
    public GameObject CCollect;
    public GameObject Health;
    public Text finishText;

    public void Start()
    {
        if (BeginCam != null)
        {
            BeginCam.gameObject.SetActive(true);
            FirstCam.gameObject.SetActive(false);
        }
        else
        {
            FirstCam.gameObject.SetActive(true);
        }
        Time.timeScale = 1f;
        BeginCam.gameObject.SetActive(true);
        anim = GetComponent<Animator>();
        rigitbody = GetComponent<Rigidbody2D>();
        _coinAudioSource = GetComponent<AudioSource>();
        finalMenu.SetActive(false);
        RestartCam.gameObject.SetActive(false);
        RestartMenu.SetActive(false);
    }

    private void GetFinishMenu()
    {
        //transform.SetPositionAndRotation(new Vector3(0, 0, -100f), transform.rotation);
        //FirstCam.gameObject.SetActive(false);
        finalMenu.SetActive(true);
        ShowStars();
        Time.timeScale = 0f;
        CCollect.gameObject.SetActive(false);
        Health.gameObject.SetActive(false);
        finishText.text = $"✘{CoinCollect.coinCount}";
        Destroy(weapon);
    }

    private void ShowStars()
    {
        foreach (var star in stars)
        {
            star.sprite = emptyStar;
        }
        if (CoinCollect.coinCount >= 10)
        {
            stars[0].sprite = fullStar;
        }
        if (CoinCollect.coinCount >= 20)
        {
            stars[1].sprite = fullStar;
        }
        if (CoinCollect.coinCount >= 30)
        {
            stars[2].sprite = fullStar;
        }
    }
    
    public void FixedUpdate()
    {
        UpdateHealthSystem();
        MovePlayer();
    }

    private void UpdateHealthSystem()
    {
        /*if (health <= 0)
        {
            //FirstCam.gameObject.SetActive(false);
            //RestartCam.gameObject.SetActive(true);
            RestartMenu.gameObject.SetActive(true);
            Time.timeScale = 0f;
        }*/
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
        if (moveInputX > 0 && !facingLeft || moveInputX < 0 && facingLeft)
            Flip();
    }

    private void Flip()
    {
        facingLeft = !facingLeft;
        var scaler = transform.localScale;
        scaler.x *= -1;
        transform.localScale = scaler;
        weapon.transform.localScale = scaler;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("DamageObject") || other.CompareTag("Monster") || other.CompareTag("Spikes"))
        {
            health--;
            heartsNumber--;
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
            CoinCollect.coinCount++;
            Destroy(other.gameObject);
        }

        if (other.CompareTag("Gem"))
        {
            //gemAudioSource.Play();
            GetFinishMenu();
            Destroy(other.gameObject);
        }

        if (other.CompareTag("HealChest"))
        {
            healingAudioSource.Play();
            health++;
            heartsNumber++;
            Instantiate(healing, transform.position, Quaternion.identity);
            Destroy(other.gameObject);
        }

        if (other.CompareTag("Button"))
        {
            buttonAudioSource.Play();
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
        if (other.CompareTag("Chest") && Input.GetKeyDown(KeyCode.C))
        {
            CoinCollect.coinCount += 5;
            Destroy(other.gameObject);
            Instantiate(openedChest, other.transform.position, Quaternion.identity);
            _coinAudioSource.Play();
            Instantiate(chestsCoins, transform.position, Quaternion.identity);
        }
    }
}
