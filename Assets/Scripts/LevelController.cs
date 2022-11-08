using Core;
using DefaultNamespace;
using UnityEngine;

public class LevelController : MonoBehaviour
{

        
        
        
    private LevelDefinition _levelDefinition;
    private int _currentLevel;
        
        
        
    private void Start()
    {
        _currentLevel = GlobalManager.Instance.GameManager.CurrentLevel;
        _levelDefinition =   GlobalManager.Instance.GameManager.LevelDefinitions[_currentLevel];
    }
}