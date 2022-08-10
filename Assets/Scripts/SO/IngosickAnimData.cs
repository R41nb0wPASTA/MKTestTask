using UnityEngine;

[CreateAssetMenu(fileName = "IngosickAnimData", menuName = "IngosickAnimData", order = 1100)]
public class IngosickAnimData : ScriptableObject
{
    public float fadeInSpeed = 0.1f;
    public float fadeOutSpeed = 0.05f;
    
    public float fadeInInAnimPart = 20f;

    public float exportScale = 10f;
}
