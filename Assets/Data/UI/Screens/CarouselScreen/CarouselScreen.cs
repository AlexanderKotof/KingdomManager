using KM.UI.Configs;
using ScreenSystem.Components;
using ScreenSystem.Screens;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace KM.UI.CarouselScreens
{
    public class CarouselScreen : BaseScreen, IPointerDownHandler, IPointerUpHandler, IDragHandler
    {
        public List<GameScreen> GameplayScreens;

        public static RectTransform canvasTransform { get; private set; }

        public Transform content;

        public ListComponent screensButtons;
        public ScreensIconsConfig screenIcons;

        private int selectedScreen = 0;

        const float referenceScreenWidth = 1080;

        private HorizontalLayoutGroup screensGroup;

        private Vector3 mouseVector;
        private Vector3 vectorTemp;
        private Vector3 startTouch;

        private bool touchStarted;
        private bool isMoved;

        public float HorizontalSensetivity = 120;

        private Vector3 targetMovement = Vector3.zero;
        private float speed = 25;
        private float sensetivity = 2.7f;
        private float screenHeight;

        private const float navigation_panel_height = 170;
        private Dictionary<Type, Sprite> _iconsDictionary = new Dictionary<Type, Sprite>();

        public event Action<int> onScreenChanged;

        private void Awake()
        {
            canvasTransform = (RectTransform)transform.parent;

            screenHeight = canvasTransform.rect.height - navigation_panel_height;

            foreach (var screen in GameplayScreens)
            {
                screen.screenHeight = screenHeight;
            }

            screensGroup = content.GetComponent<HorizontalLayoutGroup>();

            FillScreensIconsDictionary();
            UpdateScreenButtons();

            onScreenChanged += DisselectOther;
        }

        private void OnDestroy()
        {
            onScreenChanged -= DisselectOther;
        }

        private void FillScreensIconsDictionary()
        {
            foreach (var data in screenIcons.screenIcons)
            {
                _iconsDictionary.Add(data.screen.GetType(), data.icon);
            }
        }

        private void UpdateScreenButtons()
        {
            screensButtons.SetItems<CheckboxButtonComponent>(GameplayScreens.Count, (item, par) =>
            {
                _iconsDictionary.TryGetValue(GameplayScreens[par.index].GetType(), out var icon);

                item.SetImage(icon);
                item.SetCheckedState(selectedScreen == par.index, false);
                item.SetCallback(() => {
                    GoToScreen(par.index); 
                });
            });
        }

        private void DisselectOther(int index)
        {
            for (int i = 0; i < screensButtons.items.Count; i++)
            {
                ((CheckboxButtonComponent)screensButtons.items[i]).SetCheckedState(i == index, false);
            }
        }

        void MoveHorizontal(Vector2 targetMovement)
        {
            float delta = (referenceScreenWidth * selectedScreen + targetMovement.x) / referenceScreenWidth;
            int index = 0;

            if (Mathf.Abs(delta) > 0.3f)
            {
                index = Mathf.Clamp(selectedScreen - (int)Mathf.Sign(delta), 0, GameplayScreens.Count - 1);
            }
            else
                index = selectedScreen;

            GoToScreen(index);
        }

        void FixedUpdate()
        {
#if UNITY_EDITOR

            EditorControll();

#elif UNITY_ANDROID
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            mouseVector = new Vector2(Input.GetTouch(0).position.x, Input.GetTouch(0).position.y);
        }
        else if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
        {
            vectorTemp = new Vector2(Input.GetTouch(0).position.x, Input.GetTouch(0).position.y);




            if (isMoved && Input.touchCount == 1)
            {

                vectorTemp -= mouseVector;

               

              

            }

           
        }

        else if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
        {
            isMoved = true;

            if (Input.touchCount > 1 )
            {
                isMoved = false;

                vectorTemp = new Vector2(Input.GetTouch(0).position.x, Input.GetTouch(0).position.y);

                mouseVector = vectorTemp - mouseVector;


                

                mouseVector = vectorTemp;
            }
        }
        else if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Stationary)
        {

            mouseVector = new Vector2(Input.GetTouch(0).position.x, Input.GetTouch(0).position.y);
        }
        

#endif
            content.localPosition = Vector3.Lerp(content.localPosition, targetMovement, speed * Time.deltaTime);

        }

        public void AddNewMenu(BaseScreen prefab, int order)
        {
            var screen = Instantiate(prefab, content).GetComponent<GameScreen>();

            screen.screenHeight = screenHeight;

            screen.transform.SetSiblingIndex(order);

            GameplayScreens.Insert(order, screen);

            //AttentionUIButton.Attention(order);

            if (order <= selectedScreen)
            {
                GoToScreen(selectedScreen + 1);
                content.localPosition = targetMovement;
            }

            UpdateScreenButtons();
        }

        public void GoToScreen(int index)
        {
            if (selectedScreen != index)
            {
                selectedScreen = index;
                onScreenChanged?.Invoke(selectedScreen);
            }

            targetMovement.x = Mathf.Clamp(referenceScreenWidth * -selectedScreen, -(GameplayScreens.Count - 1) * referenceScreenWidth, 0);
        }


#if UNITY_EDITOR
        void EditorControll()
        {
            if (Input.GetMouseButton(0) && !touchStarted)
            {
                startTouch = mouseVector = new Vector3(Input.mousePosition.x, 0, Input.mousePosition.y);
                touchStarted = true;
            }

            if (touchStarted)
            {
                vectorTemp = new Vector3(Input.mousePosition.x, 0, Input.mousePosition.y);

                var deltaX = Mathf.Abs(vectorTemp.x - startTouch.x);

                if (deltaX > HorizontalSensetivity)
                {
                    targetMovement.x = Mathf.Clamp(targetMovement.x + (vectorTemp.x - mouseVector.x) * sensetivity, -(GameplayScreens.Count - 1) * referenceScreenWidth, 0);

                    mouseVector.x = vectorTemp.x;
                }

                    if (GameplayScreens[selectedScreen] != null)
                    {
                        //GameplayScreens[selectedScreen].MoveVertical((vectorTemp.z - mouseVector.z) * sensetivity);

                        mouseVector.z = vectorTemp.z;
                    }
                    
                

                if (!Input.GetMouseButton(0))
                {
                    MoveHorizontal(targetMovement);
                    touchStarted = false;
                }

            }
        }
#endif

        protected override void OnShow()
        {

        }

        protected override void OnHide()
        {

        }

        protected override void OnInit()
        {
            
        }

        bool dragStarted;
        Vector2 dragMagnitude;

        public void OnPointerDown(PointerEventData eventData)
        {
            dragStarted = true;
            dragMagnitude = Vector3.zero;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            dragStarted = false;
            /*
            if (dragMagnitude.x > HorizontalSensetivity)
            {
                MoveHorizontal(dragMagnitude);
            }*/
        }

        public void OnDrag(PointerEventData eventData)
        {
            /*
            dragMagnitude += eventData.delta;

            if (dragMagnitude.x < HorizontalSensetivity)
            {
                GameplayScreens[selectedScreen].MoveVertical(dragMagnitude.y * sensetivity);
            }
            else
            {
                var direction = transform.localPosition;
                direction.x += eventData.delta.x;
                transform.localPosition = Vector3.Lerp(transform.localPosition, direction, speed * Time.deltaTime);
            }
            */
        }
    }
}
