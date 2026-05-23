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
                new Vector2(-157f, 1.4f),
                "Mover: A/D ou setas",
                Color.white
            );
            CreateTutorialSign(
                "Tutorial_Jump",
                parent,
                new Vector2(-130f, 1.8f),
                "Pular: Space",
                Color.white
            );
            CreateTutorialSign(
                "Tutorial_Hub",
                parent,
                new Vector2(-95f, 8.9f),
                "Hub Central",
                new Color(0.85f, 1f, 0.72f)
            );
            CreateTutorialSign(
                "Tutorial_Checkpoint",
                parent,
                new Vector2(-95f, 1.45f),
                "Checkpoint",
                new Color(1f, 0.88f, 0.35f)
            );
            CreateTutorialSign(
                "Tutorial_Katana",
                parent,
                new Vector2(-55f, 1.45f),
                "Katana: J",
                Color.white
            );
            CreateTutorialSign(
                "Tutorial_Roots",
                parent,
                new Vector2(-91f, -4.75f),
                "Raizes",
                new Color(0.52f, 0.78f, 0.56f)
            );
            CreateTutorialSign(
                "Tutorial_RootsReturn",
                parent,
                new Vector2(-88f, -1.05f),
                "Retorno ao Hub",
                new Color(0.72f, 0.96f, 0.68f)
            );
            CreateTutorialSign(
                "Tutorial_Canopy",
                parent,
                new Vector2(-66f, 8.55f),
                "Copas",
                new Color(0.72f, 0.96f, 0.68f)
            );
            CreateTutorialSign(
                "Tutorial_CanopyAltRoute",
                parent,
                new Vector2(-82f, 9.55f),
                "Rota alternativa",
                new Color(0.72f, 0.96f, 0.68f)
            );
            CreateTutorialSign(
                "Tutorial_DashGate",
                parent,
                new Vector2(-111f, 8.55f),
                "Falta uma tecnica",
                new Color(0.55f, 0.9f, 1f)
            );
            CreateTutorialSign(
                "Tutorial_Lucarelli",
                parent,
                new Vector2(72f, 2.9f),
                "Lucarelli",
                new Color(1f, 0.5f, 0.5f)
            );
            CreateTutorialSign(
                "Tutorial_ArenaShortcut",
                parent,
                new Vector2(45f, 6.15f),
                "Atalho para o Gate",
                new Color(0.55f, 0.9f, 1f)
            );
            CreateTutorialSign(
                "Tutorial_DashReward",
                parent,
                new Vector2(-130f, 9f),
                "Dash: Shift esquerdo",
                new Color(0.55f, 0.9f, 1f)
            );
            CreateTutorialSign(
                "Tutorial_PostDashParkour",
                parent,
                new Vector2(-158f, 10.4f),
                "Use Shift esquerdo para cruzar vaos maiores",
                new Color(0.55f, 0.9f, 1f)
            );
        }

        private static void CreateDemoDecoration(Transform parent)
        {
            CreateBackgroundDecoration(
                "Hub_Glow",
                parent,
                new Vector2(-95f, 3.1f),
                new Vector2(15f, 11f),
                new Color(0.62f, 0.95f, 0.68f, 0.24f)
            );
            CreateBackgroundDecoration(
                "Hub_AncientTree_Trunk",
                parent,
                new Vector2(-95f, 2.4f),
                new Vector2(4f, 12f),
                new Color(0.16f, 0.23f, 0.14f, 0.92f)
            );
            CreateBackgroundDecoration(
                "Hub_AncientTree_Crown",
                parent,
                new Vector2(-95f, 8.6f),
                new Vector2(14f, 5f),
                new Color(0.18f, 0.34f, 0.2f, 0.76f)
            );
            CreateBackgroundDecoration(
                "Roots_FogSilhouette",
                parent,
                new Vector2(-39f, -5.15f),
                new Vector2(108f, 7f),
                new Color(0.06f, 0.12f, 0.1f, 0.58f)
            );
            CreateBackgroundDecoration(
                "Roots_TunnelShadow_West",
                parent,
                new Vector2(-61f, -5.8f),
                new Vector2(28f, 3.5f),
                new Color(0.03f, 0.08f, 0.07f, 0.48f)
            );
            CreateBackgroundDecoration(
                "Roots_ReturnGlow",
                parent,
                new Vector2(-88f, -2.2f),
                new Vector2(12f, 5f),
                new Color(0.32f, 0.72f, 0.42f, 0.18f)
            );
            CreateBackgroundDecoration(
                "Canopy_FogRibbon",
                parent,
                new Vector2(-31f, 11.35f),
                new Vector2(116f, 3.2f),
                new Color(0.19f, 0.31f, 0.28f, 0.35f)
            );
            CreateBackgroundDecoration(
                "Canopy_TallTrunk_West",
                parent,
                new Vector2(-67f, 5.4f),
                new Vector2(1.8f, 11f),
                new Color(0.12f, 0.2f, 0.11f, 0.74f)
            );
            CreateBackgroundDecoration(
                "Canopy_TallTrunk_Center",
                parent,
                new Vector2(-36f, 6.4f),
                new Vector2(2.2f, 13f),
                new Color(0.12f, 0.2f, 0.11f, 0.74f)
            );
            CreateBackgroundDecoration(
                "Canopy_HighCrown",
                parent,
                new Vector2(-23f, 13.8f),
                new Vector2(36f, 5f),
                new Color(0.17f, 0.36f, 0.18f, 0.5f)
            );
            CreateBackgroundDecoration(
                "DashGate_PrincipalGlow",
                parent,
                new Vector2(-117.5f, 6.35f),
                new Vector2(5f, 7f),
                new Color(0.18f, 0.65f, 0.95f, 0.28f)
            );
            CreateBackgroundDecoration(
                "DashGate_PrincipalPillar_Left",
                parent,
                new Vector2(-119.4f, 6.25f),
                new Vector2(0.6f, 5.8f),
                new Color(0.12f, 0.45f, 0.68f, 0.62f)
            );
            CreateBackgroundDecoration(
                "DashGate_PrincipalPillar_Right",
                parent,
                new Vector2(-115.6f, 6.25f),
                new Vector2(0.6f, 5.8f),
                new Color(0.12f, 0.45f, 0.68f, 0.62f)
            );
            CreateBackgroundDecoration(
                "Shortcut_ArenaToGate_MistTrail",
                parent,
                new Vector2(6f, 6.25f),
                new Vector2(150f, 2f),
                new Color(0.22f, 0.42f, 0.38f, 0.22f)
            );
            CreateBackgroundDecoration(
                "PostDash_MistTrail",
                parent,
                new Vector2(-180f, 9f),
                new Vector2(116f, 2.2f),
                new Color(0.18f, 0.38f, 0.45f, 0.28f)
            );
        }

        private static void CreateDemoEnd(Transform parent, int groundLayer)
        {
            CreateDemoEndMarker(
                "Fim_Demo_Marker",
                parent,
                new Vector2(-251f, 7.85f),
                new Vector2(1.6f, 2.7f),
                groundLayer
            );
            CreateSign(
                "Fim_Demo_Label",
                parent,
                new Vector2(-244f, 10.2f),
                "Fim da demo do Bosque",
                new Color(1f, 0.92f, 0.45f)
            );
            CreateBoundaryWall(
                "Fim_Demo_LeftBoundary",
                parent,
                new Vector2(-257f, 8.5f),
                new Vector2(1f, 10f),
                new Color(0.13f, 0.22f, 0.15f),
                groundLayer
            );
            CreateInvisibleWall(
                "Demo_LeftLimit",
                parent,
                new Vector2(-259f, 8.5f),
                new Vector2(1f, 18f),
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
