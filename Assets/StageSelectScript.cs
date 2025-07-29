using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageSelectScript : MonoBehaviour
{
    //�X�e�[�W��I�ԏ�őI�����邽�߂̔z��
    [Header("�X�e�[�W��I�Ԃ��߂̔z��")] public GameObject[] stageSelectPoints = default;
    // Start is called before the first frame update
    void Start()
    {
        // �X�e�[�W�I���|�C���g���ݒ肳��Ă��邩�m�F
        if (stageSelectPoints != null && stageSelectPoints.Length > 0)
        {
            // �ŏ��̃X�e�[�W�I���|�C���g�Ɉړ�
            transform.position = stageSelectPoints[0].transform.position;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // �X�e�[�W�I���|�C���g���ݒ肳��Ă��邩�m�F
        if (stageSelectPoints != null && stageSelectPoints.Length > 0)
        {
            //// ���E�̃L�[���͂ɉ����ăX�e�[�W��I��
            //if (Input.GetKeyDown(KeyCode.LeftArrow))
            //{
            //    // ���̃X�e�[�W�I���|�C���g�Ɉړ�
            //    int currentIndex = System.Array.IndexOf(stageSelectPoints, transform.position);
            //    if (currentIndex > 0)
            //    {
            //        transform.position = stageSelectPoints[currentIndex - 1].transform.position;
            //        // �������[�ɂ���ꍇ�͉������Ȃ�
            //    }
            //}
            //else if (Input.GetKeyDown(KeyCode.RightArrow))
            //{
            //    // �E�̃X�e�[�W�I���|�C���g�Ɉړ�
            //    int currentIndex = System.Array.IndexOf(stageSelectPoints, transform.position);
            //    if (currentIndex < stageSelectPoints.Length - 1)
            //    {
            //        transform.position = stageSelectPoints[currentIndex + 1].transform.position;
            //        // �����E�[�ɂ���ꍇ�͉������Ȃ�
            //    }
            //}
            // ���E�̃L�[���͂ɉ����ăX�e�[�W��I��
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                transform.position = stageSelectPoints[0].transform.position;
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2) && stageSelectPoints.Length > 1)
            {
                transform.position = stageSelectPoints[1].transform.position;
            }
            else if (Input.GetKeyDown(KeyCode.Alpha3) && stageSelectPoints.Length > 2)
            {
                transform.position = stageSelectPoints[2].transform.position;
            }
            //0,1,2���ꂼ��ɃV�[�������蓖�Ă�
            //�X�y�[�X�L�[�������ꂽ�Ƃ��ɃX�e�[�W��I��
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (transform.position == stageSelectPoints[0].transform.position)
                {
                    SceneManager.LoadScene("SampleScene");
                }
                else if (transform.position == stageSelectPoints[1].transform.position)
                {
                    SceneManager.LoadScene("Stage2");
                }
                else if (transform.position == stageSelectPoints[2].transform.position)
                {
                    SceneManager.LoadScene("Stage3");
                }
            }
        }
    }
}
