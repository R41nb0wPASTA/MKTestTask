using UnityEngine;

[CreateAssetMenu(fileName = "UserData", menuName = "UserData", order = 1100)]
public class UserData : ScriptableObject
{
    [Header("Score")]
    public int userScore;

    [Header("Dino Block")]
    public int completedLvlsDino;
    public int totalLvlsDino = 25;
    
    [Header("Numbers Block")]
    public int completedLvlsNum;
    public int totalLvlsNum = 16;
    
    [Header("Solar System Block")]
    public int completedLvlsSolarSys;
    public int totalLvlsSolarSys = 7;
    
    [Header("Alphabet Block")]
    public int completedLvlsAlphabet;
    public int totalLvlsAlphabet = 25;
    
    [Header("Alice Block")]
    public int completedLvlsAlice;
    public int totalLvlsAlice = 19;
}
