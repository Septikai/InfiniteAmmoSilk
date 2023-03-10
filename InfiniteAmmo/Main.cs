using BepInEx;
using HarmonyLib;

namespace InfiniteAmmo
{
    [BepInPlugin(ModGUID, ModName, ModVersion)]
    public class Main : BaseUnityPlugin
    {
        public const string ModName = "InfiniteAmmo";
        public const string ModAuthor  = "Septikai";
        public const string ModGUID = "me.septikai.InfiniteAmmo";
        public const string ModVersion = "1.1.1";
        internal Harmony Harmony;
        
        internal void Awake()
        {
            Harmony = new Harmony(ModGUID);

            Harmony.PatchAll();
            Logger.LogInfo($"{ModName} successfully loaded! Made by {ModAuthor}");
        }
    }

    [HarmonyPatch(typeof(Weapon), nameof(Weapon.ammo), MethodType.Getter)]
    public class InfiniteAmmoPatch
    {
        [HarmonyPrefix]
        public static bool InfiniteAmmo(Weapon __instance, ref float __result)
        {
            __result = __instance.maxAmmo;
            return false;
        }
    }
}
