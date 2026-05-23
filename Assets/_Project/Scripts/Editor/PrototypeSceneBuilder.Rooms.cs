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

        private static DemoLevelBuild CreateDemoRooms(
            Transform parent,
            Transform deathZonesParent,
            int groundLayer
        )
        {
            Color groundColor = new Color(0.18f, 0.3f, 0.18f);
            Color rootsColor = new Color(0.11f, 0.2f, 0.13f);
            Color platformColor = new Color(0.2f, 0.36f, 0.22f);
            Color canopyColor = new Color(0.24f, 0.42f, 0.24f);
            Color wallColor = new Color(0.13f, 0.22f, 0.15f);

            Transform entrance = CreateEmptyGroup("Entrance", parent);
            Transform centralHub = CreateEmptyGroup("CentralHub", parent);
            Transform combatPath = CreateEmptyGroup("CombatPath", parent);
            Transform lowerRoots = CreateEmptyGroup("LowerRoots", parent);
            Transform upperCanopy = CreateEmptyGroup("UpperCanopy", parent);
            Transform lucarelliPath = CreateEmptyGroup("LucarelliPath", parent);
            Transform lucarelliArena = CreateEmptyGroup("LucarelliArena", parent);
            Transform dashReturnGate = CreateEmptyGroup("DashReturnGate", parent);
            Transform postDashArea = CreateEmptyGroup("PostDashArea", parent);
            Transform demoEnd = CreateEmptyGroup("DemoEnd", parent);

            CreateDemoEntrance(entrance, groundColor, platformColor, groundLayer);
            CreateCentralHub(centralHub, groundColor, platformColor, groundLayer);
            CreateCombatPath(combatPath, groundColor, platformColor, groundLayer);
            CreateLowerRoots(lowerRoots, rootsColor, platformColor, groundLayer);
            CreateUpperCanopy(upperCanopy, canopyColor, groundLayer);
            CreateLucarelliPath(lucarelliPath, groundColor, platformColor, groundLayer);
            CreateLucarelliArena(lucarelliArena, groundColor, platformColor, wallColor, groundLayer);
            CreateDashReturnGate(dashReturnGate, platformColor, groundLayer);
            CreatePostDashArea(postDashArea, canopyColor, groundColor, groundLayer);
            CreateDemoEnd(demoEnd, groundLayer);
            CreateDemoDeathZones(deathZonesParent);

            return new DemoLevelBuild(lucarelliArena);
        }

        private static void CreateDemoEntrance(
            Transform parent,
            Color groundColor,
            Color platformColor,
            int groundLayer
        )
        {
            CreateGroundBlock(
                "Entrance_Ground_West",
                parent,
                new Vector2(-158f, -2f),
                new Vector2(28f, 1f),
                groundColor,
                groundLayer
            );
            CreateGroundBlock(
                "Entrance_Tutorial_Clearing",
                parent,
                new Vector2(-130f, -2f),
                new Vector2(28f, 1f),
                groundColor,
                groundLayer
            );
            CreatePlatform(
                "Entrance_Jump_Practice",
                parent,
                new Vector2(-131f, -0.55f),
                new Vector2(5f, 0.5f),
                platformColor,
                groundLayer
            );
            CreateInvisibleWall(
                "Entrance_LeftLimit",
                parent,
                new Vector2(-172.5f, 1.5f),
                new Vector2(1f, 8f),
                groundLayer
            );
        }

        private static void CreateCentralHub(
            Transform parent,
            Color groundColor,
            Color platformColor,
            int groundLayer
        )
        {
            CreateGroundBlock(
                "Hub_MainFloor",
                parent,
                new Vector2(-94f, -2f),
                new Vector2(44f, 1f),
                groundColor,
                groundLayer
            );
            CreatePlatform(
                "Hub_UpperStep_A",
                parent,
                new Vector2(-103f, -0.15f),
                new Vector2(8f, 0.5f),
                platformColor,
                groundLayer
            );
            CreatePlatform(
                "Hub_UpperStep_B",
                parent,
                new Vector2(-95f, 1.8f),
                new Vector2(8f, 0.5f),
                platformColor,
                groundLayer
            );
            CreatePlatform(
                "Hub_UpperStep_C",
                parent,
                new Vector2(-86f, 3.8f),
                new Vector2(8f, 0.5f),
                platformColor,
                groundLayer
            );
            CreateGroundBlock(
                "Hub_CombatMouth",
                parent,
                new Vector2(-66f, -2f),
                new Vector2(12f, 1f),
                groundColor,
                groundLayer
            );
        }

        private static void CreateCombatPath(
            Transform parent,
            Color groundColor,
            Color platformColor,
            int groundLayer
        )
        {
            CreateGroundBlock(
                "Combat_WestFloor",
                parent,
                new Vector2(-59f, -2f),
                new Vector2(22f, 1f),
                groundColor,
                groundLayer
            );
            CreateGroundBlock(
                "Combat_TrainingFloor",
                parent,
                new Vector2(-36f, -2f),
                new Vector2(24f, 1f),
                groundColor,
                groundLayer
            );
            CreateGroundBlock(
                "Combat_EastFloor",
                parent,
                new Vector2(-12f, -2f),
                new Vector2(24f, 1f),
                groundColor,
                groundLayer
            );
            CreatePlatform(
                "Combat_Platform_West",
                parent,
                new Vector2(-47f, 0.15f),
                new Vector2(6f, 0.5f),
                platformColor,
                groundLayer
            );
            CreatePlatform(
                "Combat_Platform_East",
                parent,
                new Vector2(-21f, 0.35f),
                new Vector2(6f, 0.5f),
                platformColor,
                groundLayer
            );
        }

        private static void CreateLowerRoots(
            Transform parent,
            Color rootsColor,
            Color platformColor,
            int groundLayer
        )
        {
            CreatePlatform(
                "Roots_Descent_A",
                parent,
                new Vector2(-100f, -3.75f),
                new Vector2(8f, 0.5f),
                platformColor,
                groundLayer
            );
            CreatePlatform(
                "Roots_Descent_B",
                parent,
                new Vector2(-91f, -5.55f),
                new Vector2(8f, 0.5f),
                platformColor,
                groundLayer
            );
            CreateGroundBlock(
                "Roots_Floor_A",
                parent,
                new Vector2(-82f, -8f),
                new Vector2(22f, 1f),
                rootsColor,
                groundLayer
            );
            CreateGroundBlock(
                "Roots_Floor_B",
                parent,
                new Vector2(-57f, -8f),
                new Vector2(22f, 1f),
                rootsColor,
                groundLayer
            );
            CreateGroundBlock(
                "Roots_Floor_C",
                parent,
                new Vector2(-29f, -8f),
                new Vector2(28f, 1f),
                rootsColor,
                groundLayer
            );
            CreateGroundBlock(
                "Roots_Floor_D",
                parent,
                new Vector2(0f, -8f),
                new Vector2(24f, 1f),
                rootsColor,
                groundLayer
            );
            CreateBoundaryWall(
                "Roots_TunnelCeiling_West",
                parent,
                new Vector2(-61f, -3.4f),
                new Vector2(22f, 0.55f),
                rootsColor,
                groundLayer
            );
            CreateBoundaryWall(
                "Roots_TunnelCeiling_East",
                parent,
                new Vector2(-28f, -3.55f),
                new Vector2(24f, 0.55f),
                rootsColor,
                groundLayer
            );
            CreatePlatform(
                "Roots_ReturnToHub_A",
                parent,
                new Vector2(-69f, -5.95f),
                new Vector2(6f, 0.5f),
                platformColor,
                groundLayer
            );
            CreatePlatform(
                "Roots_ReturnToHub_B",
                parent,
                new Vector2(-78f, -4.25f),
                new Vector2(6f, 0.5f),
                platformColor,
                groundLayer
            );
            CreatePlatform(
                "Roots_ReturnToHub_C",
                parent,
                new Vector2(-88f, -2.55f),
                new Vector2(7f, 0.5f),
                platformColor,
                groundLayer
            );
            CreatePlatform(
                "Roots_ReturnToHub_D",
                parent,
                new Vector2(-97f, -1.8f),
                new Vector2(8f, 0.5f),
                platformColor,
                groundLayer
            );
            CreatePlatform(
                "Roots_Ascent_A",
                parent,
                new Vector2(18f, -6.1f),
                new Vector2(8f, 0.5f),
                platformColor,
                groundLayer
            );
            CreatePlatform(
                "Roots_Ascent_B",
                parent,
                new Vector2(25f, -4.25f),
                new Vector2(8f, 0.5f),
                platformColor,
                groundLayer
            );
            CreatePlatform(
                "Roots_Ascent_C",
                parent,
                new Vector2(31f, -2.55f),
                new Vector2(8f, 0.5f),
                platformColor,
                groundLayer
            );
        }

        private static void CreateUpperCanopy(Transform parent, Color canopyColor, int groundLayer)
        {
            CreatePlatform(
                "Canopy_Entry_Branch",
                parent,
                new Vector2(-74f, 5.3f),
                new Vector2(12f, 0.5f),
                canopyColor,
                groundLayer
            );
            CreatePlatform(
                "Canopy_Climb_A",
                parent,
                new Vector2(-60f, 6.9f),
                new Vector2(12f, 0.5f),
                canopyColor,
                groundLayer
            );
            CreatePlatform(
                "Canopy_High_Branch",
                parent,
                new Vector2(-45f, 8.6f),
                new Vector2(12f, 0.5f),
                canopyColor,
                groundLayer
            );
            CreatePlatform(
                "Canopy_Climb_ToLookout",
                parent,
                new Vector2(-35f, 10.3f),
                new Vector2(9f, 0.5f),
                canopyColor,
                groundLayer
            );
            CreatePlatform(
                "Canopy_HighLookout",
                parent,
                new Vector2(-23f, 12.1f),
                new Vector2(10f, 0.5f),
                canopyColor,
                groundLayer
            );
            CreatePlatform(
                "Canopy_MainCrossing",
                parent,
                new Vector2(-11f, 10.25f),
                new Vector2(10f, 0.5f),
                canopyColor,
                groundLayer
            );
            CreatePlatform(
                "Canopy_Descent_A",
                parent,
                new Vector2(1f, 8.15f),
                new Vector2(10f, 0.5f),
                canopyColor,
                groundLayer
            );
            CreatePlatform(
                "Canopy_Descent_B",
                parent,
                new Vector2(10f, 5.95f),
                new Vector2(10f, 0.5f),
                canopyColor,
                groundLayer
            );
            CreatePlatform(
                "Canopy_Exit_Drop",
                parent,
                new Vector2(15f, 2.9f),
                new Vector2(12f, 0.5f),
                canopyColor,
                groundLayer
            );
            CreatePlatform(
                "Canopy_AltRoute_West",
                parent,
                new Vector2(-70f, 8.55f),
                new Vector2(8f, 0.5f),
                canopyColor,
                groundLayer
            );
            CreatePlatform(
                "Canopy_AltRoute_Mid",
                parent,
                new Vector2(-80f, 7.55f),
                new Vector2(8f, 0.5f),
                canopyColor,
                groundLayer
            );
            CreatePlatform(
                "Canopy_ReturnToHub_Ledge",
                parent,
                new Vector2(-93f, 5.75f),
                new Vector2(10f, 0.5f),
                canopyColor,
                groundLayer
            );
        }

        private static void CreateLucarelliPath(
            Transform parent,
            Color groundColor,
            Color platformColor,
            int groundLayer
        )
        {
            CreateGroundBlock(
                "LucarelliPath_Convergence",
                parent,
                new Vector2(14f, -2f),
                new Vector2(30f, 1f),
                groundColor,
                groundLayer
            );
            CreateGroundBlock(
                "LucarelliPath_Middle",
                parent,
                new Vector2(42f, -2f),
                new Vector2(26f, 1f),
                groundColor,
                groundLayer
            );
            CreateGroundBlock(
                "LucarelliPath_Threshold",
                parent,
                new Vector2(66f, -2f),
                new Vector2(24f, 1f),
                groundColor,
                groundLayer
            );
            CreatePlatform(
                "LucarelliPath_StoneRise_A",
                parent,
                new Vector2(28f, 0.05f),
                new Vector2(6f, 0.5f),
                platformColor,
                groundLayer
            );
            CreatePlatform(
                "LucarelliPath_StoneRise_B",
                parent,
                new Vector2(50f, 0.35f),
                new Vector2(6f, 0.5f),
                platformColor,
                groundLayer
            );
        }

        private static void CreateLucarelliArena(
            Transform parent,
            Color groundColor,
            Color platformColor,
            Color wallColor,
            int groundLayer
        )
        {
            CreatePlatform(
                "Arena_EntryStep",
                parent,
                new Vector2(80f, -0.85f),
                new Vector2(5f, 0.5f),
                platformColor,
                groundLayer
            );
            CreateGroundBlock(
                "Arena_MainFloor",
                parent,
                new Vector2(100f, -2f),
                new Vector2(36f, 1f),
                groundColor,
                groundLayer
            );
            CreateBoundaryWall(
                "Arena_LeftBoundary",
                parent,
                new Vector2(82f, -0.15f),
                new Vector2(0.8f, 2.7f),
                wallColor,
                groundLayer
            );
            CreateBoundaryWall(
                "Arena_RightBoundary",
                parent,
                new Vector2(118f, 0.75f),
                new Vector2(1f, 9f),
                wallColor,
                groundLayer
            );
            CreateInvisibleWall(
                "Demo_RightLimit",
                parent,
                new Vector2(121f, 2f),
                new Vector2(1f, 18f),
                groundLayer
            );
        }

        private static void CreateDashReturnGate(
            Transform parent,
            Color platformColor,
            int groundLayer
        )
        {
            CreatePlatform(
                "DashGate_Approach_A",
                parent,
                new Vector2(-97f, 4.85f),
                new Vector2(12f, 0.5f),
                platformColor,
                groundLayer
            );
            CreatePlatform(
                "DashGate_Approach_B",
                parent,
                new Vector2(-109f, 5.85f),
                new Vector2(12f, 0.5f),
                platformColor,
                groundLayer
            );
            CreatePlatform(
                "DashGate_PrincipalThreshold",
                parent,
                new Vector2(-115f, 5.85f),
                new Vector2(4f, 0.5f),
                platformColor,
                groundLayer
            );
            CreatePlatform(
                "Shortcut_ArenaToGate_Entry",
                parent,
                new Vector2(78f, 0.4f),
                new Vector2(8f, 0.5f),
                platformColor,
                groundLayer
            );
            CreatePlatform(
                "Shortcut_ArenaToGate_A",
                parent,
                new Vector2(62f, 2.8f),
                new Vector2(12f, 0.5f),
                platformColor,
                groundLayer
            );
            CreatePlatform(
                "Shortcut_ArenaToGate_B",
                parent,
                new Vector2(45f, 4.1f),
                new Vector2(14f, 0.5f),
                platformColor,
                groundLayer
            );
            CreatePlatform(
                "Shortcut_ArenaToGate_C",
                parent,
                new Vector2(25f, 4.95f),
                new Vector2(14f, 0.5f),
                platformColor,
                groundLayer
            );
            CreatePlatform(
                "Shortcut_ArenaToGate_D",
                parent,
                new Vector2(5f, 5.15f),
                new Vector2(16f, 0.5f),
                platformColor,
                groundLayer
            );
            CreatePlatform(
                "Shortcut_ArenaToGate_E",
                parent,
                new Vector2(-17f, 5.15f),
                new Vector2(16f, 0.5f),
                platformColor,
                groundLayer
            );
            CreatePlatform(
                "Shortcut_ArenaToGate_F",
                parent,
                new Vector2(-39f, 5.05f),
                new Vector2(16f, 0.5f),
                platformColor,
                groundLayer
            );
            CreatePlatform(
                "Shortcut_ArenaToGate_G",
                parent,
                new Vector2(-61f, 5.05f),
                new Vector2(16f, 0.5f),
                platformColor,
                groundLayer
            );
            CreatePlatform(
                "Shortcut_ArenaToGate_HubLanding",
                parent,
                new Vector2(-80f, 5.05f),
                new Vector2(14f, 0.5f),
                platformColor,
                groundLayer
            );
        }

        private static void CreatePostDashArea(
            Transform parent,
            Color canopyColor,
            Color groundColor,
            int groundLayer
        )
        {
            CreatePlatform(
                "PostDash_GateExit_SafeStart",
                parent,
                new Vector2(-128f, 5.85f),
                new Vector2(18f, 0.5f),
                canopyColor,
                groundLayer
            );
            CreatePlatform(
                "PostDash_DashGap_A_Landing",
                parent,
                new Vector2(-149f, 6.65f),
                new Vector2(12f, 0.5f),
                canopyColor,
                groundLayer
            );
            CreatePlatform(
                "PostDash_DashGap_B_Rest",
                parent,
                new Vector2(-168f, 7.65f),
                new Vector2(14f, 0.5f),
                canopyColor,
                groundLayer
            );
            CreatePlatform(
                "PostDash_DashGap_C_Landing",
                parent,
                new Vector2(-188f, 6.45f),
                new Vector2(14f, 0.5f),
                canopyColor,
                groundLayer
            );
            CreatePlatform(
                "PostDash_DashGap_D_Rest",
                parent,
                new Vector2(-207f, 7.85f),
                new Vector2(12f, 0.5f),
                canopyColor,
                groundLayer
            );
            CreateGroundBlock(
                "PostDash_EndFloor_SafeFinish",
                parent,
                new Vector2(-237f, 6f),
                new Vector2(36f, 1f),
                groundColor,
                groundLayer
            );
        }

        private static void CreateDemoDeathZones(Transform parent)
        {
            CreateDeathZone(
                "DeathZone_RootsGap_A",
                parent,
                new Vector2(-69.5f, -10.25f),
                new Vector2(4f, 1.25f)
            );
            CreateDeathZone(
                "DeathZone_RootsGap_B",
                parent,
                new Vector2(-44.5f, -10.25f),
                new Vector2(4f, 1.25f)
            );
            CreateDeathZone(
                "DeathZone_RootsGap_C",
                parent,
                new Vector2(-13.5f, -10.25f),
                new Vector2(4f, 1.25f)
            );
            CreateDeathZone(
                "DeathZone_PostDashGap_A",
                parent,
                new Vector2(-140f, 4.15f),
                new Vector2(5.5f, 1.5f)
            );
            CreateDeathZone(
                "DeathZone_PostDashGap_B",
                parent,
                new Vector2(-158f, 5.05f),
                new Vector2(5.5f, 1.5f)
            );
            CreateDeathZone(
                "DeathZone_PostDashGap_C",
                parent,
                new Vector2(-178f, 4.95f),
                new Vector2(5.5f, 1.5f)
            );
            CreateDeathZone(
                "DeathZone_PostDashGap_D",
                parent,
                new Vector2(-198f, 5.15f),
                new Vector2(5.5f, 1.5f)
            );
            CreateDeathZone(
                "DeathZone_PostDashGap_E",
                parent,
                new Vector2(-216f, 5.1f),
                new Vector2(5.5f, 1.5f)
            );
            CreateDeathZone(
                "DeathZone_MapBottom",
                parent,
                new Vector2(-75f, -15f),
                new Vector2(400f, 2f)
            );
        }

        private readonly struct DemoLevelBuild
        {
            public readonly Transform LucarelliArena;

            public DemoLevelBuild(Transform lucarelliArena)
            {
                LucarelliArena = lucarelliArena;
            }
        }
    }
}
