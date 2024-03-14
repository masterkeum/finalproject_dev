using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MainScene : MonoBehaviour
{

    private void Awake()
    {
        _=DataManager.Instance;
        _=UIManager.Instance;
        _=GameManager.Instance;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
