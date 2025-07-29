using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OpenDoorScript : MonoBehaviour
{
    // �v���C���[�̍��W���Q�Ƃ��邽�߂̕ϐ�
    [Header("�v���C���[�̍��W")] public Transform playerTransform; // �v���C���[��Transform���Q�Ƃ��邽�߂̕ϐ�
    // �h�A�̍��W���Q�Ƃ��邽�߂̕ϐ�
    [Header("�h�A�̍��W")] public Vector2 _doorPosition; // �h�A�̍��W
    [SerializeField] public GameObject m_targetObject; // �h�A�̃^�[�Q�b�g�I�u�W�F�N�g
    private bool _isPlayerNear = false; // �v���C���[���߂��ɂ��邩�ǂ����̃t���O
    // Start is called before the first frame update
    void Start()
    {
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
        //�h�A�����l�ɍ��W��ݒ�
        if (_doorPosition == Vector2.zero)
        {
            _doorPosition = transform.position; // �h�A�̍��W���ݒ肳��Ă��Ȃ��ꍇ�A���݂̈ʒu���g�p
        }
    }

    // Update is called once per frame
    void Update()
    {
       //�v���C���[���h�A�̋߂��ɂ��邩�A�����ɃI�u�W�F�N�g����A�N�e�B�u�ɂȂ��Ă��邩�𔻒肷��
       if (playerTransform != null)
        {
            float distance = Vector2.Distance(_doorPosition, playerTransform.position);
            _isPlayerNear = distance < 1.0f; // 1.0f�͋߂��ɂ���Ƃ݂Ȃ�������臒l
            //"Door"�^�O�����Ă��邩�ǂ������m�F
            bool isDoorTagged = m_targetObject.activeSelf;
            // �v���C���[���h�A�̋߂��A����"Door"�^�O�����Ă���ꍇ�A�V�[����J�ڂ���
            if (_isPlayerNear && !isDoorTagged && Input.GetKeyDown(KeyCode.UpArrow))
            {
                // �V�[����J�ڂ���
                SceneManager.LoadScene("SelectScene"); // "NextScene"�͑J�ڐ�̃V�[�����ɒu�������Ă�������
            }
        }
    }
}
