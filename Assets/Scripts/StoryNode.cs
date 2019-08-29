public class StoryNode
{
    StoryNode Next_giveto;
    StoryNode Prex_OwedToMe;
    GameEnums.AnimalCharcter TheAnimal;
    GameEnums.StoryObjects ObjectInHand;

    public StoryNode()
    {
        Next_giveto = null;
        Prex_OwedToMe = null;
        TheAnimal = GameEnums.AnimalCharcter.Alligator;
        ObjectInHand = GameEnums.StoryObjects.Ball;
    }


    public StoryNode(GameEnums.AnimalCharcter theAnimal, GameEnums.StoryObjects objectInHand)
    {
        Next_giveto = null;
        Prex_OwedToMe = null;
        TheAnimal = theAnimal;
        ObjectInHand = objectInHand;
    }

    public StoryNode Next_giveto1 { get => Next_giveto; set => Next_giveto = value; }
    public StoryNode Prex_OwedToMe1 { get => Prex_OwedToMe; set => Prex_OwedToMe = value; }
    public GameEnums.AnimalCharcter TheAnimal1 { get => TheAnimal; set => TheAnimal = value; }
    public GameEnums.StoryObjects ObjectInHand1 { get => ObjectInHand; set => ObjectInHand = value; }
}
