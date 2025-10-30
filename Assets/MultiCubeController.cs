using UnityEngine;

public class MultiCubeController : MonoBehaviour
{
    [Header("Настройки движения")]
    public float moveSpeed = 5.0f;

    private double totalDistanceTraveled = 0.0;

    [Header("Границы движения")]
    public int boundaryLimit = 5;

    private uint frameCounter = 0;
    private byte cubeCount;
    private bool isMoving = false;

    [Header("Кубики")]
    public GameObject[] cubes;

    private Rigidbody[] cubeRigidbodies;
    private Vector3 currentDirection = Vector3.zero;

    private const float EPSILON = 0.001f;
    private readonly Vector3 upOffset = new Vector3(0, 0.5f, 0);

    void Start()
    {
        if (cubes == null || cubes.Length == 0)
        {
            Debug.LogWarning("Массив кубиков пуст! Добавьте кубики в Inspector.");
            cubeCount = 0;
            return;
        }

        cubeCount = (byte)cubes.Length;
        cubeRigidbodies = new Rigidbody[cubes.Length];

        for (int i = 0; i < cubes.Length; i++)
        {
            if (cubes[i] == null)
                continue;

            cubeRigidbodies[i] = cubes[i].GetComponent<Rigidbody>();
        }

        Debug.Log($"Инициализировано {cubeCount} кубиков для управления.");
    }

    void Update()
    {
        frameCounter++;

        bool upPressed = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow);
        bool downPressed = Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow);
        bool leftPressed = Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow);
        bool rightPressed = Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow);

        isMoving = upPressed || downPressed || leftPressed || rightPressed;

        currentDirection = Vector3.zero;

        if (isMoving)
        {
            if (upPressed)
                currentDirection.z += 1.0f;

            if (downPressed)
                currentDirection.z -= 1.0f;

            if (leftPressed)
                currentDirection.x -= 1.0f;

            if (rightPressed)
                currentDirection.x += 1.0f;

            if (currentDirection.sqrMagnitude > EPSILON)
            {
                currentDirection.Normalize();
            }
        }
    }

    void FixedUpdate()
    {
        if (cubes == null || cubes.Length == 0)
            return;

        if (!isMoving)
            return;

        for (int i = 0; i < cubes.Length; i++)
        {
            if (cubes[i] == null)
                continue;

            Vector3 currentPosition = cubes[i].transform.position;
            Vector3 movement = currentDirection * moveSpeed * Time.fixedDeltaTime;
            Vector3 newPosition = currentPosition + movement;

            if (newPosition.x < -boundaryLimit || newPosition.x > boundaryLimit)
            {
                newPosition.x = Mathf.Clamp(newPosition.x, -boundaryLimit, boundaryLimit);
            }

            if (newPosition.z < -boundaryLimit || newPosition.z > boundaryLimit)
            {
                newPosition.z = Mathf.Clamp(newPosition.z, -boundaryLimit, boundaryLimit);
            }

            if (cubeRigidbodies[i] != null)
            {
                cubeRigidbodies[i].MovePosition(newPosition);
            }
            else
            {
                cubes[i].transform.position = newPosition;
            }

            totalDistanceTraveled += (double)Vector3.Distance(currentPosition, newPosition);
        }
    }
}
