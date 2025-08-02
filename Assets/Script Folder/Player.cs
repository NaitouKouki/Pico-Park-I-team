using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [Header("�ړ����x")] public float _PlayerSpeed;
    [Header("�ʏ�̃X�v���C�g")] public Sprite normalSprite;

    [Header("�A�C�h���X�v���C�g�̔z��")] public Sprite[] idleSprites;
    [Header("�W�����v�X�v���C�g�̔z��")] public Sprite[] jumpSprites;
    [Header("�����j���O�X�v���C�g�̔z��")] public Sprite[] runSprites;

    [Header("�X�v���C�g���ύX�����܂ł̃N�[���^�C��")] public float spriteChangeCooldown;

    private float spriteChangeTimer = 0f;

    private Rigidbody2D _rb;
    private float jumpForce = 300.0f;
    private int jumpCount = 0;
    private float _InputX;

    // �W�����v��ԃt���O  
    private bool isJumping = false;
    private bool jumpButtonJustPressed = false;

    // �摜�z��̃C���f�b�N�X�i���ԏ���j  
    private int idleIndex = 0;
    private int runIndex = 0;
    private int jumpIndex = 0;

    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // �X�v���C�g�ύX�^�C�}�[�̍X�V  
        if (spriteChangeTimer > 0)
        {
            spriteChangeTimer -= Time.deltaTime;
        }

        _InputX = Input.GetAxisRaw("Horizontal");

        // �W�����v�{�^�����������u��  
        if (Input.GetButtonDown("Jump"))
        {
            jumpButtonJustPressed = true;
        }
    }

    void FixedUpdate()
    {
        // ���ړ�  
        _rb.velocity = new Vector2(_InputX * _PlayerSpeed, _rb.velocity.y);

        // �����ύX  
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
            isJumping = true; // �󒆂ɂ�����  
            jumpButtonJustPressed = false;

            // �W�����v�X�v���C�g�̍ŏ���ݒ�  
            if (jumpSprites.Length > 0)
            {
                GetComponent<SpriteRenderer>().sprite = jumpSprites[jumpIndex];
                jumpIndex = (jumpIndex + 1) % jumpSprites.Length;
            }
        }

        // �󒆂ɂ���ꍇ�A�W�����v�X�v���C�g���ێ�����  
        if (isJumping)
        {
            if (jumpSprites.Length > 0)
            {
                GetComponent<SpriteRenderer>().sprite = jumpSprites[jumpIndex];
            }
        }
        else
        {
            // �n�ʂɂ���Ƃ��̏���  
            if (_InputX == 0 && spriteChangeTimer <= 0)
            {
                // �A�C�h���X�v���C�g  
                if (idleSprites.Length > 0)
                {
                    GetComponent<SpriteRenderer>().sprite = idleSprites[idleIndex];
                    idleIndex = (idleIndex + 1) % idleSprites.Length;
                }
                spriteChangeTimer = spriteChangeCooldown;
            }
            else if (_InputX != 0 && spriteChangeTimer <= 0)
            {
                // �����j���O�X�v���C�g  
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
            // ���n  
            jumpCount = 0;
            isJumping = false;

            // ���n��ɃA�C�h���������j���O�ɖ߂�  
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

        //�t���A�I�u�W�F�N�g�ɐG�ꂽ�ꍇ�̂݁A�W�����v�͂��グ��
        if (other.gameObject.CompareTag("FloorObject"))
        {
            jumpForce = 350.0f; // �W�����v�͂�������������
        }

        if (other.gameObject.CompareTag("DeathFloor"))
        {
            // ���S���ɐG�ꂽ��V�[�������[�h
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}