using UnityEngine;

public class WeaponFollow : MonoBehaviour
{
    public PlayerControls input;

    [SerializeField]
    float rotateSpeed = 4f;

    [SerializeField]
    float maxTurn = 3f;

    private void OnEnable()
    {
        input = new PlayerControls();
        input.Enable();
    }

    void Update()
    {
        Vector2 mouseInput = input.Player.MouseLook.ReadValue<Vector2>();

        ApplyRotation(GetRotation(mouseInput));
    }

    Quaternion GetRotation(Vector2 mouse)
    {
        mouse = Vector2.ClampMagnitude(mouse, maxTurn);

        Quaternion rotX = Quaternion.AngleAxis(-mouse.y, Vector3.right);
        Quaternion rotY = Quaternion.AngleAxis(mouse.x, Vector3.up);

        Quaternion targetRot = rotX * rotY;

        return targetRot;
    }

    void ApplyRotation(Quaternion targetRot)
    {
        transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRot, rotateSpeed * Time.deltaTime);
    }
}
