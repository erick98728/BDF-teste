using Tester.World;
using UnityEngine;

namespace Tester.Editor
{
    public static partial class PrototypeSceneBuilder
    {
        private static void CreateDemoTutorial(Transform parent)
        {
            Transform signs = CreateEmptyGroup("Signs", parent);

            CreateTutorialSign(
                "Sign_Entrance_Movement",
                signs,
                new Vector2(-157f, 1.4f),
                "Mover / Pular",
                "Use A/D ou as setas para se mover. Espaço para pular.",
                Color.white,
                new Vector2(10f, 3.2f)
            );
            CreateTutorialSign(
                "Sign_FirstEnemy_Katana",
                signs,
                new Vector2(-62f, 1.25f),
                "Katana",
                "Pressione J para atacar com a katana.",
                new Color(1f, 0.88f, 0.52f),
                new Vector2(8f, 3f)
            );
            CreateTutorialSign(
                "Sign_Checkpoint_ReturnPoint",
                signs,
                new Vector2(-95f, 1.45f),
                "Checkpoint",
                "Checkpoints definem seu ponto de retorno após uma queda ou derrota.",
                new Color(1f, 0.88f, 0.35f),
                new Vector2(8f, 3f)
            );
            CreateTutorialSign(
                "Sign_FirstPit_Caution",
                signs,
                new Vector2(-82f, -5.1f),
                "Cuidado",
                "Cuidado com os buracos. Se cair, você volta ao último checkpoint.",
                new Color(1f, 0.72f, 0.36f),
                new Vector2(10f, 3f)
            );
            CreateTutorialSign(
                "Sign_CentralHub_Paths",
                signs,
                new Vector2(-95f, 4.9f),
                "Hub Central",
                "O Bosque se divide em vários caminhos. Observe os marcos e siga com atenção.",
                new Color(0.85f, 1f, 0.72f),
                new Vector2(12f, 4f)
            );
            CreateTutorialSign(
                "Sign_LowerRoots_Warning",
                signs,
                new Vector2(-91f, -4.75f),
                "Raízes",
                "As raízes escondem quedas e passagens. Avance com cuidado.",
                new Color(0.72f, 0.82f, 1f),
                new Vector2(9f, 3f)
            );
            CreateTutorialSign(
                "Sign_RootsReturn_Hub",
                signs,
                new Vector2(-88f, -1.05f),
                "Retorno ao Hub",
                "Este retorno leva de volta ao Hub Central.",
                new Color(0.72f, 0.96f, 0.68f),
                new Vector2(8f, 3f)
            );
            CreateTutorialSign(
                "Sign_UpperCanopy_Parkour",
                signs,
                new Vector2(-66f, 8.55f),
                "Copas",
                "As copas exigem precisão nos pulos. Suba com calma.",
                new Color(0.72f, 0.96f, 0.68f),
                new Vector2(9f, 3f)
            );
            CreateTutorialSign(
                "Sign_CanopyAltRoute",
                signs,
                new Vector2(-82f, 9.55f),
                "Rota alternativa",
                "Rota alternativa: daqui você pode voltar ao Hub sem refazer todo o caminho.",
                new Color(0.72f, 0.96f, 0.68f),
                new Vector2(8f, 3f)
            );
            CreateTutorialSign(
                "Sign_DashGate_Blocked",
                signs,
                new Vector2(-111f, 8.55f),
                "Falta uma tecnica",
                "Uma força bloqueia o caminho. Talvez uma nova técnica permita atravessar.",
                new Color(0.55f, 0.9f, 1f),
                new Vector2(10f, 3.4f)
            );
            CreateTutorialSign(
                "Sign_LucarelliPath_Prepare",
                signs,
                new Vector2(72f, 2.9f),
                "Lucarelli",
                "Lucarelli guarda a passagem. Prepare-se antes de avançar.",
                new Color(1f, 0.5f, 0.5f),
                new Vector2(10f, 3.2f)
            );
            CreateTutorialSign(
                "Sign_ArenaShortcut_Return",
                signs,
                new Vector2(45f, 6.15f),
                "Atalho para o Gate",
                "Atalho para voltar ao bloqueio visto no Hub.",
                new Color(0.55f, 0.9f, 1f),
                new Vector2(9f, 3f)
            );
            CreateTutorialSign(
                "Sign_PostDash_Parkour",
                signs,
                new Vector2(-158f, 10.4f),
                "Dash",
                "Use Shift esquerdo para cruzar vãos maiores.",
                new Color(0.55f, 0.9f, 1f),
                new Vector2(12f, 3.2f)
            );
            CreateTutorialSign(
                "Sign_DemoEnd_Thanks",
                signs,
                new Vector2(-244f, 9.35f),
                "Fim da demo",
                "Fim da demo. Obrigado por jogar esta versão de teste.",
                new Color(1f, 0.92f, 0.45f),
                new Vector2(10f, 3f),
                4f
            );
        }

