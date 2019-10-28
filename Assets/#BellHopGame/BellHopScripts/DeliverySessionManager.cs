using System.Collections.Generic;
using UnityEngine;

public class DeliverySessionManager : MonoBehaviour
{


    List<DeliverySession> gameSessions = new List<DeliverySession>();


    DeliverySession curSession;
    int GameSessionID = 0;
    public void CreateSessionWhenBellHopObtainsANewItem(DeliveryItem argItem)
    {

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
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
