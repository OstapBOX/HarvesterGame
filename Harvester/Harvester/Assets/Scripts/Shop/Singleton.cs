using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour {
    [SerializeField]
    private bool dontDestroy = false;
    static T n_instance;

    public static T Instance {
        get {
            if (n_instance == null) {
                n_instance = GameObject.FindObjectOfType<T>();

                if (n_instance == null) {
                    GameObject singleton = new GameObject(typeof(T).Name);
                    n_instance = singleton.AddComponent<T>();
                }
            }
            return n_instance;
        }
    }

    public virtual void Awake() {
        if (n_instance == null) {
            n_instance = this as T;

            if (dontDestroy) {
                transform.parent = null;
                DontDestroyOnLoad(this.gameObject);
            }
        }
        else {
            Destroy(gameObject);
        }
    }

}
