using UnityEngine;
namespace LTA.Move
{
    public class ArcMoveController : LinearMoveController
    {
        float height;

        float speedUp = 0;

        public float Height
        {
            set
            {
                height = value;
            }
        }

        public float SpeedUp
        {
            get
            {
                return speedUp;
            }
        }


        public void SetSpeedY(float time)
        {
            speedUp = time * height;
            
        }
        public override void Move(Vector3 direction)
        {
            speedUp -= height;
            Debug.Log("speedUp" + speedUp + " " + height);
            direction += Vector3.up * speedUp;
            base.Move(direction);
        }
    }
}
