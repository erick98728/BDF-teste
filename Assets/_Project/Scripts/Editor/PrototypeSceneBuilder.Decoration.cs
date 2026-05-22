using UnityEngine;

namespace Tester.Editor
{
    public static partial class PrototypeSceneBuilder
    {
        private static void CreateDemoTutorial(Transform parent)
        {
            CreateTutorialSign(
                "Tutorial_Move",
                parent,
                new Vector2(-20f, 1.4f),
                "Mover: A/D ou setas",
                Color.white
            );
            CreateTutorialSign(
                "Tutorial_Jump",
                parent,
                new Vector2(-8f, 1.4f),
                "Pular: Space",
                Color.white
            );
            CreateTutorialSign(
                "Tutorial_Katana",
                parent,
                new Vector2(10f, 1.4f),
                "Katana: J",
                Color.white
            );
            CreateTutorialSign(
                "Tutorial_Checkpoint",
                parent,
                new Vector2(22f, 1.4f),
                "Checkpoint",
                new Color(1f, 0.88f, 0.35f)
            );
            CreateTutorialSign(
                "Tutorial_DashGate",
                parent,
                new Vector2(59.5f, 3f),
                "Caminho do Dash",
                new Color(0.55f, 0.9f, 1f)
            );
            CreateTutorialSign(
                "Tutorial_Lucarelli",
                parent,
                new Vector2(76f, 2.9f),
                "Lucarelli",
                new Color(1f, 0.5f, 0.5f)
            );
            CreateTutorialSign(
                "Tutorial_DashReward",
                parent,
                new Vector2(97f, 3.4f),
                "Dash: Left Shift",
                new Color(0.55f, 0.9f, 1f)
            );
        }

        private static void CreateDemoEnd(Transform parent, int groundLayer)
        {
            CreateDemoEndMarker(
                "Fim_Demo_Marker",
                parent,
                new Vector2(114f, -0.15f),
                new Vector2(1.6f, 2.7f),
                groundLayer
            );
            CreateSign(
                "Fim_Demo_Label",
                parent,
                new Vector2(111.5f, 2.1f),
                "Fim da demo do Bosque",
                new Color(1f, 0.92f, 0.45f)
            );
            CreateBoundaryWall(
                "Fim_Demo_RightBoundary",
                parent,
                new Vector2(119f, 0.2f),
                new Vector2(1f, 8f),
                new Color(0.13f, 0.22f, 0.15f),
                groundLayer
            );
        }

        private static void CreateTutorialSign(
            string name,
            Transform parent,
            Vector2 position,
            string text,
            Color color
        )
        {
            CreateSign(name, parent, position, text, color);
        }

        private static void CreateSign(
            string name,
            Transform parent,
            Vector2 position,
            string text,
            Color color
        )
        {
            GameObject label = new GameObject(name);
            label.transform.SetParent(parent);
            label.transform.position = position;

            TextMesh textMesh = label.AddComponent<TextMesh>();
            textMesh.text = text;
            textMesh.font = GetLegacyFont();
            textMesh.fontSize = 42;
            textMesh.characterSize = 0.08f;
            textMesh.anchor = TextAnchor.MiddleCenter;
            textMesh.alignment = TextAlignment.Center;
            textMesh.color = color;
        }

        private static void CreateBackgroundDecoration(
            string name,
            Transform parent,
            Vector2 position,
            Vector2 size,
            Color color
        )
        {
            GameObject decoration = CreateWorldBlock(name, parent, position, size, color, 0);
            SpriteRenderer spriteRenderer = decoration.GetComponent<SpriteRenderer>();
            spriteRenderer.sortingOrder = -10;
        }

        private static void CreateDemoEndMarker(
            string name,
            Transform parent,
            Vector2 position,
            Vector2 size,
            int layer
        )
        {
            CreateWorldBlock(
                name,
                parent,
                position,
                size,
                new Color(0.95f, 0.8f, 0.25f, 0.8f),
                layer
            );
        }
    }
}
