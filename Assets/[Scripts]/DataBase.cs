using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataBase : Singleton<DataBase>
{
    public Vector3 savedPosition;
    public Vector3 originalPosition;
    public bool isThereSave;    


    private void Awake()
    {
        var obj = FindObjectsOfType<DataBase>();
        if (obj.Length == 1)
        {
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }   

    
}

