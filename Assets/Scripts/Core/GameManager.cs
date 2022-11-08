using System;
using DefaultNamespace;
using UnityEngine;

namespace Core
{
    public class GameManager : MonoBehaviour
    {
    
        [SerializeField] public LevelDefinition[] LevelDefinitions;


        public int CurrentLevel = 0;

        public Action<bool> OnLevelFinished;

    }
}