        private static void CreateDemoDecoration(Transform parent)
        {
            Transform background = CreateEmptyGroup("Background", parent);
            Transform fog = CreateEmptyGroup("Fog", parent);
            Transform lights = CreateEmptyGroup("Lights", parent);
            Transform landmarks = CreateEmptyGroup("Landmarks", parent);
            Transform regionMarkers = CreateEmptyGroup("RegionMarkers", parent);

            CreateDemoRegionMarkers(regionMarkers);
            CreateDemoBackground(background);
            CreateDemoFog(fog);
            CreateDemoLights(lights);
            CreateDemoLandmarks(landmarks);
        }

        private static void CreateDemoRegionMarkers(Transform parent)
        {
            CreateRegionMarker(
                "Region_Entrance_SoftGreen",
                parent,
                new Vector2(-144f, 1.4f),
                new Vector2(58f, 10f),
                new Color(0.16f, 0.32f, 0.21f, 0.2f)
            );
            CreateRegionMarker(
                "Region_CentralHub_Teal",
                parent,
                new Vector2(-95f, 3.6f),
                new Vector2(54f, 15f),
                new Color(0.1f, 0.35f, 0.34f, 0.22f)
            );
            CreateRegionMarker(
                "Region_LowerRoots_DeepBlue",
                parent,
                new Vector2(-43f, -5.75f),
                new Vector2(120f, 8f),
                new Color(0.08f, 0.08f, 0.22f, 0.3f)
            );
            CreateRegionMarker(
                "Region_UpperCanopy_ColdGreen",
                parent,
                new Vector2(-29f, 9.4f),
                new Vector2(110f, 10f),
                new Color(0.12f, 0.34f, 0.24f, 0.2f)
            );
            CreateRegionMarker(
                "Region_LucarelliPath_Corruption",
                parent,
                new Vector2(40f, 0.7f),
                new Vector2(72f, 11f),
                new Color(0.2f, 0.08f, 0.28f, 0.25f)
            );
            CreateRegionMarker(
                "Region_LucarelliArena_DarkGold",
                parent,
                new Vector2(100f, 1.7f),
                new Vector2(42f, 13f),
                new Color(0.36f, 0.18f, 0.1f, 0.24f)
            );
            CreateRegionMarker(
                "Region_PostDash_CleanCyan",
                parent,
                new Vector2(-184f, 8.6f),
                new Vector2(122f, 8f),
                new Color(0.08f, 0.35f, 0.42f, 0.24f)
            );
        }

