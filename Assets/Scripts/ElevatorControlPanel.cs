using UnityEngine;
/// <summary>
///  the control panel object should be a child of the elevator so that it remains alive accross scenes.
///  
/// todo: 
/// not yet sure if this should be a canvas with ui buttons , or just a prefab with interactible button objects
/// lets start with a canvas for now.
/// </summary>
public class ElevatorControlPanel : MonoBehaviour
{
    /// <summary>
    /// Button click event should have an int as parameter
    /// </summary>
    /// <param name="argButtonNumber"></param>
    public void OnButtonClisked(int argButtonNumber)
    {
        ElevatorSceneNavigator.Instance.GoToScene((GameEnums.GameScenes)argButtonNumber);
    }



}
