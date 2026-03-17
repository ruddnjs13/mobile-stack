using UnityEngine;

namespace Code.StackSystem
{
    public static class StackUtil
    {
        // 현재 블록과 이전 블록을 비교해 StackResult 생성
        public static StackResult JudgeBlock(Vector3 currentPos, StackBlock prevBlock, Vector3 currentScale)
        {
            Vector2 offset = new Vector2(
                currentPos.x - prevBlock.transform.position.x,
                currentPos.z - prevBlock.transform.position.z
            );

            Vector2 absOffset = new Vector2(Mathf.Abs(offset.x), Mathf.Abs(offset.y));

            Vector2 remaining = new Vector2(
                Mathf.Max(currentScale.x - absOffset.x, 0),
                Mathf.Max(currentScale.z - absOffset.y, 0)
            );

            Vector2 cut = new Vector2(
                currentScale.x - remaining.x,
                currentScale.z - remaining.y
            );

            bool isSuccess = remaining.x > 0 && remaining.y > 0;

            return new StackResult
            {
                isSuccess = isSuccess,
                remainingSize = remaining,
                cutSize = cut,
                offset = offset,
                isInitialized = true
            };
        }
    }
}