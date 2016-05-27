using UnityEditor;
using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class QuizEditor : EditorWindow
{
    private Color backCol = Color.white;
    private GUISkin editorSkin;
    private QuizData instance;
    private Texture additionalImg;
    private Texture quizBoxImg, backImg, startScreenTex;
    private Texture[] abcTex = new Texture[5] { null, null, null, null, null };
    private Texture[] pairTex = new Texture[5] { null, null, null, null, null };
    private bool addImg = false, addProper = false, showNumber = false, showCorrect = false, autoAlign = false, errorBox = false, gridTex = false, horizontal = false, loadOld = false, pixelate = false, showHint = false, randomize = false, submitButton = false;
    private bool[] abcCor = new bool[5] { false, false, false, false, false };
    private int addWidth = 100;
    private int questBreak = 0;
    private int counter = 0;
    private int h = 30;
    private int hintTime = 0;
    private int levelButtonW = 0;
    private int levelIndex = 0;
    private int levels = 1;
    private int numOfQuest = 0;
    private int optionsIndex = 0;
    private int padX = 0;
    private int padY = 0;
    private int screenWidth, screenHeight;
    private int seconds;
    private int step = 1;
    private int value;
    private int w = 100;
    private string errorMsg = "";
    private string hint = "";
    private string question = "";
    private string quizName = "";
    private string[] abc = new string[5] { "", "", "", "", "" };
    private string[] abcPair = new string[5] { "", "", "", "", "" };
    private string[] options = new string[4] { "Select right answer", "Select right image", "Match", "Match images" };
    private Vector2 submitButtonPos;

    [MenuItem("Window/Quiz Maker")]
    public static void ShowWindow()
    {
        EditorWindow.GetWindowWithRect(typeof(QuizEditor), new Rect((Screen.currentResolution.width / 2) - 512, (Screen.currentResolution.height / 2) - 384, 1024, 768), true, "Quiz Maker Editor");
    }

    void Awake()
    {
        // Don't change next line
        instance = AssetDatabase.LoadMainAssetAtPath("Assets/QuizMaker/Data/data.asset") as QuizData;
        quizName = instance.quizName;
        numOfQuest = instance.numQuestion;
        screenWidth = instance.screenWidth;
        screenHeight = instance.screenHeight;
        quizBoxImg = instance.quizBoxImg;
        backImg = instance.backImg;
        startScreenTex = instance.startScreenTex;
        backCol = instance.backCol;
        levels = instance.levels;
        levelButtonW = instance.levelButtonW;
        questBreak = instance.questBreak;
        showNumber = instance.showNumber;
        showCorrect = instance.showCorrect;
        randomize = instance.randomize;
        submitButton = instance.submitButton;
        submitButtonPos = instance.submitButtonPos;

        if (numOfQuest > 0)
        {
            loadOld = true;
            LoadQuestion(counter);
        }
    }

    void OnGUI()
    {
        GUI.SetNextControlName("Dummy");
        switch (step)
        {
            case 1:
                quizName = EditorGUILayout.TextField(new GUIContent("Quiz name", "Name of your quiz"), quizName);
                GUILayout.Space(5);
                numOfQuest = EditorGUILayout.IntField(new GUIContent("Number of questions", "How many questions will your quiz have"), numOfQuest);
                GUILayout.Space(5);
                questBreak = EditorGUILayout.IntField(new GUIContent("Question break", "How long will break between questions last."), questBreak);
                GUILayout.Space(5);
                levels = EditorGUILayout.IntField(new GUIContent("Number of levels", "How many levels will your quiz have"), levels);
                GUILayout.Space(5);
                levelButtonW = EditorGUILayout.IntField(new GUIContent("Level button width", "In pixels. The height will be the same as the width."), levelButtonW);
                GUILayout.Space(5);
                screenWidth = EditorGUILayout.IntField(new GUIContent("Screen width (" + screenWidth + "%)", "Screen width is " + (((float)screenWidth / 100) * Screen.width).ToString() + "px"), screenWidth);
                GUILayout.Space(5);
                screenHeight = EditorGUILayout.IntField(new GUIContent("Screen height (" + screenHeight + "%)", "Screen height is " + (((float)screenHeight / 100) * Screen.height).ToString() + "px"), screenHeight);
                GUILayout.Space(5);
                showNumber = EditorGUILayout.Toggle(new GUIContent("Show number of question"), showNumber);
                GUILayout.Space(5);
                showCorrect = EditorGUILayout.Toggle(new GUIContent("Show correct asnwer"), showCorrect);
                GUILayout.Space(5);
                randomize = EditorGUILayout.Toggle( new GUIContent( "Randomize questions" ), randomize );
                GUILayout.Space(5);
                submitButton = EditorGUILayout.Toggle( new GUIContent( "Show submit button" ), submitButton );
                GUILayout.Space( 5 );
                submitButtonPos = EditorGUILayout.Vector2Field( new GUIContent( "Submit button position" ), submitButtonPos, GUILayout.Width(200) );
                GUILayout.Space(5);
                backCol = EditorGUILayout.ColorField("Background color", backCol, GUILayout.Width(600));
                GUILayout.Space(5);
                GUILayout.BeginHorizontal("box", GUILayout.Width(800));
                GUILayout.BeginVertical();
                GUILayout.Label(new GUIContent("Start screen image", "Start screen image"));
                startScreenTex = EditorGUILayout.ObjectField(startScreenTex, typeof(Texture), false, GUILayout.Width(50), GUILayout.Height(50)) as Texture;
                GUILayout.EndVertical();
                GUILayout.Space(5);
                GUILayout.BeginVertical();
                GUILayout.Label(new GUIContent("Background image", "Background image behing questions"));
                backImg = EditorGUILayout.ObjectField(backImg, typeof(Texture), false, GUILayout.Width(50), GUILayout.Height(50)) as Texture;
                GUILayout.EndVertical();
                GUILayout.Space(5);
                GUILayout.BeginVertical();
                GUILayout.Label(new GUIContent("Panel image", "The panel image behing questions"));
                quizBoxImg = EditorGUILayout.ObjectField(quizBoxImg, typeof(Texture), false, GUILayout.Width(50), GUILayout.Height(50)) as Texture;
                GUILayout.EndVertical();
                GUILayout.EndHorizontal();
                break;
            case 2:
                GUILayout.BeginHorizontal("box");
                GUILayout.BeginVertical("box", GUILayout.Width(200));
                for (int i = 0; i < numOfQuest; i++)
                {
                    if (GUILayout.Button("Question no. " + (i + 1).ToString()))
                    {
                        GUI.FocusControl("Dummy");
                        for (int j = 0; j < 5; j++)
                        {
                            question = "";
                            abc[j] = "";
                            abcCor[j] = false;
                            abcTex[j] = null;
                            abcPair[j] = "";
                            pairTex[j] = null;
                            levelIndex = 0;
                            value = 0;
                            pixelate = false;
                            showHint = false;
                            hintTime = 0;
                            hint = "";
                            seconds = 0;
                            levelIndex = 1;
                            addImg = false;
                            addProper = false;
                        }
                        counter = i;
                        if (loadOld)
                        {
                            LoadQuestion(counter);
                        }
                        else
                        {
                            SetAllEmpty();
                            SetAllFalse();
                        }
                    }
                }
                GUILayout.EndVertical();
                GUILayout.Space(10);
                GUILayout.BeginVertical(GUILayout.Width(790));
                question = EditorGUILayout.TextField("Question no. " + (counter + 1).ToString(), question, GUILayout.Width(600));
                GUILayout.Space(10);
                optionsIndex = EditorGUILayout.Popup("Questions type", optionsIndex, options, GUILayout.Width(400));
                GUILayout.BeginHorizontal(GUILayout.Width(600));
                if (optionsIndex == 0 || optionsIndex == 1)
                {
                    GUILayout.BeginVertical("box");
                    if (optionsIndex == 0)
                    {
                        for (int i = 0; i < 5; i++)
                        {
                            GUILayout.BeginHorizontal(GUILayout.Width(590));
                            GUILayout.Label(new GUIContent((i + 1).ToString() + "."));
                            abc[i] = EditorGUILayout.TextField(abc[i], GUILayout.Width(500));
                            abcCor[i] = EditorGUILayout.Toggle(abcCor[i]);
                            GUILayout.EndHorizontal();
                        }

                    }
                    else if (optionsIndex == 1)
                    {
                        for (int i = 0; i < 5; i++)
                        {
                            GUILayout.BeginHorizontal(GUILayout.Width(100));
                            GUILayout.Label(new GUIContent((i + 1).ToString() + "."));
                            abcTex[i] = EditorGUILayout.ObjectField(abcTex[i], typeof(Texture), false, GUILayout.Width(50), GUILayout.Height(50)) as Texture;
                            abcCor[i] = EditorGUILayout.Toggle(abcCor[i]);
                            GUILayout.EndHorizontal();
                        }
                    }
                    GUILayout.EndVertical();
                }
                if (optionsIndex == 2)
                {
                    GUILayout.BeginVertical("box");
                    for (int i = 0; i < 5; i++)
                    {
                        GUILayout.BeginHorizontal(GUILayout.Width(100));
                        GUILayout.Label(new GUIContent((i + 1).ToString() + "."));
                        abc[i] = EditorGUILayout.TextField("", abc[i], GUILayout.Width(220));
                        GUILayout.Space(10);
                        abcPair[i] = EditorGUILayout.TextField("", abcPair[i], GUILayout.Width(220));
                        GUILayout.EndHorizontal();
                    }
                    GUILayout.EndVertical();
                }
                if (optionsIndex == 3)
                {
                    GUILayout.BeginVertical("box");
                    for (int i = 0; i < 5; i++)
                    {
                        GUILayout.BeginHorizontal(GUILayout.Width(100));
                        GUILayout.Label(new GUIContent((i + 1).ToString() + "."));
                        abcTex[i] = EditorGUILayout.ObjectField(abcTex[i], typeof(Texture), false, GUILayout.Width(50), GUILayout.Height(50)) as Texture;
                        GUILayout.Space(10);
                        pairTex[i] = EditorGUILayout.ObjectField(pairTex[i], typeof(Texture), false, GUILayout.Width(50), GUILayout.Height(50)) as Texture;
                        GUILayout.EndHorizontal();
                    }
                    GUILayout.EndVertical();
                }
                GUILayout.EndHorizontal();
                if (optionsIndex == 0)
                {
                    GUILayout.Space(5);
                    addImg = EditorGUILayout.Foldout(addImg, "Additional image");
                    if (addImg)
                    {
                        GUILayout.BeginVertical("box", GUILayout.Width(600));
                        additionalImg = EditorGUILayout.ObjectField(additionalImg, typeof(Texture), false, GUILayout.Width(50), GUILayout.Height(50)) as Texture;
                        GUILayout.Label(new GUIContent("Scale image (%)", "Percentage-based scaling of the image"));
                        addWidth = EditorGUILayout.IntSlider(addWidth, 1, 100);
                        padX = EditorGUILayout.IntField("Padding top", padX);
                        padY = EditorGUILayout.IntField("Padding left", padY);
                        GUILayout.BeginHorizontal(GUILayout.Width(200));
                        GUILayout.Label(new GUIContent("Pixelate image", "Pixelisation gets smaller as time passes. Good for 'Guess who' questions."));
                        pixelate = EditorGUILayout.Toggle(pixelate);
                        GUILayout.EndHorizontal();
                        GUILayout.EndVertical();
                    }
                    GUILayout.Space(5);
                    addProper = EditorGUILayout.Foldout(addProper, "Button properties");
                    if (addProper)
                    {
                        GUILayout.BeginVertical("box", GUILayout.Width(600));
                        autoAlign = EditorGUILayout.Toggle(new GUIContent("Automatic stretch", "This will override buttons width and height!"), autoAlign);
                        w = EditorGUILayout.IntField(new GUIContent("Width", "Width of all button in this question"), w);
                        h = EditorGUILayout.IntField(new GUIContent("Height", "Height of all button in this question"), h);
                        GUILayout.EndVertical();
                    }
                }
                else if (optionsIndex == 1)
                {
                    GUILayout.Space(5);
                    horizontal = EditorGUILayout.Toggle(new GUIContent("Horizontal orientation", ""), horizontal);
                    GUILayout.Space(5);
                    addProper = EditorGUILayout.Foldout(addProper, "Button properties");
                    if (addProper)
                    {
                        GUILayout.BeginVertical("box", GUILayout.Width(600));
                        autoAlign = EditorGUILayout.Toggle(new GUIContent("Automatic stretch", "This will override buttons width and height!"), autoAlign);
                        w = EditorGUILayout.IntField(new GUIContent("Width", "Width of all images in this question"), w);
                        h = EditorGUILayout.IntField(new GUIContent("Height", "Height of all images in this question"), h);
                        GUILayout.EndVertical();
                    }
                }
                else if (optionsIndex == 2)
                {
                    GUILayout.Space(5);
                    addProper = EditorGUILayout.Foldout(addProper, "Button properties");
                    if (addProper)
                    {
                        GUILayout.BeginVertical("box", GUILayout.Width(600));
                        autoAlign = EditorGUILayout.Toggle(new GUIContent("Automatic stretch", "This will override buttons width and height!"), autoAlign);
                        w = EditorGUILayout.IntField(new GUIContent("Width", "Width of all button in this question"), w);
                        h = EditorGUILayout.IntField(new GUIContent("Height", "Height of all button in this question"), h);
                        GUILayout.EndVertical();
                    }
                }
                GUILayout.Space(5);
                showHint = EditorGUILayout.Foldout(showHint, "Show hint");
                if (showHint)
                {
                    GUILayout.BeginVertical("box", GUILayout.Width(600));
                    hint = EditorGUILayout.TextField("Hint text", hint);
                    hintTime = EditorGUILayout.IntField("Length", hintTime);
                    GUILayout.EndVertical();
                }
                GUILayout.Space(5);
                string[] levelchoice = null;
                Array.Resize(ref levelchoice, levels);
                for (int i = 0; i < levels; i++)
                {
                    levelchoice[i] = (i + 1).ToString();
                }
                GUILayout.Space(10);
                levelIndex = EditorGUILayout.Popup("Questions level", levelIndex, levelchoice, GUILayout.Width(200));
                GUILayout.Space(10);
                seconds = EditorGUILayout.IntField(new GUIContent("Time last", "How many seconds you have to answer"), seconds, GUILayout.Width(600));
                GUILayout.Space(10);
                value = EditorGUILayout.IntField(new GUIContent("Value", "Value of question. Good of score calculations."), value, GUILayout.Width(600));
                GUILayout.Space(30);
                GUILayout.BeginHorizontal();
                if (GUILayout.Button("Save", GUILayout.Width(100)))
                {
                    SaveQuestion();
                }
                GUILayout.Space(400);
                if (GUILayout.Button("Delete", GUILayout.Width(100)))
                {
                    DeleteQuestion();
                }
                GUILayout.EndHorizontal();
                GUILayout.EndVertical();

                GUILayout.EndHorizontal();

                break;
            default:
                break;
        }
        GUILayout.Space(20);
        if (errorBox)
            EditorGUILayout.HelpBox(errorMsg, MessageType.Error);

        GUILayout.BeginHorizontal("box", GUILayout.Width(1000));
        if (step > 1)
        {
            if (GUILayout.Button("Previous", GUILayout.Width(100)))
            {
                step--;
            }
        }
        if (step == 1)
        {
            if (GUILayout.Button("Next", GUILayout.Width(100)))
            {
                GUI.FocusControl("Dummy");
                if (step == 1 && quizName == "")
                {
                    errorBox = true;
                    errorMsg = "Please enter a quiz name!";
                }
                else if (step == 1 && numOfQuest <= 0)
                {
                    errorBox = true;
                    errorMsg = "You must have at least one question!";
                }
                else
                {
                    errorBox = false;
                    step++;
                }
            }
        }
        if (GUILayout.Button("Finish", GUILayout.Width(100)))
        {
            SaveAsset();
        }
        if (step == 1)
        {
            GUILayout.Space(700);
            if (GUILayout.Button("Clear All", GUILayout.Width(100)))
                ClearAll();
        }
        GUILayout.EndHorizontal();
    }

    private void DeleteQuestion()
    {
		if (instance.questions.Count>1) {
			instance.questions.RemoveAt(counter);
			instance.questionHeaders.RemoveAt(counter);
			instance.questTypes.RemoveAt(counter);
			numOfQuest--;
		}
		else {
            errorBox = true;
            errorMsg = "You can't delete all questions!";
            return;		
		}
    }

    private void SaveQuestion()
    {
        Question q = new Question();

        if (question == "")
        {
            errorBox = true;
            errorMsg = "Please enter a question text!";
            return;
        }
        else if ((optionsIndex == 0) && (String.IsNullOrEmpty(abc[0]) && String.IsNullOrEmpty(abc[1]) && String.IsNullOrEmpty(abc[2]) && String.IsNullOrEmpty(abc[3]) && String.IsNullOrEmpty(abc[4])))
        {
            errorBox = true;
            errorMsg = "Enter at least one answer!";
            return;
        }
        else if ((optionsIndex == 1) && (!abcTex[0] && !abcTex[1] && !abcTex[2] && !abcTex[3] && !abcTex[4]))
        {
            errorBox = true;
            errorMsg = "Choose at least one image!";
            return;
        }
        else if ((optionsIndex == 0 || optionsIndex == 1) && (abcCor[0] == false && abcCor[1] == false && abcCor[2] == false && abcCor[3] == false && abcCor[4] == false))
        {
            errorBox = true;
            errorMsg = "Choose at least one answer that is correct!";
            return;
        }
        else if (optionsIndex == 2)
        {
            for (int i = 0; i < 5; i++)
            {
                if (!String.IsNullOrEmpty(abc[i]) || !String.IsNullOrEmpty(abcPair[i]))
                {
                    if ((!String.IsNullOrEmpty(abc[i]) && String.IsNullOrEmpty(abcPair[i])) || (String.IsNullOrEmpty(abc[i]) && !String.IsNullOrEmpty(abcPair[i])))
                    {
                        errorBox = true;
                        errorMsg = "Uncomplete match!";
                        return;
                    }
                }
            }
        }
        else if (seconds <= 0)
        {
            errorBox = true;
            errorMsg = "Set how long will this question last!";
        }
        else if (value <= 0)
        {
            errorBox = true;
            errorMsg = "Set the value of this question!";
        }
        else errorBox = false;

        if (instance.questionHeaders.Count > counter)
            instance.questionHeaders.RemoveAt(counter);

        instance.questionHeaders.Insert(counter, question);

        if (instance.addImage.Count > counter)
            instance.addImage.RemoveAt(counter);

        instance.addImage.Insert(counter, addImg);

        if (instance.questions.Count > counter)
            instance.questions.RemoveAt(counter);

        q.horizontal = horizontal;

        if (showHint)
        {
            q.showHint = showHint;
            q.hint = hint;
            q.hintTime = hintTime;
        }

        if (optionsIndex == 0)
        {
            if (addImg)
            {
                q.addImg = additionalImg;
                q.addImgPercent = (float)addWidth / 100;
                q.padX = padX;
                q.padY = padY;
                q.pixelate = pixelate;
            }
        }

        if (addProper)
        {
            q.properties = true;
            q.autoAlign = autoAlign;
            q.width = w;
            q.height = h;
        }

        if (optionsIndex == 0)
        {
            for (int i = 0; i < 5; i++)
            {
                q.answersABC.Insert(i, abc[i]);
                q.correctAnswersABC.Insert(i, abcCor[i]);
            }
        }
        else if (optionsIndex == 1)
        {
            for (int i = 0; i < 5; i++)
            {
                if (abcTex[i])
                {
                    q.answersTex.Insert(i, abcTex[i]);
                    q.correctAnswersTex.Insert(i, abcCor[i]);
                }
            }
        }
        else if (optionsIndex == 2)
        {
            for (int i = 0; i < 5; i++)
            {
                if (!String.IsNullOrEmpty(abc[i]) && !String.IsNullOrEmpty(abcPair[i]))
                {
                    q.arrayOne.Insert(i, abc[i]);
                    q.arrayTwo.Insert(i, abcPair[i]);
                }
            }
        }
        else if (optionsIndex == 3)
        {
            for (int i = 0; i < 5; i++)
            {
                if (abcTex[i])
                {
                    if (abcTex[i].name.Length > 0 && pairTex[i].name.Length > 0)
                    {
                        q.arrayOneTex.Insert(i, abcTex[i]);
                        q.arrayTwoTex.Insert(i, pairTex[i]);
                    }
                }
            }
        }

        q.level = levelIndex + 1;
        q.value = value;
        q.seconds = seconds;
        q.gridTex = gridTex;

        instance.questions.Insert(counter, q);

        if (instance.questTypes.Count > counter)
            instance.questTypes.RemoveAt(counter);

        instance.questTypes.Insert(counter, optionsIndex);

		SaveAsset();

        errorBox = false;
    }

    private void SaveAsset()
    {
        instance.quizName = quizName;
        instance.showNumber = showNumber;
        instance.showCorrect = showCorrect;
        instance.randomize = randomize;
        instance.submitButton = submitButton;
        instance.submitButtonPos = submitButtonPos;
        instance.numQuestion = numOfQuest;
        instance.screenWidth = Mathf.Clamp(screenWidth, 1, 100);
        instance.screenHeight = Mathf.Clamp(screenHeight, 1, 100);
        instance.quizBoxImg = quizBoxImg;
        instance.startScreenTex = startScreenTex;
        instance.backImg = backImg;
        instance.backCol = backCol;
        instance.levels = levels;
        instance.levelButtonW = levelButtonW;
        instance.questBreak = Mathf.Clamp(questBreak, 0, 1000);
        AssetDatabase.SaveAssets();
        EditorUtility.SetDirty(instance);
    }

    private void ClearAll()
    {
        foreach (Question q in instance.questions)
        {
            q.answersABC.Clear();
            q.answersTex.Clear();
            q.arrayOne.Clear();
            q.arrayOneTex.Clear();
            q.arrayTwo.Clear();
            q.arrayTwoTex.Clear();
            q.correctAnswersABC.Clear();
            q.correctAnswersTex.Clear();
            q.selectedAnswersABC.Clear();
            q.selectedtAnswersTex.Clear();
        }
        instance.questions.Clear();
        instance.questionHeaders.Clear();
        instance.questTypes.Clear();
        instance.numQuestion = 0;
        instance.name = "";
        quizName = "";
        numOfQuest = 0;
        step = 1;
    }

    private void LoadQuestion(int counter)
    {
        if (counter < instance.questTypes.Count)
            optionsIndex = instance.questTypes[counter];
        else
            optionsIndex = 0;

        if (counter < instance.questions.Count)
        {
            if (instance.questionHeaders.Count > counter)
                question = instance.questionHeaders[counter];

            if (instance.questTypes[counter] == 0)
            {
                for (int i = 0; i < 5; i++)
                {
                    if (!String.IsNullOrEmpty(instance.questions[counter].answersABC[i]))
                    {
                        abc[i] = instance.questions[counter].answersABC[i];
                        abcCor[i] = instance.questions[counter].correctAnswersABC[i];
                    }
                }
            }
            else if (instance.questTypes[counter] == 1)
            {
                for (int i = 0; i < 5; i++)
                {
                    if (instance.questions[counter].answersTex.Count > i)
                    {
                        abcTex[i] = instance.questions[counter].answersTex[i];
                        abcCor[i] = instance.questions[counter].correctAnswersTex[i];
                    }
                }
            }
            else if (instance.questTypes[counter] == 2)
            {
                for (int i = 0; i < instance.questions[counter].arrayOne.Count; i++)
                {
                    if (!String.IsNullOrEmpty(instance.questions[counter].arrayOne[i]))
                    {
                        abc[i] = instance.questions[counter].arrayOne[i];
                        abcPair[i] = instance.questions[counter].arrayTwo[i];
                    }
                }
            }
            else if (instance.questTypes[counter] == 3)
            {
                for (int i = 0; i < 5; i++)
                {
                    if (instance.questions[counter].arrayOneTex.Count > i)
                    {
                        abcTex[i] = instance.questions[counter].arrayOneTex[i];
                        pairTex[i] = instance.questions[counter].arrayTwoTex[i];
                    }
                }
            }

            if (instance.questions[counter].properties)
            {
                addProper = true;
                w = instance.questions[counter].width;
                h = instance.questions[counter].height;
            }
            else
                instance.questions[counter].properties = false;

            if (instance.addImage[counter])
            {
                addImg = true;
                additionalImg = instance.questions[counter].addImg;
                addWidth = (int)(instance.questions[counter].addImgPercent * 100);
                pixelate = instance.questions[counter].pixelate;
            }
            else addImg = false;

            showHint = instance.questions[counter].showHint;
            hint = instance.questions[counter].hint;
            hintTime = instance.questions[counter].hintTime;
            gridTex = instance.questions[counter].gridTex;
            autoAlign = instance.questions[counter].autoAlign;
            levelIndex = instance.questions[counter].level - 1;
            value = instance.questions[counter].value;
            seconds = instance.questions[counter].seconds;
            horizontal = instance.questions[counter].horizontal;
        }
    }

    private void SetAllEmpty()
    {
        for (int i = 0; i < abc.Length; i++)
        {
            abc[i] = "";
        }
    }

    private void SetAllFalse()
    {
        for (int i = 0; i < abcCor.Length; i++)
        {
            abcCor[i] = false;
        }
    }
}