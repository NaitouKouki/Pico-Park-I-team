using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("移動速度")]public float _PlayerSpeed;
    private Rigidbody2D _rb;
    private float jumpForce = 300.0f ;//ジャンプ力
    private int jumpCount = 0;//ジャンプ回数
    private float _InputX;

    // Start is called before the first frame update
    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //入力を取得
        _InputX = Input.GetAxisRaw("Horizontal");
        //ジャンプの処理
        //Input.GetButtonDown("Jump")は、ジャンプボタンが押された瞬間にtrueを返す
        if (Input.GetButtonDown("Jump") && this.jumpCount < 1)
        {
            this._rb.AddForce(transform.up * jumpForce);
            jumpCount++;
        }
    }

    //確実にFixedUpdateを呼び出すために、物理演算の更新タイミングで処理を行う
    void FixedUpdate()
    {
        //入力に応じてプレイヤーを移動
        _rb.velocity = new Vector2(_InputX * _PlayerSpeed, _rb.velocity.y);
    }

    //プレイヤーが床に接触したときの処理
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Floor"))
        {
            //ジャンプ力を元に戻す
            jumpForce = 300.0f;
            jumpCount = 0;
        }
        if(other.gameObject.CompareTag("FloorObject"))
        {
            //Objectに触れている時のみジャンプ力を上げる。それ以降の処理は通常通り。
            jumpForce = 350.0f;
            jumpCount = 0;
        }
        //プレイヤーがdeathFloorに接触した場合の処理
        if (other.gameObject.CompareTag("DeathFloor"))
        {
            //シーンを再読み込み
            UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
        }
    }
}
