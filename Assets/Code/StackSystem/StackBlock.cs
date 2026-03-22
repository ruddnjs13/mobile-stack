using UnityEngine;
using UnityEngine.Serialization;

namespace Code.StackSystem
{
    public class StackBlock : MonoBehaviour
    {
        public enum MoveAxis { X, Z }

        public MoveAxis Axis { get; set; }
        private float _speed;
        private Vector3 _centerPos; // 이동의 중심점
        private bool _isMoving = false;

        public void Init(MoveAxis axis, float speed, Vector3 size, Vector3 centerPos)
        {
            Axis = axis;
            _speed = speed;
            _centerPos = centerPos;
            transform.localScale = size;
            _isMoving = true;
        }

        private void Update()
        {
            if (!_isMoving) return;

            float moveRange = 5f; // 이동 범위
            float offset = Mathf.PingPong(Time.time * _speed, moveRange * 2) - moveRange;

            if (Axis == MoveAxis.X)
                transform.position = new Vector3(_centerPos.x + offset, transform.position.y, _centerPos.z);
            else
                transform.position = new Vector3(_centerPos.x, transform.position.y, _centerPos.z + offset);
        }

        public void Stop() => _isMoving = false;
    }
}