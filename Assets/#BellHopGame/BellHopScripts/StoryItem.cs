using System.Collections;
using UnityEngine;

public class StoryItem : MonoBehaviour
{
    public GameEnums.StoryObjects MyType;




    // Movement speed in units/sec.
    float speed = 4.0F;

    // Time when the movement started.
    private float startTime;
    // Total distance between the markers.
    float journeyLength;
    float fracJourney;




    public void MoveTO(Transform startMarker, Transform endMarker, bool ToBEllhop)
    {
        if (transform.parent != null)
        {
            transform.parent = null;
        }

        journeyLength = 10000000;
        startTime = Time.time;
        journeyLength = Vector3.Distance(startMarker.position, endMarker.position);
        if (ToBEllhop)
            StartCoroutine(ToBellHOp(startMarker, endMarker, 1.14f));
        else
        {
            StartCoroutine(ToDweller(startMarker, endMarker, 2));
        }
    }



    private IEnumerator ToBellHOp(Transform startMarker, Transform endMarker, float time)
    {
        if (endMarker == null)
            yield return null;

        float elapsedTime = 0;

        //  GameManager.Instance.TheBellHop.AnimateCatch();
        while (elapsedTime < time)
        {

            float distCovered = (Time.time - startTime) * speed;
            fracJourney = distCovered / journeyLength;

            transform.position = Vector3.Lerp(startMarker.position, endMarker.position, fracJourney);
            //  Debug.Log(fracJourney);

            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        Debug.Log("ReachedDestination");
        //  GameManager.Instance.TheBellHop.Set_ItemReached(this.gameObject);

    }

    private IEnumerator ToDweller(Transform startMarker, Transform endMarker, float time)
    {
        if (endMarker == null)
            yield return null;

        float elapsedTime = 0;
        GameManager.Instance.GetCurDweller().AnimateCatch();

        while (elapsedTime < time)
        {

            float distCovered = (Time.time - startTime) * speed;
            fracJourney = distCovered / journeyLength;

            transform.position = Vector3.Lerp(startMarker.position, endMarker.position, fracJourney);
            //Debug.Log(fracJourney);

            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        //this.transform.GetChild(0).gameObject.SetActive(false);
        GameManager.Instance.SetCurDwellerReceivedObject(this.gameObject);
        Debug.Log("ReachedDestination");
        GameManager.Instance.CurDwellerTossToBEllHop();
        //   GameManager.Instance.TheBellHop.Set_ItemReached(this.gameObject);
    }
}
