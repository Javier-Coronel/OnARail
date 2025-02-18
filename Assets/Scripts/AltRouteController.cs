using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.Events;
using static Cinemachine.CinemachinePathBase;

public class AltRouteController : MonoBehaviour
{
    public int MainNodeStart;
    public int MainNodeFinish;
    public CinemachinePathBase ownPath;
    bool onWayToPath = false;
    public UnityEvent onPathEnter;
    public UnityEvent onPathExit;

    private void Start() {
        GetComponent<MeshRenderer>().enabled = false;
    }

    private void FixedUpdate()
    {
        if(onWayToPath){
            if(Mathf.Abs(GameData.Instance.playerCart.m_Position-GameData.Instance.mainPath.FromPathNativeUnits(MainNodeStart, PositionUnits.Distance))<0.01){
                goToAltRoute();
            }
            else if(GameData.Instance.playerCart.m_Position<GameData.Instance.mainPath.FromPathNativeUnits(MainNodeStart, PositionUnits.Distance))
            {
                GameData.Instance.playerCart.m_Speed = 2;
            }
            else
            {
                GameData.Instance.playerCart.m_Speed = -2;
            }
        }
        if (ownPath.Equals(GameData.Instance.playerCart.m_Path)){
            Debug.Log(GameData.Instance.mainPath.FromPathNativeUnits(MainNodeFinish, PositionUnits.Distance));
            if (Mathf.Abs(GameData.Instance.playerCart.m_Position - ownPath.MaxUnit(GameData.Instance.playerCart.m_PositionUnits))<0.01f)
            {
                GameData.Instance.playerCart.m_Path = GameData.Instance.mainPath;
                GameData.Instance.playerCart.m_Position = GameData.Instance.mainPath.FromPathNativeUnits(MainNodeFinish, PositionUnits.Distance);
                onPathExit.Invoke();
            }
        }
    }

    public void goToAltRoute()
    {
        //Debug.Log(GameData.Instance.getPosition(CinemachinePathBase.PositionUnits.PathUnits));
        GameData.Instance.playerCart.m_Path = ownPath;
        GameData.Instance.playerCart.m_Position=0;
        GameData.Instance.playerCart.m_Speed = 1;
        onWayToPath = false;
        onPathEnter.Invoke();

    }
    public void setAltRoute(){
        onWayToPath = true;
    }
}
