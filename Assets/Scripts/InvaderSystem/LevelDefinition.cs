using UnityEngine;

namespace DefaultNamespace
{
    [CreateAssetMenu(fileName = "LevelDefinition", menuName = "LevelDefinition", order = 0)]
    public class LevelDefinition : ScriptableObject
    {
        [SerializeField] private WaveDefinition[] _WaveDefinitions;
    }
}