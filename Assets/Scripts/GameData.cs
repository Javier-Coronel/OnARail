using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using static Cinemachine.CinemachinePathBase;
using UnityEngine.UIElements;

public class GameData : MonoBehaviour
{
    public static GameData Instance { get; private set; }
    public CinemachineSmoothPath mainPath;
    public CinemachineDollyCart playerCart;
    public List<GameObject> objectsToDelete;
    public SpawnPosition actualSpawnPosition;
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
    public float getPosition(CinemachinePathBase.PositionUnits typeOfUnit)
    {
        CinemachinePathBase.PositionUnits actualUnit = playerCart.m_PositionUnits;
        playerCart.m_PositionUnits = typeOfUnit;
        float position = playerCart.m_Position;
        playerCart.m_PositionUnits = actualUnit;
        return position;
    }
    public void respawn()
    {
        playerCart.m_Position = actualSpawnPosition ? actualSpawnPosition.position:0;
        playerCart.GetComponentInChildren<MovementAndControl>().transform.position=Vector3.zero;
        deleteTemporalObjects();
    }
    public void deleteTemporalObjects()
    {
        
        while (objectsToDelete.Count>0)
        {
            Destroy(objectsToDelete[0]);
            objectsToDelete.RemoveAt(0);
        }
    }
}
