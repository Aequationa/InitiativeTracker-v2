using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;

namespace InitiativeTracker
{
    public static class FileParser
    {
        // === Parse File to Object ===
        private static Settings ReadSettings(string[] text, Action<string> AddError, Func<int> GetErrorCount) {
            Settings settings = new Settings();

            if(text.Length != 7) {
                AddError("[Settings] Bad Number of Lines");
                return settings;
            }

            // ColoringType
            string defaultColoringTypeString = "defaultColoringType=";
            if (!text[0].StartsWith(defaultColoringTypeString)) {
                AddError("[Settings] Argument not found: defaultColoringType");
            }
            settings.defaultColoringType = text[0].Substring(defaultColoringTypeString.Length);

            // Remove
            string defaultRemoveString = "defaultRemove=";
            if (!text[1].StartsWith(defaultRemoveString)) {
                AddError("[Settings] Argument not found: defaultRemove");
            }
            else {
                var defaultRemove = ObjectParser.GetBoolean(text[1].Substring(defaultRemoveString.Length));
                if (!defaultRemove.HasValue) {
                    AddError("[Settings] Unable to Interpret: defaultRemove");
                }
                else {
                    settings.defaultRemove = defaultRemove.Value;
                }
            }

            // Type
            string defaultTypeString = "defaultType=";
            if(!text[2].StartsWith(defaultTypeString)) {
                AddError("[Settings] Argument not found: defaultType");
            }
            else {
                var defaultType = ObjectParser.GetInt10(text[2], defaultTypeString.Length, text[2].Length);
                if (!defaultType.HasValue) {
                    AddError("[Settings] Unable to Interpret: defaultType");
                }
                else {
                    settings.defaultType = defaultType.Value;
                }
            }

            // Tab Width
            string tabWidthString = "tabWidth=";
            if(!text[3].StartsWith(tabWidthString)) {
                AddError("[Settings] Argument not found: tabWidth");
            }
            else {
                var tabWidth = ObjectParser.GetInt10(text[3], tabWidthString.Length, text[3].Length);
                if (!tabWidth.HasValue) {
                    AddError("[Settings] Unable to Interpret: tabWidth");
                }
                else {
                    settings.tabWidth = tabWidth.Value;
                }
            }
            

            // Resive Console
            var resizeConsoleString = "resizeConsole=";
            if (!text[4].StartsWith(resizeConsoleString)) {
                AddError("[Settings] Argument not found: resizeConsole");
            }
            else {
                var resizeConsole = ObjectParser.GetBoolean(text[4].Substring(resizeConsoleString.Length));
                if (!resizeConsole.HasValue) {
                    AddError("[Settings] Unable to Interpret: resizeConsole");
                }
                else {
                    settings.resizeConsole = resizeConsole.Value;
                }
            }

            // Resive Buffer
            var resizeBufferString = "resizeBuffer=";
            if (!text[5].StartsWith(resizeBufferString)) {
                AddError("[Settings] Argument not found: resizeBuffer");
            }
            else {
                var resizeBuffer = ObjectParser.GetBoolean(text[5].Substring(resizeBufferString.Length));
                if (!resizeBuffer.HasValue) {
                    AddError("[Settings] Unable to Interpret: resizeBuffer");
                }
                else {
                    settings.resizeBuffer = resizeBuffer.Value;
                }
            }

            // Use Special
            var useSpecialString = "useSpecial=";
            if (!text[6].StartsWith(useSpecialString)) {
                AddError("[Settings] Argument not found: useSpecial");
            }
            else {
                var useSpecial = ObjectParser.GetBoolean(text[6].Substring(useSpecialString.Length));
                if (!useSpecial.HasValue) {
                    AddError("[Settings] Unable to Interpret: useSpecial");
                }
                else {
                    settings.useSpecial = useSpecial.Value;
                }
            }

            return settings;
        }

