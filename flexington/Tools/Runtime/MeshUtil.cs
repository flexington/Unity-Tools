using System;
using UnityEngine;

namespace flexington.Tools
{
    public static class MeshUtil
    {
        #region CodeMonkey
        private static Quaternion[] cachedQuaternionEulerArr;
        private static void CacheQuaternionEuler()
        {
            if (cachedQuaternionEulerArr != null) return;
            cachedQuaternionEulerArr = new Quaternion[360];
            for (int i = 0; i < 360; i++)
            {
                cachedQuaternionEulerArr[i] = Quaternion.Euler(0, 0, i);
            }
        }
        private static Quaternion GetQuaternionEuler(float rotFloat)
        {
            int rot = Mathf.RoundToInt(rotFloat);
            rot = rot % 360;
            if (rot < 0) rot += 360;
            //if (rot >= 360) rot -= 360;
            if (cachedQuaternionEulerArr == null) CacheQuaternionEuler();
            return cachedQuaternionEulerArr[rot];
        }
        #endregion


        /// <summary>
        /// Creates the arrays for vertices, triangles and uvs based on the given size
        /// </summary>
        public static void CreateMeshArray(int size, out Vector3[] vertices, out int[] triangles, out Vector2[] uvs)
        {
            vertices = new Vector3[size * 4];
            triangles = new int[size * 6];
            uvs = new Vector2[size * 4];
        }

        public static void AddToMeshArray(Vector3[] vertices, int[] triangles, Vector2[] uvs, int index, Vector3 position, float rotation, Vector3 baseSize, Vector2 uv00, Vector2 uv11)
        {
            // Relocate vertex index
            int vIndex = 4 * index;
            int vIndex0 = vIndex;
            int vIndex1 = vIndex + 1;
            int vIndex2 = vIndex + 2;
            int vIndex3 = vIndex + 3;

            // Adjust base size for offset
            baseSize *= 0.5f;

            // Determine if the mesh is distorted
            bool distorted = baseSize.x != baseSize.y;

            // Calculate vertex position
            if (distorted)
            {
                vertices[vIndex0] = position + GetQuaternionEuler(rotation) * new Vector3(-baseSize.x, baseSize.y);
                vertices[vIndex1] = position + GetQuaternionEuler(rotation) * new Vector3(-baseSize.x, -baseSize.y);
                vertices[vIndex2] = position + GetQuaternionEuler(rotation) * new Vector3(baseSize.x, -baseSize.y);
                vertices[vIndex3] = position + GetQuaternionEuler(rotation) * baseSize;
            }
            else
            {
                vertices[vIndex0] = position + GetQuaternionEuler(rotation - 270) * baseSize;
                vertices[vIndex1] = position + GetQuaternionEuler(rotation - 180) * baseSize;
                vertices[vIndex2] = position + GetQuaternionEuler(rotation - 90) * baseSize;
                vertices[vIndex3] = position + GetQuaternionEuler(rotation - 0) * baseSize;
            }

            // Relocate UV Index
            uvs[vIndex0] = new Vector2(uv00.x, uv11.y);
            uvs[vIndex1] = new Vector2(uv00.x, uv00.y);
            uvs[vIndex2] = new Vector2(uv11.x, uv11.y);
            uvs[vIndex3] = new Vector2(uv11.x, uv11.y);

            // Create triangles
            int tIndex = index * 6;
            triangles[tIndex] = vIndex0;
            triangles[tIndex + 1] = vIndex3;
            triangles[tIndex + 2] = vIndex1;

            triangles[tIndex + 3] = vIndex1;
            triangles[tIndex + 4] = vIndex3;
            triangles[tIndex + 5] = vIndex2;
        }

        public static void CreateSquare(ref Mesh mesh, float size, Vector3[] normals = default, Vector2[] uv = default)
        {
            mesh = new Mesh();
            float offset = size / 2f;

            Vector3[] vertices = new Vector3[4]{
                new Vector3(0,0,0),
                new Vector3 (size,0,0),
                new Vector3(0,size,0),
                new Vector3(size,size,0)
            };

            // Vector3[] vertices = new Vector3[4]{
            //     new Vector3(-offset,-offset,0),
            //     new Vector3 (size-offset,-offset,0),
            //     new Vector3(-offset,size-offset,0),
            //     new Vector3(size-offset,size-offset,0)
            // };

            int[] triangles = new int[6]{
                0,2,1,
                2,3,1
            };

            if (normals == default || normals.Length != 4)
            {
                normals = new Vector3[4]{
                    -Vector3.forward,
                    -Vector3.forward,
                    -Vector3.forward,
                    -Vector3.forward
                };
            }

            if (uv == default || uv.Length != 4)
            {
                uv = new Vector2[4]{
                    new Vector2(0,0),
                    new Vector2(1,0),
                    new Vector2(0,1),
                    new Vector2(1,1)
                };
            }

            mesh.vertices = vertices;
            mesh.triangles = triangles;
            mesh.normals = normals;
            mesh.uv = uv;
        }

        public static void ChangeColor(ref Mesh mesh, Color color)
        {
            Vector3[] vertices = mesh.vertices;
            Color[] vertexColor = new Color[vertices.Length];

            for (int i = 0; i < vertices.Length; i++)
            {
                vertexColor[i] = color;
            }
            mesh.colors = vertexColor;
        }
    }
}