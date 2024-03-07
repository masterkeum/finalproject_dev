using System;
using UnityEngine;

public class SingletoneBase<T> : MonoBehaviour where T : Component // where = 제약절
{
    private static bool _ShuttingDown = false;
    private static object _Lock = new object();
    private static T _instance;

    protected static Guid _pid;

    public static T Instance
    {
        get
        {
            // 어플리케이션 종료떄 처리가 필요
            // 상속받은 Object 보다 싱글톤의 OnDestroy가 먼저 호출되는 경우
            if (_ShuttingDown)
            {
                Debug.LogWarning($"[Singleton] Instance {typeof(T)} already destroyed. Returning null.");
                return null; // default; ? 
            }
            // 쓰레드 락
            lock (_Lock)    //Thread Safe
            {
                if (_instance == null)
                {
                    _instance = (T)FindAnyObjectByType(typeof(T));
                    if (_instance == null)
                    {
                        _pid = Guid.NewGuid();
                        GameObject obj = new GameObject(typeof(T).Name, typeof(T)); // 이름을 세팅해준다
                        _instance = obj.GetComponent<T>();
                    }

                    DontDestroyOnLoad(_instance.gameObject);
                }

                return _instance;
            }
        }
    }

    protected void Awake()
    {
        Init(); // 상속받은 클래스에서 override 해놓고 호출
    }

    private void OnApplicationQuit()
    {
        _ShuttingDown = true;
    }

    private void OnDestroy()
    {
        if (_instance == this)
        {
            _instance = null;
            _ShuttingDown = true;
        }
    }

    protected virtual void Init()
    {
        Debug.Log($"{transform.name} is Init ({_pid})");
    }

    public virtual void Dispose()
    {
        OnDestroy();
        Destroy(gameObject);
    }
}