        private static void CreateDemoBackground(Transform parent)
        {
            CreateBackgroundDecoration(
                "Entrance_BackTreeLine",
                parent,
                new Vector2(-144f, 2.2f),
                new Vector2(62f, 7f),
                new Color(0.08f, 0.18f, 0.12f, 0.55f)
            );
            CreateBackgroundDecoration(
                "Entrance_Trunk_West",
                parent,
                new Vector2(-161f, 2.4f),
                new Vector2(1.5f, 8.8f),
                new Color(0.08f, 0.14f, 0.1f, 0.78f)
            );
            CreateBackgroundDecoration(
                "Entrance_Trunk_East",
                parent,
                new Vector2(-127f, 2.7f),
                new Vector2(1.2f, 9.5f),
                new Color(0.08f, 0.14f, 0.1f, 0.72f)
            );
            CreateBackgroundDecoration(
                "Combat_BackTreeLine",
                parent,
                new Vector2(-36f, 1f),
                new Vector2(70f, 7.2f),
                new Color(0.07f, 0.16f, 0.11f, 0.48f)
            );
            CreateBackgroundDecoration(
                "Roots_BackRoot_Mass",
                parent,
                new Vector2(-43f, -5.15f),
                new Vector2(118f, 7f),
                new Color(0.05f, 0.08f, 0.13f, 0.66f)
            );
            CreateBackgroundDecoration(
                "Roots_BackRoot_West",
                parent,
                new Vector2(-79f, -5.35f),
                new Vector2(24f, 2.6f),
                new Color(0.1f, 0.08f, 0.18f, 0.62f)
            );
            CreateBackgroundDecoration(
                "Roots_BackRoot_East",
                parent,
                new Vector2(-15f, -5.45f),
                new Vector2(34f, 2.5f),
                new Color(0.1f, 0.08f, 0.18f, 0.62f)
            );
            CreateBackgroundDecoration(
                "Canopy_BackTreeMass",
                parent,
                new Vector2(-31f, 10.3f),
                new Vector2(116f, 8.8f),
                new Color(0.08f, 0.2f, 0.13f, 0.52f)
            );
            CreateBackgroundDecoration(
                "Canopy_TallTrunk_West",
                parent,
                new Vector2(-67f, 5.4f),
                new Vector2(1.8f, 11f),
                new Color(0.08f, 0.15f, 0.09f, 0.8f)
            );
            CreateBackgroundDecoration(
                "Canopy_TallTrunk_Center",
                parent,
                new Vector2(-36f, 6.4f),
                new Vector2(2.2f, 13f),
                new Color(0.08f, 0.15f, 0.09f, 0.8f)
            );
            CreateBackgroundDecoration(
                "Canopy_TallTrunk_East",
                parent,
                new Vector2(5f, 5.8f),
                new Vector2(1.7f, 10.5f),
                new Color(0.08f, 0.15f, 0.09f, 0.72f)
            );
            CreateBackgroundDecoration(
                "Canopy_HighCrown",
                parent,
                new Vector2(-23f, 13.8f),
                new Vector2(42f, 5f),
                new Color(0.15f, 0.37f, 0.2f, 0.52f)
            );
            CreateBackgroundDecoration(
                "LucarelliPath_BackCorruption",
                parent,
                new Vector2(39f, 1.6f),
                new Vector2(76f, 8.5f),
                new Color(0.12f, 0.05f, 0.18f, 0.62f)
            );
            CreateBackgroundDecoration(
                "LucarelliPath_BrokenGuardianShape",
                parent,
                new Vector2(59f, 2.5f),
                new Vector2(5f, 8f),
                new Color(0.18f, 0.08f, 0.2f, 0.66f)
            );
            CreateBackgroundDecoration(
                "Arena_BackWall_Dramatic",
                parent,
                new Vector2(100f, 2.2f),
                new Vector2(40f, 12f),
                new Color(0.18f, 0.07f, 0.12f, 0.68f)
            );
            CreateBackgroundDecoration(
                "Arena_BackSeal_BrokenMemory",
                parent,
                new Vector2(100f, 4.6f),
                new Vector2(12f, 5f),
                new Color(0.58f, 0.32f, 0.12f, 0.28f)
            );
            CreateBackgroundDecoration(
                "PostDash_CleanBackCanopy",
                parent,
                new Vector2(-184f, 8.9f),
                new Vector2(122f, 6f),
                new Color(0.07f, 0.24f, 0.25f, 0.48f)
            );
        }

