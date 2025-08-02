using UnityEngine;
using UnityEngine.SceneManagement;

public class StageSelectScript : MonoBehaviour
{
    [Header("ステージ選択ポイントの配列")]
    public GameObject[] stageSelectPoints = default; // ステージの選択ポイントの配列
    private float inputCooldown = 0.3f; // 入力のクールダウン時間（秒）
    private float inputTimer = 0f; // 入力処理のタイマー
    private int currentStageIndex = 0; // 現在選択中のステージのインデックス

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
        if (stageSelectPoints == null || stageSelectPoints.Length == 0) { return; }

        // 入力クールダウン時間のカウントダウン
        if (inputTimer > 0)
        {
            inputTimer -= Time.deltaTime;
        }

        // 水平軸の入力取得
        float horizontalInput = Input.GetAxis("Horizontal");
        float threshold = 0.1f; // 入力判定の閾値

        // 入力クールダウン済みかつ閾値を超えた入力があった場合
        if (inputTimer <= 0)
        {
            // 左に入力された場合
            if (horizontalInput < -threshold)
            {
                // ステージインデックスを減らす（左に移動）
                currentStageIndex = Mathf.Max(currentStageIndex - 1, 0);
                transform.position = stageSelectPoints[currentStageIndex].transform.position;
                inputTimer = inputCooldown;
            }
            // 右に入力された場合
            else if (horizontalInput > threshold)
            {
                // ステージインデックスを増やす（右に移動）
                currentStageIndex = Mathf.Min(currentStageIndex + 1, stageSelectPoints.Length - 1);
                transform.position = stageSelectPoints[currentStageIndex].transform.position;
                inputTimer = inputCooldown;
            }

            // 戻るキー（Backspace or Cancelボタン）でタイトルシーンへ
            if (Input.GetKeyDown(KeyCode.Backspace) || Input.GetButtonDown("Cancel"))
            {
                SceneManager.LoadScene("TitleScene");
            }

            // ジャンプキーでシーンを切り替え
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