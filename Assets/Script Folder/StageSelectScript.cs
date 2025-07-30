using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageSelectScript : MonoBehaviour
{
    [Header("ステージ選択ポイントの配列")]
    public GameObject[] stageSelectPoints = default; // ステージの選択ポイントの配列

    private int currentStageIndex = 0; // 現在の選択ステージのインデックス

    void Start()
    {
        if (stageSelectPoints != null && stageSelectPoints.Length > 0)
        {
            // 最初のステージ選択ポイントに移動
            transform.position = stageSelectPoints[currentStageIndex].transform.position;
        }
    }

    void Update()
    {
        if (stageSelectPoints != null && stageSelectPoints.Length > 0)
        {
            // 十字キーの左が押されたとき

            if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
            {
                // インデックスを減らす（範囲内に収める）
                currentStageIndex = Mathf.Max(currentStageIndex - 1, 0);
                transform.position = stageSelectPoints[currentStageIndex].transform.position;
            }

            // 右キー（D）が押されたとき
            if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
            {
                // インデックスを増やす（範囲内に収める）
                currentStageIndex = Mathf.Min(currentStageIndex + 1, stageSelectPoints.Length - 1);
                transform.position = stageSelectPoints[currentStageIndex].transform.position;
            }

            // ジャンプキーでシーン切り替え
            if (Input.GetButtonDown("Jump"))
            {
                switch (currentStageIndex)
                {
                    case 0:
                        SceneManager.LoadScene("SampleScene");
                        break;
                    case 1:
                        SceneManager.LoadScene("2 Scene");
                        break;
                    case 2:
                        SceneManager.LoadScene("3 Scene");
                        break;
                    default:
                        Debug.LogWarning("割り当てられていないステージです");
                        break;
                }
            }
        }
    }
}