        private static void CreateDemoFog(Transform parent)
        {
            CreateFogDecoration(
                "Fog_Entrance_Light",
                parent,
                new Vector2(-143f, 0.4f),
                new Vector2(58f, 2.6f),
                new Color(0.55f, 0.75f, 0.68f, 0.1f)
            );
            CreateFogDecoration(
                "Fog_Hub_Light",
                parent,
                new Vector2(-95f, 2.6f),
                new Vector2(42f, 4.2f),
                new Color(0.44f, 0.9f, 0.82f, 0.12f)
            );
            CreateFogDecoration(
                "Fog_Roots_Dense_West",
                parent,
                new Vector2(-71f, -6.4f),
                new Vector2(36f, 4.5f),
                new Color(0.36f, 0.32f, 0.68f, 0.26f)
            );
            CreateFogDecoration(
                "Fog_Roots_Dense_East",
                parent,
                new Vector2(-25f, -6.35f),
                new Vector2(52f, 4.6f),
                new Color(0.36f, 0.32f, 0.68f, 0.25f)
            );
            CreateFogDecoration(
                "Fog_RootsGap_A",
                parent,
                new Vector2(-69.5f, -8.6f),
                new Vector2(8f, 2.2f),
                new Color(0.5f, 0.45f, 0.82f, 0.23f)
            );
            CreateFogDecoration(
                "Fog_RootsGap_B",
                parent,
                new Vector2(-44.5f, -8.55f),
                new Vector2(8f, 2.2f),
                new Color(0.5f, 0.45f, 0.82f, 0.23f)
            );
            CreateFogDecoration(
                "Fog_RootsGap_C",
                parent,
                new Vector2(-13.5f, -8.55f),
                new Vector2(8f, 2.2f),
                new Color(0.5f, 0.45f, 0.82f, 0.23f)
            );
            CreateFogDecoration(
                "Fog_Canopy_LightRibbon",
                parent,
                new Vector2(-31f, 11.35f),
                new Vector2(116f, 3.2f),
                new Color(0.42f, 0.68f, 0.58f, 0.14f)
            );
            CreateFogDecoration(
                "Fog_LucarelliPath_Heavy",
                parent,
                new Vector2(38f, 0.75f),
                new Vector2(72f, 5.2f),
                new Color(0.5f, 0.28f, 0.7f, 0.24f)
            );
            CreateFogDecoration(
                "Fog_Arena_Base",
                parent,
                new Vector2(100f, -0.15f),
                new Vector2(38f, 4f),
                new Color(0.62f, 0.25f, 0.45f, 0.22f)
            );
            CreateFogDecoration(
                "Fog_PostDash_Light",
                parent,
                new Vector2(-184f, 8.4f),
                new Vector2(116f, 2.2f),
                new Color(0.36f, 0.88f, 0.95f, 0.13f)
            );
        }

        private static void CreateDemoLights(Transform parent)
        {
            CreateGuideLight("Light_Entrance_Path", parent, new Vector2(-137f, 0.25f), new Color(0.8f, 1f, 0.72f, 0.42f));
            CreateGuideLight("Light_Hub_ToRoots", parent, new Vector2(-101f, -1.05f), new Color(0.65f, 0.9f, 0.6f, 0.36f));
            CreateGuideLight("Light_Hub_ToCanopy", parent, new Vector2(-92f, 4.45f), new Color(0.65f, 1f, 0.82f, 0.38f));
            CreateGuideLight("Light_Hub_ToLucarelli", parent, new Vector2(-66f, -0.85f), new Color(0.82f, 0.68f, 1f, 0.32f));
            CreateGuideLight("Light_Checkpoint_01", parent, new Vector2(-95f, 0.65f), new Color(1f, 0.92f, 0.38f, 0.45f));
            CreateGuideLight("Light_Checkpoint_02", parent, new Vector2(34f, 0.65f), new Color(1f, 0.92f, 0.38f, 0.42f));
            CreateGuideLight("Light_Checkpoint_03", parent, new Vector2(73f, 0.65f), new Color(1f, 0.92f, 0.38f, 0.42f));
            CreateGuideLight("Light_DashGate_Warning", parent, new Vector2(-117.5f, 8.6f), new Color(0.35f, 0.85f, 1f, 0.55f));
            CreateGuideLight("Light_Arena_MemoryGlow", parent, new Vector2(100f, 4.8f), new Color(0.9f, 0.45f, 0.2f, 0.38f));
            CreateGuideLight("Light_PostDash_Path", parent, new Vector2(-151f, 8.4f), new Color(0.38f, 0.95f, 1f, 0.38f));
            CreateGuideLight("Light_DemoEnd_Beacon", parent, new Vector2(-244f, 9f), new Color(1f, 0.95f, 0.45f, 0.5f));
        }

