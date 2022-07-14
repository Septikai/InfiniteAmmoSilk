using BepInEx;
using HarmonyLib;

namespace InfiniteAmmo
{
    [BepInPlugin(ModName, ModGUID, ModVersion)]
    public class Main : BaseUnityPlugin
    {
        public const string ModName = "InfiniteAmmo";
        public const string ModAuthor  = "Septikai";
        public const string ModGUID = "me.septikai.InfiniteAmmo";
        public const string ModVersion = "1.0.0";
        internal Harmony Harmony;
        internal void Awake()
        {
            // Creating new harmony instance
            Harmony = new Harmony(ModGUID);

            // Applying patches
            Harmony.PatchAll();
            Logger.LogInfo($"{ModName} successfully loaded! Made by {ModAuthor}");
        }
    }
    
    [HarmonyPatch(typeof(VersionNumberTextMesh), nameof(VersionNumberTextMesh.Start))]
    public class VersionNumberTextMeshPatch
    {
        public static void Postfix(VersionNumberTextMesh __instance)
        {
            __instance.textMesh.text += $"\n<color=red>{Main.ModName} v{Main.ModVersion} by {Main.ModAuthor}</color>";
        }
    }

    [HarmonyPatch(typeof(Weapon), nameof(Weapon.ammo), MethodType.Getter)]
    public class InfiniteAmmoPatch
    {
        [HarmonyPrefix]
        public static bool InfiniteAmmo(Weapon __instance, ref float __result)
        {
            var playerCount = LobbyController.instance.GetPlayerCount();
            if (playerCount != 1) return true;
            __result = __instance.maxAmmo;
            return false;
        }
    }
}
