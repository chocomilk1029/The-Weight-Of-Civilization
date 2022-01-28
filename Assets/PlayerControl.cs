using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

public class PlayerControl : MonoBehaviour
{
    // animal image
    public GameObject Animal_1;
    public GameObject Animal_2;
    public GameObject Animal_3;
    public GameObject Animal_4;
    public Sprite[] AnimalImages;
    private int[] AnimalLine;
    private int[] AnimalImageArr;
    private GameObject[] AnimalArray;
    // human image
    public GameObject BG;
    public Sprite[] BGArr;
    public Sprite[] HumanImages;
    // marks related
    public GameObject markBoard;
    public int marks;
    // end game
    public GameObject EndGame;
    int IdleTimeSetting = 10;
    float LastIdleTime;
    // music
    public GameObject Audio;
    public AudioClip[] AudioArr;

    // Start is called before the first frame update
    void Start()
    {
        marks = 0;
        AnimalLine = new int[4];
        AnimalImageArr = new int[4];
        AnimalArray = new GameObject[4];
        // Attack = GameObject.FindGameObjectsWithTag("HumanAttack");
        AnimalArray[0] = Animal_1;
        AnimalArray[1] = Animal_2;
        AnimalArray[2] = Animal_3;
        AnimalArray[3] = Animal_4;

        for (int i = 0; i < 4; i++)
        {
            AnimalLine[i] = UnityEngine.Random.Range(1, 6);
            AnimalImageArr[i] = UnityEngine.Random.Range(0, 12);
        }
        initAnimalPlace(AnimalLine, AnimalArray);
        initAnimalSprite(AnimalImageArr, AnimalArray);
        LastIdleTime = Time.time;
    }

    // Update is called once per frame
    void Update() {
        if(Input.anyKey){
            LastIdleTime = Time.time;
        }
        // move
        if (Input.GetKeyDown(KeyCode.Alpha1)) {
            transform.position = new Vector3(-9.58f, -3.72f, 0f);
            // check attack
            CheckAttack(1);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2)) {
            transform.position = new Vector3(-7.61f, -3.72f, 0f);
            CheckAttack(2);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3)) {
            transform.position = new Vector3(-5.42f, -3.72f, 0f);
            CheckAttack(3);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4)) {
            transform.position = new Vector3(-3.1f, -3.72f, 0f);
            CheckAttack(4);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5)) {
            transform.position = new Vector3(-0.93f, -3.72f, 0f);
            CheckAttack(5);
        }
        Evolution(marks);
        IdleCheck();
    }

    void Evolution(int marks) {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        SpriteRenderer BGsr = BG.GetComponent<SpriteRenderer>();
        if (marks < 1500) {
            sr.sprite = HumanImages[0];
            BGsr.sprite = BGArr[0];
            if (Audio.GetComponent<AudioSource>().clip != AudioArr[0]) {
                Audio.GetComponent<AudioSource>().clip = AudioArr[0];
                Audio.GetComponent<AudioSource>().Play();
            }
            EndGame.SetActive(false);
        }
        else if (marks >= 1500 && marks < 3000) {
            sr.sprite = HumanImages[1];
            BGsr.sprite = BGArr[1];
            if (Audio.GetComponent<AudioSource>().clip != AudioArr[1]) {
                Audio.GetComponent<AudioSource>().clip = AudioArr[1];
                Audio.GetComponent<AudioSource>().Play();
            }
            EndGame.SetActive(false);
        }
        else {
            sr.sprite = HumanImages[2];
            BGsr.sprite = BGArr[2];
            if (Audio.GetComponent<AudioSource>().clip != AudioArr[2]) {
                Audio.GetComponent<AudioSource>().clip = AudioArr[2];
                Audio.GetComponent<AudioSource>().Play();
            }
            EndGame.SetActive(true);
        }
    }

    void CheckAttack(int AttackRoad) {
        if (AttackRoad == AnimalLine[0]) {
            UpdateMarks(100);
            for (int i = 0; i < 3; i++) {
                AnimalLine[i] = AnimalLine[i + 1];
                AnimalImageArr[i] = AnimalImageArr[i + 1];
            }
            AnimalLine[3] = UnityEngine.Random.Range(1, 6);
            AnimalImageArr[3] = UnityEngine.Random.Range(0, 12);
            initAnimalPlace(AnimalLine, AnimalArray);
            initAnimalSprite(AnimalImageArr, AnimalArray);
        }
        else {
            UpdateMarks(-50);
        }
    }

    void initAnimalSprite(int[] AnimalImageArr, GameObject[] AnimalObject) {
        for (int i = 0; i < 4; i++)
        {
            SpriteRenderer sr = AnimalObject[i].GetComponent<SpriteRenderer>();
            sr.sprite = AnimalImages[AnimalImageArr[i]];
        }
    }
    void initAnimalPlace(int[] AnimalLine, GameObject[] AnimalObject) {
        int position = 0;
        for (int i = 0; i < 4; i++)
        {
            position = AnimalLine[i] - 1;
            AnimalObject[i].transform.position = new Vector2(-9.6f + (position * 2.15f), -1.43f + (i * 2.3f));
        }
    }

    void UpdateMarks(int addDrop) {
        marks = marks + addDrop;
        markBoard.GetComponent<TextMesh>().text = "Marks: " + marks.ToString(); 
        if (addDrop <= 0) {
            markBoard.GetComponent<TextMesh>().color = Color.red;
        }
        else
        {
            markBoard.GetComponent<TextMesh>().color = Color.green;
        }
    }

    // Enter end scene - keep old if idle for 15 sec and marks == 0
    void IdleCheck(){
        if (marks == 0 && Time.time - LastIdleTime > IdleTimeSetting)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

}
