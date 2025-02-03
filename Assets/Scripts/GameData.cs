using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class GameData : MonoBehaviour
{
    public static GameData Instance { get; private set; }
    public CinemachineSmoothPath mainPath;
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

}
