using UnityEngine;

[CreateAssetMenu(fileName = "NewStatsBulletConfig", menuName = "Bullets/StatsConfig", order = 0)]
public class StatsBulletConfig : ScriptableObject
{
    public LayerMask CollideLayer;
    public float Speed = 100f;
    public float LifeTime = 5f;
}