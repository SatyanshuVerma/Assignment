using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System;

public class MenuController : MonoBehaviour
{
    public static MenuController instance;

    [SerializeField] private GameObject playMenuPanel;
    //[SerializeField] private GameObject highScoreMenuPanel;

    //[SerializeField] private GameObject highScoreUIPrefab;
    //[SerializeField] private GameObject highScoreList;

    [SerializeField] private Color activeColor;
    [SerializeField] private Color inactiveColor;
    [SerializeField] private InputField rowVal;
    [SerializeField] private InputField colVal;

    [SerializeField] private int gridWidth, gridHeight;

    public int GridWidth { get => gridWidth; set => gridWidth = value; }
    public int GridHeight { get => gridHeight; set => gridHeight = value; }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }

      
        
    }

   

    public void PlayGame(   )
    {
       

       //Constraint for the game to be only allowed to be initialized for multiples of two to pair succesfully
        if (Int32.TryParse(colVal.text, out int valueX) && Int32.TryParse(rowVal.text, out int valueY))
        {
            if ((valueX * valueY) % 2 == 0)
            {
                GridWidth = valueX;
                GridHeight = valueY;
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
            else 
            {
                colVal.text = "";
                rowVal.text = "";
            }
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
