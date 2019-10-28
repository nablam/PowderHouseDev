using System.Collections.Generic;
using UnityEngine;

public class DeliverySessionManager : MonoBehaviour
{


    List<DeliverySession> gameSessions;


    DeliverySession curSession;
    int GameSessionID = 0;
    public void CreateSessionWhenBellHopObtainsANewItem(DeliveryItem argItem)
    {
        if (gameSessions == null)
        {
            gameSessions = new List<DeliverySession>();
        }
        print("making new Session itenm dest is floor " + argItem.GetDestFloorDweller().MyFinalResidenceFloorNumber);
        curSession = new DeliverySession(argItem, GameSessionID);
        gameSessions.Add(curSession);
        GameSessionID++;
    }

    public int GetFloorsVisitedINThisSession()
    {

        if (curSession == null) return 0;
        if (curSession.FloorsVisited == null) return 0;
        return curSession.FloorsVisited.Count;
    }

    // Start is called before the first frame update
    void Awake()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