        private static ColoringType ReadColoringType(XElement element, Action<string> AddError, Func<int> GetErrorCount) {
            ColoringType coloringType = new ColoringType();

            bool name_set = false;
            bool base_text_set = false;
            bool base_bg_set = false;
            bool active_text_set = false;
            bool active_bg_set = false;

            using (var attributeEnum = element.Attributes().GetEnumerator()) {
                while (attributeEnum.MoveNext()) {
                    var attributeName = attributeEnum.Current.Name.ToString();
                    var attributeNameLower = attributeName.ToLowerInvariant();
                    var attributeValue = attributeEnum.Current.Value;

                    if (attributeNameLower == "name") {
                        if (name_set) {
                            AddError("[Coloring] Attribute found more than once: " + attributeName);
                            return coloringType;
                        }
                        else {
                            coloringType.name = ObjectParser.FormatLine(attributeValue);
                            name_set = true;
                        }
                    }
                    else if (attributeNameLower == "base_text") {
                        if (base_text_set) {
                            AddError("[Coloring] Attribute found more than once: " + attributeName);
                        }
                        else {
                            var consoleColor = ObjectParser.GetConsoleColor(attributeValue);
                            if (!consoleColor.HasValue) {
                                AddError("[Coloring] Unable to Interpret: " + attributeName);
                            }
                            else {
                                coloringType.base_text = consoleColor.Value;
                                base_text_set = true;
                            }
                        }
                    }
                    else if (attributeNameLower == "base_bg") {
                        if (base_bg_set) {
                            AddError("[Coloring] Attribute found more than once: " + attributeName);
                        }
                        else {
                            var consoleColor = ObjectParser.GetConsoleColor(attributeValue);
                            if (!consoleColor.HasValue) {
                                AddError("[Coloring] Unable to Interpret: " + attributeName);
                            }
                            else {
                                coloringType.base_bg = consoleColor.Value;
                                base_bg_set = true;
                            }
                        }
                    }
                    else if (attributeNameLower == "active_text") {
                        if (active_text_set) {
                            AddError("[Coloring] Attribute found more than once: " + attributeName);
                        }
                        else {
                            var consoleColor = ObjectParser.GetConsoleColor(attributeValue);
                            if (!consoleColor.HasValue) {
                                AddError("[Coloring] Unable to Interpret: " + attributeName);
                            }
                            else {
                                coloringType.active_text = consoleColor.Value;
                                active_text_set = true;
                            }
                        }
                    }
                    else if (attributeNameLower == "active_bg") {
                        if (active_bg_set) {
                            AddError("[Coloring] Attribute found more than once: " + attributeName);
                        }
                        else {
                            var consoleColor = ObjectParser.GetConsoleColor(attributeValue);
                            if (!consoleColor.HasValue) {
                                AddError("[Coloring] Unable to Interpret: " + attributeName);
                            }
                            else {
                                coloringType.active_bg = consoleColor.Value;
                                active_bg_set = true;
                            }
                        }
                    }
                    else {
                        AddError("[Coloring] Attribute Name not expected: " + attributeName);
                    }
                }
            }
            if (!name_set) {
                AddError("[Coloring] Attribute not found: name");
            }
            if (!base_text_set) {
                AddError("[Coloring] Attribute not found: base_text");
            }
            if (!base_bg_set) {
                AddError("[Coloring] Attribute not found: base_bg");
            }
            if (!active_text_set) {
                AddError("[Coloring] Attribute not found: active_text");
            }
            if (!active_bg_set) {
                AddError("[Coloring] Attribute not found: active_bg");
            }

            return coloringType;
        }

