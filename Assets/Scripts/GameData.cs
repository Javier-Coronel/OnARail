using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class GameData : MonoBehaviour
{
    public static GameData Instance { get; private set; }
    public CinemachineSmoothPath mainPath;
    public CinemachineDollyCart playerCart;
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

}
