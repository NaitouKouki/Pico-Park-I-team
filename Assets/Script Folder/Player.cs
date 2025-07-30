using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("�ړ����x")]public float _PlayerSpeed;
    private Rigidbody2D _rb;
    private float jumpForce = 300.0f ;//�W�����v��
    private int jumpCount = 0;//�W�����v��
    private float _InputX;

    // Start is called before the first frame update
    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //���͂��擾
        _InputX = Input.GetAxisRaw("Horizontal");
        //�W�����v�̏���
        //Input.GetButtonDown("Jump")�́A�W�����v�{�^���������ꂽ�u�Ԃ�true��Ԃ�
        if (Input.GetButtonDown("Jump") && this.jumpCount < 1)
        {
            this._rb.AddForce(transform.up * jumpForce);
            jumpCount++;
        }
    }

    //�m����FixedUpdate���Ăяo�����߂ɁA�������Z�̍X�V�^�C�~���O�ŏ������s��
    void FixedUpdate()
    {
        //���͂ɉ����ăv���C���[���ړ�
        _rb.velocity = new Vector2(_InputX * _PlayerSpeed, _rb.velocity.y);
    }

    //�v���C���[�����ɐڐG�����Ƃ��̏���
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Floor"))
        {
            //�W�����v�͂����ɖ߂�
            jumpForce = 300.0f;
            jumpCount = 0;
        }
        if(other.gameObject.CompareTag("FloorObject"))
        {
            //Object�ɐG��Ă��鎞�̂݃W�����v�͂��グ��B����ȍ~�̏����͒ʏ�ʂ�B
            jumpForce = 350.0f;
            jumpCount = 0;
        }
        //�v���C���[��deathFloor�ɐڐG�����ꍇ�̏���
        if (other.gameObject.CompareTag("DeathFloor"))
        {
            //�V�[�����ēǂݍ���
            UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
        }
    }
}
