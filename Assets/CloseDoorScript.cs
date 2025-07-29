using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CloseDoorScript : MonoBehaviour
{
    [Header("�h�A�̍��W")] public Vector2 _doorPosition; // �h�A�̍��W
    //�v���C���[�̍��W���Q�Ƃ��邽�߂̕ϐ�
    [Header("�v���C���[�̍��W")] public Transform playerTransform; // �v���C���[��Transform���Q�Ƃ��邽�߂̕ϐ�
    private bool _isPlayerNear = false; // �v���C���[���߂��ɂ��邩�ǂ����̃t���O
    [SerializeField] public GameObject m_targetObject;
    //�h�A�̃e�N�X�`����؂�ւ��邽�߂̕ϐ�
    [Header("�h�A�̃e�N�X�`��")] public Texture2D doorTexture; // �h�A�̃e�N�X�`��
    // Start is called before the first frame update
    void Start()
    {
        //�h�A�̍��W���Q�Ƃ��A_doorPosition�ɐݒ�
        if (_doorPosition == Vector2.zero)
        {
            _doorPosition = transform.position; // �h�A�̍��W���ݒ肳��Ă��Ȃ��ꍇ�A���݂̈ʒu���g�p
        }
        // �v���C���[��Transform���ݒ肳��Ă��Ȃ��ꍇ�A�V�[������Player�I�u�W�F�N�g��T��
        if (playerTransform == null)
        {
            GameObject playerObject = GameObject.FindWithTag("Player");
            if (playerObject != null)
            {
                playerTransform = playerObject.transform; // Player�I�u�W�F�N�g��Transform���擾
            }
            else
            {
                Debug.LogWarning("Player�I�u�W�F�N�g��������܂���B�v���C���[�̍��W���Q�Ƃł��܂���B");
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        //�h�A�̍��W�ƃv���C���[�̍��W���r���āA�v���C���[���߂��ɂ��邩�ǂ����𔻒�
        if (_isPlayerNear) {
            // �v���C���[���h�A�̋߂��ɂ���ꍇ�A�h�A�̍��W�ƃv���C���[�̍��W���r
            float distance = Vector2.Distance(_doorPosition, playerTransform.position);
            _isPlayerNear = distance < 1.0f; // 1.0f�͋߂��ɂ���Ƃ݂Ȃ�������臒l
        } else {
            // �v���C���[���h�A�̋߂��ɂ��Ȃ��ꍇ�A�h�A�̍��W�ƃv���C���[�̍��W���r
            float distance = Vector2.Distance(_doorPosition, playerTransform.position);
            _isPlayerNear = distance < 1.0f; // 1.0f�͋߂��ɂ���Ƃ݂Ȃ�������臒l
        }
       //�v���C���[���h�A�̋߂��A����{�^�������͂��ꂽ�ꍇ�A�h�A���A�N�e�B�u�ɂ���
        if (_isPlayerNear && Input.GetKeyDown(KeyCode.UpArrow))
        {
            // �h�A���A�N�e�B�u�ɂ���
            gameObject.SetActive(false);
            // �h�A�̃e�N�X�`����؂�ւ���
            if (doorTexture != null)
            {
                Renderer renderer = GetComponent<Renderer>();
                if (renderer != null)
                {
                    renderer.material.mainTexture = doorTexture; // �h�A�̃e�N�X�`����ݒ�
                }
            }
            // �^�[�Q�b�g�I�u�W�F�N�g���A�N�e�B�u�ɂ���
            if (m_targetObject != null)
            {
                m_targetObject.SetActive(false);
            }
        }
    }
}
