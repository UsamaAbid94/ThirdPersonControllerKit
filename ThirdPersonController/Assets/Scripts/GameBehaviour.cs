using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameBehaviour : MonoBehaviour,IManager
{
    private int _itemCollected = 0;
    private int _playerHP = 10;
    public string labelText = "Collect all Items and win your freedom!";
    public int maxItems = 2;

    public bool showWinScreen = false;
    public bool showLoseScreen = false;
    private string _state;

    public Stack<string> lootStack = new Stack<string>();
    private void Start()
    {
        Initialize();
    }
    public int Items

    {
        get
        {
            return _itemCollected;
        }
        set
        {
            _itemCollected = value;
            if(_itemCollected >= maxItems)
            {
                labelText = "YOU HAVE FOUND ALL THE ITEMS!";
                showWinScreen = true;
                Time.timeScale = 0f;

            }
            else
            {
                labelText = "ITEM FOUND ONLY " + " "+(maxItems - _itemCollected) + " " + "MORE TO GO!";

            }
        }
    }

    public int HP
    {
        get
        {
            return _playerHP;
            
        }
        set
        {
            _playerHP = value;
            if (_playerHP <= 0)
            {
                labelText = "YOU WANT A NOTHER LIFE WITH THAT?";
                showLoseScreen = true;
                Time.timeScale = 0f;

            }
            else
            {
                labelText = "Ouch... that's got hurt";
            }
        }
    }

  public string State
    {
        get
        {
            return _state;
        }
        set
        {
            _state = value;
        }
    }

    private void OnGUI()
    {
        GUI.Box(new Rect(20, 20, 150, 25), "PLAYER HEALTH: " + _playerHP);
        GUI.Box(new Rect(20, 50, 150, 25), "ITEMS COLLECTED: " + _itemCollected);
        GUI.Label(new Rect(Screen.width / 2 - 100, Screen.height - 50, 300, 50), labelText);
        if (showWinScreen)
        {
            if(GUI.Button(new Rect(Screen.width/2-100,Screen.height/2-50,200,100),"YOU WON!"))
            {
                Utilites.RestartLevel();

            }
          

        }
        if (showLoseScreen)
        {
            if (GUI.Button(new Rect(Screen.width / 2 - 100, Screen.height / 2 - 50, 200, 100), "YOU LOSE!"))
            {
                Utilites.RestartLevel();
            }
        }

    }

    public void Initialize()
    {
        lootStack.Push("SWORD OF DOOM");
        lootStack.Push("HP+");
        lootStack.Push("Golden Key");
    }
    public void PrintLootReport()
    {
        Debug.LogFormat("THere are {0} random loot items waiting for you!", lootStack.Count);
    }
}
