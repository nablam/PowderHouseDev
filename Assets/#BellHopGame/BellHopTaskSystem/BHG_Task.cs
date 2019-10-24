public class BHG_Task : ITaskAction
{

    string _theActionName;
    ICharacterAnim _theCharacter;
    DeliveryItem _theContextItem;


    public BHG_Task(string actionName, ICharacterAnim theCharacter, DeliveryItem theContextItem)
    {
        TheActionName = actionName;
        TheCharacter = theCharacter;
        TheContextItem = theContextItem;

    }

    public string TheActionName { get => _theActionName; private set => _theActionName = value; }
    public ICharacterAnim TheCharacter { get => _theCharacter; set => _theCharacter = value; }
    public DeliveryItem TheContextItem { get => _theContextItem; private set => _theContextItem = value; }

    void ITaskAction.RunME()
    {
        throw new System.NotImplementedException();
    }



    //public BHG_Task() { }
}
