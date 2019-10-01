using UnityEngine;

public class DwellerMeshComposer : MonoBehaviour
{
    public GameObject Body;
    SkinnedMeshRenderer BodyRenderer;
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

    GameObject ShirtPtr;
    GameObject PantsPtr;
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
    public void ShowHead(string argHeadName)
    {
        HideHeads();
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
    public void ShowFeet(string argFeetName)
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
    public void ShowShirts(string argShirtsName)
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
    public void ShowPants(string agPantsNAme)
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
    // Start is called before the first frame update
    void Start()
    {
        HideHeads();
        HideFeet();
        HidePants();
        HideShirts();
        BodyRenderer = Body.GetComponent<SkinnedMeshRenderer>();

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
        ComposeMesh(GameEnums.DynAnimal.sheep, GameEnums.Shirts.shirtshort, GameEnums.MatColors.yellow, GameEnums.Pants.pantsshort, GameEnums.MatColors.pink, GameEnums.Shoes.feetchicken, GameEnums.MatColors.green);
    }

    public void ComposeMesh(GameEnums.DynAnimal arganimal, GameEnums.Shirts argShirt, GameEnums.MatColors argshirtcolor, GameEnums.Pants argpants, GameEnums.MatColors argPantscolor, GameEnums.Shoes argshoes, GameEnums.MatColors argshoecolor)
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

    // Update is called once per frame
    void Update()
    {

    }
}
