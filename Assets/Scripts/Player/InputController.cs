using System;
using UnityEngine;

namespace HECS.Controllers
{
    public enum InputType { KeyboardMouse, GamePad }

    [DefaultExecutionOrder(-990)]
    public class InputController : MonoBehaviour
    {
        //это константы для конфига управления в префах проекта
        //управление для джойстика и клавы
        private const string Vertical = "Vertical";
        private const string Horizontal = "Horizontal";
        private const string LeftStickY = "LeftStickY";
        private const string LeftStickX = "LeftStickX";
        private const string RightStickX = "RightStickX";
        private const string RightStickY = "RightStickY";
        private const string MouseX = "Mouse X";
        private const string MouseY = "Mouse Y";

        //настройка активных кнопок
        private const string ActionX = "ActionX"; //эквивалентно Х на джойстике, как основная кнопка экшена там надо альтернативу прописать для клавиатуры 
        private const string ActionQr = "ActionQr"; // квадратик на джойстике
        private const string L1button = "L1button"; // верхние клавиши на джойстике
        private const string R1button = "R1button"; // верхние клавиши на джойстике
        private const string MouseLC = "MouseLC"; //лкм
        private const string MouseRC = "MouseRC"; //пкм


        private const float GamePadCompensationY = 1f;
        public ReactiveValue<InputType> InputTypeReactive { get; private set; } = new ReactiveValue<InputType>();
        public InputType InputType => InputTypeReactive.CurrentValue;

        private float verticalMove;
        private float horizontalMove;

        public ReactivePressButton ActionXButton = new ReactivePressButton();
        public ReactivePressButton ActionQRutton = new ReactivePressButton();
        public ReactivePressButton L1Button = new ReactivePressButton();
        public ReactivePressButton R1Button = new ReactivePressButton();
        public ReactivePressButton MouseLCButton = new ReactivePressButton();
        public ReactivePressButton MouseRCButton = new ReactivePressButton();

        private Vector3 cameraDirection;
        public Vector3 Direction => new Vector3(verticalMove*-1, 0, horizontalMove);

        public Vector3 CameraDirection => cameraDirection;

        public static InputController Instance;
        public Camera cameraMain;

        public float InputCameraX { get; private set; }
        public float InputCameraY { get; private set; }
        public float VerticalMove => verticalMove;
        public float HorizontalMove => horizontalMove;

        private bool haveGamePad;

        private void Awake()
        {
            if (Instance != null)
                Destroy(Instance.gameObject);
            Instance = this;
            CheckInputType();
        }

        private void Start()
        {
            cameraMain = Camera.main;
            GlobalCommander.Commander.RegisterInject<InputController>(this);
        }

        // Update is called once per frame
        void Update()
        {
            CheckInputType();
            Move();
        }

        private void CheckInputType()
        {
            haveGamePad = Input.GetJoystickNames().Length > 0 && (Input.GetJoystickNames())[0] != string.Empty;

            if (!haveGamePad && InputTypeReactive.CurrentValue == InputType.KeyboardMouse)
                return;
            else if (haveGamePad && InputTypeReactive.CurrentValue == InputType.GamePad)
                return;

            if (haveGamePad && InputTypeReactive.CurrentValue == InputType.KeyboardMouse)
                InputTypeReactive.CurrentValue = InputType.GamePad;
            else if (!haveGamePad && InputTypeReactive.CurrentValue == InputType.GamePad)
                InputTypeReactive.CurrentValue = InputType.KeyboardMouse;
        }

        public void ChainedActionsSubscribe(Action action, PressOrRelease pressOrRelease, ReactivePressButton neededButton, params ReactivePressButton[] chainedButtons)
        {
            AssignSubscriber(action, neededButton, pressOrRelease);

            foreach (var r in chainedButtons)
                AssignSubscriber(action, r, pressOrRelease);
        }

        private void AssignSubscriber(Action action, ReactivePressButton reactivePressButton, PressOrRelease pressOrRelease)
        {
            switch (pressOrRelease)
            {
                case PressOrRelease.Press:
                    reactivePressButton.Pressed += action;
                    break;
                case PressOrRelease.Release:
                    reactivePressButton.Released += action;
                    break;
            }
        }

        private void ActionButtons()
        {
            ActionXButton.SetPressed(Input.GetButtonDown(ActionX));
            ActionXButton.SetReleased(Input.GetButtonUp(ActionX));
            
            L1Button.SetPressed(Input.GetButtonDown(L1button));
            L1Button.SetReleased(Input.GetButtonUp(L1button));
            
            R1Button.SetPressed(Input.GetButtonDown(R1button));
            R1Button.SetReleased(Input.GetButtonUp(R1button));

            ActionQRutton.SetPressed(Input.GetButtonDown(ActionQr));
            ActionQRutton.SetReleased(Input.GetButtonUp(ActionQr));

            MouseLCButton.SetPressed(Input.GetButtonDown(MouseLC));
            MouseLCButton.SetReleased(Input.GetButtonUp(MouseLC));

            MouseRCButton.SetPressed(Input.GetButtonDown(MouseRC));
            MouseRCButton.SetReleased(Input.GetButtonUp(MouseRC));
        }

        private void CalculateDirection()
        {
            cameraDirection = Vector3.Scale(cameraMain.transform.forward, new Vector3(1, 0, 1))
                        * verticalMove + cameraMain.transform.right * horizontalMove;
        }

        private void Move()
        {
            switch (InputTypeReactive.CurrentValue)
            {
                case InputType.KeyboardMouse:
                    KeyboardMove();
                    MouseMove();
                    break;
                case InputType.GamePad:
                    GamePadInput();
                    break;
            }
        }

        private void GamePadInput()
        {
            verticalMove = Input.GetAxis(LeftStickY);
            horizontalMove = Input.GetAxis(LeftStickX);

            InputCameraX = Input.GetAxis(RightStickX);
            InputCameraY = Input.GetAxis(RightStickY) * GamePadCompensationY;
            ActionButtons();
            CalculateDirection();
        }

        private void MouseMove()
        {
            InputCameraX = Input.GetAxis(MouseX);
            InputCameraY = Input.GetAxis(MouseY);
            ActionButtons();
            CalculateDirection();
        }

        private void KeyboardMove()
        {
            verticalMove = Input.GetAxis(Vertical);
            horizontalMove = Input.GetAxis(Horizontal);
        }
    }

    public enum PressOrRelease { Press, Release }
}

