using UnityEngine;

namespace Tester.Editor
{
    public static partial class PrototypeSceneBuilder
    {
        private static void CreatePrototypeLevel(Transform parent, int groundLayer)
        {
            CreateGroundBlock(
                "Ground_Main",
                parent,
                new Vector2(0f, -2f),
                new Vector2(28f, 1f),
                new Color(0.18f, 0.3f, 0.18f),
                groundLayer
            );
            CreatePlatform(
                "Platform_Low",
                parent,
                new Vector2(-4f, -0.15f),
                new Vector2(3.5f, 0.45f),
                new Color(0.2f, 0.36f, 0.22f),
                groundLayer
            );
            CreatePlatform(
                "Platform_Arena",
                parent,
                new Vector2(7.5f, -0.1f),
                new Vector2(3.2f, 0.45f),
                new Color(0.2f, 0.36f, 0.22f),
                groundLayer
            );
            CreateGroundBlock(
                "Ground_Exit",
                parent,
                new Vector2(17f, -2f),
                new Vector2(6f, 1f),
                new Color(0.18f, 0.3f, 0.18f),
                groundLayer
            );
        }

        private static void CreateDemoRooms(Transform parent, int groundLayer)
        {
            Color groundColor = new Color(0.18f, 0.3f, 0.18f);
            Color platformColor = new Color(0.2f, 0.36f, 0.22f);
            Color wallColor = new Color(0.13f, 0.22f, 0.15f);

            CreateDemoEntrance(parent, groundColor, wallColor, groundLayer);
            CreateDemoMovementTutorial(parent, groundColor, platformColor, groundLayer);
            CreateDemoCombatRoom(parent, groundColor, groundLayer);
            CreateDemoParkourRoom(parent, groundColor, platformColor, groundLayer);
            CreateDemoDashBifurcation(parent, groundColor, platformColor, groundLayer);
            CreateDemoBossPath(parent, groundColor, groundLayer);
            CreateDemoArena(parent, groundColor, wallColor, groundLayer);
            CreateDemoPostDashRoom(parent, groundColor, platformColor, groundLayer);
            CreateDemoSafetyDeathZones(parent);
        }

        private static void CreateDemoEntrance(
            Transform parent,
            Color groundColor,
            Color wallColor,
            int groundLayer
        )
        {
            Transform entrance = CreateEmptyGroup("01_Entrada_Bosque", parent);
            CreateGroundBlock(
                "Entry_Ground",
                entrance,
                new Vector2(-19f, -2f),
                new Vector2(14f, 1f),
                groundColor,
                groundLayer
            );
            CreateBoundaryWall(
                "Entry_LeftBoundary",
                entrance,
                new Vector2(-26.5f, -0.25f),
                new Vector2(1f, 8f),
                wallColor,
                groundLayer
            );
        }

        private static void CreateDemoMovementTutorial(
            Transform parent,
            Color groundColor,
            Color platformColor,
            int groundLayer
        )
        {
            Transform movementTutorial = CreateEmptyGroup("02_Tutorial_Movement", parent);
            CreateGroundBlock(
                "Tutorial_Ground_A",
                movementTutorial,
                new Vector2(-8.5f, -2f),
                new Vector2(6f, 1f),
                groundColor,
                groundLayer
            );
            CreatePlatform(
                "Tutorial_Platform_Low",
                movementTutorial,
                new Vector2(-2.5f, -0.95f),
                new Vector2(3.4f, 0.5f),
                platformColor,
                groundLayer
            );
            CreateGroundBlock(
                "Tutorial_Ground_B",
                movementTutorial,
                new Vector2(2.9f, -2f),
                new Vector2(6.8f, 1f),
                groundColor,
                groundLayer
            );
        }

        private static void CreateDemoCombatRoom(
            Transform parent,
            Color groundColor,
            int groundLayer
        )
        {
            Transform combat = CreateEmptyGroup("03_Combat_Basic", parent);
            CreateGroundBlock(
                "Combat_Ground",
                combat,
                new Vector2(12.5f, -2f),
                new Vector2(13f, 1f),
                groundColor,
                groundLayer
            );
            CreateGroundBlock(
                "Checkpoint01_Ground",
                combat,
                new Vector2(22f, -2f),
                new Vector2(6f, 1f),
                groundColor,
                groundLayer
            );
        }

        private static void CreateDemoParkourRoom(
            Transform parent,
            Color groundColor,
            Color platformColor,
            int groundLayer
        )
        {
            Transform parkour = CreateEmptyGroup("04_Parkour_Basic", parent);
            CreateGroundBlock(
                "Parkour_Start",
                parkour,
                new Vector2(27f, -2f),
                new Vector2(4f, 1f),
                groundColor,
                groundLayer
            );
            CreatePlatform(
                "Parkour_Platform_01",
                parkour,
                new Vector2(31.5f, -0.8f),
                new Vector2(3.2f, 0.5f),
                platformColor,
                groundLayer
            );
            CreatePlatform(
                "Parkour_Platform_02",
                parkour,
                new Vector2(36.5f, 0.2f),
                new Vector2(3.2f, 0.5f),
                platformColor,
                groundLayer
            );
            CreatePlatform(
                "Parkour_Rest_Platform",
                parkour,
                new Vector2(42f, -0.45f),
                new Vector2(4.4f, 0.5f),
                platformColor,
                groundLayer
            );
            CreatePlatform(
                "Parkour_Platform_04",
                parkour,
                new Vector2(47.5f, -1f),
                new Vector2(3.4f, 0.5f),
                platformColor,
                groundLayer
            );
        }