        private static Coloring ReadColoring(XElement element, Action<string> AddError, Func<int> GetErrorCount) {
            Coloring coloring = new Coloring();

            bool base_text_set = false;
            bool base_bg_set = false;
            bool active_text_set = false;
            bool active_bg_set = false;

            using (var attributeEnum = element.Attributes().GetEnumerator()) {
                while (attributeEnum.MoveNext()) {
                    var attributeName = attributeEnum.Current.Name.ToString();
                    var attributeNameLower = attributeName.ToLowerInvariant();
                    var attributeValue = attributeEnum.Current.Value;

                    if (attributeNameLower == "base_text") {
                        if (base_text_set) {
                            AddError("[Coloring] Attribute found more than once: " + attributeName);
                        }
                        else {
                            var consoleColor = ObjectParser.GetConsoleColor(attributeValue);
                            if (!consoleColor.HasValue) {
                                AddError("[Coloring] Unable to Interpret: " + attributeName);
                            }
                            else {
                                coloring.base_text = consoleColor.Value;
                                base_text_set = true;
                            }
                        }
                    }
                    else if (attributeNameLower == "base_bg") {
                        if (base_bg_set) {
                            AddError("[Coloring] Attribute found more than once: " + attributeName);
                        }
                        else {
                            var consoleColor = ObjectParser.GetConsoleColor(attributeValue);
                            if (!consoleColor.HasValue) {
                                AddError("[Coloring] Unable to Interpret: " + attributeName);
                            }
                            else {
                                coloring.base_bg = consoleColor.Value;
                                base_bg_set = true;
                            }
                        }
                    }
                    else if (attributeNameLower == "active_text") {
                        if (active_text_set) {
                            AddError("[Coloring] Attribute found more than once: " + attributeName);
                        }
                        else {
                            var consoleColor = ObjectParser.GetConsoleColor(attributeValue);
                            if (!consoleColor.HasValue) {
                                AddError("[Coloring] Unable to Interpret: " + attributeName);
                            }
                            else {
                                coloring.active_text = consoleColor.Value;
                                active_text_set = true;
                            }
                        }
                    }
                    else if (attributeNameLower == "active_bg") {
                        if (active_bg_set) {
                            AddError("[Coloring] Attribute found more than once: " + attributeName);
                        }
                        else {
                            var consoleColor = ObjectParser.GetConsoleColor(attributeValue);
                            if (!consoleColor.HasValue) {
                                AddError("[Coloring] Unable to Interpret: " + attributeName);
                            }
                            else {
                                coloring.active_bg = consoleColor.Value;
                                active_bg_set = true;
                            }
                        }
                    }
                    else {
                        AddError("[Coloring] Attribute Name not expected: " + attributeName);
                    }
                }
            }
            if (!base_text_set) {
                AddError("[Coloring] Attribute not found: base_text");
            }
            if (!base_bg_set) {
                AddError("[Coloring] Attribute not found: base_bg");
            }
            if (!active_text_set) {
                AddError("[Coloring] Attribute not found: active_text");
            }
            if (!active_bg_set) {
                AddError("[Coloring] Attribute not found: active_bg");
            }

            return coloring;
        }

