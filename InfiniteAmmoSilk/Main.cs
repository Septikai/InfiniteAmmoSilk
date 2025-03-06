using Silk;
using Logger = Silk.Logger; // Alias for Silk.Logger to Logger
using HarmonyLib; // Library for runtime method patching
using UnityEngine; // Unity's core namespace

namespace InfiniteAmmo
{
    [SilkMod("InfiniteAmmo", new[] { "Septikai" }, "1.4.0", "0.4.0", "infinite-ammo-silk")]
    public class Main : SilkMod
    {
        public override void Initialize()
        {
            // your manifest is the mw.mod.toml file
            // use Metadata to access the values you provided in the manifest. Manifest is also available, and provides the other data such as your dependencies and incompats
            Logger.LogInfo("Loading InfiniteAmmo 1.4.0 by Septikai!");
            
            // this section currently patches any [HarmonyPatch]s you use, like the one named NoMoreLaserCubes below. if you don't patch anything, you can remove these
            // you should keep the logging messages as they help users and developers with debugging
            Logger.LogInfo("Setting up patcher...");
            Harmony harmony = new Harmony("me.septikai.infiniteammo"); 
            Logger.LogInfo("Patching...");
            harmony.PatchAll();
            Logger.LogInfo("Patches applied!");
        }

        public void Awake()
        {
            Logger.LogInfo("Loaded InfiniteAmmo!");
        }

        public override void Unload()
        {
            Logger.LogInfo("Unloaded InfiniteAmmo!");
        }
    }

    [HarmonyPatch(typeof(Weapon), nameof(Weapon.ammo), MethodType.Getter)]
    internal class InfiniteAmmoPatch
    {
        public static bool Prefix(Weapon __instance, ref float __result)
        {
            if (__instance is DeathCube) return true;
            __result = __instance.maxAmmo;
            return false;
        }
    }
    
    [HarmonyPatch(typeof(Flare), nameof(Flare.FixedUpdate))]
    internal class InfiniteFlarePatch
    {
        public static void Prefix(Flare __instance, ref float ____burnsStart)
        {
            ____burnsStart = Time.time + __instance.ammo;
        }
    }
}