        private static void CreateDemoLandmarks(Transform parent)
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
                "Hub_LuminousStone",
                parent,
                new Vector2(-94f, -0.65f),
                new Vector2(2.3f, 1.4f),
                new Color(0.55f, 1f, 0.76f, 0.45f)
            );
            CreateBackgroundDecoration(
                "Roots_ReturnGlow",
                parent,
                new Vector2(-88f, -2.2f),
                new Vector2(12f, 5f),
                new Color(0.32f, 0.72f, 0.42f, 0.18f)
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
                "DashGate_PrincipalSeal",
                parent,
                new Vector2(-117.5f, 6.4f),
                new Vector2(2.6f, 3.5f),
                new Color(0.08f, 0.75f, 0.95f, 0.32f)
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
            CreateBackgroundDecoration(
                "DemoEnd_BeaconColumn",
                parent,
                new Vector2(-244f, 8.6f),
                new Vector2(1.1f, 5.6f),
                new Color(0.95f, 0.88f, 0.32f, 0.55f)
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
            string label,
            string message,
            Color color,
            Vector2 triggerSize,
            float messageDuration = 3.25f
        )
        {
            GameObject sign = new GameObject(name);
            sign.transform.SetParent(parent);
            sign.transform.position = position;

            ConfigureBoxCollider2D(sign, triggerSize, true);

            TutorialSign tutorialSign = sign.AddComponent<TutorialSign>();
            tutorialSign.Configure(message, true, messageDuration);

            GameObject plate = new GameObject("VisualPlate");
            plate.transform.SetParent(sign.transform);
            plate.transform.localPosition = Vector3.zero;
            ConfigurePlaceholderSprite(plate, new Color(color.r, color.g, color.b, 0.68f), new Vector2(1.7f, 0.8f));
            SpriteRenderer plateRenderer = plate.GetComponent<SpriteRenderer>();
            plateRenderer.sortingOrder = 4;

            GameObject lantern = new GameObject("Lantern");
            lantern.transform.SetParent(sign.transform);
            lantern.transform.localPosition = new Vector3(-1.1f, 0.05f, 0f);
            ConfigurePlaceholderSprite(lantern, new Color(color.r, color.g, color.b, 0.5f), new Vector2(0.45f, 1.2f));
            SpriteRenderer lanternRenderer = lantern.GetComponent<SpriteRenderer>();
            lanternRenderer.sortingOrder = 3;

            GameObject labelObject = new GameObject("Label");
            labelObject.transform.SetParent(sign.transform);
            labelObject.transform.localPosition = new Vector3(0f, 0.95f, 0f);

            TextMesh textMesh = labelObject.AddComponent<TextMesh>();
            textMesh.text = label;
            textMesh.font = GetLegacyFont();
            textMesh.fontSize = 42;
            textMesh.characterSize = 0.075f;
            textMesh.anchor = TextAnchor.MiddleCenter;
            textMesh.alignment = TextAlignment.Center;
            textMesh.color = color;
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
            spriteRenderer.sortingOrder = -20;
        }

        private static void CreateRegionMarker(
            string name,
            Transform parent,
            Vector2 position,
            Vector2 size,
            Color color
        )
        {
            GameObject marker = CreateWorldBlock(name, parent, position, size, color, 0);
            SpriteRenderer spriteRenderer = marker.GetComponent<SpriteRenderer>();
            spriteRenderer.sortingOrder = -30;
        }

        private static void CreateFogDecoration(
            string name,
            Transform parent,
            Vector2 position,
            Vector2 size,
            Color color
        )
        {
            GameObject fog = CreateWorldBlock(name, parent, position, size, color, 0);
            SpriteRenderer spriteRenderer = fog.GetComponent<SpriteRenderer>();
            spriteRenderer.sortingOrder = -8;
        }

        private static void CreateGuideLight(
            string name,
            Transform parent,
            Vector2 position,
            Color color
        )
        {
            CreateLightDecoration(name + "_Glow", parent, position, new Vector2(3f, 3f), color);
            CreateLightDecoration(
                name + "_Core",
                parent,
                position,
                new Vector2(0.55f, 1.05f),
                new Color(color.r, color.g, color.b, Mathf.Min(color.a + 0.18f, 0.72f))
            );
        }

        private static void CreateLightDecoration(
            string name,
            Transform parent,
            Vector2 position,
            Vector2 size,
            Color color
        )
        {
            GameObject light = CreateWorldBlock(name, parent, position, size, color, 0);
            SpriteRenderer spriteRenderer = light.GetComponent<SpriteRenderer>();
            spriteRenderer.sortingOrder = -12;
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