        private static AbstractScores ReadScores(XElement element, Action<string> AddError, Func<int> GetErrorCount) {
            AbstractScores scores = new AbstractScores();

            bool strength_set = false;
            bool dexterity_set = false;
            bool constitution_set = false;
            bool intelligence_set = false;
            bool wisdom_set = false;
            bool charisma_set = false;

            using (var attributeEnum = element.Attributes().GetEnumerator()) {
                while (attributeEnum.MoveNext()) {
                    var attributeName = attributeEnum.Current.Name.ToString();
                    var attributeNameLower = attributeName.ToLowerInvariant();
                    var attributeValue = attributeEnum.Current.Value;

                    if (attributeNameLower == "str") {
                        if (strength_set) {
                            AddError("[Scores] Attribute found more than once: " + attributeName);
                        }
                        else {
                            var tokens = ObjectParser.GetTokens(attributeValue);
                            if (!tokens.HasValue || !tokens.Value.Validate()) {
                                AddError("[Scores] Unable to Interpret: " + attributeName);
                            }
                            else {
                                scores.strength = tokens.Value;
                                strength_set = true;
                            }
                        }
                    }
                    else if (attributeNameLower == "dex") {
                        if (dexterity_set) {
                            AddError("[Scores] Attribute found more than once: " + attributeName);
                        }
                        else {
                            var tokens = ObjectParser.GetTokens(attributeValue);
                            if (!tokens.HasValue || !tokens.Value.Validate()) {
                                AddError("[Scores] Unable to Interpret: " + attributeName);
                            }
                            else {
                                scores.dexterity = tokens.Value;
                                dexterity_set = true;
                            }
                        }
                    }
                    else if(attributeNameLower == "con") {
                        if (constitution_set) {
                            AddError("[Scores] Attribute found more than once: " + attributeName);
                        }
                        else {
                            var tokens = ObjectParser.GetTokens(attributeValue);
                            if (!tokens.HasValue || !tokens.Value.Validate()) {
                                AddError("[Scores] Unable to Interpret: " + attributeName);
                            }
                            else {
                                scores.constitution = tokens.Value;
                                constitution_set = true;
                            }
                        }
                    }
                    else if (attributeNameLower == "int") {
                        if (intelligence_set) {
                            AddError("[Scores] Attribute found more than once: " + attributeName);
                        }
                        else {
                            var tokens = ObjectParser.GetTokens(attributeValue);
                            if (!tokens.HasValue || !tokens.Value.Validate()) {
                                AddError("[Scores] Unable to Interpret: " + attributeName);
                            }
                            else {
                                scores.intelligence = tokens.Value;
                                intelligence_set = true;
                            }
                        }
                    }
                    else if(attributeNameLower == "wis") {
                        if (wisdom_set) {
                            AddError("[Scores] Attribute found more than once: " + attributeName);
                        }
                        else {
                            var tokens = ObjectParser.GetTokens(attributeValue);
                            if (!tokens.HasValue || !tokens.Value.Validate()) {
                                AddError("[Scores] Unable to Interpret: " + attributeName);
                            }
                            else {
                                scores.wisdom = tokens.Value;
                                wisdom_set = true;
                            }
                        }
                    }
                    else if (attributeNameLower == "cha") {
                        if (charisma_set) {
                            AddError("[Scores] Attribute found more than once: " + attributeName);
                        }
                        else {
                            var tokens = ObjectParser.GetTokens(attributeValue);
                            if (!tokens.HasValue || !tokens.Value.Validate()) {
                                AddError("[Scores] Unable to Interpret: " + attributeName);
                            }
                            else {
                                scores.charisma = tokens.Value;
                                charisma_set = true;
                            }
                        }
                    }
                    else {
                        AddError("[Scores] Attribute Name not expected: " + attributeName);
                    }
                }
            }
            if (!strength_set) {
                AddError("[Scores] Attribute not found: str");
            }
            if (!dexterity_set) {
                AddError("[Scores] Attribute not found: dex");
            }
            if (!constitution_set) {
                AddError("[Scores] Attribute not found: con");
            }
            if (!intelligence_set) {
                AddError("[Scores] Attribute not found: int");
            }
            if (!wisdom_set) {
                AddError("[Scores] Attribute not found: wis");
            }
            if (!charisma_set) {
                AddError("[Scores] Attribute not found: cha");
            }
            return scores;
        }

        private static Attack ReadAttack(XElement element, Action<string> AddError, Func<int> GetErrorCount) {
            Attack attack = new Attack();

            bool name_set = false;
            bool attack_set = false;
            bool damage_set = false;

            using(var attributeEnum = element.Attributes().GetEnumerator()) {
                while (attributeEnum.MoveNext()) {
                    var attributeName = attributeEnum.Current.Name.ToString();
                    var attributeNameLower = attributeName.ToLowerInvariant();
                    var attributeValue = attributeEnum.Current.Value;

                    if(attributeNameLower == "name") {
                        if (name_set) {
                            AddError("[Attack] Attribute found more than once: " + attributeName);
                        }
                        else {
                            attack.name = ObjectParser.FormatLine(attributeValue);
                            name_set = true;
                        } 
                    }
                    if (attributeNameLower == "atk") {
                        if (attack_set) {
                            AddError("[Attack] Attribute found more than once: " + attributeName);
                        }
                        else {
                            var tokens = ObjectParser.GetTokens(attributeValue);
                            if (!tokens.HasValue || !tokens.Value.Validate()) {
                                AddError("[Attack] Unable to Interpret: " + attributeName);
                            }
                            else {
                                attack.attack = tokens.Value;
                                attack_set = true;
                            }
                        }
                    }
                    else if (attributeNameLower == "mod") {
                        if (attack_set) {
                            AddError("[Attack] Attribute found more than once: " + attributeName);
                        }
                        else {
                            var tokens = ObjectParser.GetTokens("D20+" + attributeValue);
                            if (!tokens.HasValue || !tokens.Value.Validate()) {
                                AddError("[Attack] Unable to Interpret: " + attributeName);
                            }
                            else {
                                attack.attack = tokens.Value;
                                attack_set = true;
                            }
                        }
                    }
                    else if(attributeNameLower == "dmg") {
                        if (damage_set) {
                            AddError("[Attack] Attribute found more than once: " + attributeName);
                        }
                        else {
                            var tokens = ObjectParser.GetTokens(attributeValue);
                            if (!tokens.HasValue || !tokens.Value.Validate()) {
                                AddError("[Attack] Unable to Interpret: " + attributeName);
                            }
                            else {
                                attack.damage = tokens.Value;
                                damage_set = true;
                            }
                        }
                    }
                    else {
                        AddError("[Attack] Attribute Name not expected: " + attributeName);
                    }
                }
            }
            if (!name_set) {
                AddError("[Attack] Attribute not found: name");
            }
            if (!attack_set) {
                AddError("[Attack] Attribute not found: atk/mod");
            }
            if (!damage_set) {
                AddError("[Attack] Attribute not found: dmg");
            }
            return attack;
        }

