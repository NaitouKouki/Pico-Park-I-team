using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [Header("�v���C���[�X�s�[�h")]
    public float _PlayerSpeed;
    [Header("�ʏ��Ԃ̃X�v���C�g")]
    public Sprite normalSprite;

    [Header("�A�C�h�����̃X�v���C�g�z��")]
    public Sprite[] idleSprites;
    [Header("�W�����v���̃X�v���C�g�z��")]
    public Sprite[] jumpSprites;
    [Header("���s���̃X�v���C�g�z��")]
    public Sprite[] runSprites;

    [Header("�X�v���C�g�؂�ւ��N�[���_�E���i�b�j")]
    public float spriteChangeCooldown;

    private float spriteChangeTimer = 0f;

    private Rigidbody2D _rb;
    private float jumpForce = 300.0f; //�W�����v�̗�
    private int jumpCount = 0;       //�W�����v��
    private float _InputX;           //���E����

    // �W�����v�����ǂ���
    private bool isJumping = false;
    private bool jumpButtonJustPressed = false; //�W�����v�{�^���������ꂽ�u��

    // �A�j���[�V�����p�C���f�b�N�X
    private int idleIndex = 0;
    private int runIndex = 0;
    private int jumpIndex = 0;

    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // �X�v���C�g�؂�ւ��^�C�}�[�̌���
        if (spriteChangeTimer > 0)
        {
            spriteChangeTimer -= Time.deltaTime;
        }

        // ���E���͂̎擾
        _InputX = Input.GetAxisRaw("Horizontal");

        // �W�����v�{�^�������̃`�F�b�N
        if (Input.GetButtonDown("Jump"))
        {
            jumpButtonJustPressed = true;
        }
    }

    void FixedUpdate()
    {
        // �������̈ړ�
        _rb.velocity = new Vector2(_InputX * _PlayerSpeed, _rb.velocity.y);

        // �L�����N�^�[�̌����ύX
        if (_InputX > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (_InputX < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }

        // �W�����v����
        if (jumpButtonJustPressed && jumpCount < 1)
        {
            _rb.AddForce(transform.up * jumpForce);
            jumpCount++;
            isJumping = true; //�W�����v���t���O
            jumpButtonJustPressed = false;

            // �W�����v���̃X�v���C�g
            if (jumpSprites.Length > 0)
            {
                GetComponent<SpriteRenderer>().sprite = jumpSprites[jumpIndex];
                jumpIndex = (jumpIndex + 1) % jumpSprites.Length;
            }
        }

        // �W�����v���̃X�v���C�g�\��
        if (isJumping)
        {
            if (jumpSprites.Length > 0)
            {
                GetComponent<SpriteRenderer>().sprite = jumpSprites[jumpIndex];
            }
        }
        else
        {
            // �n�ʂɂ���Ƃ��̑ҋ@�E���s�A�j���[�V����
            if (_InputX == 0 && spriteChangeTimer <= 0)
            {
                // �A�C�h�����
                if (idleSprites.Length > 0)
                {
                    GetComponent<SpriteRenderer>().sprite = idleSprites[idleIndex];
                    idleIndex = (idleIndex + 1) % idleSprites.Length;
                }
                spriteChangeTimer = spriteChangeCooldown;
            }
            else if (_InputX != 0 && spriteChangeTimer <= 0)
            {
                // ���s��
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
        // �n�ʂɒ������Ƃ�
        if (other.gameObject.CompareTag("Floor") || other.gameObject.CompareTag("FloorObject"))
        {
            // �W�����v�񐔃��Z�b�g
            jumpCount = 0;
            isJumping = false;

            // �n�ʂɗ����Ă���Ƃ��̓A�C�h���܂��͑��s�A�j���[�V����
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

        // FloorObject�ɐG�ꂽ�ꍇ�i�W�����v�͑����j
        if (other.gameObject.CompareTag("FloorObject"))
        {
            jumpForce = 350f;
        }

        // ���S�G���A�ɗ������ꍇ
        if (other.gameObject.CompareTag("DeathFloor"))
        {
            //�V�[���ēǂݍ���
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}