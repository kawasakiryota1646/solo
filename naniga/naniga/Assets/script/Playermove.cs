using UnityEngine;
using UnityEngine.InputSystem;
public class Playermove : MonoBehaviour
{
    // --- 設定 ---
    public float speed = 5f;
    public float jumpForce = 5f;

    private bool isground = true;

    private Rigidbody rb; // プレイヤーのRigidbodyコンポーネント
    private Vector2 moveInput; // プレイヤーの移動入力

    // --- 初期化 ---
    void Start()
    {
        // Rigidbodyコンポーネントを取得
        rb = GetComponent<Rigidbody>();

    }

    // --- 毎フレーム呼び出し ---
    void Update()
    {
        Vector3 move = new Vector3(moveInput.x, 0, moveInput.y);
        transform.Translate(move * speed * Time.deltaTime);

    }


    // --- プレイヤーの移動入力を受け取るメソッド ---
    public void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    public void OnJump(InputValue value)
    {
        if (value.isPressed && isground)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isground = false;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isground = true;
        }
    }

}
