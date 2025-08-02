using UnityEngine;
using UnityEngine.SceneManagement;

public class StageSelectScript : MonoBehaviour
{
    [Header("�X�e�[�W�I���|�C���g�̔z��")]
    public GameObject[] stageSelectPoints = default;// �X�e�[�W�I���|�C���g�̔z��
    private float inputCooldown = 0.3f; //���̑��삪���s�ł���悤�ɂȂ�܂ł̃N�[���^�C��
    private float inputTimer = 0f;//�������͂���^�C�}�[
    private int currentStageIndex = 0;//���݂̃X�e�[�W�������ׂ̃C���f�b�N�X

    void Start()
    {
        //�X�e�[�W�I���|�C���g�̍��W��z��ɍ��킹�擾
        if (stageSelectPoints != null && stageSelectPoints.Length > 0)
        {
            transform.position = stageSelectPoints[currentStageIndex].transform.position;
        }
    }

    void Update()
    {
        //�X�e�[�W�I���|�C���g���ݒ肳��Ă��Ȃ��ꍇ�͉������Ȃ�
        if (stageSelectPoints == null || stageSelectPoints.Length == 0) { return; }

        // �^�C�}�[�X�V
        if (inputTimer > 0)
        {
            inputTimer -= Time.deltaTime;
        }

        float horizontalInput = Input.GetAxis("Horizontal");// ���������̓��͂��擾
        float threshold = 0.1f;// ���͂̂������l

        // ���͂��������l�𒴂����ꍇ�ɃX�e�[�W��؂�ւ���
        if (inputTimer <= 0)
        {
            // ���������̓��͂��������l�𒴂����ꍇ
            if (horizontalInput < -threshold)
            {
                // �����Ɉړ�
                currentStageIndex = Mathf.Max(currentStageIndex - 1, 0);
                transform.position = stageSelectPoints[currentStageIndex].transform.position;
                inputTimer = inputCooldown;
            }
            //����������l��
            else if (horizontalInput > threshold)
            {
                // �E���Ɉړ�
                currentStageIndex = Mathf.Min(currentStageIndex + 1, stageSelectPoints.Length - 1);
                transform.position = stageSelectPoints[currentStageIndex].transform.position;
                inputTimer = inputCooldown;
            }

            //BackSpace�L�[�A�܂��́A�Q�[���p�b�h��B�{�^���������ꂽ�ꍇ
            if (Input.GetKeyDown(KeyCode.Backspace) || Input.GetButtonDown("Cancel"))
            {
                // �^�C�g���V�[���ɖ߂�
                SceneManager.LoadScene("TitleScene");
            }

            // �X�e�[�W���I�����ꂽ��ԂŃW�����v�{�^���������ꂽ�ꍇ
            // �����ł̓W�����v�{�^�����������ƂŃX�e�[�W��I�����鎖���ł���
            if (Input.GetButtonDown("Jump"))
            {
                // ���݂̃X�e�[�W�C���f�b�N�X�ɉ����ăV�[�������[�h
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