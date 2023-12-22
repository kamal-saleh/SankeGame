using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    public static Settings instance;

    [SerializeField] private Button foodColorButton;
    [SerializeField] private List<Color> colors;
    [SerializeField] private Slider snakeSpeedSlider;
    [SerializeField] private Slider foodRespawnSpeedSlider;
    [SerializeField] private Snake snake;

    [HideInInspector] public Color foodColor;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        snakeSpeedSlider.value = snake.speed;
        foodRespawnSpeedSlider.value =  1.0f - GameManager.instance.foodRespawnSpeed;
    }

    public void OnSnakeSpeedChanged()
    {
        if (snake != null)
        {
            snake.speed = snakeSpeedSlider.value;
        }
    }

    public void OnFoodRespawnSpeedChanged()
    {
        GameManager.instance.foodRespawnSpeed = 1.0f - foodRespawnSpeedSlider.value;
    }

    public void Restart(string sceneToLoad)
    {
        SceneManager.LoadScene(sceneToLoad);
        Time.timeScale = 1.0f;
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void ChangeFoodColor(int index)
    {
        //food.GetComponent<Renderer>().material.color = colors[index];
        foodColorButton.GetComponent<UnityEngine.UI.Image>().color = colors[index];
        foodColor = colors[index];
        foreach (GameObject item in GameManager.instance.createdFood)
        {
            item.GetComponent<Renderer>().material.color = colors[index];
        }
    }
}