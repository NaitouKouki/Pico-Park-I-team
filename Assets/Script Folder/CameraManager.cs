using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    //オブジェクトを追従するカメラのスクリプト
    [SerializeField] private GameObject target;
    Vector3 pos;
    // Start is called before the first frame update
    void Start()
    {
        pos = Camera.main.gameObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 cameraPos = target.transform.position; // cameraPosという変数を作り、追従する対象の位置を入れる

        // もし対象の横位置が0より小さい場合
        if (target.transform.position.x < 0)
        {
            cameraPos.x = 0; // カメラの横位置に0を入れる
        }

        // もし対象の縦位置が0より小さい場合
        if (target.transform.position.y < 0)
        {
            cameraPos.y = 1;  // カメラの縦位置に0を入れる
        }

        // もし対象の縦位置が0より大きい場合
        if (target.transform.position.y > 0)
        {
            cameraPos.y = target.transform.position.y;   // カメラの縦位置に対象の位置を入れる
        }

        cameraPos.z = -15; // カメラの奥行きの位置に-10を入れる
        Camera.main.gameObject.transform.position = cameraPos; //　カメラの位置に変数cameraPosの位置を入れる

    }
}