        private static Conditions ReadConditions(XElement element, Action<string> AddError, Func<int> GetErrorCount) {
            string[] conditionNames = element.Value.Replace(" ", string.Empty).Split(',');
            Conditions conditions = new Conditions(conditionNames.Length);

            for (int index = 0; index < conditionNames.Length; ++index) {
                var condition = conditionNames[index].GetCondition();
                if (!condition.HasValue) {
                    AddError("[Conditons] Unable to Interpret: " + conditionNames[index]);
                }
                else {
                    conditions.Add(condition.Value);
                }
            }
            return conditions;
        }

        private static AbstractActor ReadActor(XElement element, Action<string> AddError, Func<int> GetErrorCount) {
            AbstractActor actor = new AbstractActor();

            // Read Attributes
            bool name_set = false;
            bool ini_set = false;
            bool hp_set = false;
            bool temp_set = false;
            bool coloringType_set = false;
            bool ac_set = false;
            bool speed_set = false;
            bool remove_set = false;
            
            using (var attributeEnum = element.Attributes().GetEnumerator()) {
                while (attributeEnum.MoveNext()) {
                    var attributeName = attributeEnum.Current.Name.ToString();
                    var attributeNameLower = attributeName.ToLowerInvariant();
                    var attributeValue = attributeEnum.Current.Value;

                    if(attributeNameLower == "name") {
                        if (name_set) {
                            AddError("[Actor] Attribute found more than once: " + attributeName);
                        }
                        else {
                            actor.name = ObjectParser.FormatLine(attributeValue);
                            name_set = true;
                        }
                    }
                    else if(attributeNameLower == "ini") {
                        if (ini_set) {
                            AddError("[Actor] Attribute found more than once: " + attributeName);
                        }
                        else {
                            var tokens = ObjectParser.GetTokens(attributeValue);
                            if (!tokens.HasValue || !tokens.Value.Validate()) {
                                AddError("[Actor] Unable to Interpret: " + attributeName);
                            }
                            else {
                                actor.initiative = tokens.Value;
                                ini_set = true;
                            }
                        }
                    }
                    else if(attributeNameLower == "hp") {
                        if (hp_set) {
                            AddError("[Actor] Attribute found more than once: " + attributeName);
                        }
                        else {
                            var tokens = ObjectParser.GetTokens(attributeValue);
                            if (!tokens.HasValue || !tokens.Value.Validate()) {
                                AddError("[Actor] Unable to Interpret: " + attributeName);
                            }
                            else {
                                actor.HP = tokens.Value;
                                hp_set = true;
                            }
                        }
                    }
                    else if(attributeNameLower == "temp") {
                        if (temp_set) {
                            AddError("[Actor] Attribute found more than once: " + attributeName);
                        }
                        else {
                            var tokens = ObjectParser.GetTokens(attributeValue);
                            if (!tokens.HasValue || !tokens.Value.Validate()) {
                                AddError("[Actor] Unable to Interpret: " + attributeName);
                            }
                            else {
                                actor.temporary = tokens.Value;
                                temp_set = true;
                            }
                        }
                    }
                    else if(attributeNameLower == "coloring") {
                        if (coloringType_set) {
                            AddError("[Actor] Attribute found more than once: " + attributeName);
                        }
                        else {
                            actor.coloringType = ObjectParser.FormatLine(attributeValue);
                            coloringType_set = true;
                        }
                    }
                    else if(attributeNameLower == "ac") {
                        if (ac_set) {
                            AddError("[Actor] Attribute found more than once: " + attributeName);
                        }
                        else {
                            var tokens = ObjectParser.GetTokens(attributeValue);
                            if (!tokens.HasValue || !tokens.Value.Validate()) {
                                AddError("[Actor] Unable to Interpret: " + attributeName);
                            }
                            else {
                                actor.armorClass = tokens.Value;
                                ac_set = true;
                            }
                        }
                    }
                    else if(attributeNameLower == "speed") {
                        if (speed_set) {
                            AddError("[Actor] Attribute found more than once: " + attributeName);
                        }
                        else {
                            actor.speed = ObjectParser.FormatLine(attributeValue);
                            speed_set = true;
                        }
                    }
                    else if(attributeNameLower == "remove") {
                        if (remove_set) {
                            AddError("[Actor] Attribute found more than once: " + attributeName);
                        }
                        else {
                            var tokens = ObjectParser.GetBoolean(attributeValue);
                            if (!tokens.HasValue) {
                                AddError("[Actor] Unable to Interpret: " + attributeName);
                            }
                            else {
                                actor.remove = tokens.Value;
                                remove_set = true;
                            }
                        }
                    }
                    else {
                        AddError("[Actor] Attribute not expected: " + attributeName);
                    }
                }
            }

            if (!name_set) {
                AddError("[Actor] Attribute not found: name");
            }
            if (!hp_set) {
                AddError("[Actor] Attribute not found: hp");
            }
            // Read Elements

            bool scores_set = false;
            bool description_set = false;
            bool conditions_set = false;
            bool coloring_set = false;

            using(var childEnum = element.Elements().GetEnumerator()) {
                while (childEnum.MoveNext()) {
                    var child = childEnum.Current;
                    var childName = child.Name.ToString();
                    var childNameLower = childName.ToLowerInvariant();

                    if(childNameLower == "scores") {
                        if (scores_set) {
                            AddError("[Actor] Element found more than once: " + childName);
                        }
                        else {
                            int errorCount = GetErrorCount();
                            var scores = ReadScores(child, AddError, GetErrorCount);
                            if(GetErrorCount() == errorCount) {
                                actor.scores = scores;
                                scores_set = true;
                            }
                        }
                    }
                    else if(childNameLower == "attack") {
                        int errorCount = GetErrorCount();
                        var attack = ReadAttack(child, AddError, GetErrorCount);
                        if(GetErrorCount() == errorCount) {
                            actor.attacks.Add(attack);
                        }
                    }
                    else if(childNameLower == "conditions") {
                        if (conditions_set) {
                            AddError("[Actor] Element found more than once: " + childName);
                        }
                        else {
                            int errorCount = GetErrorCount();
                            var conditions = ReadConditions(child, AddError, GetErrorCount);
                            if(GetErrorCount() == errorCount) {
                                actor.conditions = conditions;
                                conditions_set = true;
                            }
                        }
                    }
                    else if(childNameLower == "note") {
                        actor.notes.Add(ObjectParser.FormatLine(child.Value));
                    }
                    else if (childNameLower == "description") {
                        if (description_set) {
                            AddError("[Actor] Element found more than once: " + childName);
                        }
                        else {
                            actor.description = ObjectParser.FormatText(child.Value);
                            description_set = true;
                        }
                    }
                    else if (childNameLower == "coloring") {
                        if (coloring_set) {
                            AddError("[Actor] Element found more than once: " + childName);
                        }
                        else {
                            int errorCount = GetErrorCount();
                            var coloring = ReadColoring(child, AddError, GetErrorCount);
                            if(GetErrorCount() == errorCount) {
                                actor.coloring = coloring;
                                coloring_set = true;
                            }
                        }
                    }
                    else {
                        AddError("[Actor] Element not expected: " + childName);
                    }
                }
            }
            if(!ini_set && !scores_set) {
                AddError("[Actor] Unable to Determine Value: ini");
            }
            if(coloring_set && coloringType_set) {
                AddError("[Actor] Ambiguity: Coloring is defined more than once");
            }
            return actor;
        }

