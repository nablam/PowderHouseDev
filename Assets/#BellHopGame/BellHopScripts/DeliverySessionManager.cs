using System.Collections.Generic;
using UnityEngine;

public class DeliverySessionManager : MonoBehaviour
{


    List<DeliverySession> gameSessions = null;


    DeliverySession curSession;
    int GameSessionID = 0;
    public void CreateSessionWhenBellHopObtainsANewItem(DeliveryItem argItem)
    {

        print("making new Session itenm dest is floor " + argItem.GetDestFloorDweller().MyFinalResidenceFloorNumber);
        DeliverySession temp = new DeliverySession(argItem, GameSessionID);
        curSession = temp;
        gameSessions.Add(temp);
        GameSessionID++;
    }

    public int GetFloorsVisitedINThisSession()
    {

        if (curSession == null) return 0;
        if (curSession.FloorsVisited == null) return 0;
        return curSession.FloorsVisited.Count;
    }

    public void AddFloorVisitFloorsVisitedINThisSession(int curfloornum)
    {
        if (curSession != null)

            curSession.AddFloorVisited(curfloornum);
    }


    public void Init()
    {
        if (gameSessions == null)
        {
            gameSessions = new List<DeliverySession>();
        }

    }
}
