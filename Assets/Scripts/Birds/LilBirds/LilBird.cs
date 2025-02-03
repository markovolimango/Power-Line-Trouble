using UnityEngine;

namespace Birds
{
    public abstract class LilBird : Bird
    {
        protected Vector2Int JumpDir;

        public override void Start()
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, -4);
            base.Start();
        }
    }
}