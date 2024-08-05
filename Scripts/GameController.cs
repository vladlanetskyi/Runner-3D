using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    [Range(0, 100)] public int coinsRandomChance;
    public int coins;
    public int score;
    public int highscore;
    private AudioSource audioSource;

    [Header("UI")]
    [SerializeField] private TMP_Text coinsText;
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text highscoreText;
    private GameObject player;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        audioSource = GetComponent<AudioSource>();
    }


    private void Start()
    {
        highscore = PlayerPrefs.GetInt("Highscore");
        coins = PlayerPrefs.GetInt("Coins");

        highscoreText.text = highscore.ToString();
        coinsText.text = coins.ToString();
    }

    public void CollectCoin()
    {
        audioSource.Play();
        coins++;
        coinsText.text = coins.ToString();
        

    }

    private void Update()
    {
        if (score < player.transform.position.z)
        {
            score = (int)player.transform.position.z;

            scoreText.text = score.ToString();
        }

    }

    public void Save()
    {
        if (score > highscore)
        {
            PlayerPrefs.SetInt("Highscore", score);
        }
        PlayerPrefs.SetInt("Coins", coins);

    }

    public void LoadScene(int index)
    {
        SceneManager.LoadScene(index);
    }
}