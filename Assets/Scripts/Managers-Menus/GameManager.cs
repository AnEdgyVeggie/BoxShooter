using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    UIManager _uiManager;
    Player _player;
    Rotate _rotate;

    // Start is called before the first frame update
    void Start()
    {
        _uiManager = GameObject.Find("UIManager").GetComponent<UIManager>();
        _player = GameObject.Find("Player").GetComponent<Player>();
        _rotate = GameObject.Find("Player").GetComponent<Rotate>();

    }


    // Update is called once per frame
    void Update()
    {
        MouseVisibility();

        if (Input.GetKeyDown(KeyCode.P)) {
            PauseGame();
        }
    }

    void MouseVisibility()
    {
        if (Input.GetKeyDown(KeyCode.P))
        { Cursor.visible = true; }
        else if (Input.GetMouseButtonDown(0) && _uiManager.GetInMenu() == false)
        { Cursor.visible = false; }
    }
    void PauseGame()
    {
        if (Time.timeScale > 0)
        {
            _uiManager.PausedGame(true);
            Time.timeScale = 0;
            _rotate.SetPaused(true);
            _player.SetPaused(true);
        }
        else if (Time.timeScale == 0)
        {
            Time.timeScale = 1;
            _uiManager.PausedGame(false);
            _rotate.SetPaused(false);
            _player.SetPaused(false);
        }
    }
}
