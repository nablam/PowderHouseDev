using UnityEngine;

public class GameSettings : MonoBehaviour
{
    /// <summary>
    /// MAKE sure this class is instantiated first ! plz
    /// 
    /// </summary>
    public static GameSettings Instance = null;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
            Destroy(this.gameObject);

        Master_Number_of_Floors = 6;
        if (Master_Number_of_Floors > _Master_max_Available_Dwellers_sofar)
            Master_Number_of_Floors = _Master_max_Available_Dwellers_sofar;

        ElevatorSpeed = 0.15f;


        Debug.Log(nameof(_Master_max_Available_Dwellers_sofar));
    }

    int _master_Number_of_Floors;
    public int Master_Number_of_Floors { get => _master_Number_of_Floors; set => _master_Number_of_Floors = value; }


    //we only have 9 animals right now , we can create 18 animals male/female. lets keep the max at a power of 2 for now 
    //UPDATE: we only have 12 butons, so lets make this max 12 for now
    const int _Master_max_Available_Dwellers_sofar = 12;

    float _elevatorSpeed;
    public float ElevatorSpeed { get => _elevatorSpeed; set => _elevatorSpeed = value; }

}
