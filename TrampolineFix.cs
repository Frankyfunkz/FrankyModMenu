using RedLoader;
using Harmony;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace FrankyModMenu
{
    
    public class TrampolineFix : Module
    {
        public override void OnPatch()
        {
            Type.GetType("Il2CppInterop.HarmonySupport.Il2CppDetourMethodPatcher, Il2CppInterop.HarmonySupport").PatchMethod("ReportException", GetType(), "TrampolinePatch", HarmonyHelper.PatchType.Prefix);
        }

        private static bool TrampolinePatch(Exception __0)
        {
            RLog.Error((object)__0);
            return false;
        }
    }
}
