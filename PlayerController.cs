using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    float currentTime = 0f;
    float startingTime = 10f;
    public float movementspeed;
    public Text countText;
    public Text winText;
    public Text timerText;
    public Text gameOverText;

    public AudioSource musicSource;
    public AudioClip musicClipOne;
    public AudioClip musicClipTwo;

    private bool gameOver;
    private int count;

    void Start()
    {
        currentTime = startingTime;
        count = 0;
        gameOver = false;
        winText.text = "";
        gameOverText.text = "";
        SetCountText();
    }


    void Update()
    {

        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 0;
        Vector3 objectPos = Camera.main.WorldToScreenPoint(transform.position);

        mousePos.x = mousePos.x - objectPos.x;
        mousePos.y = mousePos.y - objectPos.y;

        float angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;
       

        Vector3 targetPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        targetPos.z = 0;
        transform.position = Vector3.MoveTowards(transform.position, targetPos, movementspeed * Time.deltaTime);


        currentTime -= 1 * Time.deltaTime;
        timerText.text = currentTime.ToString("0");

        if (currentTime <= 0)
        {
            currentTime = 0;
            gameOver = true;
            GameOver();
        }


        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }

    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Carrots"))
        {
            other.gameObject.SetActive (false);
            count = count + 1;
            SetCountText();
            gameOver = true;
            GetComponent<AudioSource>().Play();

        }

    }

    void SetCountText()
    {
        countText.text = " " + count.ToString();
        if (count >=10)
        {
            winText.text = "IMPRESSIVE!";
            gameOver = true;
            musicSource.clip = musicClipOne;
            musicSource.Play();
        }
    }

    public void GameOver()
    {

        gameOverText.text = "Game Over!";
        gameOver = true;
        musicSource.clip = musicClipTwo;
        musicSource.Play();

    }

}
