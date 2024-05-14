using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Borders
{
    [Tooltip("offset from viewport borders for player's movement")]
    public float minXOffset = 1.5f, maxXOffset = 1.5f, minYOffset = 1.5f, maxYOffset = 1.5f;
    [HideInInspector] public float minX, maxX, minY, maxY;
}

public class PlayerMoving : MonoBehaviour {
    public string playerMoveJoyStickName = "PlayerControl";
    public float moveSpeed = 25f;

    public GameObject vfx_NormalEngineFire;
    public GameObject vfx_VicEngineFire;

    [Tooltip("offset from viewport borders for player's movement")]
    public Borders borders;
    Camera mainCamera;
    bool controlIsActive = true; 

    public static PlayerMoving instance; // unique instance of the script for easy access to the script
    
    public void PlayerMoveToNextLevel() {
        GetComponent<PlayerShooting>().DisableShooting();
        controlIsActive = false;

        if (vfx_NormalEngineFire != null) vfx_NormalEngineFire.SetActive(false);
        if (vfx_VicEngineFire != null) vfx_VicEngineFire.SetActive(true);

        StartCoroutine(MoveTowardsNextLevel());
    }

    IEnumerator MoveTowardsNextLevel() {
        float elapsedTime = 0f;
        float moveDuration = 4f;
        Vector2 tagetPos = (Vector2)transform.position + Vector2.up * 20f;

        while (elapsedTime < moveDuration) {
            elapsedTime += Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, tagetPos, moveSpeed * 2f * Time.deltaTime);

            yield return null;
        }
    }

    public void EnableController() {
        controlIsActive = true;
        GetComponent<PlayerHealth>().DisableInvincibility();
    }

    public void DisableController() {
        controlIsActive = false;
        GetComponent<PlayerHealth>().EnableInvincibility();
    }

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    private void OnEnable() {
        EasyJoystick.On_JoystickMove += OnJoyStickMove;
    }

    private void OnDisable() {
        EasyJoystick.On_JoystickMove -= OnJoyStickMove;
    }

    private void Start()
    {
        mainCamera = Camera.main;
        ResizeBorders();                //setting 'Player's' moving borders deending on Viewport's size
    }

    private void Update() {
        #region Previous Player Control
        /*
            if (controlIsActive)
            {
                #if UNITY_STANDALONE || UNITY_EDITOR    //if the current platform is not mobile, setting mouse handling 

                            if (Input.GetMouseButton(0)) //if mouse button was pressed       
                            {
                                Vector3 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition); //calculating mouse position in the worldspace
                                mousePosition.z = transform.position.z;
                                transform.position = Vector3.MoveTowards(transform.position, mousePosition, 30 * Time.deltaTime);
                            }
                #endif

                #if UNITY_IOS || UNITY_ANDROID //if current platform is mobile, 

                            if (Input.touchCount == 1) // if there is a touch
                            {
                                Touch touch = Input.touches[0];
                                Vector3 touchPosition = mainCamera.ScreenToWorldPoint(touch.position);  //calculating touch position in the world space
                                touchPosition.z = transform.position.z;
                                transform.position = Vector3.MoveTowards(transform.position, touchPosition, 30 * Time.deltaTime);
                            }
                #endif
                            transform.position = new Vector3    //if 'Player' crossed the movement borders, returning him back 
                                (
                                Mathf.Clamp(transform.position.x, borders.minX, borders.maxX),
                                Mathf.Clamp(transform.position.y, borders.minY, borders.maxY),
                                0
                                );
                */
        #endregion

#if UNITY_EDITOR || UNITY_STANDALONE_WIN
        if (controlIsActive) {
            if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0) {
                Vector2 keyboardAxis = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
                transform.Translate(keyboardAxis * moveSpeed * Time.deltaTime);
                ClampPosition();
            }
        }
#endif
    }

    //  setting 'Player's' movement borders according to Viewport size and defined offset
    void ResizeBorders() 
    {
        borders.minX = mainCamera.ViewportToWorldPoint(Vector2.zero).x + borders.minXOffset;
        borders.minY = mainCamera.ViewportToWorldPoint(Vector2.zero).y + borders.minYOffset;
        borders.maxX = mainCamera.ViewportToWorldPoint(Vector2.right).x - borders.maxXOffset;
        borders.maxY = mainCamera.ViewportToWorldPoint(Vector2.up).y - borders.maxYOffset;
    }

    private void OnJoyStickMove(MovingJoystick move) {
        if (controlIsActive && move.joystickName != playerMoveJoyStickName) {
            return;
        }
        transform.Translate(move.joystickAxis * moveSpeed * Time.deltaTime);
        ClampPosition();
    }

    private void ClampPosition() {
        transform.position = new Vector2    //if 'Player' crossed the movement borders, returning him back 
        (
            Mathf.Clamp(transform.position.x, borders.minX, borders.maxX),
            Mathf.Clamp(transform.position.y, borders.minY, borders.maxY)
        );
    }
}
