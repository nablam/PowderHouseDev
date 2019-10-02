using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class DwellerMeshComposer : MonoBehaviour, IAnimLisener
{
    #region PublicVars
    public string AnimalName;
    public string AnimalType;
    public GameObject Body;

    public GameObject ShirtLong;
    public GameObject ShirtShort;
    public GameObject PantsLong;
    public GameObject PantsShort;
    public GameObject FeetCat;
    public GameObject FeetChiken;
    public GameObject FeetBoots;
    public GameObject FeetCartoon;
    public GameObject Gloves;




    public GameObject HeadCat;
    public GameObject HeadDuck;
    public GameObject HeadMole;
    public GameObject HeadMonkey;
    public GameObject HeadMouse;
    public GameObject HeadPenguin;
    public GameObject HeadPig;
    public GameObject HeadRabbit;
    public GameObject HeadSheep;

    public Material MatCat;
    public Material MatDuck;
    public Material MatMole;
    public Material MatMonkey;
    public Material MatMouse;
    public Material MatPenguin;
    public Material MatPig;
    public Material MatRabbit;
    public Material MatSheep;


    public Material White;
    public Material Red;
    public Material Pink;
    public Material Orange;
    public Material Yellow;
    public Material Green;
    public Material Blue;
    public Material Purple;
    public Material Black;
    public Material Grey;
    public Material Brown;



    public Transform RightHandHoldPos;
    public Transform LeftHandHoldPos;
    #endregion

    int _id;

    SkinnedMeshRenderer BodyRenderer;

    GameObject _CurHeldObject = null;
    GameObject _CurReceivedObject = null;

    GameObject ShirtPtr;
    GameObject PantsPtr;

    public int Id { get => _id; set => _id = value; }

    #region BodySetup
    Material GetMatByEnum(GameEnums.MatColors argMatcolor)
    {

        switch (argMatcolor)
        {
            case GameEnums.MatColors.black:
                return Black;
            case GameEnums.MatColors.white:
                return White;
            case GameEnums.MatColors.red:
                return Red;
            case GameEnums.MatColors.pink:
                return Pink;
            case GameEnums.MatColors.orange:
                return Orange;
            case GameEnums.MatColors.yellow:
                return Yellow;
            case GameEnums.MatColors.green:
                return Green;
            case GameEnums.MatColors.blue:
                return Blue;
            case GameEnums.MatColors.purple:
                return Purple;
            case GameEnums.MatColors.grey:
                return Grey;
            case GameEnums.MatColors.brown:
                return Brown;

            default:
                return White;

        }

    }

    void HideHeads()
    {
        HeadCat.SetActive(false);
        HeadDuck.SetActive(false);
        HeadMole.SetActive(false);
        HeadMonkey.SetActive(false);
        HeadMouse.SetActive(false);
        HeadPenguin.SetActive(false);
        HeadPig.SetActive(false);
        HeadRabbit.SetActive(false);
        HeadSheep.SetActive(false);

    }
    void ShowHead(string argHeadName)
    {
        HideHeads();
        //Debug.Log("show head " + argHeadName);
        switch (argHeadName)
        {
            case "cat":
                HeadCat.SetActive(true);
                break;
            case "duck":
                HeadDuck.SetActive(true);
                break;
            case "mole":
                HeadMole.SetActive(true);
                break;
            case "monkey":
                HeadMonkey.SetActive(true);
                break;

            case "mouse":
                HeadMouse.SetActive(true);
                break;




            case "penguin":
                HeadPenguin.SetActive(true);
                break;



            case "pig":
                HeadPig.SetActive(true);
                break;

            case "rabbit":
                HeadRabbit.SetActive(true);
                break;

            case "sheep":
                HeadSheep.SetActive(true);
                break;
            default:
                Debug.LogError("no head match");
                break;
        }
    }

    void HideFeet()
    {
        FeetCat.SetActive(false);
        FeetChiken.SetActive(false);
        FeetBoots.SetActive(false);
        FeetCartoon.SetActive(false);
    }
    void HideShirts()
    {
        ShirtPtr = null;
        ShirtLong.SetActive(false);
        ShirtShort.SetActive(false);

    }
    void HidePants()
    {
        PantsPtr = null;
        PantsLong.SetActive(false);
        PantsShort.SetActive(false);
    }
    void ShowFeet(string argFeetName)
    {
        HideFeet();
        switch (argFeetName)
        {
            case "feetcat":
                FeetCat.SetActive(true);
                break;
            case "feetchicken":
                FeetChiken.SetActive(true);
                break;
            case "feetboots":
                FeetBoots.SetActive(true);
                break;
            case "feetcartoon":
                FeetCartoon.SetActive(true);
                break;

            default:
                Debug.LogError("no feet match");
                break;
        }
    }
    void ShowShirts(string argShirtsName)
    {
        HideShirts();
        switch (argShirtsName)
        {
            case "shirtlong":
                ShirtLong.SetActive(true);
                ShirtPtr = ShirtLong;
                break;
            case "shirtshort":
                ShirtShort.SetActive(true);
                ShirtPtr = ShirtShort;
                break;
            case "none":
                ShirtPtr = null;
                ShirtShort.SetActive(false);
                ShirtLong.SetActive(false);
                break;


            default:
                Debug.LogError("no shirt match");
                break;
        }
    }
    void ShowPants(string agPantsNAme)
    {
        HidePants();
        switch (agPantsNAme)
        {
            case "pantslong":
                PantsLong.SetActive(true);
                PantsPtr = PantsLong;
                break;
            case "pantsshort":
                PantsShort.SetActive(true);
                PantsPtr = PantsShort;
                break;
            case "none":
                PantsPtr = null;
                PantsLong.SetActive(false);
                PantsShort.SetActive(false);
                break;


            default:
                Debug.LogError("no feet match");
                break;
        }
    }



    void SetBodyMat(string argAnimalName)
    {

        switch (argAnimalName)
        {
            case "cat":
                BodyRenderer.material = MatCat;
                break;
            case "duck":
                BodyRenderer.material = MatDuck;
                break;
            case "mole":
                BodyRenderer.material = MatMole;
                break;
            case "monkey":
                BodyRenderer.material = MatMonkey;
                break;

            case "mouse":
                BodyRenderer.material = MatMouse;
                break;




            case "penguin":
                BodyRenderer.material = MatPenguin;
                break;



            case "pig":
                BodyRenderer.material = MatPig;
                break;

            case "rabbit":
                BodyRenderer.material = MatRabbit;
                break;

            case "sheep":
                BodyRenderer.material = MatSheep;
                break;
            default:
                Debug.LogError("no body match");
                break;
        }

    }

    void SetFeetMat()
    {
        FeetCat.GetComponent<SkinnedMeshRenderer>().material = BodyRenderer.material;
        FeetBoots.GetComponent<SkinnedMeshRenderer>().material = BodyRenderer.material;
        FeetCartoon.GetComponent<SkinnedMeshRenderer>().material = BodyRenderer.material;
        FeetChiken.GetComponent<SkinnedMeshRenderer>().material = BodyRenderer.material;
    }

    void ComposeMesh(GameEnums.DynAnimal arganimal, GameEnums.Shirts argShirt, GameEnums.MatColors argshirtcolor, GameEnums.Pants argpants, GameEnums.MatColors argPantscolor, GameEnums.Shoes argshoes, GameEnums.MatColors argshoecolor)
    {
        SetBodyMat(arganimal.ToString());
        ShowHead(arganimal.ToString());
        SetFeetMat();
        ShowShirts(argShirt.ToString());
        ShowPants(argpants.ToString());
        ShowFeet(argshoes.ToString());

        if (ShirtPtr != null)
        {
            ShirtPtr.GetComponent<SkinnedMeshRenderer>().material = GetMatByEnum(argshirtcolor);
        }

        if (PantsPtr != null)
        {
            PantsPtr.GetComponent<SkinnedMeshRenderer>().material = GetMatByEnum(argPantscolor);
        }
    }


    GameEnums.Shirts GetRandShirt() { return (GameEnums.Shirts)Random.Range(0, Enum.GetNames(typeof(GameEnums.Shirts)).Length); }
    GameEnums.Pants GetRandPants() { return (GameEnums.Pants)Random.Range(0, Enum.GetNames(typeof(GameEnums.Pants)).Length); }
    GameEnums.MatColors GetRandColor() { return (GameEnums.MatColors)Random.Range(0, Enum.GetNames(typeof(GameEnums.MatColors)).Length); }
    GameEnums.Shoes GetRandShoes() { return (GameEnums.Shoes)Random.Range(0, Enum.GetNames(typeof(GameEnums.Shoes)).Length); }
    #endregion

    private void Awake()
    {
        BodyRenderer = Body.GetComponent<SkinnedMeshRenderer>();
    }
    // Start is called before the first frame update
    void Start()
    {
        //HideHeads();
        //HideFeet();
        //HidePants();
        //HideShirts();


        //string animal = "monkey";
        //string feet = "feetcat";// "feetboots";
        //string pants = "none";
        //string shirt = "shirtlong";
        //SetBodyMat(animal);
        //SetFeetMat();
        //ShowHead(animal);
        //ShowShirts(shirt);
        //ShowPants(pants);
        //ShowFeet(feet);
        // ComposeMesh(GameEnums.DynAnimal.sheep, GameEnums.Shirts.shirtshort, GameEnums.MatColors.yellow, GameEnums.Pants.pantsshort, GameEnums.MatColors.pink, GameEnums.Shoes.feetchicken, GameEnums.MatColors.green);

        //Make(GameEnums.DynAnimal.mole, "milke");
    }


    public void Make(GameEnums.DynAnimal arganimal, string argMyName)
    {

        HideHeads();
        HideFeet();
        HidePants();
        HideShirts();


        ComposeMesh(arganimal, GetRandShirt(), GetRandColor(), GetRandPants(), GetRandColor(), GetRandShoes(), GetRandColor());

        AnimalName = argMyName;
        AnimalType = arganimal.ToString(); ;
    }




    // Update is called once per frame
    void Update()
    {

    }

    public void InitializeHeldObject(GameObject argItemInHand)
    {
        _CurHeldObject = argItemInHand;
        _CurHeldObject.transform.position = new Vector3(RightHandHoldPos.position.x, RightHandHoldPos.position.y, RightHandHoldPos.position.z);
        _CurHeldObject.transform.parent = RightHandHoldPos.transform;
    }

    public DwellerMeshComposer GetMyHeldObjectDestination()
    {
        if (_CurHeldObject != null)
        {


            return _CurHeldObject.GetComponent<DeliveryItem>().GetDestFloorNumber();
        }
        else
            return null;
    }


    #region InterfaceForAnimatorListen
    public void TossPeack()
    {
        throw new NotImplementedException();
    }

    public void CatchPeack()
    {
        throw new NotImplementedException();
    }
    #endregion
}
