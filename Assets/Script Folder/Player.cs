using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [Header("プレイヤースピード")]
    public float _PlayerSpeed;
    [Header("通常状態のスプライト")]
    public Sprite normalSprite;

    [Header("アイドル時のスプライト配列")]
    public Sprite[] idleSprites;
    [Header("ジャンプ時のスプライト配列")]
    public Sprite[] jumpSprites;
    [Header("走行時のスプライト配列")]
    public Sprite[] runSprites;

    [Header("スプライト切り替えクールダウン（秒）")]
    public float spriteChangeCooldown;

    private float spriteChangeTimer = 0f;

    private Rigidbody2D _rb;
    private float jumpForce = 300.0f; //ジャンプの力
    private int jumpCount = 0;       //ジャンプ回数
    private float _InputX;           //左右入力

    // ジャンプ中かどうか
    private bool isJumping = false;
    private bool jumpButtonJustPressed = false; //ジャンプボタンが押された瞬間

    // アニメーション用インデックス
    private int idleIndex = 0;
    private int runIndex = 0;
    private int jumpIndex = 0;

    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // スプライト切り替えタイマーの減少
        if (spriteChangeTimer > 0)
        {
            spriteChangeTimer -= Time.deltaTime;
        }

        // 左右入力の取得
        _InputX = Input.GetAxisRaw("Horizontal");

        // ジャンプボタン押下のチェック
        if (Input.GetButtonDown("Jump"))
        {
            jumpButtonJustPressed = true;
        }
    }

    void FixedUpdate()
    {
        // 横方向の移動
        _rb.velocity = new Vector2(_InputX * _PlayerSpeed, _rb.velocity.y);

        // キャラクターの向き変更
        if (_InputX > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (_InputX < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }

        // ジャンプ処理
        if (jumpButtonJustPressed && jumpCount < 1)
        {
            _rb.AddForce(transform.up * jumpForce);
            jumpCount++;
            isJumping = true; //ジャンプ中フラグ
            jumpButtonJustPressed = false;

            // ジャンプ中のスプライト
            if (jumpSprites.Length > 0)
            {
                GetComponent<SpriteRenderer>().sprite = jumpSprites[jumpIndex];
                jumpIndex = (jumpIndex + 1) % jumpSprites.Length;
            }
        }

        // ジャンプ中のスプライト表示
        if (isJumping)
        {
            if (jumpSprites.Length > 0)
            {
                GetComponent<SpriteRenderer>().sprite = jumpSprites[jumpIndex];
            }
        }
        else
        {
            // 地面にいるときの待機・走行アニメーション
            if (_InputX == 0 && spriteChangeTimer <= 0)
            {
                // アイドル状態
                if (idleSprites.Length > 0)
                {
                    GetComponent<SpriteRenderer>().sprite = idleSprites[idleIndex];
                    idleIndex = (idleIndex + 1) % idleSprites.Length;
                }
                spriteChangeTimer = spriteChangeCooldown;
            }
            else if (_InputX != 0 && spriteChangeTimer <= 0)
            {
                // 走行中
                if (runSprites.Length > 0)
                {
                    GetComponent<SpriteRenderer>().sprite = runSprites[runIndex];
                    runIndex = (runIndex + 1) % runSprites.Length;
                }
                spriteChangeTimer = spriteChangeCooldown;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        // 地面に着いたとき
        if (other.gameObject.CompareTag("Floor") || other.gameObject.CompareTag("FloorObject"))
        {
            // ジャンプ回数リセット
            jumpCount = 0;
            isJumping = false;

            // 地面に立っているときはアイドルまたは走行アニメーション
            if (_InputX == 0)
            {
                if (idleSprites.Length > 0)
                {
                    GetComponent<SpriteRenderer>().sprite = idleSprites[idleIndex];
                    idleIndex = (idleIndex + 1) % idleSprites.Length;
                }
            }
            else
            {
                if (runSprites.Length > 0)
                {
                    GetComponent<SpriteRenderer>().sprite = runSprites[runIndex];
                    runIndex = (runIndex + 1) % runSprites.Length;
                }
            }
        }

        // FloorObjectに触れた場合（ジャンプ力増加）
        if (other.gameObject.CompareTag("FloorObject"))
        {
            jumpForce = 350f;
        }

        // 死亡エリアに落ちた場合
        if (other.gameObject.CompareTag("DeathFloor"))
        {
            //シーン再読み込み
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}