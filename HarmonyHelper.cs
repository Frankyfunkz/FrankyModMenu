using System.Reflection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;




namespace FrankyModMenu
{
    internal static class HarmonyHelper
    {
        private static HarmonyLib.Harmony harmonyInstance = new(Assembly.GetExecutingAssembly().GetName().Name);

        internal static void PatchMethod(
          this Type originalType,
          string originalMethodName,
          Type newType,
          string newMethodName,
          HarmonyHelper.PatchType patchType)
        {
            AccessTools.Method(originalType, originalMethodName, (Type[])null, (Type[])null).Patch(newType, newMethodName, patchType);
        }

        internal static void PatchMethod(
          this Type originalType,
          string originalMethodName,
          Type[] typeParameters,
          Type newType,
          string newMethodName,
          HarmonyHelper.PatchType patchType)
        {
            AccessTools.Method(originalType, originalMethodName, typeParameters, (Type[])null).Patch(newType, newMethodName, patchType);
        }

        internal static void PatchProperty(
          this Type originalType,
          string originalPropertyName,
          HarmonyHelper.PropertyType propertyType,
          Type newType,
          string newMethodName,
          HarmonyHelper.PatchType patchType)
        {
            AccessTools.Property(originalType, originalPropertyName).Patch(propertyType, newType, newMethodName, patchType);
        }

        internal static void Patch(
          this MethodInfo originalMethod,
          Type newType,
          string newMethodName,
          HarmonyHelper.PatchType patchType)
        {
            HarmonyHelper.harmonyInstance.Patch((MethodBase)originalMethod, patchType == HarmonyHelper.PatchType.Prefix ? new HarmonyMethod(newType, newMethodName, (Type[])null) : (HarmonyMethod)null, patchType == HarmonyHelper.PatchType.Postfix ? new HarmonyMethod(newType, newMethodName, (Type[])null) : (HarmonyMethod)null, (HarmonyMethod)null, (HarmonyMethod)null, (HarmonyMethod)null);
        }

        internal static void Patch(
          this PropertyInfo originalProperty,
          HarmonyHelper.PropertyType propertyType,
          Type newType,
          string newMethodName,
          HarmonyHelper.PatchType patchType)
        {
            HarmonyHelper.harmonyInstance.Patch(propertyType == HarmonyHelper.PropertyType.Getter ? (MethodBase)originalProperty.GetGetMethod() : (MethodBase)originalProperty.GetSetMethod(), patchType == HarmonyHelper.PatchType.Prefix ? new HarmonyMethod(newType, newMethodName, (Type[])null) : (HarmonyMethod)null, patchType == HarmonyHelper.PatchType.Postfix ? new HarmonyMethod(newType, newMethodName, (Type[])null) : (HarmonyMethod)null, (HarmonyMethod)null, (HarmonyMethod)null, (HarmonyMethod)null);
        }

        internal enum PatchType
        {
            Prefix,
            Postfix,
        }

        internal enum PropertyType
        {
            Getter,
            Setter,
        }
    }
}