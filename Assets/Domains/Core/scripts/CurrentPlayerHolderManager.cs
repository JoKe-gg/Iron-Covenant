using UnityEngine;

public class CurrentArenaHolderManager : MonoBehaviour
{
    public static CurrentArenaHolderManager Instance;
    public int currentPlayerID { get; private set; } = 0;
    public int currentMapID { get; private set; } = 1;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void SetCurrentPlayerID(int index)
    {
        if (currentPlayerID == index || index < 0)
        {
            return;
        }   
        currentPlayerID = index;
    }
}
