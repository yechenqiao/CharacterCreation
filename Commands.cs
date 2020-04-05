﻿using System;
using System.Collections.Generic;
using System.Reflection;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.SandBox.CampaignBehaviors;
using TaleWorlds.Core;
using TaleWorlds.Library;

namespace CharacterCreation
{
    public static class Commands
    {
        [CommandLineFunctionality.CommandLineArgumentFunction("toggle", "dcc")]
        public static string Override(List<string> strings)
        {
            if (Campaign.Current == null)
            {
                //InformationManager.DisplayMessage(new InformationMessage("Campaign not loaded.", Color.FromUint(4282569842U)));
                TaleWorlds.Core.FaceGen.ShowDebugValues = false; // Enable developer facegen
                return "Detailed Character Creation disabled.";
            }
            else
            {
                TaleWorlds.Core.FaceGen.ShowDebugValues = true; // Enable developer facegen
                return "You have enabled Detailed Character Creation. Press V to access.";
            }
        }
    }
}