        private static Group ReadGroup(XElement element, Action<string> AddError, Func<int> GetErrorCount) {
            Group group = new Group();

            // Read Attributes
            bool groupName_set = false;
            using (var attributeEnum = element.Attributes().GetEnumerator()) {
                while (attributeEnum.MoveNext()) {
                    var attributeName = attributeEnum.Current.Name.ToString();
                    var attributeNameLower = attributeName.ToLowerInvariant();
                    var attributeValue = attributeEnum.Current.Value;

                    if(attributeNameLower == "name") {
                        if (groupName_set) {
                            AddError("[Group] Attribute found twice: " + attributeName);
                            return group;
                        }
                        group.name = ObjectParser.FormatLine(attributeValue);
                        groupName_set = true;
                    }
                    else {
                        AddError("[Group] Attribute not expected: " + attributeName);
                    }
                }
            }
            if (!groupName_set) {
                AddError("[Group] Attribute not found: name");
                return group;
            }

            // Read Elements
            using (var childEnum = element.Elements().GetEnumerator()) {
                while (childEnum.MoveNext()) {
                    bool actorName_set = false;
                    bool actorAmount_set = false;
                    string actorName = null;
                    List<Token> actorAmount = null;

                    // Get Attributes
                    using (var attributeEnum = childEnum.Current.Attributes().GetEnumerator()) {
                        while (attributeEnum.MoveNext()) {
                            var attributeName = attributeEnum.Current.Name.ToString();
                            var attributeNameLower = attributeName.ToLowerInvariant();
                            var attributeValue = attributeEnum.Current.Value;

                            if(attributeNameLower == "name") {
                                if (actorName_set) {
                                    AddError("[Group, name='" + group.name + "'] Attribute found twice: " + attributeName);
                                }
                                else {
                                    actorName = ObjectParser.FormatLine(attributeValue);
                                    actorName_set = true;
                                }
                            }
                            else if(attributeNameLower == "amount") {
                                if (actorAmount_set) {
                                    AddError("[Group, name='" + group.name + "'] Attribute found twice: " + attributeName);
                                }
                                else {
                                    var tokens = ObjectParser.GetTokens(attributeValue);
                                    if (!tokens.HasValue || !tokens.Value.Validate()) {
                                        AddError("[Group, name='" + group.name + "'] Unable to Interpret: " + attributeName);
                                    }
                                    else {
                                        actorAmount = tokens.Value;
                                        actorAmount_set = true;
                                    }
                                }
                            }
                            else {
                                AddError("[Group, name='" + group.name + "'] Attribute not expected: " + attributeName);
                            }
                        }
                    }
                    // Add Actor(s)
                    if (!actorName_set) {
                        AddError("[Group, name='" + group.name + "'] Child Attribute not found: name");
                        continue;
                    }
                    if (!actorAmount_set) {
                        actorAmount = new List<Token> { new Token(TokenType.Integer, 1) };
                    }
                    group.actors.Add(new Tuple<string, List<Token>>(actorName, actorAmount));
                }
            }
            return group;
        }
        

