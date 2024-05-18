using UnityEngine;

public class GlobalManager : MonoBehaviour
{
    static GlobalManager globalManagerInstance;
    static public GlobalManager GlobalManagerInstance
    {
        get
        {
            if (globalManagerInstance == null)
            {
                globalManagerInstance = Object.FindObjectOfType(typeof(GlobalManager)) as GlobalManager;
                if (globalManagerInstance == null)
                {
                    GameObject globalObject = new GameObject("globalObject");
                    DontDestroyOnLoad(globalObject);
                    globalManagerInstance = globalObject.AddComponent<GlobalManager>();
                }
            }
            return globalManagerInstance;
        }
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
