using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Goal : MonoBehaviour
{
    [Header("ドアの座標")] public Vector2 _doorPosition; // ドアの座標
    //プレイヤーの座標を参照するための変数
    [Header("プレイヤーの座標")] public Transform playerTransform; // プレイヤーのTransformを参照するための変数
    private bool _isPlayerNear = false; // プレイヤーが近くにいるかどうかのフラグ
    [SerializeField] public GameObject m_targetObject;
    //ドアのテクスチャを切り替えるための変数
    [Header("ドアのテクスチャ")] public Texture2D doorTexture; // ドアのテクスチャ
    // Start is called before the first frame update
    void Start()
    {
        //ドアの座標を参照し、_doorPositionに設定
        if (_doorPosition == Vector2.zero)
        {
            _doorPosition = transform.position; // ドアの座標が設定されていない場合、現在の位置を使用
        }
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
    }

    // Update is called once per frame
    void Update()
    {
        //ドアの座標とプレイヤーの座標を比較して、プレイヤーが近くにいるかどうかを判定
        if (_isPlayerNear)
        {
            // プレイヤーがドアの近くにいる場合、ドアの座標とプレイヤーの座標を比較
            float distance = Vector2.Distance(_doorPosition, playerTransform.position);
            _isPlayerNear = distance < 0.5f; // 1.0fは近くにいるとみなす距離の閾値
        }
        else
        {
            // プレイヤーがドアの近くにいない場合、ドアの座標とプレイヤーの座標を比較
            float distance = Vector2.Distance(_doorPosition, playerTransform.position);
            _isPlayerNear = distance < 0.5f; // 1.0fは近くにいるとみなす距離の閾値
        }
        //プレイヤーがドアの近く、かつ上ボタンが入力された場合、シーンを切り替える
        if (_isPlayerNear && Input.GetButtonDown("Jump"))
        {
            // シーンを切り替える
            SceneManager.LoadScene("SelectScene"); // "NextScene"は次のシーンの名前に置き換えてください
            // ドアのテクスチャを切り替える
            if (doorTexture != null)
            {
                Renderer renderer = GetComponent<Renderer>();
                if (renderer != null)
                {
                    renderer.material.mainTexture = doorTexture; // ドアのテクスチャを設定
                }
            }
        }
    }
}
