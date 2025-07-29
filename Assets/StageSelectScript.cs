using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageSelectScript : MonoBehaviour
{
    //ステージを選ぶ上で選択するための配列
    [Header("ステージを選ぶための配列")] public GameObject[] stageSelectPoints = default;
    // Start is called before the first frame update
    void Start()
    {
        // ステージ選択ポイントが設定されているか確認
        if (stageSelectPoints != null && stageSelectPoints.Length > 0)
        {
            // 最初のステージ選択ポイントに移動
            transform.position = stageSelectPoints[0].transform.position;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // ステージ選択ポイントが設定されているか確認
        if (stageSelectPoints != null && stageSelectPoints.Length > 0)
        {
            //// 左右のキー入力に応じてステージを選択
            //if (Input.GetKeyDown(KeyCode.LeftArrow))
            //{
            //    // 左のステージ選択ポイントに移動
            //    int currentIndex = System.Array.IndexOf(stageSelectPoints, transform.position);
            //    if (currentIndex > 0)
            //    {
            //        transform.position = stageSelectPoints[currentIndex - 1].transform.position;
            //        // もし左端にいる場合は何もしない
            //    }
            //}
            //else if (Input.GetKeyDown(KeyCode.RightArrow))
            //{
            //    // 右のステージ選択ポイントに移動
            //    int currentIndex = System.Array.IndexOf(stageSelectPoints, transform.position);
            //    if (currentIndex < stageSelectPoints.Length - 1)
            //    {
            //        transform.position = stageSelectPoints[currentIndex + 1].transform.position;
            //        // もし右端にいる場合は何もしない
            //    }
            //}
            // 左右のキー入力に応じてステージを選択
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                transform.position = stageSelectPoints[0].transform.position;
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2) && stageSelectPoints.Length > 1)
            {
                transform.position = stageSelectPoints[1].transform.position;
            }
            else if (Input.GetKeyDown(KeyCode.Alpha3) && stageSelectPoints.Length > 2)
            {
                transform.position = stageSelectPoints[2].transform.position;
            }
            //0,1,2それぞれにシーンを割り当てる
            //スペースキーが押されたときにステージを選択
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (transform.position == stageSelectPoints[0].transform.position)
                {
                    SceneManager.LoadScene("SampleScene");
                }
                else if (transform.position == stageSelectPoints[1].transform.position)
                {
                    SceneManager.LoadScene("Stage2");
                }
                else if (transform.position == stageSelectPoints[2].transform.position)
                {
                    SceneManager.LoadScene("Stage3");
                }
            }
        }
    }
}
