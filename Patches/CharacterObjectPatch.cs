﻿using System;
using System.Windows.Forms;
using HarmonyLib;
using TaleWorlds.Core;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade;
using TaleWorlds.CampaignSystem;
using TaleWorlds.Localization;
using TaleWorlds.SaveSystem;
using System.Reflection;
using Helpers;

namespace CharacterCreation.Patches
{
    [HarmonyPatch(typeof(CharacterObject), nameof(CharacterObject.UpdatePlayerCharacterBodyProperties))]
    static class CharacterObjectPatch
    {
        private static readonly TextObject HeroUpdatedMsg = new TextObject("{=CharacterCreation_HeroUpdatedMsg}Hero updated: ");

        static void Postfix(CharacterObject __instance, BodyProperties properties, bool isFemale)
        {
            if (__instance.IsHero)
            {
                if (!__instance.IsPlayerCharacter)
                {
                    AccessTools.Property(typeof(Hero), "StaticBodyProperties").SetValue(__instance.HeroObject, properties.StaticProperties);
                    __instance.HeroObject.DynamicBodyProperties = properties.DynamicProperties;
                    __instance.HeroObject.UpdatePlayerGender(isFemale);
                }

                if (DCCSettings.Instance != null && DCCSettings.Instance.DebugMode)
                    InformationManager.DisplayMessage(new InformationMessage(HeroUpdatedMsg.ToString() + __instance.HeroObject.Name, ColorManager.Purple));

                if (DCCSettings.Instance != null && !DCCSettings.Instance.OverrideAge)
                    __instance.HeroObject.BirthDay = HeroHelper.GetRandomBirthDayForAge(properties.DynamicProperties.Age);
            }
        }
    }
}