using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set; }
    public Transform StartPos;
    private Vector2 lastCheckpoint;

    private void Awake()
    {
        if (instance != null)
            Destroy(gameObject);
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        lastCheckpoint = StartPos.position;
        SetPlayerPos();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            GameManager.instance.Restart();
        }
    }

    private void SetPlayerPos()
    {
        PlayerManager.instance.transform.position = lastCheckpoint;

    }

    public void SetCheckPoint(Vector2 position)
    {
        if(lastCheckpoint != position)
        {
            lastCheckpoint = position;
        }
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        SetPlayerPos();
    }
}
