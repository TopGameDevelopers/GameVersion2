using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
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

    private AudioSource _audioSource;

    public void Start()
    {
        anim = GetComponent<Animator>();
        rigitbody = GetComponent<Rigidbody2D>();
        _audioSource = GetComponent<AudioSource>();
    }

    public void FixedUpdate()
    {
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
            _audioSource.Play();
            CoinCollect.coinCount++;
            FinishCoins.count++;
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
        }
    }
}
