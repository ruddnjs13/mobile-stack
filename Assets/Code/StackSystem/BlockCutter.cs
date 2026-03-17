using UnityEngine;

namespace Code.StackSystem
{
    public static class BlockCutter
    {
        public static void ApplyCut(StackBlock block, StackResult result)
        {
            if (!result.isSuccess) return;

            Vector3 scale = block.transform.localScale;
            Vector3 pos = block.transform.position;

            // 남은 블록 크기 적용
            scale.x = result.remainingSize.x;
            scale.z = result.remainingSize.y;
            block.transform.localScale = scale;

            // 위치 보정
            pos.x -= result.offset.x / 2f;
            pos.z -= result.offset.y / 2f;
            block.transform.position = pos;

            // 잘린 블록 생성
            if (result.cutSize.x > 0f || result.cutSize.y > 0f)
                CreateFallingBlock(result, pos, scale.y);
        }

        private static void CreateFallingBlock(StackResult result, Vector3 parentPos, float height)
        {
            Vector3 cutScale = new Vector3(result.cutSize.x, height, result.cutSize.y);
            GameObject cutBlock = GameObject.CreatePrimitive(PrimitiveType.Cube);
            cutBlock.transform.localScale = cutScale;

            float cutPosX = parentPos.x + (result.remainingSize.x / 2f + cutScale.x / 2f) * Mathf.Sign(result.offset.x);
            float cutPosZ = parentPos.z + (result.remainingSize.y / 2f + cutScale.z / 2f) * Mathf.Sign(result.offset.y);
            cutBlock.transform.position = new Vector3(cutPosX, parentPos.y, cutPosZ);

            Rigidbody rb = cutBlock.AddComponent<Rigidbody>();
            rb.mass = 1f;
            Renderer r = cutBlock.GetComponent<Renderer>();
            if (r != null) r.material.color = Color.red;
        }
    }
}