        private static void CreateDemoDashBifurcation(
            Transform parent,
            Color groundColor,
            Color platformColor,
            int groundLayer
        )
        {
            Transform bifurcation = CreateEmptyGroup("05_Bifurcacao_Dash", parent);
            CreateGroundBlock(
                "Bifurcation_Ground",
                bifurcation,
                new Vector2(54f, -2f),
                new Vector2(10f, 1f),
                groundColor,
                groundLayer
            );
            CreatePlatform(
                "DashBranch_Step",
                bifurcation,
                new Vector2(57.2f, -0.35f),
                new Vector2(3f, 0.5f),
                platformColor,
                groundLayer
            );
            CreatePlatform(
                "DashBranch_GateApproach",
                bifurcation,
                new Vector2(60.2f, 0.1f),
                new Vector2(3.2f, 0.5f),
                platformColor,
                groundLayer
            );
        }

        private static void CreateDemoBossPath(
            Transform parent,
            Color groundColor,
            int groundLayer
        )
        {
            Transform bossPath = CreateEmptyGroup("06_Caminho_Lucarelli", parent);
            CreateGroundBlock(
                "BossPath_Ground_A",
                bossPath,
                new Vector2(63.5f, -2f),
                new Vector2(9f, 1f),
                groundColor,
                groundLayer
            );
            CreateGroundBlock(
                "BossPath_Ground_B",
                bossPath,
                new Vector2(70f, -2f),
                new Vector2(4f, 1f),
                groundColor,
                groundLayer
            );
        }

        private static void CreateDemoArena(
            Transform parent,
            Color groundColor,
            Color wallColor,
            int groundLayer
        )
        {
            Transform arena = CreateEmptyGroup("07_Arena_Lucarelli", parent);
            CreateGroundBlock(
                "Arena_Ground",
                arena,
                new Vector2(83f, -2f),
                new Vector2(22f, 1f),
                groundColor,
                groundLayer
            );
            CreateBoundaryWall(
                "Arena_LeftBoundary",
                arena,
                new Vector2(72f, -0.25f),
                new Vector2(0.8f, 2.5f),
                wallColor,
                groundLayer
            );
            CreateBoundaryWall(
                "Arena_RightBoundary",
                arena,
                new Vector2(94f, -0.25f),
                new Vector2(0.8f, 2.5f),
                wallColor,
                groundLayer
            );
        }

        private static void CreateDemoPostDashRoom(
            Transform parent,
            Color groundColor,
            Color platformColor,
            int groundLayer
        )
        {
            Transform postDash = CreateEmptyGroup("08_Area_PosDash", parent);
            CreatePlatform(
                "PostDash_GateExit",
                postDash,
                new Vector2(67f, 0.1f),
                new Vector2(8f, 0.5f),
                platformColor,
                groundLayer
            );
            CreatePlatform(
                "PostDash_OverArena_01",
                postDash,
                new Vector2(76f, 0.9f),
                new Vector2(4f, 0.5f),
                platformColor,
                groundLayer
            );
            CreatePlatform(
                "PostDash_OverArena_02",
                postDash,
                new Vector2(84f, 1.05f),
                new Vector2(8f, 0.5f),
                platformColor,
                groundLayer
            );
            CreatePlatform(
                "PostDash_DashStrip_A",
                postDash,
                new Vector2(94f, 1.6f),
                new Vector2(6f, 0.5f),
                platformColor,
                groundLayer
            );
            CreatePlatform(
                "PostDash_DashStrip_B",
                postDash,
                new Vector2(104f, 1.6f),
                new Vector2(6f, 0.5f),
                platformColor,
                groundLayer
            );
            CreateGroundBlock(
                "PostDash_Exit_Ground",
                postDash,
                new Vector2(112f, -2f),
                new Vector2(10f, 1f),
                groundColor,
                groundLayer
            );
        }

        private static void CreateDemoSafetyDeathZones(Transform parent)
        {
            Transform safety = CreateEmptyGroup("09_Safety_DeathZones", parent);
            CreateDeathZone(
                "DeathZone_TutorialGap",
                safety,
                new Vector2(-11.75f, -3.6f),
                new Vector2(1.4f, 1f)
            );
            CreateDeathZone(
                "DeathZone_Parkour",
                safety,
                new Vector2(38f, -3.6f),
                new Vector2(24f, 1f)
            );
            CreateDeathZone(
                "DeathZone_PostDashGap",
                safety,
                new Vector2(99f, -3.6f),
                new Vector2(4.5f, 1f)
            );
            CreateDeathZone(
                "DeathZone_MapBottom",
                safety,
                new Vector2(46f, -8f),
                new Vector2(150f, 1.5f)
            );
        }
    }
}
