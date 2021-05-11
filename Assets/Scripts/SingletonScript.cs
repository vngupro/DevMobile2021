using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingletonScript : MonoBehaviour
{
    public static SingletonScript instance { get; private set; }
    private void Awake()
    {
        if(instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);

            //Add code here
        }
    }
}
