using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AnimalControl : MonoBehaviour
{
    private float moveSpeed ;
    private float x;
    private float y;
    private float timer_f;
    private int timer_i;
    public GameObject timer;
    public GameObject heart_1;
    public GameObject heart_2;
    public GameObject heart_3;
    public GameObject heart_4;
    private GameObject[] heartArr;
    private int heartNum;

    // Start is called before the first frame update
    void Start()
    {
        heartNum = 4;
        heartArr = new GameObject[heartNum];
        heartArr[0] = heart_1;
        heartArr[1] = heart_2;
        heartArr[2] = heart_3;
        heartArr[3] = heart_4;
        moveSpeed = 5.0f;
        timer_f = 0.0f;
        timer_i = 0;
    }

    // Update is called once per frame
    void Update() {
        x = Input.GetAxis("Horizontal");
        y = Input.GetAxis("Vertical");

        transform.position += new Vector3(x, y, 0) * Time.deltaTime * moveSpeed;
        timer_f = timer_f + Time.deltaTime;
        timer_i = (int)timer_f;
        timer.GetComponent<TextMesh>().text = "Survival time(s): " + timer_i.ToString(); 
        if (heartNum <= 0) {
            EndGameScene();
        } 
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "HumanAttack")
        { 
            if (heartNum > 0) {
                heartNum = heartNum - 1;
                Destroy(heartArr[heartNum]);
            }
        }
    }

    void EndGameScene()
    {
        // Massacre end
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 3);
        
    }
}
