using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class QuizManager : MonoBehaviour {
    public class RectButton {
        public Rect rect;
        public string label;
        public int id;
        public string tag;
    }

    public class RectTexture {
        public Rect rect;
        public Texture texture;
        public string label;
        public int id;
        public string tag;
    }

    public QuizData data;
    public GUISkin quizSkin;
    public AudioSource backgroundMusic, soundEffects;
    public Texture menuIcon, retryIcon, nextIcon, redDot, grayDot;
    [HideInInspector]
    public bool pause = false;
    [HideInInspector]
    public List<bool> results = new List<bool>();
    private List<Texture> arrayOneTex_ = new List<Texture>();
    private List<Texture> arrayTwoTex_ = new List<Texture>();
    private List<Texture> tempArrayOneTex = new List<Texture>();
    private List<Texture> tempArrayTwoTex = new List<Texture>();
    private List<string> arrayOne = new List<string>();
    private List<string> arrayTwo = new List<string>();
    private List<string> tempArrayOne = new List<string>();
    private List<string> tempArrayTwo = new List<string>();
    private Rect rectHolder;
    private RectButton firstRectBtn = new RectButton();
    private RectButton secondRectBtn = new RectButton();
    private RectTexture firstRectTex = new RectTexture();
    private RectTexture secondRectTex = new RectTexture();
    private Texture2D bckUp;
    private Texture2D pixelate;
    private Vector2 mousePos;
    private bool sound = true;
    private bool timeCount = false, running = false, dragging = false, showHint = false, finished = false, showLevels = false, mobile = false;
    private bool[] answers = new bool[5] { false, false, false, false, false };
    private float questTime;
    private float startTime = 0;
    private float time = 0;
    private int activeScreenX = 0;
    private int counter = -1;
    private int current = 0;
    private int loadedLevel;
    private int questionsInLevel = 0;
    private int timer = 10;
    private int w, h;
    private int xindent = 0;
    public List<RectButton> rectButtons = new List<RectButton>();
    public List<RectTexture> rectTextures = new List<RectTexture>();
    private bool startScreen = true;
    private bool checking = false;
    private bool skip = false;
    private List<int> randomOrder = new List<int>();

    private static QuizManager instance;
    public static QuizManager Instance {
        get {
            return instance;
        }
    }

    void Awake() {
        instance = this;
        // Quiz Maker has it's own way of controlling time in the game. Don't change next line. It's responsible for time calculetions every second.
        InvokeRepeating( "Timer", 0, 1 );
        // Checks on which platform is application running
        mobile = Mobile();
        // Sets the background color you choosed in editor. 
        Camera.main.backgroundColor = data.backCol;
        // Starts the background music 
        backgroundMusic.Play();
        // changes the color of all used element (active GUI skin is affected)
        ChangeFontColor( Color.white );
    }


    void Start() {
        for ( int i = 0; i < data.questions.Count; i++ ) {
            randomOrder.Add( i );
        }
        // should we randomize questions?
        if ( data.randomize ) {
            randomOrder = RandomizeI( randomOrder );
        }
    }

    public List<int> RandomizeI( List<int> value ) {
        for ( int t = 0; t < value.Count; t++ ) {
            int tmp = value[t];
            int r = UnityEngine.Random.Range( t, value.Count );
            value[t] = value[r];
            value[r] = tmp;
        }
        return value;
    }

    void Update() {

        // the pause
        if ( Input.GetKeyDown( KeyCode.P ) )
            pause = !pause;

        // what happens when you press any key during starting screen with quiz name
        if ( startScreen && Input.anyKey )
            HideStart();

        if ( startScreen && Time.timeSinceLevelLoad > 10 )
            HideStart();

        // Time management!!!
        // Don't edit this part of code if youd don't know what are you doing!
        if ( time == 0 ) {
            timeCount = false;
        }

        if ( !running )
            return;

        if ( !skip && ( int )Time.time - ( int )( questTime + data.questBreak ) == data.questions[randomOrder[counter]].seconds && !checking ) {
            TimeIsUp();
        }

        if ( counter == data.questions.Count )
            return;

        // Input management

        // This IF statment allows you to enter answers via keyboard
        if ( data.questTypes[randomOrder[counter]] == 0 || data.questTypes[randomOrder[counter]] == 1 ) {
            if ( Input.GetKeyDown( KeyCode.A ) || Input.GetKeyDown( KeyCode.Keypad1 ) || Input.GetKeyDown( KeyCode.Alpha1 ) ) {
                answers[0] = !answers[0];
                StartCoroutine( Answer() );
            } else if ( Input.GetKeyDown( KeyCode.B ) || Input.GetKeyDown( KeyCode.Keypad2 ) || Input.GetKeyDown( KeyCode.Alpha2 ) ) {
                answers[1] = !answers[1];
                StartCoroutine( Answer() );
            } else if ( Input.GetKeyDown( KeyCode.C ) || Input.GetKeyDown( KeyCode.Keypad3 ) || Input.GetKeyDown( KeyCode.Alpha3 ) ) {
                answers[1] = !answers[2];
                StartCoroutine( Answer() );
            } else if ( Input.GetKeyDown( KeyCode.D ) || Input.GetKeyDown( KeyCode.Keypad4 ) || Input.GetKeyDown( KeyCode.Alpha4 ) ) {
                answers[1] = !answers[3];
                StartCoroutine( Answer() );
            } else if ( Input.GetKeyDown( KeyCode.E ) || Input.GetKeyDown( KeyCode.Keypad5 ) || Input.GetKeyDown( KeyCode.Alpha5 ) ) {
                answers[1] = !answers[4];
                StartCoroutine( Answer() );
            }
        }

        // following lines are responsible for drag'n'drop functionality of buttons
        if ( !mobile ) {
            if ( Input.GetMouseButton( 0 ) && dragging ) {
                rectHolder = new Rect( mousePos.x - ( rectHolder.width / 2 ), mousePos.y - ( rectHolder.height / 2 ), rectHolder.width, rectHolder.height );
            }
            if ( Input.GetMouseButtonDown( 0 ) && !dragging ) {
                dragging = true;
                if ( data.questTypes[randomOrder[counter]] == 2 ) {
                    CheckPosition( mousePos, true );
                } else if ( data.questTypes[randomOrder[counter]] == 3 ) {
                    CheckPositionTex( mousePos, true );
                }
            }

            if ( Input.GetMouseButtonUp( 0 ) && dragging ) {
                if ( data.questTypes[randomOrder[counter]] == 2 ) {
                    CheckOverlap( mousePos, false );
                } else if ( data.questTypes[randomOrder[counter]] == 3 ) {
                    CheckOverlapTex( mousePos, false );
                }
                dragging = false;
            }
        } else {
            foreach ( Touch touch in Input.touches ) {
                for ( int i = 0; i < Input.touchCount; ++i ) {
                    if ( Input.GetTouch( i ).phase.Equals( TouchPhase.Began ) ) {
                        // Construct a ray from the current touch coordinates     
                    }
                }
                if ( Input.touchCount > 0 && touch.phase == TouchPhase.Moved && touch.phase != TouchPhase.Canceled && dragging )
                    rectHolder = new Rect( mousePos.x - ( rectHolder.width / 2 ), mousePos.y - ( rectHolder.height / 2 ), rectHolder.width, rectHolder.height );

                if ( Input.touchCount > 0 && touch.phase == TouchPhase.Began && touch.phase != TouchPhase.Canceled && !dragging ) {
                    dragging = true;
                    if ( data.questTypes[randomOrder[counter]] == 2 ) {
                        CheckPosition( mousePos, true );
                    } else if ( data.questTypes[randomOrder[counter]] == 3 ) {
                        CheckPositionTex( mousePos, true );
                    }
                }

                if ( touch.phase == TouchPhase.Ended && dragging ) {
                    mousePos = Input.touches[Input.touches.Length - 1].position;
                    if ( data.questTypes[randomOrder[counter]] == 2 ) {
                        CheckOverlap( new Vector2( mousePos.x, Screen.height - mousePos.y ), false );
                    } else if ( data.questTypes[randomOrder[counter]] == 3 ) {
                        CheckOverlapTex( new Vector2( mousePos.x, Screen.height - mousePos.y ), false );
                    }
                    dragging = false;
                }
            }
        }
    }

    // Android quit
    void FixedUpdate() {
        if ( Input.GetKeyUp( KeyCode.Escape ) && Mobile() ) {
            Application.Quit();
        }
    }

    void OnGUI() {
        if ( GUI.skin != quizSkin )
            GUI.skin = quizSkin;

        int ypos = 0;
        mousePos = Event.current.mousePosition;
        
        if ( startScreen ) {
            GUI.DrawTexture( new Rect( 0, 0, Screen.width, Screen.height ), data.startScreenTex );
            GUI.Label( new Rect( Constants.Instance.halfScreenW - 300, Constants.Instance.halfScreenH - 100, 600, 200 ), data.quizName, quizSkin.customStyles[1] );
        } else if ( data.backImg && !startScreen && !finished )
            GUI.DrawTexture( new Rect( 0, 0, Screen.width, Screen.height ), data.backImg as Texture );

        if ( showLevels )
            ShowLevels();

        // what happens when a quiz is finished
        if ( finished ) {
            GUI.DrawTexture( new Rect( 0, 0, Screen.width, Screen.height ), data.startScreenTex );
            if ( data.quizBoxImg )
                GUI.DrawTexture( Constants.Instance.quizBoxRect, data.quizBoxImg as Texture );

            if ( data.levels > 1 ) {   // if there is more than one level
                Recapitulation();
            } else // if there is only one level
                Finished();
        }

        // sound button manipulation
        if ( sound ) {
            if ( GUI.Button( new Rect( 5, 5, 25, 25 ), "", quizSkin.customStyles[8] ) ) {
                sound = !sound;
                backgroundMusic.mute = true;
                soundEffects.mute = true;
            }
        } else if ( !sound ) {
            if ( GUI.Button( new Rect( 5, 5, 25, 25 ), "", quizSkin.customStyles[9] ) ) {
                sound = !sound;
                backgroundMusic.mute = false;
                soundEffects.mute = false;
            }
        }

        if ( !running )
            return;


        // shows pause information
        if ( pause )
            GUI.Box( new Rect( Constants.Instance.halfScreenW - 200, Constants.Instance.halfScreenH - 20, 400, 40 ), "PAUSE" );

        // show correct answers
        if ( timer < 0 ) {
            int holder = 0;
            if ( data.showNumber ) {
                string questTitle = current.ToString() + "/" + questionsInLevel.ToString() + "   " + data.questionHeaders[randomOrder[counter]];
                GUI.Label( new Rect( Constants.Instance.quizBoxInnerRect.x, Constants.Instance.quizBoxInnerRect.y - 50, Constants.Instance.quizBoxInnerRect.width, 50 ), questTitle, quizSkin.customStyles[0] );
            } else {
                GUI.Label( new Rect( Constants.Instance.quizBoxInnerRect.x, Constants.Instance.quizBoxInnerRect.y - 50, Constants.Instance.quizBoxInnerRect.width, 50 ), data.questionHeaders[randomOrder[counter]], quizSkin.customStyles[0] );
            }
            GUI.BeginGroup( new Rect( Constants.Instance.quizBoxInnerRect.x, Constants.Instance.quizBoxInnerRect.y, Constants.Instance.quizBoxInnerRect.width, Constants.Instance.quizBoxInnerRect.height ) );
            GUI.Label( new Rect( 0, 0, 400, 20 ), "The correct asnwer is..." );
            if ( data.questTypes[randomOrder[counter]] == 0 ) {
                for ( int i = 0; i < 5; i++ ) {
                    if ( data.questions[randomOrder[counter]].correctAnswersABC[i] ) {
                        GUI.Label( new Rect( 0, ( holder * 20 ) + 20, 400, 20 ), data.questions[randomOrder[counter]].answersABC[i] );
                        holder++;
                    }
                }
            } else if ( data.questTypes[randomOrder[counter]] == 1 ) {
                for ( int i = 0; i < 5; i++ ) {
                    if ( data.questions[randomOrder[counter]].correctAnswersTex[i] ) {
                        GUI.DrawTexture( new Rect( ( holder * 110 ), 20, 100, 100 ), data.questions[randomOrder[counter]].answersTex[i] as Texture );
                        holder++;
                    }
                }
            } else if ( data.questTypes[randomOrder[counter]] == 2 ) {
                for ( int i = 0; i < data.questions[randomOrder[counter]].arrayOne.Count; i++ ) {
                    GUI.Label( new Rect( 0, ( holder * 20 ) + 20, 300, 20 ), data.questions[randomOrder[counter]].arrayOne[i] );
                    GUI.Label( new Rect( 310, ( holder * 20 ) + 20, 300, 20 ), data.questions[randomOrder[counter]].arrayTwo[i] );
                    holder++;
                }
            } else if ( data.questTypes[randomOrder[counter]] == 3 ) {
                for ( int i = 0; i < data.questions[randomOrder[counter]].arrayOneTex.Count; i++ ) {
                    GUI.DrawTexture( new Rect( 0, ( holder * 20 ) + 60, 60, 60 ), data.questions[randomOrder[counter]].arrayOneTex[i] );
                    GUI.DrawTexture( new Rect( 100, ( holder * 20 ) + 60, 60, 60 ), data.questions[randomOrder[counter]].arrayTwoTex[i] );
                    holder++;
                }
            }
            GUI.EndGroup();
        }

        if ( pause || timer < 0 )
            return;

        // THE QUIZ GAME PART

        if ( data.showNumber ) {
            string questTitle = current.ToString() + "/" + questionsInLevel.ToString() + "   " + data.questionHeaders[randomOrder[counter]];
            GUI.Label( new Rect( Constants.Instance.quizBoxInnerRect.x, Constants.Instance.quizBoxInnerRect.y - 50, Constants.Instance.quizBoxInnerRect.width, 50 ), questTitle, quizSkin.customStyles[0] );
        } else {
            GUI.Label( new Rect( Constants.Instance.quizBoxInnerRect.x, Constants.Instance.quizBoxInnerRect.y - 50, Constants.Instance.quizBoxInnerRect.width, 50 ), data.questionHeaders[randomOrder[counter]], quizSkin.customStyles[0] );
        }

        if ( data.questTypes[randomOrder[counter]] == 0 ) {
            if ( data.addImage[randomOrder[counter]] ) {
                if ( data.questions[randomOrder[counter]].pixelate && pixelate )
                    GUI.DrawTexture( new Rect( Constants.Instance.quizBoxInnerRect.x + data.questions[randomOrder[counter]].padX, Constants.Instance.quizBoxInnerRect.y + data.questions[randomOrder[counter]].padY, pixelate.width * data.questions[randomOrder[counter]].addImgPercent, pixelate.height * data.questions[randomOrder[counter]].addImgPercent ), pixelate as Texture );
                else if ( !data.questions[randomOrder[counter]].pixelate )
                    GUI.DrawTexture( new Rect( Constants.Instance.quizBoxInnerRect.x + data.questions[randomOrder[counter]].padX, Constants.Instance.quizBoxInnerRect.y + data.questions[randomOrder[counter]].padY, data.questions[randomOrder[counter]].addImg.width * data.questions[randomOrder[counter]].addImgPercent, data.questions[randomOrder[counter]].addImg.height * data.questions[randomOrder[counter]].addImgPercent ), data.questions[randomOrder[counter]].addImg );
            }
            GUI.BeginGroup( new Rect( Constants.Instance.halfScreenW - ( ( ( Screen.width * Constants.Instance.screenPercentW ) - xindent ) / 2 ) + xindent, Constants.Instance.quizBoxInnerRect.y, ( Screen.width * Constants.Instance.screenPercentW ) - xindent, 400 ) );

            for ( int i = 0; i < 5; i++ ) {
                if ( !String.IsNullOrEmpty( data.questions[randomOrder[counter]].answersABC[i] ) ) {
                    if ( answers[i] ) {
                        if ( GUI.Button( new Rect( 0, ypos, w, h ), data.questions[randomOrder[counter]].answersABC[i], quizSkin.customStyles[7] ) ) {
                            answers[i] = !answers[i];
                            //StartCoroutine( Answer() );
                        }
                    } else {
                        if ( GUI.Button( new Rect( 0, ypos, w, h ), data.questions[randomOrder[counter]].answersABC[i] ) ) {
                            answers[i] = !answers[i];
                            //StartCoroutine( Answer() );
                        }
                    }

                    ypos += ( h + 5 );
                }
            }
            GUI.EndGroup();
        } else if ( data.questTypes[randomOrder[counter]] == 1 ) {
            GUI.BeginGroup( new Rect( Constants.Instance.halfScreenW - ( ( ( Screen.width * Constants.Instance.screenPercentW ) - xindent ) / 2 ) + xindent, Constants.Instance.quizBoxInnerRect.y, ( Screen.width * Constants.Instance.screenPercentW ) - xindent, 400 ) );
            for ( int i = 0; i < 5; i++ ) {
                if ( data.questions[randomOrder[counter]].answersTex.Count > i ) {
                    if ( answers[i] ) {
                        if ( !data.questions[randomOrder[counter]].horizontal ) {
                            if ( GUI.Button( new Rect( 0, ypos, w, h ), data.questions[randomOrder[counter]].answersTex[i] as Texture, quizSkin.customStyles[7] ) ) {
                                answers[i] = !answers[i];
                                //StartCoroutine( Answer() );
                            }
                        } else {
                            if ( GUI.Button( new Rect( ypos, 0, w, h ), data.questions[randomOrder[counter]].answersTex[i] as Texture, quizSkin.customStyles[7] ) ) {
                                answers[i] = !answers[i];
                               // StartCoroutine( Answer() );
                            }
                        }
                    } else {
                        if ( !data.questions[randomOrder[counter]].horizontal ) {
                            if ( GUI.Button( new Rect( 0, ypos, w, h ), data.questions[randomOrder[counter]].answersTex[i] as Texture ) ) {
                                answers[i] = !answers[i];
                               // StartCoroutine( Answer() );
                            }
                        } else {
                            if ( GUI.Button( new Rect( ypos, 0, w, h ), data.questions[randomOrder[counter]].answersTex[i] as Texture ) ) {
                                answers[i] = !answers[i];
                               // StartCoroutine( Answer() );
                            }
                        }
                    }

                    if ( !data.questions[randomOrder[counter]].horizontal )
                        ypos += ( h + 5 );
                    else
                        ypos += ( w + 5 );
                }
            }
            GUI.EndGroup();
        } else if ( data.questTypes[randomOrder[counter]] == 2 ) {
            foreach ( RectButton r in rectButtons ) {
                if ( dragging && firstRectBtn.id == r.id && firstRectBtn.tag == r.tag )
                    GUI.Button( rectHolder, r.label );
                else
                    GUI.Button( r.rect, r.label );
            }
        } else if ( data.questTypes[randomOrder[counter]] == 3 ) {
            foreach ( RectTexture r in rectTextures ) {
                if ( dragging && firstRectTex.id == r.id && firstRectTex.tag == r.tag )
                    GUI.Button( rectHolder, r.texture );
                else
                    GUI.Button( r.rect, r.texture );
            }
        }

        // show submit button
        if ( data.submitButton ) {
            if ( GUI.Button( new Rect( data.submitButtonPos.x, data.submitButtonPos.y, 100, 30 ), "Submit" ) ) {
                timer = -1;
                skip = true;
                Invoke( "TimeIsUp", data.questBreak );
            }
        }
        // shows hint
        if ( showHint )
            GUI.Label( new Rect( Constants.Instance.quizBoxInnerRect.x , Constants.Instance.quizBoxInnerRect.y + Constants.Instance.quizBoxInnerRect.height, Constants.Instance.quizBoxInnerRect.width, 100 ), data.questions[randomOrder[counter]].hint, quizSkin.customStyles[0] );

        // shows time
        if ( timeCount ) {
            GUI.Label( Constants.Instance.timeText, "Time:", quizSkin.customStyles[3] );
            GUI.Label( Constants.Instance.time, GetTimer(), quizSkin.customStyles[3] );
        }

        // shows score
        GUI.Label( Constants.Instance.scoreText, "Score:", quizSkin.customStyles[3] );
        GUI.Label( Constants.Instance.score, GetScore().ToString(), quizSkin.customStyles[3] );    
    }

    void OnApplicationQuit() {

    }

    // changes color of font in gui skin
    private void ChangeFontColor( Color fontColor ) {
        quizSkin.box.normal.textColor = fontColor;
        quizSkin.box.active.textColor = fontColor;
        quizSkin.box.hover.textColor = fontColor;
        quizSkin.box.focused.textColor = fontColor;

        quizSkin.button.normal.textColor = fontColor;
        quizSkin.button.active.textColor = fontColor;
        quizSkin.button.hover.textColor = fontColor;
        quizSkin.button.focused.textColor = fontColor;

        quizSkin.label.normal.textColor = fontColor;
        quizSkin.label.active.textColor = fontColor;
        quizSkin.label.hover.textColor = fontColor;
        quizSkin.label.focused.textColor = fontColor;

        for ( int i = 0; i < quizSkin.customStyles.Length; i++ ) {
            quizSkin.customStyles[i].normal.textColor = fontColor;
            quizSkin.customStyles[i].active.textColor = fontColor;
            quizSkin.customStyles[i].hover.textColor = fontColor;
            quizSkin.customStyles[i].focused.textColor = fontColor;
        }
    }

    // check answers 2
    IEnumerator Answer() {
        checking = true;
        timer = -1;
        yield return new WaitForSeconds( 2 );
        results.Add( CheckAnswer( counter ) );
        counter++;
        showHint = false;

        if ( current == questionsInLevel ) {
            StopQuiz();
        } else {
            if ( data.questions[randomOrder[counter]].level != loadedLevel ) {
                while ( data.questions[randomOrder[counter]].level != loadedLevel ) {
                    if ( counter != data.questions.Count )
                        counter++;
                }
            }
        }
        Rotation();
    }

    private void TimeIsUp() {
        results.Add( CheckAnswer( counter ) );
        counter++;
        showHint = false;

        if ( current == questionsInLevel ) {
            StopQuiz();
        } else {
            if ( data.questions[randomOrder[counter]].level != loadedLevel ) {
                while ( data.questions[randomOrder[counter]].level != loadedLevel ) {
                    if ( counter != data.questions.Count )
                        counter++;
                }
            }
        }
        Rotation();
    }

    // check the answers
    private bool CheckAnswer( int count ) {
        bool correct = true;

        // select right answer type
        if ( data.questTypes[count] == 0 ) {
            for ( int i = 0; i < 5; i++ ) {
                if ( !String.IsNullOrEmpty( data.questions[count].answersABC[i] ) ) {
                    if ( data.questions[count].correctAnswersABC[i] != answers[i] )
                        correct = false;
                }
            }
        }	// select right image type
        else if ( data.questTypes[count] == 1 ) {
            for ( int i = 0; i < 5; i++ ) {
                if ( i < data.questions[count].answersTex.Count ) {
                    if ( data.questions[count].correctAnswersTex[i] != answers[i] )
                        correct = false;
                }
            }
        } else if ( data.questTypes[count] == 2 ) // pair type
        {
            List<string> l1 = new List<string>();
            List<string> l2 = new List<string>();
            Dictionary<string, string> niz1 = new Dictionary<string, string>();
            Dictionary<string, string> niz2 = new Dictionary<string, string>();

            for ( int i = 0; i < data.questions[randomOrder[counter]].arrayOne.Count; i++ ) {
                niz1.Add( data.questions[randomOrder[counter]].arrayOne[i], data.questions[randomOrder[counter]].arrayTwo[i] );
            }

            foreach ( RectButton r in rectButtons ) {
                if ( r.tag == "left" )
                    l1.Add( r.label );
            }
            foreach ( RectButton r in rectButtons ) {
                if ( r.tag == "right" )
                    l2.Add( r.label );
            }

            for ( int i = 0; i < l1.Count; i++ ) {
                niz2.Add( l1[i], l2[i] );
            }

            foreach ( KeyValuePair<string, string> k in niz1 ) {
                if ( niz2.ContainsKey( k.Key ) ) {
                    foreach ( KeyValuePair<string, string> k2 in niz2 ) {
                        if ( k2.Key == k.Key ) {
                            if ( k.Value != k2.Value )
                                correct = false;
                        }
                    }
                }
            }
        } else if ( data.questTypes[count] == 3 ) // pair images type
        {
            List<string> l1 = new List<string>();
            List<string> l2 = new List<string>();
            Dictionary<string, string> niz1 = new Dictionary<string, string>();
            Dictionary<string, string> niz2 = new Dictionary<string, string>();

            for ( int i = 0; i < data.questions[randomOrder[counter]].arrayOne.Count; i++ ) {
                niz1.Add( data.questions[randomOrder[counter]].arrayOneTex[i].name, data.questions[randomOrder[counter]].arrayTwoTex[i].name );
            }

            foreach ( RectTexture r in rectTextures ) {
                if ( r.tag == "left" )
                    l1.Add( r.texture.name );
            }
            foreach ( RectTexture r in rectTextures ) {
                if ( r.tag == "right" )
                    l2.Add( r.texture.name );
            }

            for ( int i = 0; i < l1.Count; i++ ) {
                niz2.Add( l1[i], l2[i] );
            }

            foreach ( KeyValuePair<string, string> k in niz1 ) {
                if ( niz2.ContainsKey( k.Key ) ) {
                    foreach ( KeyValuePair<string, string> k2 in niz2 ) {
                        if ( k2.Key == k.Key ) {
                            if ( k.Value != k2.Value )
                                correct = false;
                        }
                    }
                }
            }
        }

        return correct;
    }

    // check if the button is draggable
    private void CheckPosition( Vector2 pos, bool first ) {
        foreach ( RectButton r in rectButtons ) {
            if ( r.rect.Contains( pos ) ) {
                firstRectBtn = r;
                rectHolder = r.rect;
            }
        }
    }

    // check if the image is draggable
    private void CheckPositionTex( Vector2 pos, bool first ) {
        foreach ( RectTexture r in rectTextures ) {
            if ( r.rect.Contains( pos ) ) {
                firstRectTex = r;
                rectHolder = r.rect;
            }
        }
    }

    // checks if two buttons are overlapping
    private void CheckOverlap( Vector2 pos, bool first ) {
        if ( !dragging )
            return;
        foreach ( RectButton r in rectButtons ) {
            if ( r.rect.Contains( pos ) ) {
                secondRectBtn = r;
            }
        }

        if ( firstRectBtn.tag == secondRectBtn.tag ) {
            ReplaceValues( firstRectBtn, secondRectBtn );
        }
    }

    // checks if two images are overlapping
    private void CheckOverlapTex( Vector2 pos, bool first ) {
        if ( !dragging )
            return;
        foreach ( RectTexture r in rectTextures ) {
            if ( r.rect.Contains( pos ) )
                secondRectTex = r;
        }

        if ( firstRectTex.tag == secondRectTex.tag )
            ReplaceValuesTex( firstRectTex, secondRectTex );
    }

    private void ClearAll() {
        rectTextures.Clear();
        rectButtons.Clear();
        tempArrayOne.Clear();
        tempArrayTwo.Clear();
        tempArrayOneTex.Clear();
        tempArrayTwoTex.Clear();
        arrayOne.Clear();
        arrayOneTex_.Clear();
        arrayTwo.Clear();
        arrayTwoTex_.Clear();
        for ( int i = 0; i < 5; i++ ) {
            answers[i] = false;
        }
    }

    private void Finished() {
        GUI.BeginGroup( new Rect( Constants.Instance.halfScreenW - 200, Constants.Instance.halfScreenH - 20, 400, 80 ) );
        GUI.Label( new Rect( 0, 0, 400, 70 ), "GAME OVER\nYour score is " + GetScore().ToString(), quizSkin.customStyles[6] );
        GUI.EndGroup();
    }

    private void FormatPair() {
        foreach ( string s in data.questions[randomOrder[counter]].arrayOne ) {
            if ( !String.IsNullOrEmpty( s ) )
                arrayOne.Add( s );
        }
        foreach ( string s in data.questions[randomOrder[counter]].arrayTwo ) {
            if ( !String.IsNullOrEmpty( s ) )
                arrayTwo.Add( s );
        }

        tempArrayOne = arrayOne;
        tempArrayTwo = arrayTwo;

        Shuffle( tempArrayOne );
        Shuffle( tempArrayTwo );

        if ( !data.questions[randomOrder[counter]].autoAlign ) {
            if ( data.questions[randomOrder[counter]].properties ) {
                w = data.questions[randomOrder[counter]].width;
                h = data.questions[randomOrder[counter]].height;
            } else {
                w = 300;
                h = 30;
            }
        } else {
            w = ( int )( ( Screen.width * Constants.Instance.screenPercentW ) / 2 );
            h = 30;
        }

        for ( int i = 0; i < arrayOne.Count; i++ ) {
            if ( !String.IsNullOrEmpty( tempArrayOne[i] ) ) {
                RectButton r = new RectButton();
                r.id = i;
                r.label = tempArrayOne[i];
                r.rect = new Rect( Constants.Instance.quizBoxInnerRect.x + 5, Constants.Instance.quizBoxInnerRect.y + ( i * ( 30 + 10 ) ), w, h );
                r.tag = "left";
                rectButtons.Add( r );
            }
            if ( !String.IsNullOrEmpty( tempArrayTwo[i] ) ) {
                RectButton r = new RectButton();
                r.id = i;
                r.label = tempArrayTwo[i];
                r.rect = new Rect( Constants.Instance.quizBoxInnerRect.x + w + 10, Constants.Instance.quizBoxInnerRect.y + ( i * ( 30 + 10 ) ), w - 15, h );
                r.tag = "right";
                rectButtons.Add( r );
            }
        }
    }

    private void FormatPairTex() {
        arrayOneTex_ = data.questions[randomOrder[counter]].arrayOneTex;
        arrayTwoTex_ = data.questions[randomOrder[counter]].arrayTwoTex;

        foreach ( Texture t in arrayOneTex_ )
            tempArrayOneTex.Add( t );

        foreach ( Texture t in arrayTwoTex_ )
            tempArrayTwoTex.Add( t );

        ShuffleTex( tempArrayOneTex );
        ShuffleTex( tempArrayTwoTex );

        for ( int i = 0; i < tempArrayOneTex.Count; i++ ) {
            RectTexture r = new RectTexture();
            r.id = i;
            r.texture = tempArrayOneTex[i];
            r.label = i.ToString();
            r.rect = new Rect( Constants.Instance.quizBoxInnerRect.x + 5, Constants.Instance.quizBoxInnerRect.y + ( i * ( 100 + 10 ) ), 100, 100 );
            r.tag = "left";
            rectTextures.Add( r );
        }

        for ( int i = 0; i < tempArrayTwoTex.Count; i++ ) {
            RectTexture r = new RectTexture();
            r.id = i;
            r.texture = tempArrayTwoTex[i];
            r.label = i.ToString();
            r.rect = new Rect( Constants.Instance.quizBoxInnerRect.x + 305, Constants.Instance.quizBoxInnerRect.y + ( i * ( 100 + 10 ) ), 100, 100 );
            r.tag = "right";
            rectTextures.Add( r );
        }
    }

    public int GetScore() {
        int score = 0;
        for ( int i = 0; i < results.Count; i++ ) {
            if ( results[i] ) {
                score += data.questions[i].value;
            }
        }
        return score;
    }

    public void ResetScore() {
        for ( int i = 0; i < results.Count; i++ ) {
            results[i] = false;
        }
    }

    private string GetTimer() {
        return ( ( int )timer ).ToString();
    }

    private void HideStart() {
        startScreen = false;
        // if there is more then one level  you should see level numbers
        // or the only level will be automaticaly loaded
        if ( data.levels > 1 ) {
            showLevels = true;
        } else {
            loadedLevel = 1;
            LoadLevel( loadedLevel );
        }
    }

    private void LoadLevel( int l ) {
        ResetScore();
        current = 0;
        questionsInLevel = 0;
        foreach ( Question q in data.questions ) {
            if ( q.level == l )
                questionsInLevel++;
        }

        if ( questionsInLevel == 0 ) {
            questionsInLevel = data.numQuestion;
            finished = false;
            counter = -1;
            time = startTime;
            showLevels = true;
            return;
        }

        startTime = Time.time;
        questTime = Time.time;
        showLevels = false;
        counter++;
        if ( data.questions[randomOrder[counter]].level != loadedLevel ) {
            while ( data.questions[randomOrder[counter]].level != loadedLevel ) {
                counter++;
            }
        }
        Rotation();
    }

    private bool Mobile() {
        if ( Application.platform == RuntimePlatform.WindowsEditor || 
        Application.platform == RuntimePlatform.WindowsWebPlayer || 
        Application.platform == RuntimePlatform.WindowsPlayer || 
        Application.platform == RuntimePlatform.FlashPlayer || 
        Application.platform == RuntimePlatform.OSXDashboardPlayer || 
        Application.platform == RuntimePlatform.OSXEditor || 
        Application.platform == RuntimePlatform.OSXPlayer || 
        Application.platform == RuntimePlatform.OSXWebPlayer )
            return false;
        else
            return true;
    }

    private void Recapitulation() {
        GUI.BeginGroup( new Rect( Constants.Instance.halfScreenW - 200, Constants.Instance.halfScreenH - 20, 400, 100 ) );
        GUI.Label( new Rect( 0, 0, 400, 70 ), "GAME OVER\nYour score is " + GetScore().ToString(), quizSkin.customStyles[6] );
        GUI.DrawTexture( new Rect( 20, 70, 25, 25 ), menuIcon as Texture );
        if ( GUI.Button( new Rect( 45, 70, 50, 25 ), "Menu", quizSkin.customStyles[10] ) ) {
            finished = false;
            counter = -1;
            time = startTime;
            showLevels = true;
        }
        GUI.DrawTexture( new Rect( 120, 70, 25, 25 ), retryIcon as Texture );
        if ( GUI.Button( new Rect( 145, 70, 35, 25 ), "Retry", quizSkin.customStyles[10] ) ) {
            finished = false;
            counter = -1;
            questTime = time;
            ReloadLevel( loadedLevel );
        }
        GUI.DrawTexture( new Rect( 230, 70, 25, 25 ), nextIcon as Texture );
        if ( GUI.Button( new Rect( 255, 70, 55, 25 ), "Next Level", quizSkin.customStyles[10] ) ) {
            finished = false;
            counter = -1;
            questTime = time;
            loadedLevel++;
            LoadLevel( loadedLevel );
        }
        GUI.EndGroup();
    }

    private void ReloadLevel( int l ) {
        ResetScore();
        showLevels = false;
        current = 0;
        counter++;
        if ( data.questions[randomOrder[counter]].level != loadedLevel ) {
            while ( data.questions[randomOrder[counter]].level != loadedLevel ) {
                counter++;
            }
        }
        Rotation();
    }

    private void ReplaceValues( RectButton r1, RectButton r2 ) {
        string value1 = r1.label;
        string value2 = r2.label;

        foreach ( RectButton r in rectButtons ) {
            if ( r.id == r1.id && r.tag == r1.tag )
                r.label = value2;
            else if ( r.id == r2.id && r.tag == r2.tag )
                r.label = value1;
        }
    }

    private void ReplaceValuesTex( RectTexture r1, RectTexture r2 ) {
        Texture value1 = r1.texture;
        Texture value2 = r2.texture;

        foreach ( RectTexture r in rectTextures ) {
            if ( r.id == r1.id && r.tag == r1.tag )
                r.texture = value2;
            else if ( r.id == r2.id && r.tag == r2.tag )
                r.texture = value1;
        }
    }

    // main function that rotates the questions and perform actions to diplay them properly
    private void Rotation() {
        if ( finished )
            return;
        if ( !running )
            running = true;
        questTime = Time.time;
        if ( !timeCount ) {
            timeCount = true;
        }
        checking = false;
        dragging = false;

        // clear all values
        ClearAll();

        timer = data.questions[randomOrder[counter]].seconds;

        if ( counter > data.numQuestion ) {
            running = false;
            return;
        }

        xindent = 0;
        if ( data.questTypes[randomOrder[counter]] == 0 ) {
            if ( data.addImage[randomOrder[counter]] ) {
                xindent = ( int )( data.questions[randomOrder[counter]].addImg.width * data.questions[randomOrder[counter]].addImgPercent );
            }

            if ( !data.questions[randomOrder[counter]].autoAlign ) {
                if ( data.questions[randomOrder[counter]].properties ) {
                    w = data.questions[randomOrder[counter]].width;
                    h = data.questions[randomOrder[counter]].height;
                } else {
                    w = 300;
                    h = 30;
                }
            } else {
                w = ( int )( ( Screen.width * Constants.Instance.screenPercentW ) - xindent ) - ( int )Constants.Instance.quizBoxInnerRect.x;
                h = 30;
            }
        } else if ( data.questTypes[randomOrder[counter]] == 1 ) {
            if ( data.addImage[randomOrder[counter]] )
                xindent = ( int )( data.questions[randomOrder[counter]].addImg.width * data.questions[randomOrder[counter]].addImgPercent );

            if ( !data.questions[randomOrder[counter]].autoAlign ) {
                if ( data.questions[randomOrder[counter]].properties ) {
                    w = data.questions[randomOrder[counter]].width;
                    h = data.questions[randomOrder[counter]].height;
                } else {
                    w = 100;
                    h = 100;
                }
            } else {
                Debug.Log( xindent );
                w = ( int )( ( Screen.width * Constants.Instance.screenPercentW ) - xindent * 2 );
                h = 30;
            }
        } else if ( data.questTypes[randomOrder[counter]] == 2 )
            FormatPair();
        else if ( data.questTypes[randomOrder[counter]] == 3 )
            FormatPairTex();

        current++;
    }

    // randomize order of buttons
    private void Shuffle( List<string> s ) {
        List<string> temp = s;
        for ( int t = 0; t < temp.Count; t++ ) {
            if ( !String.IsNullOrEmpty( temp[t] ) ) {
                string tmp = temp[t];
                int r = UnityEngine.Random.Range( t, temp.Count );
                temp[t] = temp[r];
                temp[r] = tmp;
            }
        }
        s = temp;
    }

    // randomize order of images
    private void ShuffleTex( List<Texture> s ) {
        List<Texture> temp = s;
        for ( int t = 0; t < temp.Count; t++ ) {
            Texture tmp = temp[t];
            int r = UnityEngine.Random.Range( t, temp.Count );
            temp[t] = temp[r];
            temp[r] = tmp;
        }
        s = temp;
    }

    private void StopQuiz() {
        running = false;
        finished = true;
        CancelInvoke( "Rotation" );
    }

    private void ShowLevels() {
        GUI.Label( new Rect( Constants.Instance.quizBoxInnerRect.x, Constants.Instance.quizBoxInnerRect.y - 50, Constants.Instance.quizBoxInnerRect.width, 50 ), "Select level", quizSkin.customStyles[0] );

        GUI.BeginGroup( Constants.Instance.quizBoxInnerRect );
        int xpos = 0;

        GUI.BeginGroup( new Rect( activeScreenX, 0, Constants.Instance.quizBoxInnerRect.width * Constants.Instance.numOfScreens, Constants.Instance.quizBoxInnerRect.height ) );
        for ( int s = 0; s < Constants.Instance.numOfScreens; s++ ) {
            if ( ( 0 - ( s * Constants.Instance.quizBoxInnerRect.width ) ) == activeScreenX ) {
                GUI.BeginGroup( new Rect( s * Constants.Instance.quizBoxInnerRect.width, 0, Constants.Instance.quizBoxInnerRect.width, Constants.Instance.quizBoxInnerRect.height ) );
                for ( int i = ( s * Constants.Instance.oneScreen ); i < ( ( s * Constants.Instance.oneScreen ) + Constants.Instance.oneScreen ); i++ ) {
                    if ( xpos == Constants.Instance.levelsPerRow )
                        xpos = 0;

                    if ( i < data.levels ) {
                        if ( GUI.Button( new Rect( ( xpos * data.levelButtonW ), ( int )( ( ( i - ( s * Constants.Instance.oneScreen ) ) / Constants.Instance.levelsPerRow ) * data.levelButtonW ), data.levelButtonW - 5, data.levelButtonW - 5 ), ( i + 1 ).ToString(), quizSkin.customStyles[4] ) ) {
                            loadedLevel = i + 1;
                            LoadLevel( loadedLevel );
                        }
                        xpos++;
                    }
                }
                GUI.EndGroup();
            }
        }
        GUI.EndGroup();
        GUI.EndGroup();

        if ( Constants.Instance.numOfScreens > 1 ) {
            GUI.BeginGroup( new Rect( Constants.Instance.halfScreenW - ( ( ( Constants.Instance.numOfScreens * 30 ) + 20 ) / 2 ), Constants.Instance.quizBoxInnerRect.height + Constants.Instance.quizBoxInnerRect.y + 10, ( Constants.Instance.numOfScreens * 30 ) + 20, 22 ) );
            if ( GUI.Button( new Rect( 0, 0, 22, 22 ), "<" ) && activeScreenX < 0 ) {
                activeScreenX = 0 + ( ( ( activeScreenX / ( int )Constants.Instance.quizBoxInnerRect.width ) + 1 ) * ( int )Constants.Instance.quizBoxInnerRect.width );
            }
            for ( int h = 0; h < Constants.Instance.numOfScreens; h++ ) {
                if ( ( h * -1 ) == activeScreenX / ( int )Constants.Instance.quizBoxInnerRect.width )
                    GUI.DrawTexture( new Rect( ( h * 16 ) + 25, 5, 12, 12 ), redDot as Texture );
                else
                    GUI.DrawTexture( new Rect( ( h * 16 ) + 25, 5, 12, 12 ), grayDot as Texture );
            }
            if ( GUI.Button( new Rect( ( Constants.Instance.numOfScreens * 16 ) + 25, 0, 22, 22 ), ">" ) && ( activeScreenX * -1 ) < ( ( Constants.Instance.numOfScreens * ( int )Constants.Instance.quizBoxInnerRect.width ) - ( int )Constants.Instance.quizBoxInnerRect.width ) ) {
                activeScreenX = 0 + ( ( ( activeScreenX / ( int )Constants.Instance.quizBoxInnerRect.width ) - 1 ) * ( int )Constants.Instance.quizBoxInnerRect.width );
            }
            GUI.EndGroup();
        }

    }

    private void Timer() {
        if ( pause )
            return;
        time++;

        if ( !running )
            return;

        if ( counter >= 0 ) {
            if ( data.questions[randomOrder[counter]].showHint ) {
                if ( timer == data.questions[randomOrder[counter]].hintTime )
                    showHint = true;
            }
            if ( data.addImage[randomOrder[counter]] ) {
                if ( data.questions[randomOrder[counter]].pixelate && timer > 0 ) {
                    pixelate = data.questions[randomOrder[counter]].addImg as Texture2D;
                    pixelate = ImageProcess.SetPixelate( pixelate, timer );
                }
            }

            if ( soundEffects.clip )
                soundEffects.Play();
        }

        if ( timer > 0 )
            soundEffects.Play();

        timer--;
    }
}

