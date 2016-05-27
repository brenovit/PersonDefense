using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

[Serializable]
public class QuizData : ScriptableObject
{
	public string quizName = "";
    public bool showNumber, showCorrect, randomize, submitButton;
	public int numQuestion = 0;
    public int screenWidth, screenHeight;
    public Texture quizBoxImg, backImg, startScreenTex;
	[SerializeField]
	public List<int> questTypes = new List<int>();
    public List<bool> addImage = new List<bool>();
    public List<string> questionHeaders = new List<string>();
    public Color backCol;
    public int levels;
    public int levelButtonW;
    public int questBreak;
    public Vector2 submitButtonPos;
	[SerializeField] public List<Question> questions = new List<Question>();
}

[Serializable]
public class Question {
    // pogodi odgovor
    public List<string> answersABC = new List<string>();
    public List<bool> correctAnswersABC = new List<bool>();
    public List<bool> selectedAnswersABC = new List<bool>();
    // pogodi odgovor teksture
    public List<Texture> answersTex = new List<Texture>();
    public List<bool> correctAnswersTex = new List<bool>();
    public List<bool> selectedtAnswersTex = new List<bool>();
    // upari
    public List<string> arrayOne = new List<string>();
    public List<string> arrayTwo = new List<string>();
    // upari slike
    public List<Texture> arrayOneTex = new List<Texture>();
    public List<Texture> arrayTwoTex = new List<Texture>();

    public Texture addImg;
    public float addImgPercent;
    public bool properties;
    public int width;
    public int height;
    public int padX;
    public int padY;
    public bool showHint;
    public bool gridTex;
    public string hint;
    public int hintTime;
    public bool autoAlign;
    public int level;
    public int value;
    public int finalValue;
    public int seconds;
    public bool pixelate;
    public bool horizontal;
}

	