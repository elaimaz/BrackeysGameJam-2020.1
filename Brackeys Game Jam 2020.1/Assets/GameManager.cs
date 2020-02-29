using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set; }
    public Transform StartPos;
    public GameObject BlobPrefab;
    public GameObject SkeletonPrefab;
    public GameObject DrillPrefab;
    public GameObject ShieldPrefab;
    public GameObject EyePrefab;
    public GameObject FinalPrefab;
    private Vector2 lastCheckpoint;
    private Vector2[] BlobSpawn;
    private Vector2[] SkeletonSpawn;
    private Vector2[] DrillSpawn;
    private Vector2[] ShieldSpawn;
    private Vector2[] EyeSpawn;
    private Vector2[] FinalSpawn;

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
        SetSpawns("BlobSpawner", out BlobSpawn);
        SetSpawns("SkeletonSpawner", out SkeletonSpawn); 
        SetSpawns("DrillSpawner", out DrillSpawn); 
        SetSpawns("ShieldSpawner", out ShieldSpawn); 
        SetSpawns("EyeSpawner", out EyeSpawn); 
        SetSpawns("FinalSpawner", out FinalSpawn); 
        SetPlayerPos();
    }

    private void SetSpawns(string tag, out Vector2[] pos)
    {
        GameObject[] Enemies = GameObject.FindGameObjectsWithTag(tag);
        
        pos = new Vector2[Enemies.Length];
        for (int i = 0; i < Enemies.Length; i++)
        {
            pos[i] = Enemies[i].transform.position;
        }
    }

    private void SpawnEnemy(Vector2[] pos , GameObject prefab)
    {
        for (int i = 0; i < pos.Length; i++)
        {
            Instantiate(prefab, pos[i], Quaternion.identity);
        }
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
        SpawnEnemy(BlobSpawn, BlobPrefab);
        SpawnEnemy(SkeletonSpawn, SkeletonPrefab);
        SpawnEnemy(DrillSpawn, DrillPrefab);
        SpawnEnemy(ShieldSpawn, ShieldPrefab);
        SpawnEnemy(EyeSpawn, EyePrefab);
        SpawnEnemy(FinalSpawn, FinalPrefab);
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
