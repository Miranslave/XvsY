using System.Collections.Generic;
using Script;
using UnityEngine;

public class PausingManager : MonoBehaviour
{
    [SerializeField] private List<MonoBehaviour> pausableGameObjects = new List<MonoBehaviour>();
    
    
    private void OnEnable()
    {
        GameEvents.OnPauseRequested += PauseAll;
        GameEvents.OnResumeRequested += UnPauseAll;
    }

    private void OnDisable()
    {
        GameEvents.OnPauseRequested -= PauseAll;
        GameEvents.OnResumeRequested -= UnPauseAll;
    }
    
    
    void GetAllPausable()
    {
        pausableGameObjects.Clear();
        // On récupère tous les MonoBehaviour dans la scène
        MonoBehaviour[] allBehaviours = FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.None);
        foreach (var behaviour in allBehaviours)
        {
            if (behaviour is IPausable pausable)
            {
                pausableGameObjects.Add(behaviour);
            }
        }
    }

    public void PauseAll()
    {
        GetAllPausable();

        foreach (var obj in pausableGameObjects)
        {
            if (obj is IPausable pausable)
                pausable.OnPause();
        }
    }
    
    public void UnPauseAll(){
        GetAllPausable();
        foreach (var obj in pausableGameObjects)
        {
            if (obj is IPausable pausable)
                pausable.OnResume();
        }
    }
}
