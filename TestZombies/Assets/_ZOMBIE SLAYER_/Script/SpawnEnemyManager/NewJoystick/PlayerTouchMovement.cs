//using System;
//using UnityEngine;
//using UnityEngine.InputSystem.EnhancedTouch;
//using ETouch = UnityEngine.InputSystem.EnhancedTouch;

//public class PlayerTouchMovement : MonoBehaviour
//{
//    [SerializeField] private Vector2 joystickSize = new Vector2(300, 300);
//    [SerializeField] private FloatingJoystick joystick;
//    [SerializeField] private Canvas canvas;

//    private Finger movementFinger;
//    private Vector2 movementAmount;
//    [SerializeField] Transform player;

//    private void OnEnable()
//    {
//        EnhancedTouchSupport.Enable();
//        ETouch.Touch.onFingerDown += HandleFingerDown;
//        ETouch.Touch.onFingerUp += HandleLoseFinger;
//        ETouch.Touch.onFingerMove += HandleFingerMove;
//    }

//    private void OnDisable()
//    {
//        ETouch.Touch.onFingerDown -= HandleFingerDown;
//        ETouch.Touch.onFingerUp -= HandleLoseFinger;
//        ETouch.Touch.onFingerMove -= HandleFingerMove;
//        EnhancedTouchSupport.Disable();
//    }

//    private void HandleLoseFinger(Finger lostFinger)
//    {
//        if (lostFinger == movementFinger)
//        {
//            movementFinger = null;
//            joystick.knob.anchoredPosition = Vector2.zero;
//            joystick.gameObject.SetActive(false);
//            movementAmount = Vector2.zero;
//        }
//    }

//    private void HandleFingerMove(Finger movedFinger)
//    {
//        if (movedFinger == movementFinger)
//        {
//            Vector2 knobPositon;
//            float maxMovement = joystickSize.x / 2;
//            ETouch.Touch currentTouch = movedFinger.currentTouch;

//            if (Vector2.Distance(currentTouch.screenPosition, joystick.rectTransform.anchoredPosition) > maxMovement)
//            {
//                knobPositon = (currentTouch.screenPosition - joystick.rectTransform.anchoredPosition).normalized * maxMovement;
//            }
//            else
//            {
//                knobPositon = currentTouch.screenPosition - joystick.rectTransform.anchoredPosition;
//            }

//            joystick.knob.anchoredPosition = knobPositon;
//            movementAmount = knobPositon / maxMovement;
//        }
//    }

//    private void HandleFingerDown(Finger touchedFinger)
//    {
//        //if (movementFinger == null && touchedFinger.screenPosition.x <= Screen.width / 2f)
//        if (movementFinger == null && touchedFinger.screenPosition.x <= canvas.pixelRect.width * canvas.scaleFactor / 2f)
//        {
//            movementFinger = touchedFinger;
//            movementAmount = Vector2.zero;
//            joystick.gameObject.SetActive(true);
//            joystick.rectTransform.sizeDelta = joystickSize;
//            joystick.rectTransform.anchoredPosition = ClampStartPosition(touchedFinger.screenPosition);
//        }
//    }

//    private Vector2 ClampStartPosition(Vector2 startPosition)
//    {
//        if (startPosition.x < joystickSize.x / 2)
//        {
//            startPosition.x = joystickSize.x / 2;
//        }

//        if (startPosition.y < joystickSize.y / 2)
//        {
//            startPosition.y = joystickSize.y / 2;
//        }
//        else if (startPosition.y > /*Screen.height*/canvas.pixelRect.height * canvas.scaleFactor - joystickSize.y / 2)
//        {
//            startPosition.y = /*Screen.height*/canvas.pixelRect.height * canvas.scaleFactor - joystickSize.y / 2;
//        }

//        return startPosition;
//    }

//    public Vector2 GetJoystickMovement()
//    {
//        return new Vector2(movementAmount.x, movementAmount.y);
//    }

//    //private void Update()
//    //{
//    //    //Vector2 scaledMovement = player.speed * Time.deltaTime * new Vector2(movementAmount.x, movementAmount.y);
//    //}
//}
