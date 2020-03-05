using System;
using Interfaces;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(UpdateProvider))]
public class GamePlayManager : MonoBehaviour
{
    [SerializeField] private int wavesCount = Constants.WavesCount;
    [SerializeField] private Transform parent;
    [SerializeField] private GameItem itemPrefab;
    [SerializeField] private Transform poolParent;
    [SerializeField] private Text gamePlayText;
    
    private IUpdateProvider updateProvider;
    private LevelObstacle[] obstacleLevels;
    private LevelObstacle[] obstacleMasks;
    private bool isMove = false; //todo
    
    public bool IsDead = false;
    
    public LevelObstacle[] ObstacleLevels 
    {
        get
        {
            if (obstacleLevels == null)
            {
                obstacleLevels = Resources.LoadAll<LevelObstacle>(Constants.LevelFolderName);
            }

            return obstacleLevels;
        }
    }
    
    public LevelObstacle[] ObstacleMasks 
    {
        get
        {
            if (obstacleMasks == null)
            {
                obstacleMasks = Resources.LoadAll<LevelObstacle>(Constants.MaskFolderName);
            }

            return obstacleMasks;
        }
    }

    private void Start()
    {
        Init();
    }

    private void Init()
    {
        var objectPool = poolParent.GetComponent<ObjectPool>();

        updateProvider = GetComponent<UpdateProvider>();
        objectPool.Init(updateProvider);
        
        var levelGenerator = new LevelGenerator(parent, ObstacleLevels, ObstacleMasks, itemPrefab, objectPool, updateProvider);
        
        levelGenerator.CreateLevel(wavesCount);
        
        updateProvider.OnUpdate += OnUpdate;
    }

    private void OnUpdate()
    {
        if (Input.GetKey(KeyCode.Mouse0) && IsDead)
        {
            SceneManager.LoadScene(Constants.MainMenuScene);
        }
    }

    public bool IsMove()
    {
        return isMove;
    }

    public void OnDead()
    {
        gamePlayText.text = Constants.ReturnToMenuText;
        
        isMove = false;
    }

    public void OnStart()
    {
        isMove = true;

        gamePlayText.text = string.Empty;
    }
}