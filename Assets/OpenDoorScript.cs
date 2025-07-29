using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OpenDoorScript : MonoBehaviour
{
    // プレイヤーの座標を参照するための変数
    [Header("プレイヤーの座標")] public Transform playerTransform; // プレイヤーのTransformを参照するための変数
    // ドアの座標を参照するための変数
    [Header("ドアの座標")] public Vector2 _doorPosition; // ドアの座標
    [SerializeField] public GameObject m_targetObject; // ドアのターゲットオブジェクト
    private bool _isPlayerNear = false; // プレイヤーが近くにいるかどうかのフラグ
    // Start is called before the first frame update
    void Start()
    {
        // プレイヤーのTransformが設定されていない場合、シーン内のPlayerオブジェクトを探す
        if (playerTransform == null)
        {
            GameObject playerObject = GameObject.FindWithTag("Player");
            if (playerObject != null)
            {
                playerTransform = playerObject.transform; // PlayerオブジェクトのTransformを取得
            }
            else
            {
                Debug.LogWarning("Playerオブジェクトが見つかりません。プレイヤーの座標を参照できません。");
            }
        }
        //ドアも同様に座標を設定
        if (_doorPosition == Vector2.zero)
        {
            _doorPosition = transform.position; // ドアの座標が設定されていない場合、現在の位置を使用
        }
    }

    // Update is called once per frame
    void Update()
    {
       //プレイヤーがドアの近くにいるか、同時にオブジェクトが非アクティブになっているかを判定する
       if (playerTransform != null)
        {
            float distance = Vector2.Distance(_doorPosition, playerTransform.position);
            _isPlayerNear = distance < 1.0f; // 1.0fは近くにいるとみなす距離の閾値
            //"Door"タグがついているかどうかを確認
            bool isDoorTagged = m_targetObject.activeSelf;
            // プレイヤーがドアの近く、かつ"Door"タグがついている場合、シーンを遷移する
            if (_isPlayerNear && !isDoorTagged && Input.GetKeyDown(KeyCode.UpArrow))
            {
                // シーンを遷移する
                SceneManager.LoadScene("SelectScene"); // "NextScene"は遷移先のシーン名に置き換えてください
            }
        }
    }
}
