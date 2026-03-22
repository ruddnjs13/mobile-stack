using UnityEngine;

namespace Code.StackSystem
{
    public static class StackUtil
    {
        public struct SliceResult
        {
            public bool isOver;
            public float remainingSize;
            public float remainingCenter;
            public float fallingSize;
            public float fallingCenter;
            public float offset; // 방향 판정용
        }

        // StackUtil.cs 내부
        public static SliceResult CalculateSlice(float prevPos, float prevSize, float currPos)
        {
            float offset = currPos - prevPos;
            float absOffset = Mathf.Abs(offset);
            float direction = offset > 0 ? 1 : -1;

            if (absOffset >= prevSize) return new SliceResult { isOver = true };

            float remainingSize = prevSize - absOffset;
    
            // 남은 블록의 중심
            float remainingCenter = prevPos + (offset / 2f);
    
            // 💡 핵심: 파편의 중심은 (남은 블록의 끝단) + (파편 크기의 절반 * 방향)
            float edgeOfRemaining = remainingCenter + (direction * (remainingSize / 2f));
            float fallingCenter = edgeOfRemaining + (direction * (absOffset / 2f));

            return new SliceResult
            {
                isOver = false,
                remainingSize = remainingSize,
                remainingCenter = remainingCenter,
                fallingSize = absOffset,
                fallingCenter = fallingCenter,
                offset = offset
            };
        }
    }
}