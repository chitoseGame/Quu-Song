using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//******************************
// 自作クラス DictionaryにIndex番号を搭載したクラス
// index(int)でkey(string)とvalue(int)を管理
// AddでKeyを追加していくと自動でDictionaryを生成する
// 要素数(keyの種類)はindexMaxで取り出す
//******************************
public class MyDictionary:List<MyDictionary>
{
    //int index; // 要素番号(Addした順番に格納される)
    public int indexMax = 0; // keyの種類数
    List<string> Keys = new List<string>(); // keyの格納リスト
    List<int> Values = new List<int>(); // valueの格納リスト

    /// <summary>
    /// 要素を追加する(同じkeyがある場合はvalueを1増やす、ない場合は追加する)
    /// </summary>
    public void Add(string key)
    {
        if (Keys.Contains(key))
        { // すでにKeyがあった場合はqtyを増やす
            Values[GetIndexByKey(key)]++;
        }
        else
        { // ない場合はKeyを新しく追加、qtyを１
            Keys.Add(key);
            Values.Add(1);
            indexMax++;
        }
    }

    /// <summary>
    /// 特定のkey(str)が含まれているか 含まれていればtrue
    /// </summary>
    public bool Contains(string key)
    {
        if (Keys.Contains(key))
            return true;
        return false;
    }

    /// <summary>
    /// keyのindex番号取得、含まれていない場合は-1を返す
    /// </summary>
    int GetIndexByKey(string key)
    {
        if (!Keys.Contains(key))
            return -1;
        return Keys.IndexOf(key);
    }

    /// <summary>
    /// 要素番号(index)からkeyを取得 引数：調べたいKeyの要素番号
    /// </summary>
    public string GetKeyByIndex(int index)
    {
        return Keys[index];
    }

    /// <summary>
    /// 要素番号(index)から数を取得 引数：調べたいKeyの要素番号
    /// </summary>
    public int GetQtyByIndex(int index)
    {
        return Values[index];
    }

    /// <summary>おまけ
    /// keyから全種類においての通し番号を取得 (１種類の最初の番号)
    /// keyがなければ-1を返す
    /// </summary>
    public int GetSerialIndexByKey(string key)
    {
        int serialIndex = 0; // 通し番号カウント用
        for (int i = 0; i < Keys.Count; i++)
        { // k
            if (Keys[i] == key)
            { // 同じkeyに当たれば検索終了
                break;
            }
            serialIndex += Values[i]; // なければそこまでの数をIndexに加える
        }
        // 以下keyが含まれてない場合-1を返す
        int sum = 0; // 全種類の合計数
        for (int i = 0; i < Values.Count; i++)
        {
            sum += Values[i];
        }
        if (serialIndex == sum)
        { // 最後まで言った場合はkeyがなかったということ
            return -1;
        }
        else
            return serialIndex;
    }

    // テスト用(Dicの中身をコンソール画面に表示する)
    public void Test()
    {
        for (int i = 0; i < indexMax; i++)
        {
            Debug.Log($"{i}番目のkeyは {Keys[i]} で、valueは {Values[i]} です");
        }
    }
}