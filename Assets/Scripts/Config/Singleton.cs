using UnityEngine;

namespace Config
{
    public abstract class Singleton<T> : MonoBehaviour where T : Singleton<T>
    {
        private static T _instance;


        public static T Instance
        {
            get
            {
                //値が参照されたタイミングで判定
                if (_instance == null)
                {
                    //nullだった場合は全オブジェクトを探索
                    //名前が一致するクラスがあった場合は取得する
                    _instance = FindFirstObjectByType<T>();

                    //名前が一致するものがなかった場合
                    if (_instance == null)
                    {
                        //新しくシングルトンを生成する
                        _instance = new GameObject($"{typeof(T).Name}").AddComponent<T>();
                    }
                }
                return _instance;
            }
        }

        //継承先でもAwakeを呼び出したい場合は、overrideする
        protected virtual void Awake()
        {
            //既に同一名のクラスが存在していた場合はオブジェクトごと破棄
            if (Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            DontDestroyOnLoad(gameObject);
        }
    }
}