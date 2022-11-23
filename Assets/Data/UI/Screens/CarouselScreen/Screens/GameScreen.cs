using ScreenSystem.Screens;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KM.UI.CarouselScreens
{
    public class GameScreen : BaseScreen
    {
        private RectTransform thisTransform;
        public RectTransform MovingMenu;

        public Vector3 destination;

        float speed = 5;

        public float menuHeight;
        public float screenHeight = 0;

        const float MIN_MENU_HEIGHT_TO_SWIPE = 500 + MIN_OFFSET;
        const float MIN_OFFSET = 200;

        protected override void OnInit()
        {
        }

        protected override void OnShow()
        {
            
        }

        protected override void OnHide()
        {
            
        }
        /*
void Awake()
{
  destination = MovingMenu.localPosition;
  menuHeight = MovingMenu.rect.height;

  thisTransform = (RectTransform)transform;

   screenHeight = thisTransform.rect.height;
}


void Update()
{
   if (menuHeight != MovingMenu.rect.height)
   {
       menuHeight = MovingMenu.rect.height;
   }

   destination.y = ClampY(destination.y);

   if (MovingMenu.anchoredPosition.y != destination.y)
       MovingMenu.anchoredPosition = Vector3.Lerp(MovingMenu.anchoredPosition, destination, speed * Time.deltaTime);
}

public void MoveVertical(float deltaY)
{
   //Debug.Log("Move vertical" + deltaY);

   destination.y = ClampY(destination.y + deltaY);
}


float ClampY(float value)
{
   float maxY = -(screenHeight / 2) + menuHeight / 2 + MIN_OFFSET;
   float minY = -(screenHeight / 2) - menuHeight / 2 + MIN_MENU_HEIGHT_TO_SWIPE;

   if (menuHeight < MIN_MENU_HEIGHT_TO_SWIPE)
       return maxY;

   return Mathf.Clamp(value, minY , maxY );
}
*/
    }
}