        // === High Level Access ===
        public static string GetFolderDirectory() {
#if DEBUG
            return new DirectoryInfo(Environment.CurrentDirectory).Parent.Parent.Parent.FullName;
#else
            return new DirectoryInfo(Environment.CurrentDirectory).Parent.FullName;
#endif
        }

        public static Settings GetSettings(string path, List<string> errors) {
            string[] text;
            try {
                text = File.ReadAllLines(path);
            }
            catch (Exception ex) {
                errors.Add("[Settings, path='" + path + "'] Encountered Issue when Loading Settings: " + ex.ToString());
                return default;
            }
            void AddError(string error) { errors.Add("[Settings, path='" + path + "']" + error); }
            int GetCount() => errors.Count;
            return ReadSettings(text, AddError, GetCount);
        }

        /// <summary>
        /// Adds the Colors in the .xml Document to coloringTypes, and any encountered Errors to errorMessages
        /// </summary>
        public static void GetColorings(string path, List<ColoringType> coloringTypes, List<string> errors) {
            XElement root;
            try {
                root = XDocument.Load(path).Root;
            }
            catch (Exception ex) {
                errors.Add("[Colorings] Encountered issue when Loading from " + path + ": " + ex.ToString());
                return;
            }
            void AddError(string error) { errors.Add("[Colorings, path='" + path + "']" + error); }
            int GetCount() => errors.Count;

            using (var elementEnum = root.Elements().GetEnumerator()) {
                while (elementEnum.MoveNext()) {
                    int errorCount = errors.Count;
                    var coloring = ReadColoringType(elementEnum.Current, AddError, GetCount);
                    if(errors.Count == errorCount) {
                        coloringTypes.Add(coloring);
                    }
                }
            }
        }

