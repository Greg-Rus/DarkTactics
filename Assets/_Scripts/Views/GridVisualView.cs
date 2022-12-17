using Shapes;
using UnityEngine;

namespace _Scripts.Views
{
    public class GridVisualView : MonoBehaviour
    {
        public Rectangle Fill;
        public Line BorderNorth;
        public Line BorderSouth;
        public Line BorderEast;
        public Line BorderWest;
        public Color WalkFill;
        public Color WalkBorder;
        public Color AttackFill;
        public Color AttackcBorder;

        public void SetWalkScheme()
        {
            Fill.Color = WalkFill;
            SetBordersColors(WalkBorder);
        }

        public void SetAttackScheme()
        {
            Fill.Color = AttackFill;
            SetBordersColors(AttackcBorder);
        }

        private void SetBordersColors(Color color)
        {
            BorderNorth.Color = color;
            BorderSouth.Color = color;
            BorderEast.Color = color;
            BorderWest.Color = color;
        }
    }
}