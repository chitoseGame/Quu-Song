using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//******************************
// ����N���X Dictionary��Index�ԍ��𓋍ڂ����N���X
// index(int)��key(string)��value(int)���Ǘ�
// Add��Key��ǉ����Ă����Ǝ�����Dictionary�𐶐�����
// �v�f��(key�̎��)��indexMax�Ŏ��o��
//******************************
public class MyDictionary:List<MyDictionary>
{
    //int index; // �v�f�ԍ�(Add�������ԂɊi�[�����)
    public int indexMax = 0; // key�̎�ސ�
    List<string> Keys = new List<string>(); // key�̊i�[���X�g
    List<int> Values = new List<int>(); // value�̊i�[���X�g

    /// <summary>
    /// �v�f��ǉ�����(����key������ꍇ��value��1���₷�A�Ȃ��ꍇ�͒ǉ�����)
    /// </summary>
    public void Add(string key)
    {
        if (Keys.Contains(key))
        { // ���ł�Key���������ꍇ��qty�𑝂₷
            Values[GetIndexByKey(key)]++;
        }
        else
        { // �Ȃ��ꍇ��Key��V�����ǉ��Aqty���P
            Keys.Add(key);
            Values.Add(1);
            indexMax++;
        }
    }

    /// <summary>
    /// �����key(str)���܂܂�Ă��邩 �܂܂�Ă����true
    /// </summary>
    public bool Contains(string key)
    {
        if (Keys.Contains(key))
            return true;
        return false;
    }

    /// <summary>
    /// key��index�ԍ��擾�A�܂܂�Ă��Ȃ��ꍇ��-1��Ԃ�
    /// </summary>
    int GetIndexByKey(string key)
    {
        if (!Keys.Contains(key))
            return -1;
        return Keys.IndexOf(key);
    }

    /// <summary>
    /// �v�f�ԍ�(index)����key���擾 �����F���ׂ���Key�̗v�f�ԍ�
    /// </summary>
    public string GetKeyByIndex(int index)
    {
        return Keys[index];
    }

    /// <summary>
    /// �v�f�ԍ�(index)���琔���擾 �����F���ׂ���Key�̗v�f�ԍ�
    /// </summary>
    public int GetQtyByIndex(int index)
    {
        return Values[index];
    }

    /// <summary>���܂�
    /// key����S��ނɂ����Ă̒ʂ��ԍ����擾 (�P��ނ̍ŏ��̔ԍ�)
    /// key���Ȃ����-1��Ԃ�
    /// </summary>
    public int GetSerialIndexByKey(string key)
    {
        int serialIndex = 0; // �ʂ��ԍ��J�E���g�p
        for (int i = 0; i < Keys.Count; i++)
        { // k
            if (Keys[i] == key)
            { // ����key�ɓ�����Ό����I��
                break;
            }
            serialIndex += Values[i]; // �Ȃ���΂����܂ł̐���Index�ɉ�����
        }
        // �ȉ�key���܂܂�ĂȂ��ꍇ-1��Ԃ�
        int sum = 0; // �S��ނ̍��v��
        for (int i = 0; i < Values.Count; i++)
        {
            sum += Values[i];
        }
        if (serialIndex == sum)
        { // �Ō�܂Ō������ꍇ��key���Ȃ������Ƃ�������
            return -1;
        }
        else
            return serialIndex;
    }

    // �e�X�g�p(Dic�̒��g���R���\�[����ʂɕ\������)
    public void Test()
    {
        for (int i = 0; i < indexMax; i++)
        {
            Debug.Log($"{i}�Ԗڂ�key�� {Keys[i]} �ŁAvalue�� {Values[i]} �ł�");
        }
    }
}