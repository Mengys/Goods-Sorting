using UnityEngine;

[CreateAssetMenu(fileName = "BoosterConfig", menuName = "Game/BoosterConfig")]
public class BoosterConfig : ScriptableObject
{
    public string _boosterId;
    public string _boosterName;
    public string _description;
    public int _price;
}
