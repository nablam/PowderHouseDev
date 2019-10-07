public class GameEnums
{
    public enum BlockType
    {

        FL_corner,
        FrontRow,
        FR_corner,
        RighColumn,
        BR_corner,
        BackRow,
        BL_corner,
        Rightcolumn

    }

    public enum AnimalCharcter
    {

        Alligator,
        Bear,
        Cat,
        Dog,
        Elephant,
        Flamingo,
        Gorilla,
        Hippo,
        Iguana,
        Jellyfish,
        Koala,
        Lion,
        Monkey,
        Newt,
        Octopus,
        Panda,
        Quail,
        Raccoon,
        Snail,
        Turtle,
        Uakari,
        Vulture,
        Walrus,
        Xenarthra,
        Yak,
        Zebra,

    }

    //make sure to run the prefab 
    public enum StoryObjects
    {
        aaNone,
        Book,
        Laptop,
        Mirror,
        Mouse,
        Bottle,
        Bag,
        Glasses,
        Calculator,
        Charger,
        Spoon,
        Toy_Car,
        Slippers,
        Desk_fan,
        Pajamas,
        Shorts,
        Flipflops,
        Phone,
        Tophat,
        Umbrella,
        Wrench,
        Screwdriver,
        Chapstick,
        Flowers,
        Chocolate,
        Night_light,
        Jacket,
        Binoculars,
        Lunch_box,
        Remote_controle,
        Medicine,
        Map,
        Telescope,
        Markers,
        Tape,
        Cake,
        Raincoat,
        Watch,
        Head_phones,
        Shoes,
        Sweater,
        HairBrush,
        Ball,
        Tooth_brush,
        Pillow,
        Juice_box



    }
    public enum Gender
    {
        Mrs,
        Mr
    }


    public enum State
    {
        Playing,
        Finished
    }

    public enum DynAnimal
    {
        cat,
        duck,
        mole,
        mouse,
        monkey,
        penguin,
        pig,
        rabbit,
        sheep
    }

    public enum MatColors
    {
        white,
        black,
        yellow,
        orange,
        red,
        pink,
        purple,
        brown,
        green,
        blue,
        grey
    }

    public enum Shirts
    {
        none,
        shirtlong,
        shirtshort
    }
    public enum Pants
    {
        none,
        pantslong,
        pantsshort
    }
    public enum Shoes
    {
        feetcat,
        feetboots,
        feetcartoon,
        feetchicken
    }

    public enum MyItems
    {
        Avocado,
        Banana,
        Bread,
        Burger,
        Cheese_01,
        Cheese_02,
        Chicken,
        Chili,
        Chips,
        Chips_Bag,
        Coconut,
        Croissant,
        Donut,
        Drink_01,
        Drink_02,
        Drink_03,
        Eclair_Chocolate,
        Egg,
        Fries,
        Gyoza,
        HotDog,
        IceCream,
        IceCream_Cone,
        Maki,
        MeatBall,
        Mochi,
        Muffin,
        Mushroom,
        Omelette,
        Onigiri,
        Onion,
        Pepper,
        Pineapple,
        Pizza,
        Rice_Bowl,
        Salad,
        Saucisson,
        Sausage,
        Shrimp,
        Steak_Cooked,
        Steak_Uncooked,
        Sushi,
        SweetPepper,
        Tacos,
        Tempura,
        Toast,
        Tomato,
        Watermelon,
        Wine_Bottle
    }


    //chain dooraction, player input, when finished start dooraction, 
    public enum GameActionsType
    {
        CinematicAction,
        PlayerInput,
        DoorAction,
        Character_Reaction,
        Character_Action,
        ElevatorAction,



    }

    public enum GameSequenceType
    {
        GameStart,

        ReachedFloor,
        DoorsOppned,

        FloorActionsFinished,

        //DwellerReactionFinished,
        //  BunnyReleasedObject,
        //  BunnyCaughtObject,
        // DwellerReleaseObject,
        // DwellerCaughtObject,
        //BunnyReactionEnd,


        PlayerInputs,

        DoorsClosed,

        GameEnd,

    }


    //new search , how many missed , can access the global discovered floors
    public enum SolvingState
    {
        Memory,
        Logic,
        DirrectHint

    }

    //public enum DwellerAnimTrigger
    //{
    //    TrigToss,
    //    TrigCatch, // goes into a looped pose
    //    TrigCatch1,
    //    TrigCatch2,
    //    TrigTurn,
    //    TrigUnTurn,
    //    TrigWave1,
    //    TrigWave2,
    //    TrigHello,
    //    TrigDance1,
    //    TrigDance2,
    //    TrigBad,
    //    TrigShrug,
    //    TrigGood,
    //    TrigHappy,
    //    TrigCome

    //}
}
