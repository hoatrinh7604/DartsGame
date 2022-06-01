using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePlayController : MonoBehaviour
{
    public static GamePlayController Instance { get; private set; }
    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    [SerializeField] float score;
    [SerializeField] float highscore;
    public Color[] template = { new Color32(255, 81, 81, 255), new Color32(255, 129, 82, 255), new Color32(255, 233, 82, 255), new Color32(163, 255, 82, 255), new Color32(82, 207, 255, 255), new Color32(170, 82, 255, 255) };

    private int currentTarget = 0;
    [SerializeField] Image colorImage;
    private int nextTarget = 0;
    [SerializeField] Image colorNextImage;
    private UIController uiController;

    private float time;
    [SerializeField] float timeToChangeColor;
    [SerializeField] float timeOfGame;

    [SerializeField] BackgroundController bgController;
    [SerializeField] AnimalSearching animalSearching;
    private int currentMapIndex;

    private int remainingAnimals;
    private int currentIndex;

    private int currentControl = 0;
    private float sliderValue;
    private float timeSpaw;
    [SerializeField] float delayTimeSpaw;
    [SerializeField] int maxFire;

    [SerializeField] LineController lineController;
    [SerializeField] PillowController pillowController;
    [SerializeField] PointController pointController;

    // Start is called before the first frame update
    void Start()
    {
        uiController = GetComponent<UIController>();
        Reset();
    }

    // Update is called once per frame
    void Update()
    {
        time -= Time.deltaTime;
        UpdateSlider();
    }

    public void UpdatePillowPosition()
    {
        Vector3 pos = lineController.GetCutPoint();
        pillowController.UpdateTransform(pos);
        pillowController.gameObject.SetActive(true);

        int point = pointController.GetPointOfPosition(pos);
        if (point == 0) GameOver();

        score += point;
        time += point;
        if(time > timeOfGame)
        {
            time = timeOfGame;
        }
        UpdateScore();

        StartCoroutine(NextTurn());
    }

    public void UpdateSlider()
    {
        uiController.UpdateSlider(time);

        if(time <= 0)
        {
            GameOver();
        }
    }

    public void SetSlider()
    {
        uiController.SetSlider(timeOfGame);
    }

    public void OnPressVertical()
    {
        currentControl++;
        lineController.HandleVertical(false);
        if(currentControl == 2)
        {
            UpdatePillowPosition();
        }
    }

    public void OnPressHorizontal()
    {
        currentControl++;
        lineController.HandleHorizontal(false);
        if (currentControl == 2)
        {
            UpdatePillowPosition();
        }
    }

    public void OnPressHandle()
    {
    }

    IEnumerator NextTurn(bool isStartGame = false)
    {
        if(!isStartGame)
            yield return new WaitForSeconds(0.5f);
        currentControl = 0;
        pillowController.gameObject.SetActive(false);

        lineController.ResetVerticalLine();
        lineController.HandleVertical(true);

        lineController.ResetHorizontalLine();
        lineController.HandleHorizontal(true);
    }

    public void GameOver(bool isWin = false)
    {
        Time.timeScale = 0;
        //check fastest time
        
        highscore = PlayerPrefs.GetFloat("score");
        if (highscore < score)
        {
            highscore = score;
            PlayerPrefs.SetFloat("score", highscore);
        }

        uiController.GameOver(isWin);
    }

    public void UpdateScore()
    {
        uiController.UpdateScore(score);
    }

    public void SetCurrentTarget()
    {
        animalSearching.SetTarget(currentTarget);
    }

    public void Reset()
    {
        Time.timeScale = 1;
        currentControl = 0;
        currentTarget = 0;

        currentIndex = 0;
        time = timeOfGame;
        SetSlider();
        score = 0;
        highscore = PlayerPrefs.GetFloat("score");
        uiController.UpdateScore(score);
        uiController.UpdateHighScore(highscore);
        //uiController.UpdateHighScore(highscore);
        StartCoroutine(NextTurn(true));
    }

}
