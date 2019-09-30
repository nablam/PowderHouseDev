using UnityEngine;

public class CharacterAnimCtrl : MonoBehaviour, IAnimLisener
{

    public ICharacterAnim _myGuy;

    void Awake()
    {
        _myGuy = GetComponent<ICharacterAnim>();
        if (_myGuy == null)
        {
            _myGuy = GetComponentInParent<ICharacterAnim>();
        }
    }

    public void CatchPeack()
    {
        _myGuy.AnimCatchPeack();
    }

    public void TossPeack()
    {
        _myGuy.AnimTossPeack();
    }
}
