using UnityEngine;
using UnityEngine.SceneManagement;

public class StageSelectScript : MonoBehaviour
{
    [Header("�X�e�[�W�I���|�C���g�̔z��")]
    public GameObject[] stageSelectPoints = default; // �X�e�[�W�̑I���|�C���g�̔z��
    private float inputCooldown = 0.3f; // ���͂̃N�[���_�E�����ԁi�b�j
    private float inputTimer = 0f; // ���͏����̃^�C�}�[
    private int currentStageIndex = 0; // ���ݑI�𒆂̃X�e�[�W�̃C���f�b�N�X

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
        if (stageSelectPoints == null || stageSelectPoints.Length == 0) { return; }

        // ���̓N�[���_�E�����Ԃ̃J�E���g�_�E��
        if (inputTimer > 0)
        {
            inputTimer -= Time.deltaTime;
        }

        // �������̓��͎擾
        float horizontalInput = Input.GetAxis("Horizontal");
        float threshold = 0.1f; // ���͔����臒l

        // ���̓N�[���_�E���ς݂���臒l�𒴂������͂��������ꍇ
        if (inputTimer <= 0)
        {
            // ���ɓ��͂��ꂽ�ꍇ
            if (horizontalInput < -threshold)
            {
                // �X�e�[�W�C���f�b�N�X�����炷�i���Ɉړ��j
                currentStageIndex = Mathf.Max(currentStageIndex - 1, 0);
                transform.position = stageSelectPoints[currentStageIndex].transform.position;
                inputTimer = inputCooldown;
            }
            // �E�ɓ��͂��ꂽ�ꍇ
            else if (horizontalInput > threshold)
            {
                // �X�e�[�W�C���f�b�N�X�𑝂₷�i�E�Ɉړ��j
                currentStageIndex = Mathf.Min(currentStageIndex + 1, stageSelectPoints.Length - 1);
                transform.position = stageSelectPoints[currentStageIndex].transform.position;
                inputTimer = inputCooldown;
            }

            // �߂�L�[�iBackspace or Cancel�{�^���j�Ń^�C�g���V�[����
            if (Input.GetKeyDown(KeyCode.Backspace) || Input.GetButtonDown("Cancel"))
            {
                SceneManager.LoadScene("TitleScene");
            }

            // �W�����v�L�[�ŃV�[����؂�ւ�
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