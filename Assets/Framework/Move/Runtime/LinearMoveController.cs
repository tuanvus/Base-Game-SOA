using UnityEngine;
namespace LTA.Move
{
    [DisallowMultipleComponent]
    public class LinearMoveController : MoveController<MoveInfo>
    {
        public override void Move(Vector3 direction)
        {
            if (isStop) return;
            base.Move(direction);
            this.direction = direction;
            transform.position += this.direction * info.speed * Time.fixedDeltaTime;
        }

        
    }
}
