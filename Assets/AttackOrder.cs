using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackOrder : MonoBehaviour
{
    public GameObject HumanPlayer;
    public Sprite[] AttackImages;
    private float moveSpeed;
    public float x;
    public float y;
    public int record;
    private int PlayerMarks;

    // Start is called before the first frame update
    void Start()
    {
        record = 0;
        x = Random.Range(-1.0f, 1.0f);
        y = Random.Range(-1.0f, 1.0f);
        HumanPlayer = GameObject.Find("hito_0");
        moveSpeed = 5.0f;
        
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMarks = HumanPlayer.GetComponent<PlayerControl>().marks;
        Evolution(PlayerMarks);
        transform.position += new Vector3(x, y, 0) * Time.deltaTime * moveSpeed;
    }

    void Evolution(int marks) {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        if (marks < 1500) {
            sr.sprite = AttackImages[0];
        }
        else if (marks >= 1500 && marks < 3000) {
            sr.sprite = AttackImages[1];
        }
        else {
            sr.sprite = AttackImages[2];
        }
        if (record < PlayerMarks) {
            record = PlayerMarks;
            // gen attack every 1000 marks
            int AttackNum = GameObject.FindGameObjectsWithTag("HumanAttack").Length - 1;
            if ((int)(record/500) > AttackNum && AttackNum < 15) {
                moveSpeed += 1.0f;
                float genX = Random.Range(0.8f, 9.9f);
                float genY = Random.Range(-4.0f, 6.2f);
                Instantiate(this, new Vector3(genX, genY, 0), transform.rotation);
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Wall")
        {
            if (x >= 0) {
                x = Random.Range(-1.0f, 0.0f);
            }
            else {
                x = Random.Range(0.0f, 1.0f);
            }
            
            if (y >= 0) {
                y = Random.Range(-1.0f, 0.0f);
            }
            else {
                y = Random.Range(0.0f, 1.0f);
            }
        }
    }
}
