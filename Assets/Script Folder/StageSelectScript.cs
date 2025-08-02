using UnityEngine;
using UnityEngine.SceneManagement;

public class StageSelectScript : MonoBehaviour
{
    [Header("ステージ選択ポイントの配列")]
    public GameObject[] stageSelectPoints = default;// ステージ選択ポイントの配列
    private float inputCooldown = 0.3f; //次の操作が実行できるようになるまでのクールタイム
    private float inputTimer = 0f;//それを入力するタイマー
    private int currentStageIndex = 0;//現在のステージを示す為のインデックス

    void Start()
    {
        //ステージ選択ポイントの座標を配列に合わせつつ取得
        if (stageSelectPoints != null && stageSelectPoints.Length > 0)
        {
            transform.position = stageSelectPoints[currentStageIndex].transform.position;
        }
    }

    void Update()
    {
        //ステージ選択ポイントが設定されていない場合は何もしない
        if (stageSelectPoints == null || stageSelectPoints.Length == 0) { return; }

        // タイマー更新
        if (inputTimer > 0)
        {
            inputTimer -= Time.deltaTime;
        }

        float horizontalInput = Input.GetAxis("Horizontal");// 水平方向の入力を取得
        float threshold = 0.1f;// 入力のしきい値

        // 入力がしきい値を超えた場合にステージを切り替える
        if (inputTimer <= 0)
        {
            // 水平方向の入力がしきい値を超えた場合
            if (horizontalInput < -threshold)
            {
                // 左側に移動
                currentStageIndex = Mathf.Max(currentStageIndex - 1, 0);
                transform.position = stageSelectPoints[currentStageIndex].transform.position;
                inputTimer = inputCooldown;
            }
            //こちらも同様に
            else if (horizontalInput > threshold)
            {
                // 右側に移動
                currentStageIndex = Mathf.Min(currentStageIndex + 1, stageSelectPoints.Length - 1);
                transform.position = stageSelectPoints[currentStageIndex].transform.position;
                inputTimer = inputCooldown;
            }

            //BackSpaceキー、または、ゲームパッドのBボタンが押された場合
            if (Input.GetKeyDown(KeyCode.Backspace) || Input.GetButtonDown("Cancel"))
            {
                // タイトルシーンに戻る
                SceneManager.LoadScene("TitleScene");
            }

            // ステージが選択された状態でジャンプボタンが押された場合
            // ここではジャンプボタンを押すことでステージを選択する事ができる
            if (Input.GetButtonDown("Jump"))
            {
                // 現在のステージインデックスに応じてシーンをロード
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