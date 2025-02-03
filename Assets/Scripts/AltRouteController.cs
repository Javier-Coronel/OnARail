using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;
using static Cinemachine.CinemachinePathBase;

public class AltRouteController : MonoBehaviour
{
    public int MainNodeStart;
    public int MainNodeFinish;
    public CinemachinePathBase ownPath;

    private void Start() {
        GetComponent<MeshRenderer>().enabled = false;
    }

    private void FixedUpdate()
    {
        Debug.Log(GameData.Instance.mainPath.ToNativePathUnits(MainNodeFinish, PositionUnits.Distance));
        if (ownPath.Equals(GameData.Instance.playerCart.m_Path) && GameData.Instance.playerCart.m_Position==ownPath.MaxUnit(PositionUnits.Distance))
        {
            GameData.Instance.playerCart.m_Path = GameData.Instance.mainPath;
            GameData.Instance.playerCart.m_Position = GameData.Instance.mainPath.ToNativePathUnits(MainNodeFinish, PositionUnits.Distance);
        }
    }

    public void goToAltRoute()
    {
        //Debug.Log(GameData.Instance.getPosition(CinemachinePathBase.PositionUnits.PathUnits));
        GameData.Instance.playerCart.m_Path = ownPath;
        GameData.Instance.playerCart.m_Position=0;
    }
}
