using UnityEngine;
using System.Collections;

public class Constants : MonoBehaviour {
    [HideInInspector]
    public int halfScreenW, halfScreenH, numOfScreens, oneScreen, levelsPerRow = 0, levelsPerColl = 0;
    [HideInInspector]
    public float screenPercentW, screenPercentH;
    [HideInInspector]
    public Rect quizBoxRect, quizBoxInnerRect, scoreText, score, timeText, time;
    private static Constants instance;
    public static Constants Instance
   	{
       get
       {
           return instance;
       }
   	}

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        halfScreenW = Screen.width / 2;
        halfScreenH = Screen.height / 2;

        screenPercentW = (float)QuizManager.Instance.data.screenWidth / 100;
        screenPercentH = (float)QuizManager.Instance.data.screenHeight / 100;
        quizBoxRect = new Rect(halfScreenW - ((Screen.width * screenPercentW) / 2), halfScreenH - ((Screen.height * screenPercentH) / 2), Screen.width * screenPercentW, Screen.height * screenPercentH);
        quizBoxInnerRect = new Rect(quizBoxRect.x + 10, quizBoxRect.y + 10, quizBoxRect.width - 20, quizBoxRect.height - 20);

        if (QuizManager.Instance.data.levels > 1)
        {
            levelsPerRow = (int)((Screen.width * screenPercentW) / (QuizManager.Instance.data.levelButtonW + 5));
            levelsPerColl = (int)((Screen.height * screenPercentH) / (QuizManager.Instance.data.levelButtonW + 5));
        }
        oneScreen = levelsPerRow * levelsPerColl;
        if (oneScreen != 0)
        {
            numOfScreens = QuizManager.Instance.data.levels / oneScreen;
            if ((QuizManager.Instance.data.levels % oneScreen) != 0)
                numOfScreens++;
        }
        else
            numOfScreens = 0;

        timeText = new Rect(Screen.width - 125, 5, 80, 22);
        time = new Rect(Screen.width - 25, 5, 20, 22);
        scoreText = new Rect(Screen.width - 125, 30, 80, 22);
        score = new Rect(Screen.width - 25, 30, 20, 22);
    }
}
