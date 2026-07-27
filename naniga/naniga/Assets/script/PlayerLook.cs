using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerLook : MonoBehaviour
{
    // --- 設定 ---
    public float mouseSensitivity = 200f; // マウス感度（回転の速さ）
    public Transform cam; // 首を上下させるためのカメラ

    private float xRotation = 0f; // 現在の上下の回転角
    private Vector2 lookInput; // マウスの移動量（入力値）

    // --- 初期化 ---
    void Start()
    {
        // マウスカーソルを画面中央に固定し、非表示にする
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // --- 毎フレーム呼び出し ---
    void Update()
    {
        HandleMouseLook();
    }

    // --- 入力の受け取り ---
    public void OnLook(InputValue value)
    {
        // マウスの「動いた量」を取得
        lookInput = value.Get<Vector2>();
    }

  
    void HandleMouseLook()
    {
        // 1フレームあたりの回転量に変換
        float mouseX = lookInput.x * mouseSensitivity * Time.deltaTime;
        float mouseY = lookInput.y * mouseSensitivity * Time.deltaTime;

        // 【上下回転】マウスを上に動かすと(首が)上を向くようにする
        xRotation -= mouseY;
        // 首が回りすぎないよう、真上・真下の90度で制限
        xRotation = Mathf.Clamp(xRotation, -90, 90);

        // カメラ（首）だけを上下に回転させる
        cam.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        // 【左右回転】プレイヤーの体ごと左右（Y軸）に回す
        transform.Rotate(Vector3.up * mouseX);
    }
}