        public static void GetActors(string path, List<AbstractActor> actors, List<Group> groups, List<string> errors) {
            XElement root;
            try {
                root = XDocument.Load(path).Root;
            }
            catch (Exception ex) {
                errors.Add("[Actors] Encountered Issue when loading from " + path + ": " + ex.ToString());
                return;
            }
            void AddError(string error) { errors.Add("[Actors, path='" + path + "']" + error); }
            int GetCount() => errors.Count;

            using (var elementEnum = root.Elements().GetEnumerator()) {
                while (elementEnum.MoveNext()) {
                    var element = elementEnum.Current;
                    var elementName = element.Name.ToString();
                    var elementNameLower = elementName.ToLowerInvariant();

                    if(elementNameLower == "actor") {
                        int errorCount = errors.Count;
                        var actor = ReadActor(element, AddError, GetCount);
                        if(errors.Count == errorCount) {
                            actors.Add(actor);
                        }
                    }
                    else if(elementNameLower == "group") {
                        int errorCount = errors.Count;
                        var group = ReadGroup(element, AddError, GetCount);
                        if(errors.Count == errorCount) {
                            groups.Add(group);
                        }
                    }
                    else {
                        errors.Add("[Actors, path='" + path + "'] Element Name not expected: " + elementName);
                    }
                }
            }
        }

        /// <summary>
        /// Returns whether Loading was Successful and creates the Appropriate Screen
        /// </summary>
        public static bool Load(bool isStartUp) {
            var folderDir = GetFolderDirectory();
            var errors = new List<string>();
            if (isStartUp) {
                // Load Settings
                Program.settings = GetSettings(Path.Combine(folderDir, "Settings", "settings.txt"), errors);
                if (errors.Count != 0) {
                    Program.screenWriter.Write(Output.GetFatalErrorLoadingScreen(errors));
                    return false;
                }
            }
            // Load Special Chars
            Program.specialCharacters.Setup(Program.settings.useSpecial);

            // Load Colorings
            try {
                var coloringPaths = Directory.GetFiles(Path.Combine(folderDir, "Colorings"), "*.xml", SearchOption.TopDirectoryOnly);
                for (int index = 0; index < coloringPaths.Length; ++index) {
                    GetColorings(coloringPaths[index], Program.data.loadedColoringTypes, errors);
                }
            }
            catch (Exception coloringEx) {
                errors.Add("[Colorings] Encountered Issue when Loading Colorings: '" + coloringEx.ToString());
            }
            // Load Actors
            try {
                var actorPaths = Directory.GetFiles(Path.Combine(folderDir, "Actors"), "*.xml", SearchOption.TopDirectoryOnly);
                for (int index = 0; index < actorPaths.Length; ++index) {
                    GetActors(actorPaths[index], Program.data.loadedActors, Program.data.loadedGroups, errors);
                }
            }
            catch (Exception actorEx) {
                errors.Add("[Actors] Encountered Issue when loading Actors: " + actorEx.ToString());
            }
            Program.data.ValidateGroups(errors);
            if (isStartUp) {
                Program.data.AutoaddGroups(errors);
            }

            Program.screenWriter.Write(Output.GetSuccessfulLoadingScreen(errors, true));
            return true;
        }

        public static void WriteToXML(XElement element, string path, List<string> errors) {
            if(Path.GetExtension(path) != ".xml") {
                errors.Add("[Saving] Bad Path Extension");
                return;
            }
            XDocument document;
            if (File.Exists(path)) {
                try {
                    document = XDocument.Load(path);
                }
                catch(Exception readEx) {
                    errors.Add("[Saving] Encountered Issue when Loading: " + readEx.ToString());
                    return;
                }
                document.Root.Add(element);
            }
            else {
                // Create new
                document = new XDocument();
                document.Declaration = new XDeclaration("1.0", "utf-8", "yes");
                document.Add(new XElement("root", element));
            }

            try {
                document.Save(path);
            }
            catch (Exception saveEx) {
                errors.Add("[Saving] Encountered Issue when Saving: " + saveEx.ToString());
                return;
            }
        }
    }
}