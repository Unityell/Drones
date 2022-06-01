using UnityEngine;

public class GameProgress : MonoBehaviour
{
    public static int GP;
    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }
}
