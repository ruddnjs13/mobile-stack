using UnityEngine;

namespace Code.StackSystem
{
    public class BlockCutter : MonoBehaviour
    {
        [SerializeField] private GameObject _fallingBlockPrefab; // Rigidbody가 붙은 프리팹

        public void Slice(StackBlock current, Vector3 prevPos, Vector3 prevSize, StackBlock.MoveAxis axis)
        {
            float curPos = (axis == StackBlock.MoveAxis.X) ? current.transform.position.x : current.transform.position.z;
            float pPos = (axis == StackBlock.MoveAxis.X) ? prevPos.x : prevPos.z;
            float pSize = (axis == StackBlock.MoveAxis.X) ? prevSize.x : prevSize.z;

            var result = StackUtil.CalculateSlice(pPos, pSize, curPos);

            if (result.isOver)
            {
                // Game Over 처리 (EventBus 호출)
                Debug.Log("Game Over!");
                return;
            }

            // 1. 남은 블록 처리
            Vector3 newScale = current.transform.localScale;
            Vector3 newPos = current.transform.position;

            if (axis == StackBlock.MoveAxis.X)
            {
                newScale.x = result.remainingSize;
                newPos.x = result.remainingCenter;
            }
            else
            {
                newScale.z = result.remainingSize;
                newPos.z = result.remainingCenter;
            }

            current.transform.localScale = newScale;
            current.transform.position = newPos;

            // 2. 파편 블록 생성
            SpawnFallingBlock(result, current.transform.position.y, current.transform.localScale, axis);
        }

        private void SpawnFallingBlock(StackUtil.SliceResult res, float y, Vector3 currentScale, StackBlock.MoveAxis axis)
        {
            GameObject falling = Instantiate(_fallingBlockPrefab);
            Vector3 fallScale = currentScale;
            Vector3 fallPos = new Vector3(res.remainingCenter, y, res.remainingCenter); // 기본값

            if (axis == StackBlock.MoveAxis.X)
            {
                fallScale.x = res.fallingSize;
                fallPos.x = res.fallingCenter;
                fallPos.z = currentScale.z; // 기존 Z축 유지
            }
            else
            {
                fallScale.z = res.fallingSize;
                fallPos.z = res.fallingCenter;
                fallPos.x = currentScale.x; // 기존 X축 유지
            }

            falling.transform.localScale = fallScale;
            falling.transform.position = new Vector3(
                axis == StackBlock.MoveAxis.X ? res.fallingCenter : fallPos.x,
                y,
                axis == StackBlock.MoveAxis.Z ? res.fallingCenter : fallPos.z
            );
        }
    }
}