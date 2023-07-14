using BepInEx;
using HarmonyLib;
using UnityEngine;

namespace InfiniteAmmo
{
    [BepInPlugin(ModGUID, ModName, ModVersion)]
    public class Main : BaseUnityPlugin
    {
        public const string ModName = "InfiniteAmmo";
        public const string ModAuthor  = "Septikai";
        public const string ModGUID = "me.septikai.InfiniteAmmo";
        public const string ModVersion = "1.2.0";
        internal Harmony Harmony;
        
        internal void Awake()
        {
            Harmony = new Harmony(ModGUID);

            Harmony.PatchAll();
            Logger.LogInfo($"{ModName} successfully loaded! Made by {ModAuthor}");
        }
    }

    [HarmonyPatch(typeof(Weapon), nameof(Weapon.ammo), MethodType.Getter)]
    internal class InfiniteAmmoPatch
    {
        [HarmonyPrefix]
        public static bool InfiniteAmmo(Weapon __instance, ref float __result)
        {
            __result = __instance.maxAmmo;
            return false;
        }
    }
    
    [HarmonyPatch(typeof(Flare), nameof(Flare.FixedUpdate))]
    internal class InfiniteFlarePatch
    {
        [HarmonyPrefix]
        public static void InfiniteFlarePostfix(Flare __instance)
        {
            __instance._burnsStart = Time.time + __instance.ammo;
        }
    }
}
