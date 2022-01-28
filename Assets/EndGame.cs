using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndGame : MonoBehaviour
{
    public GameObject HumanPlayer;
    public int PlayerMarks;
    // Start is called before the first frame update
    void Start()
    {
        Button btn = this.GetComponent<Button>();
        btn.onClick.AddListener(EndGameScene);

        PlayerMarks = HumanPlayer.GetComponent<PlayerControl>().marks;       
    }

    void Update() {
        PlayerMarks = HumanPlayer.GetComponent<PlayerControl>().marks;
    }
    void EndGameScene()
    {
        // Massacre End
        if (PlayerMarks > 5000) {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 3);
        } 
        // Normal End
        else{
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);
        }
    }
}
