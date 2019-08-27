public class StoryPacket
{
    public int AnimalCharcterFloorNumber;
    public GameEnums.AnimalCharcter FloorAnimalCharacter;
    public GameEnums.StoryObjects ObjectNeededByFloorAnimal;
    public int RecipentAnimalCharcterFloorNumber;
    public GameEnums.AnimalCharcter RecipientFloorAnimalCharacter;
    public GameEnums.StoryObjects ObjectToSendToRecipient;

    public StoryPacket(int argMyFloor, GameEnums.AnimalCharcter argMyFloorAnimalCharacter, GameEnums.StoryObjects argMyObjNeeded, int argTheirFloor, GameEnums.AnimalCharcter argRecipientFloorAnimalCharacter, GameEnums.StoryObjects argRecipientObj)
    {
        AnimalCharcterFloorNumber = argMyFloor;
        FloorAnimalCharacter = argMyFloorAnimalCharacter;
        ObjectNeededByFloorAnimal = argMyObjNeeded;
        RecipentAnimalCharcterFloorNumber = argTheirFloor;
        RecipientFloorAnimalCharacter = argRecipientFloorAnimalCharacter;
        ObjectToSendToRecipient = argRecipientObj;
    }


    public override string ToString()
    {
        return "The " + FloorAnimalCharacter.ToString() + " on Floor " + RecipentAnimalCharcterFloorNumber.ToString() + " needs the " + ObjectNeededByFloorAnimal.ToString();
    }
}
