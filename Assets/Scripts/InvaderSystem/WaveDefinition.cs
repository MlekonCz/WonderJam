using UnityEngine;
using UnityEngine.Serialization;

namespace DefaultNamespace
{
    [CreateAssetMenu(fileName = "WaveDefinition", menuName = "WaveDefinition", order = 0)]
    public class WaveDefinition : ScriptableObject
    {
        [SerializeField] private int _EnemiesInWave = 10;

        [SerializeField] private AInvader[] _AInvaders;

        [SerializeField] private float _TimeInBetweenOfUnits;

        [SerializeField] private int _NumberOFSubways = 2;

        [SerializeField] private float _TimeInBetweenOfSubways;
        
        
    }
}