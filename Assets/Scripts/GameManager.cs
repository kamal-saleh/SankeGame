using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

internal class GameManager : MonoBehaviour
{
    public static GameManager instance;
    
    [SerializeField] private GameObject food;
    [SerializeField] private GameObject foodParent;
    [SerializeField] private int score;
    [SerializeField] private GameObject scoreText;
    [SerializeField] private GameObject winPanel;
    [SerializeField] private GameObject losePanel;
    [SerializeField] private GameObject pauseMenu;

    [HideInInspector] public List<GameObject> createdFood = new List<GameObject>();

    public float foodRespawnSpeed;

    private bool isPaused = false;
    private bool allowFoodInstantiate = true;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        StartCoroutine(generateFood());
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseGame();
        }

        if (score >= 1000)
        {
            WinGame();
        }
    }

    private void PauseGame()
    {
        if(!isPaused)
        {
            isPaused = true;
            pauseMenu.SetActive(true);
            Time.timeScale = 0f;
            allowFoodInstantiate = false;
        }
    }

    public void ResumeGame()
    {
        allowFoodInstantiate = true;
        pauseMenu.SetActive(false);
        Time.timeScale = 1.0f;
        isPaused = false;
    }

    public void HighlightNearestFood()
    {
        GameObject nearestFood = FindNearestFood();
        if (nearestFood != null)
        {
            foreach (GameObject foodItem in GameObject.FindGameObjectsWithTag("Food"))
            {
                foodItem.GetComponent<Renderer>().material.color = Settings.instance.foodColor;
            }

            nearestFood.GetComponent<Renderer>().material.color = Color.green;
        }
    }

    GameObject FindNearestFood()
    {
        GameObject[] foodItems = GameObject.FindGameObjectsWithTag("Food");
        GameObject nearestFood = null;
        float minDistance = float.MaxValue;
        Vector3 snakeHeadPosition = GameObject.FindGameObjectWithTag("Snake").transform.position;

        foreach (GameObject foodItem in foodItems)
        {
            float distance = Vector3.Distance(snakeHeadPosition, foodItem.transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                nearestFood = foodItem;
            }
        }

        return nearestFood;
    }

    internal void AddPoints(int points)
    {
        score += points;
        scoreText.GetComponent<TextMeshProUGUI>().text = score + "";
    }

    void WinGame()
    {
        winPanel.SetActive(true);
        Time.timeScale = 0f;
        allowFoodInstantiate = false;
    }

    public void LoseGame()
    {
        losePanel.SetActive(true);
        Time.timeScale = 0f;
        allowFoodInstantiate = false;
    }

    IEnumerator generateFood()
    {
        if (allowFoodInstantiate)
        {
            yield return new WaitForSeconds(foodRespawnSpeed);
            float randomx, randomy, randomz;
            randomx = UnityEngine.Random.Range(-10.0f, 10.0f);
            randomy = UnityEngine.Random.Range(0.5f, 0.5f);
            randomz = UnityEngine.Random.Range(-10.0f, 10.0f);
            GameObject foodPrefab = Instantiate(food, new Vector3(randomx, randomy, randomz), Quaternion.identity);
            foodPrefab.transform.parent = foodParent.transform;
            createdFood.Add(foodPrefab);
            StartCoroutine(generateFood());
        }
    }
}