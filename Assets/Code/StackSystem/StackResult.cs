using System;
using UnityEngine;

namespace Code.StackSystem
{
    [Serializable]
    public struct StackResult : IComparable<StackResult>
    {
        public bool isSuccess;           // 블록 성공 여부
        public Vector2 remainingSize;    // 남은 블록 크기 (X,Z)
        public Vector2 cutSize;          // 잘린 블록 크기 (X,Z)
        public Vector2 offset;           // 중심에서 벗어난 거리
        public bool isInitialized;       // 초기화 여부

        public int CompareTo(StackResult other)
        {
            // 면적 기준 비교
            float thisArea = remainingSize.x * remainingSize.y;
            float otherArea = other.remainingSize.x * other.remainingSize.y;
            return thisArea.CompareTo(otherArea);
        }
    }
}