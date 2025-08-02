using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [Header("移動速度")] public float _PlayerSpeed;
    [Header("通常のスプライト")] public Sprite normalSprite;

    [Header("アイドルスプライトの配列")] public Sprite[] idleSprites;
    [Header("ジャンプスプライトの配列")] public Sprite[] jumpSprites;
    [Header("ランニングスプライトの配列")] public Sprite[] runSprites;

    [Header("スプライトが変更されるまでのクールタイム")] public float spriteChangeCooldown;

    private float spriteChangeTimer = 0f;

    private Rigidbody2D _rb;
    private float jumpForce = 300.0f;
    private int jumpCount = 0;
    private float _InputX;

    // ジャンプ状態フラグ  
    private bool isJumping = false;
    private bool jumpButtonJustPressed = false;

    // 画像配列のインデックス（順番巡回）  
    private int idleIndex = 0;
    private int runIndex = 0;
    private int jumpIndex = 0;

    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // スプライト変更タイマーの更新  
        if (spriteChangeTimer > 0)
        {
            spriteChangeTimer -= Time.deltaTime;
        }

        _InputX = Input.GetAxisRaw("Horizontal");

        // ジャンプボタンを押した瞬間  
        if (Input.GetButtonDown("Jump"))
        {
            jumpButtonJustPressed = true;
        }
    }

    void FixedUpdate()
    {
        // 横移動  
        _rb.velocity = new Vector2(_InputX * _PlayerSpeed, _rb.velocity.y);

        // 向き変更  
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
            isJumping = true; // 空中にいる状態  
            jumpButtonJustPressed = false;

            // ジャンプスプライトの最初を設定  
            if (jumpSprites.Length > 0)
            {
                GetComponent<SpriteRenderer>().sprite = jumpSprites[jumpIndex];
                jumpIndex = (jumpIndex + 1) % jumpSprites.Length;
            }
        }

        // 空中にいる場合、ジャンプスプライトを維持する  
        if (isJumping)
        {
            if (jumpSprites.Length > 0)
            {
                GetComponent<SpriteRenderer>().sprite = jumpSprites[jumpIndex];
            }
        }
        else
        {
            // 地面にいるときの処理  
            if (_InputX == 0 && spriteChangeTimer <= 0)
            {
                // アイドルスプライト  
                if (idleSprites.Length > 0)
                {
                    GetComponent<SpriteRenderer>().sprite = idleSprites[idleIndex];
                    idleIndex = (idleIndex + 1) % idleSprites.Length;
                }
                spriteChangeTimer = spriteChangeCooldown;
            }
            else if (_InputX != 0 && spriteChangeTimer <= 0)
            {
                // ランニングスプライト  
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
        if (other.gameObject.CompareTag("Floor") || other.gameObject.CompareTag("FloorObject"))
        {
            // 着地  
            jumpCount = 0;
            isJumping = false;

            // 着地後にアイドルかランニングに戻す  
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

        //フロアオブジェクトに触れた場合のみ、ジャンプ力を上げる
        if (other.gameObject.CompareTag("FloorObject"))
        {
            jumpForce = 350.0f; // ジャンプ力を少し高くする
        }

        if (other.gameObject.CompareTag("DeathFloor"))
        {
            // 死亡床に触れたらシーンリロード
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}