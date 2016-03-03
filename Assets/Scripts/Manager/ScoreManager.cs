using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class ScoreManager : MonoBehaviour {


    [SerializeField]
    private int _P1Score;

    [SerializeField]
    private int _P2Score;

    [SerializeField]
    GameObject _TextObject1;

    [SerializeField]
    GameObject _TextObject2;

    private Text _TextObj1;
    private Text _TextObj2;

    // Use this for initialization
    void Start()
    {

        _P1Score = PlayerPrefs.GetInt("Player1Score");
        _P2Score = PlayerPrefs.GetInt("Player2Score");

        _TextObj1 = _TextObject1.GetComponent<Text>();
        _TextObj2 = _TextObject2.GetComponent<Text>();
        _TextObj1.text = _P1Score.ToString();
        _TextObj2.text = _P2Score.ToString();

       

    }
	
	// Update is called once per frame
	void Update ()
    {
        if(Input.GetKey("escape"))
        {
            PlayerPrefs.SetInt("Player1Score", 0);
            PlayerPrefs.SetInt("Player2Score", 0);
            Application.Quit();
        }
	}
}
