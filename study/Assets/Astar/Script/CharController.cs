// CharacterController.cs - 캐릭터 이동 및 회전 제어
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;

public class CharController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    public float rotationSpeed = 10f;
    public float stopDistance = 0.1f;

    [Header("Ground Detection")]
    public LayerMask groundLayer = 6;

    private AStarPathfinder pathfinder;
    private Camera playerCamera;
    private List<GridNode> currentPath;
    private int currentPathIndex;
    private bool isMoving;

    [Header("Path Visualization")]
    public LineRenderer pathLine;
    public bool showPath = true;

    private InputAction clickAction;
    private InputAction mousePosAction;

    // private void OnEnable()
    // {
    //     // 왼쪽 마우스 버튼 입력 (mouse/leftButton)
    //     clickAction = new InputAction(type: InputActionType.Button, binding: "<Mouse>/leftButton");
    //     clickAction.Enable();

    //     // 마우스 위치 입력
    //     mousePosAction = new InputAction(type: InputActionType.Value, binding: "<Mouse>/position");
    //     mousePosAction.Enable();
    // }

    // private void OnDisable()
    // {
    //     clickAction.Disable();
    //     mousePosAction.Disable();
    // }

    void Start()
    {
        pathfinder = FindObjectOfType<AStarPathfinder>();
        playerCamera = Camera.main;

        if (pathLine == null)
        {
            GameObject lineObj = new GameObject("PathLine");
            lineObj.transform.SetParent(transform);
            pathLine = lineObj.AddComponent<LineRenderer>();
            pathLine.material = new Material(Shader.Find("Sprites/Default"));
            pathLine.startColor = Color.cyan;
            pathLine.startWidth = 0.2f;
            pathLine.positionCount = 0;
        }
    }

    void Update()
    {
        HandleInput();
        MoveAlongPath();
    }

    void HandleInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = playerCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, groundLayer))
            {
                MoveToPosition(hit.point);
            }
        }

        // if (clickAction.WasPerformedThisFrame()) // 마우스 클릭 감지
        // {
        //     Vector2 mousePos = mousePosAction.ReadValue<Vector2>(); // 화면 좌표
        //     Ray ray = playerCamera.ScreenPointToRay(mousePos);
        //     RaycastHit hit;

        //     if (Physics.Raycast(ray, out hit, Mathf.Infinity, groundLayer))
        //     {
        //         Debug.Log(0);
        //         MoveToPosition(hit.point);
        //     }
        // }
    }

    void MoveToPosition(Vector3 targetPosition)
    {
        List<GridNode> path = pathfinder.FindPath(transform.position, targetPosition);

        if (path != null && path.Count > 0)
        {
            currentPath = path;
            currentPathIndex = 0;
            isMoving = true;

            if (showPath)
                DrawPath();
        }
    }

    void MoveAlongPath()
    {
        if (!isMoving || currentPath == null || currentPathIndex >= currentPath.Count)
            return;

        Vector3 targetPosition = currentPath[currentPathIndex].worldPosition;
        Vector3 direction = (targetPosition - transform.position).normalized;

        // Y축 회전만 적용 (탑뷰이므로)
        direction.y = 0;

        if (direction != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }

        // 이동
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

        // 목표 지점에 도달했는지 확인
        if (Vector3.Distance(transform.position, targetPosition) < stopDistance)
        {
            currentPathIndex++;

            if (currentPathIndex >= currentPath.Count)
            {
                isMoving = false;
                currentPath = null;

                if (pathLine != null)
                    pathLine.positionCount = 0;
            }
        }
    }

    void DrawPath()
    {
        if (pathLine == null || currentPath == null) return;

        pathLine.positionCount = currentPath.Count + 1;
        pathLine.SetPosition(0, transform.position);

        for (int i = 0; i < currentPath.Count; i++)
        {
            pathLine.SetPosition(i + 1, currentPath[i].worldPosition + Vector3.up * 0.1f);
        }
    }
}
