using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public Camera FirstCam;
    public Camera FinCam;
    public Camera PauseCam;

    public float speed;

    private bool facingLeft;

    public Rigidbody2D rigitbody;

    public Animator anim;

    public GameObject weapon;
    public GameObject openedChest;

    private int coinCount;

    public int health;
    public int heartsNumber;

    public Image[] hearts;
    public Sprite heart;
    public Sprite emptyHeart;

    private AudioSource _coinAudioSource;
    public GameObject chestsCoins;

    public GameObject Gem;

    public GameObject Menu;
    public GameObject CCollect;
    public GameObject Health;
    public Text finishText;

    public void Start()
    {
        anim = GetComponent<Animator>();
        rigitbody = GetComponent<Rigidbody2D>();
        _coinAudioSource = GetComponent<AudioSource>();
        Menu.SetActive(false);
        FirstCam.gameObject.SetActive(true);
        FinCam.gameObject.SetActive(false);
        PauseCam.gameObject.SetActive(false);
    }

    private void GetFinishMenu()
    {
        //transform.SetPositionAndRotation(new Vector3(0, 0, -100f), transform.rotation);
        FirstCam.gameObject.SetActive(false);
        FinCam.gameObject.SetActive(true);
        Menu.SetActive(true);
        Time.timeScale = 0f;
        CCollect.gameObject.SetActive(false);
        Health.gameObject.SetActive(false);
        finishText.text = $"✘{CoinCollect.coinCount}";
        Destroy(weapon);
    }
    
    public void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Time.timeScale = 0f;
            FirstCam.gameObject.SetActive(false);
            PauseCam.gameObject.SetActive(true);
            Debug.Log("click");
        }
        for (var i = 0; i < hearts.Length; i++)
        {
            if (i < health)
            {
                hearts[i].sprite = heart;
            }
            else
            {
                hearts[i].sprite = emptyHeart;
            }

            if (i < heartsNumber)
            {
                hearts[i].enabled = true;
            }
            else
            {
                hearts[i].enabled = false;
            }
        }
        /*if (health <= 0)
            Destroy(this.gameObject);*/
        var moveInputX = Input.GetAxis("Horizontal");
        var moveInputY = Input.GetAxis("Vertical");
        if (moveInputX == 0 && moveInputY == 0)
        {
            anim.SetBool("IsRunning", false);
        }
        else
        {
            anim.SetBool("IsRunning", true);
        }
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
            GetFinishMenu();
            Destroy(other.gameObject);
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
