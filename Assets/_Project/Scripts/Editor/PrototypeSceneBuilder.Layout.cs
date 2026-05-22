using Tester.World;
using UnityEngine;

namespace Tester.Editor
{
    public static partial class PrototypeSceneBuilder
    {
        private static void CreateGroundBlock(
            string name,
            Transform parent,
            Vector2 position,
            Vector2 size,
            Color color,
            int groundLayer
        )
        {
            CreateLayoutBlock(name, parent, position, size, color, groundLayer);
        }

        private static void CreatePlatform(
            string name,
            Transform parent,
            Vector2 position,
            Vector2 size,
            Color color,
            int groundLayer
        )
        {
            CreateLayoutBlock(name, parent, position, size, color, groundLayer);
        }

        private static void CreateBoundaryWall(
            string name,
            Transform parent,
            Vector2 position,
            Vector2 size,
            Color color,
            int groundLayer
        )
        {
            CreateLayoutBlock(name, parent, position, size, color, groundLayer);
        }

        private static void CreateInvisibleWall(
            string name,
            Transform parent,
            Vector2 position,
            Vector2 size,
            int groundLayer
        )
        {
            GameObject wall = new GameObject(name);
            wall.transform.SetParent(parent);
            wall.transform.position = position;
            ConfigureLayer(wall, groundLayer);
            ConfigureBoxCollider2D(wall, size);
        }

        private static void CreateDeathZone(
            string name,
            Transform parent,
            Vector2 position,
            Vector2 size
        )
        {
            GameObject deathZone = CreateWorldBlock(
                name,
                parent,
                position,
                size,
                new Color(0.85f, 0.18f, 0.12f, 0.35f),
                0
            );

            ConfigureBoxCollider2D(deathZone, size, true);
            deathZone.AddComponent<DeathZone>();
        }

        private static void CreateLayoutBlock(
            string name,
            Transform parent,
            Vector2 position,
            Vector2 size,
            Color color,
            int layer
        )
        {
            GameObject block = CreateWorldBlock(name, parent, position, size, color, layer);
            ConfigureBoxCollider2D(block, size);
        }
    }
}
