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
            CreateLayoutBlock(name, parent, position, size, color, groundLayer, PlaceholderSpriteKind.Ground, true);
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
            CreateLayoutBlock(name, parent, position, size, color, groundLayer, PlaceholderSpriteKind.Platform, true);
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
            CreateLayoutBlock(name, parent, position, size, color, groundLayer, PlaceholderSpriteKind.Wall, false);
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
                new Color(0.05f, 0f, 0.02f, 0.06f),
                0,
                PlaceholderSpriteKind.PitShadow
            );

            SpriteRenderer renderer = deathZone.GetComponent<SpriteRenderer>();
            renderer.sortingOrder = SortDeathZone;

            ConfigureBoxCollider2D(deathZone, size, true);
            deathZone.AddComponent<DeathZone>();
        }

        private static void CreateLayoutBlock(
            string name,
            Transform parent,
            Vector2 position,
            Vector2 size,
            Color color,
            int layer,
            PlaceholderSpriteKind spriteKind,
            bool showWalkableEdge
        )
        {
            GameObject block = CreateWorldBlock(name, parent, position, size, color, layer, spriteKind);
            SpriteRenderer renderer = block.GetComponent<SpriteRenderer>();
            renderer.sortingOrder = SortGameplaySolid;
            ConfigureBoxCollider2D(block, size);

            if (!showWalkableEdge)
            {
                return;
            }

            CreateVisualChild(
                "WalkableTopEdge",
                block.transform,
                new Vector3(0f, (size.y * 0.5f) + 0.025f, 0f),
                new Vector2(size.x, 0.08f),
                DemoWalkableEdgeColor,
                SortGameplayEdge,
                PlaceholderSpriteKind.Platform
            );
        }
    }
}
