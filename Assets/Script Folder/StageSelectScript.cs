using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageSelectScript : MonoBehaviour
{
    [Header("�X�e�[�W�I���|�C���g�̔z��")]
    public GameObject[] stageSelectPoints = default; // �X�e�[�W�̑I���|�C���g�̔z��

    private int currentStageIndex = 0; // ���݂̑I���X�e�[�W�̃C���f�b�N�X

    void Start()
    {
        if (stageSelectPoints != null && stageSelectPoints.Length > 0)
        {
            // �ŏ��̃X�e�[�W�I���|�C���g�Ɉړ�
            transform.position = stageSelectPoints[currentStageIndex].transform.position;
        }
    }

    void Update()
    {
        if (stageSelectPoints != null && stageSelectPoints.Length > 0)
        {
            // �\���L�[�̍��������ꂽ�Ƃ�

            if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
            {
                // �C���f�b�N�X�����炷�i�͈͓��Ɏ��߂�j
                currentStageIndex = Mathf.Max(currentStageIndex - 1, 0);
                transform.position = stageSelectPoints[currentStageIndex].transform.position;
            }

            // �E�L�[�iD�j�������ꂽ�Ƃ�
            if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
            {
                // �C���f�b�N�X�𑝂₷�i�͈͓��Ɏ��߂�j
                currentStageIndex = Mathf.Min(currentStageIndex + 1, stageSelectPoints.Length - 1);
                transform.position = stageSelectPoints[currentStageIndex].transform.position;
            }

            // �W�����v�L�[�ŃV�[���؂�ւ�
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
                        Debug.LogWarning("���蓖�Ă��Ă��Ȃ��X�e�[�W�ł�");
                        break;
                }
            }
        }
    }
}