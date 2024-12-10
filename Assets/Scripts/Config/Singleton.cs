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
                //�l���Q�Ƃ��ꂽ�^�C�~���O�Ŕ���
                if (_instance == null)
                {
                    //null�������ꍇ�͑S�I�u�W�F�N�g��T��
                    //���O����v����N���X���������ꍇ�͎擾����
                    _instance = FindFirstObjectByType<T>();

                    //���O����v������̂��Ȃ������ꍇ
                    if (_instance == null)
                    {
                        //�V�����V���O���g���𐶐�����
                        _instance = new GameObject($"{typeof(T).Name}").AddComponent<T>();
                    }
                }
                return _instance;
            }
        }

        //�p����ł�Awake���Ăяo�������ꍇ�́Aoverride����
        protected virtual void Awake()
        {
            //���ɓ��ꖼ�̃N���X�����݂��Ă����ꍇ�̓I�u�W�F�N�g���Ɣj��
            if (Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            DontDestroyOnLoad(gameObject);
        }
    }
}