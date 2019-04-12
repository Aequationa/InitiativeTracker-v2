using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;

namespace InitiativeTracker
{
    public static class FileParser
    {
        // === Parse File to Object ===
        private static Settings ReadSettings(string[] text, out string errorMessage) {
            Settings settings = new Settings();

            if(text.Length < 4) {
                errorMessage = "[Settings] Not enough Arguments";
                return settings;
            }

            // ColoringType
            string defaultColoringTypeString = "defaultColoringType=";
            if (!text[0].StartsWith(defaultColoringTypeString)) {
                errorMessage = "[Settings] Argument not found: defaultColoringType";
                return settings;
            }
            settings.defaultColoringType = text[0].Substring(defaultColoringTypeString.Length);

            // Remove
            string defaultRemoveString = "defaultRemove=";
            if (!text[1].StartsWith(defaultRemoveString)) {
                errorMessage = "[Settings] Argument not found: defaultRemove";
                return settings;
            }
            var defaultRemove = ObjectParser.GetBoolean(text[1].Substring(defaultRemoveString.Length));
            if (!defaultRemove.HasValue) {
                errorMessage = "[Settings] Unable to Interpret: defaultRemove";
                return settings;
            }
            settings.defaultRemove = defaultRemove.Value;

            // Type
            string defaultTypeString = "defaultType=";
            if(!text[2].StartsWith(defaultTypeString)) {
                errorMessage = "[Settings] Argument not found: defaultType";
                return settings;
            }
            var defaultType = ObjectParser.GetInt10(text[2], defaultTypeString.Length, text[2].Length);
            if (!defaultType.HasValue) {
                errorMessage = "[Settings] Unable to Interpret: defaultType";
                return settings;
            }
            settings.defaultType = defaultType.Value;

            // Tab Width
            string tabWidthString = "tabWidth=";
            if(!text[3].StartsWith(tabWidthString)) {
                errorMessage = "[Settings] Argument not found: tabWidth";
                return settings;
            }
            for (int index = 0; index < tabWidthString.Length; ++index) {
                if (text[3][index] != tabWidthString[index]) {
                    errorMessage = "[Settings] Argument not found: tabWidth";
                    return settings;
                }
            }
            var tabWidth = ObjectParser.GetInt10(text[3], tabWidthString.Length, text[3].Length);
            if (!tabWidth.HasValue) {
                errorMessage = "[Settings] Unable to Interpret: tabWidth";
                return settings;
            }
            settings.tabWidth = tabWidth.Value;

            // Return Settings
            errorMessage = null;
            return settings;
        }

        private static ColoringType ReadColoringType(XElement element, out string errorMessage) {
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
                            errorMessage = "[Coloring] Attribute found twice: " + attributeName;
                            return coloringType;
                        }
                        coloringType.name = attributeValue;
                        name_set = true;
                    }
                    else if (attributeNameLower == "base_text") {
                        if (base_text_set) {
                            errorMessage = "[Coloring] Attribute found twice: " + attributeName;
                            return coloringType;
                        }
                        var consoleColor = ObjectParser.GetConsoleColor(attributeValue);
                        if (!consoleColor.HasValue) {
                            errorMessage = "[Coloring] Unable to Interpret: " + attributeName;
                            return coloringType;
                        }
                        coloringType.base_text = consoleColor.Value;
                        base_text_set = true;
                    }
                    else if (attributeNameLower == "base_bg") {
                        if (base_bg_set) {
                            errorMessage = "[Coloring] Attribute found twice: " + attributeName;
                            return coloringType;
                        }
                        var consoleColor = ObjectParser.GetConsoleColor(attributeValue);
                        if (!consoleColor.HasValue) {
                            errorMessage = "[Coloring] Unable to Interpret: " + attributeName;
                            return coloringType;
                        }
                        coloringType.base_bg = consoleColor.Value;
                        base_bg_set = true;
                    }
                    else if (attributeNameLower == "active_text") {
                        if (active_text_set) {
                            errorMessage = "[Coloring] Attribute found twice: " + attributeName;
                            return coloringType;
                        }
                        var consoleColor = ObjectParser.GetConsoleColor(attributeValue);
                        if (!consoleColor.HasValue) {
                            errorMessage = "[Coloring] Unable to Interpret: " + attributeName;
                            return coloringType;
                        }
                        coloringType.active_text = consoleColor.Value;
                        active_text_set = true;
                    }
                    else if (attributeNameLower == "active_bg") {
                        if (active_bg_set) {
                            errorMessage = "[Coloring] Attribute found twice: " + attributeName;
                            return coloringType;
                        }
                        var consoleColor = ObjectParser.GetConsoleColor(attributeValue);
                        if (!consoleColor.HasValue) {
                            errorMessage = "[Coloring] Unable to Interpret: " + attributeName;
                            return coloringType;
                        }
                        coloringType.active_bg = consoleColor.Value;
                        active_bg_set = true;
                    }
                    else {
                        errorMessage = "[Coloring] Attribute Name not expected: " + attributeName ;
                        return coloringType;
                    }
                }
            }
            if (!name_set) {
                errorMessage = "[Coloring] Attribute not found: name";
                return coloringType;
            }
            else if (!base_text_set) {
                errorMessage = "[Coloring] Attribute not found: base_text";
                return coloringType;
            }
            else if (!base_bg_set) {
                errorMessage = "[Coloring] Attribute not found: base_bg";
                return coloringType;
            }
            else if (!active_text_set) {
                errorMessage = "[Coloring] Attribute not found: active_text";
                return coloringType;
            }
            else if (!active_bg_set) {
                errorMessage = "[Coloring] Attribute not found: active_bg";
                return coloringType;
            }
            errorMessage = null;
            return coloringType;
        }

        private static Coloring ReadColoring(XElement element, out string errorMessage) {
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
                            errorMessage = "[Coloring] Attribute found twice: " + attributeName;
                            return coloring;
                        }
                        var consoleColor = ObjectParser.GetConsoleColor(attributeValue);
                        if (!consoleColor.HasValue) {
                            errorMessage = "[Coloring] Unable to Interpret: " + attributeName;
                            return coloring;
                        }
                        coloring.base_text = consoleColor.Value;
                        base_text_set = true;
                    }
                    else if (attributeNameLower == "base_bg") {
                        if (base_bg_set) {
                            errorMessage = "[Coloring] Attribute found twice: " + attributeName;
                            return coloring;
                        }
                        var consoleColor = ObjectParser.GetConsoleColor(attributeValue);
                        if (!consoleColor.HasValue) {
                            errorMessage = "[Coloring] Unable to Interpret: " + attributeName;
                            return coloring;
                        }
                        coloring.base_bg = consoleColor.Value;
                        base_bg_set = true;
                    }
                    else if (attributeNameLower == "active_text") {
                        if (active_text_set) {
                            errorMessage = "[Coloring] Attribute found twice: " + attributeName;
                            return coloring;
                        }
                        var consoleColor = ObjectParser.GetConsoleColor(attributeValue);
                        if (!consoleColor.HasValue) {
                            errorMessage = "[Coloring] Unable to Interpret: " + attributeName;
                            return coloring;
                        }
                        coloring.active_text = consoleColor.Value;
                        active_text_set = true;
                    }
                    else if (attributeNameLower == "active_bg") {
                        if (active_bg_set) {
                            errorMessage = "[Coloring] Attribute found twice: " + attributeName;
                            return coloring;
                        }
                        var consoleColor = ObjectParser.GetConsoleColor(attributeValue);
                        if (!consoleColor.HasValue) {
                            errorMessage = "[Coloring] Unable to Interpret: " + attributeName;
                            return coloring;
                        }
                        coloring.active_bg = consoleColor.Value;
                        active_bg_set = true;
                    }
                    else {
                        errorMessage = "[Coloring] Attribute Name not expected: " + attributeName;
                        return coloring;
                    }
                }
            }
            if (!base_text_set) {
                errorMessage = "[Coloring] Attribute not found: base_text";
                return coloring;
            }
            else if (!base_bg_set) {
                errorMessage = "[Coloring] Attribute not found: base_bg";
                return coloring;
            }
            else if (!active_text_set) {
                errorMessage = "[Coloring] Attribute not found: active_text";
                return coloring;
            }
            else if (!active_bg_set) {
                errorMessage = "[Coloring] Attribute not found: active_bg";
                return coloring;
            }
            errorMessage = null;
            return coloring;
        }

        private static AbstractScores ReadScores(XElement element, out string errorMessage) {
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
                            errorMessage = "[Scores] Attribute found twice: " + attributeName;
                            return scores;
                        }
                        var tokens = ObjectParser.GetTokens(attributeValue);
                        if (!tokens.HasValue || !tokens.Value.Validate()) {
                            errorMessage = "[Scores] Unable to Interpret: " + attributeName;
                            return scores;
                        }
                        scores.strength = tokens.Value;
                        strength_set = true;
                    }
                    else if (attributeNameLower == "dex") {
                        if (dexterity_set) {
                            errorMessage = "[Scores] Attribute found twice: " + attributeName;
                            return scores;
                        }
                        var tokens = ObjectParser.GetTokens(attributeValue);
                        if (!tokens.HasValue || !tokens.Value.Validate()) {
                            errorMessage = "[Scores] Unable to Interpret: " + attributeName;
                            return scores;
                        }
                        scores.dexterity = tokens.Value;
                        dexterity_set = true;
                    }
                    else if(attributeNameLower == "con") {
                        if (constitution_set) {
                            errorMessage = "[Scores] Attribute found twice: " + attributeName;
                            return scores;
                        }
                        var tokens = ObjectParser.GetTokens(attributeValue);
                        if (!tokens.HasValue || !tokens.Value.Validate()) {
                            errorMessage = "[Scores] Unable to Interpret: " + attributeName;
                            return scores;
                        }
                        scores.constitution = tokens.Value;
                        constitution_set = true;
                    }
                    else if (attributeNameLower == "int") {
                        if (intelligence_set) {
                            errorMessage = "[Scores] Attribute found twice: " + attributeName;
                            return scores;
                        }
                        var tokens = ObjectParser.GetTokens(attributeValue);
                        if (!tokens.HasValue || !tokens.Value.Validate()) {
                            errorMessage = "[Scores] Unable to Interpret: " + attributeName;
                            return scores;
                        }
                        scores.intelligence = tokens.Value;
                        intelligence_set = true;
                    }
                    else if(attributeNameLower == "wis") {
                        if (wisdom_set) {
                            errorMessage = "[Scores] Attribute found twice: " + attributeName;
                            return scores;
                        }
                        var tokens = ObjectParser.GetTokens(attributeValue);
                        if (!tokens.HasValue || !tokens.Value.Validate()) {
                            errorMessage = "[Scores] Unable to Interpret: " + attributeName;
                            return scores;
                        }
                        scores.wisdom = tokens.Value;
                        wisdom_set = true;
                    }
                    else if (attributeNameLower == "cha") {
                        if (charisma_set) {
                            errorMessage = "[Scores] Attribute found twice: " + attributeName;
                            return scores;
                        }
                        var tokens = ObjectParser.GetTokens(attributeValue);
                        if (!tokens.HasValue || !tokens.Value.Validate()) {
                            errorMessage = "[Scores] Unable to Interpret: " + attributeName;
                            return scores;
                        }
                        scores.charisma = tokens.Value;
                        charisma_set = true;
                    }
                }
            }
            if (!strength_set) {
                errorMessage = "[Scores] Attribute not found: str";
                return scores;
            }
            if (!dexterity_set) {
                errorMessage = "[Scores] Attribute not found: dex";
                return scores;
            }
            if (!constitution_set) {
                errorMessage = "[Scores] Attribute not found: con";
                return scores;
            }
            if (!intelligence_set) {
                errorMessage = "[Scores] Attribute not found: int";
                return scores;
            }
            if (!wisdom_set) {
                errorMessage = "[Scores] Attribute not found: wis";
                return scores;
            }
            if (!charisma_set) {
                errorMessage = "[Scores] Attribute not found: cha";
                return scores;
            }
            errorMessage = null;
            return scores;
        }

        private static Attack ReadAttack(XElement element, out string errorMessage) {
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
                            errorMessage = "[Attack] Attribute found twice: " + attributeName;
                            return attack;
                        }
                        attack.name = ObjectParser.FormatLine(attributeValue);
                        name_set = true;
                    }
                    if (attributeNameLower == "atk") {
                        if (attack_set) {
                            errorMessage = "[Attack] Attribute found twice: " + attributeName;
                            return attack;
                        }
                        var tokens = ObjectParser.GetTokens(attributeValue);
                        if (!tokens.HasValue || !tokens.Value.Validate()) {
                            errorMessage = "[Attack] Unable to Interpret: " + attributeName;
                            return attack;
                        }
                        attack.attack = tokens.Value;
                        attack_set = true;
                    }
                    else if (attributeNameLower == "mod") {
                        if (attack_set) {
                            errorMessage = "[Attack] Attribute found twice: " + attributeName;
                            return attack;
                        }
                        var tokens = ObjectParser.GetTokens("D20+" + attributeValue);
                        if (!tokens.HasValue || !tokens.Value.Validate()) {
                            errorMessage = "[Attack] Unable to Interpret: " + attributeName;
                            return attack;
                        }
                        attack.attack = tokens.Value;
                        attack_set = true;
                    }
                    else if(attributeNameLower == "dmg") {
                        if (damage_set) {
                            errorMessage = "[Attack] Attribute found twice: " + attributeName;
                            return attack;
                        }
                        var tokens = ObjectParser.GetTokens(attributeValue);
                        if (!tokens.HasValue || !tokens.Value.Validate()) {
                            errorMessage = "[Attack] Unable to Interpret: " + attributeName;
                            return attack;
                        }
                        attack.damage = tokens.Value;
                        damage_set = true;
                    }
                }
            }
            if (!name_set) {
                errorMessage = "[Attack] Attribute not found: name";
                return attack;
            }
            if (!attack_set) {
                errorMessage = "[Attack] Attribute not found: atk/mod";
                return attack;
            }
            if (!damage_set) {
                errorMessage = "[Attack] Attribute not found: dmg";
                return attack;
            }
            errorMessage = null;
            return attack;
        }

        private static Conditions ReadConditions(XElement element, out string errorMessage) {
            string[] conditionNames = element.Value.Replace(" ", string.Empty).Split(',');
            Conditions conditions = new Conditions(conditionNames.Length);

            for (int index = 0; index < conditionNames.Length; ++index) {
                var condition = conditionNames[index].GetCondition();
                if (condition.HasValue) {
                    conditions.Add(condition.Value);
                }
                else {
                    errorMessage = "[Conditons] Unable to Interpret: " + conditionNames[index];
                    return conditions;
                }
            }
            errorMessage = null;
            return conditions;
        }

        private static AbstractActor ReadActor(XElement element, out string errorMessage) {
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
                            errorMessage = "[Actor] Attribute found twice: " + attributeName;
                            return actor;
                        }
                        actor.name = ObjectParser.FormatLine(attributeValue);
                        name_set = true;
                    }
                    else if(attributeNameLower == "ini") {
                        if (ini_set) {
                            errorMessage = "[Actor] Attribute found twice: " + attributeName;
                            return actor;
                        }
                        var tokens = ObjectParser.GetTokens(attributeValue);
                        if (!tokens.HasValue || !tokens.Value.Validate()) {
                            errorMessage = "[Actor] Unable to Interpret: " + attributeName;
                            return actor;
                        }
                        actor.initiative = tokens.Value;
                        ini_set = true;
                    }
                    else if(attributeNameLower == "hp") {
                        if (hp_set) {
                            errorMessage = "[Actor] Attribute found twice: " + attributeName;
                            return actor;
                        }
                        var tokens = ObjectParser.GetTokens(attributeValue);
                        if (!tokens.HasValue || !tokens.Value.Validate()) {
                            errorMessage = "[Actor] Unable to Interpret: " + attributeName;
                            return actor;
                        }
                        actor.HP = tokens.Value;
                        hp_set = true;
                    }
                    else if(attributeNameLower == "temp") {
                        if (temp_set) {
                            errorMessage = "[Actor] Attribute found twice: " + attributeName;
                            return actor;
                        }
                        var tokens = ObjectParser.GetTokens(attributeValue);
                        if (!tokens.HasValue || !tokens.Value.Validate()) {
                            errorMessage = "[Actor] Unable to Interpret: " + attributeName;
                            return actor;
                        }
                        actor.temporary = tokens.Value;
                        temp_set = true;
                    }
                    else if(attributeNameLower == "coloring") {
                        if (coloringType_set) {
                            errorMessage = "[Actor] Attribute found twice: " + attributeName;
                            return actor;
                        }
                        actor.coloringType = attributeValue;
                        coloringType_set = true;
                    }
                    else if(attributeNameLower == "ac") {
                        if (ac_set) {
                            errorMessage = "[Actor] Attribute found twice: " + attributeName;
                            return actor;
                        }
                        var tokens = ObjectParser.GetTokens(attributeValue);
                        if (!tokens.HasValue || !tokens.Value.Validate()) {
                            errorMessage = "[Actor] Unable to Interpret: " + attributeName;
                            return actor;
                        }
                        actor.armorClass = tokens.Value;
                        ac_set = true;
                    }
                    else if(attributeNameLower == "speed") {
                        if (speed_set) {
                            errorMessage = "[Actor] Attribute found twice: " + attributeName;
                            return actor;
                        }
                        actor.speed = ObjectParser.FormatLine(attributeValue);
                        speed_set = true;
                    }
                    else if(attributeNameLower == "remove") {
                        if (remove_set) {
                            errorMessage = "[Actor] Attribute found twice: " + attributeName;
                            return actor;
                        }
                        var tokens = ObjectParser.GetBoolean(attributeValue);
                        if (!tokens.HasValue) {
                            errorMessage = "[Actor] Unable to Interpret: " + attributeName;
                            return actor;
                        }
                        actor.remove = tokens.Value;
                        remove_set = true;
                    }
                }
            }

            if (!name_set) {
                errorMessage = "[Actor] Attribute not found: name";
                return actor;
            }
            if (!hp_set) {
                errorMessage = "[Actor] Attribute not found: hp";
                return actor;
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
                            errorMessage = "[Actor] Element found twice: " + childName;
                            return actor;
                        }
                        var scores = ReadScores(child, out string scoresMessage);
                        if(scoresMessage != null) {
                            errorMessage = "[Actor]" + scoresMessage;
                            return actor;
                        }
                        actor.scores = scores;
                        scores_set = true;
                    }
                    else if(childNameLower == "attack") {
                        var attack = ReadAttack(child, out string attackMessage);
                        if(attackMessage != null) {
                            errorMessage = "[Actor]" + attackMessage;
                            return actor;
                        }
                        actor.attacks.Add(attack);
                    }
                    else if(childNameLower == "conditions") {
                        if (conditions_set) {
                            errorMessage = "[Actor] Element found twice: " + childName;
                            return actor;
                        }
                        actor.conditions = ReadConditions(child, out string conditionsMessage);
                        if(conditionsMessage != null) {
                            errorMessage = "[Actor]" + conditionsMessage;
                            return actor;
                        }
                        conditions_set = true;
                    }
                    else if(childNameLower == "note") {
                        actor.notes.Add(ObjectParser.FormatLine(child.Value));
                    }
                    else if (childNameLower == "description") {
                        if (description_set) {
                            errorMessage = "[Actor] Element found twice: " + childName;
                            return actor;
                        }
                        actor.description = ObjectParser.FormatText(child.Value);
                        description_set = true;
                    }
                    else if (childNameLower == "coloring") {
                        if (coloring_set) {
                            errorMessage = "[Actor] Element found twice: " + childName;
                            return actor;
                        }
                        var coloring = ReadColoring(child, out string coloringMessage);
                        if (coloringMessage != null) {
                            errorMessage = "[Actor]" + coloringMessage;
                            return actor;
                        }
                        actor.coloring = coloring;
                        coloring_set = true;
                    }
                }
            }
            if(!ini_set && !scores_set) {
                errorMessage = "[Actor] Unable to Determine Value: ini";
                return actor;
            }
            if(coloring_set && coloringType_set) {
                errorMessage = "[Actor] Ambiguity: Coloring is defined twice";
                return actor;
            }
            errorMessage = null;
            return actor;
        }

        private static Group ReadGroup(XElement element, out List<string> errorMessages) {
            errorMessages = new List<string>();
            Group group = new Group();

            // Read Attributes
            bool name_set = false;
            using (var attributeEnum = element.Attributes().GetEnumerator()) {
                while (attributeEnum.MoveNext()) {
                    var attributeName = attributeEnum.Current.Name.ToString();
                    var attributeNameLower = attributeName.ToLowerInvariant();
                    var attributeValue = attributeEnum.Current.Value;

                    if(attributeNameLower == "name") {
                        if (name_set) {
                            errorMessages.Add("[Group] Attribute found twice: " + attributeName);
                            return group;
                        }
                        group.name = ObjectParser.FormatLine(attributeValue);
                        name_set = true;
                    }
                }
            }
            if (!name_set) {
                errorMessages.Add("[Group] Attribute not found: name");
                return group;
            }

            // Read Elements
            using (var childEnum = element.Elements().GetEnumerator()) {
                while (childEnum.MoveNext()) {
                    var actor = ReadActor(childEnum.Current, out string actorMessage);
                    if (actorMessage == null) {
                        group.actors.Add(actor);
                    }
                    else {
                        errorMessages.Add("[Group]" + actorMessage);
                    }
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

        public static Settings GetSettings(string path, out string errorMessage) {
            string[] text;
            try {
                text = File.ReadAllLines(path);
            }
            catch (Exception) {
                errorMessage = "[Settings] Encountered Issue when loading from '" + path + "'";
                return default;
            }
            return ReadSettings(text, out errorMessage);
        }

        /// <summary>
        /// Adds the Colors in the .xml Document to coloringTypes, and any encountered Errors to errorMessages
        /// </summary>
        public static void GetColorings(string path, ref List<ColoringType> coloringTypes, ref List<string> errorMessages) {
            XElement root;
            try {
                root = XDocument.Load(path).Root;
            }
            catch (Exception) {
                errorMessages.Add("[Colorings] Encountered Issue when loading from '" + path + "'");
                return;
            }

            using (var elementEnum = root.Elements().GetEnumerator()) {
                while (elementEnum.MoveNext()) {
                    var coloring = ReadColoringType(elementEnum.Current, out string errorMessage);
                    if(errorMessage == null) {
                        coloringTypes.Add(coloring);
                    }
                    else {
                        errorMessages.Add("[Colorings, path='" + path + "']" + errorMessage);
                    }
                }
            }
        }

        public static void GetActors(string path, ref List<AbstractActor> actors, ref List<Group> groups, ref List<string> errorMessages) {
            XElement root;
            try {
                root = XDocument.Load(path).Root;
            }
            catch (Exception) {
                errorMessages.Add("[Actors] Encountered Issue when loading from '" + path + "'");
                return;
            }

            using (var elementEnum = root.Elements().GetEnumerator()) {
                while (elementEnum.MoveNext()) {
                    var element = elementEnum.Current;
                    var elementName = element.Name.ToString();
                    var elementNameLower = elementName.ToLowerInvariant();

                    if(elementNameLower == "actor") {
                        var actor = ReadActor(element, out string actorMessage);
                        if(actorMessage == null) {
                            actors.Add(actor);
                        }
                        else {
                            errorMessages.Add("[Actors, path='" + path + "']" + actorMessage);
                        }
                    }
                    else if(elementNameLower == "group") {
                        var group = ReadGroup(element, out List<string> groupMessages);
                        if(groupMessages == null || groupMessages.Count == 0) {
                            groups.Add(group);
                        }
                        else {
                            for(int index = 0; index < groupMessages.Count; ++index) {
                                errorMessages.Add("[Actors, path='" + path + "']" + groupMessages[index]);
                            }
                        }
                    }
                    else {
                        errorMessages.Add("[Actors, path='" + path + "'] Element Name not expected: " + elementName);
                    }
                }
            }
        }

        /// <summary>
        /// Returns whether Loading was Successful and creates the Appropriate Screen
        /// </summary>
        public static bool Load(bool loadSettings) {
            var folderDir = GetFolderDirectory();
            if (loadSettings) {
                // Temporary Settings required in case loading Settings fails
                Program.settings.tabWidth = 4;

                // Load Settings
                Program.settings = GetSettings(folderDir + @"\Settings\settings.txt", out string settingsError);
                if (settingsError != null) {
                    Program.screenWriter.Write(Output.GetFatalErrorLoadingScreen(settingsError));
                    return false;
                }
            }
            
            var errorMessages = new List<string>();
            // Load Colorings
            try {
                var coloringPaths = Directory.GetFiles(folderDir + @"\Colorings", "*.xml", SearchOption.TopDirectoryOnly);
                for (int index = 0; index < coloringPaths.Length; ++index) {
                    FileParser.GetColorings(coloringPaths[index], ref Program.data.loadedColorings, ref errorMessages);
                }
            }
            catch (Exception) {
                errorMessages.Add("[Colorings] Encountered Issue when loading from '" + folderDir + @"\Colorings'");
            }
            // Load Actors
            try {
                var actorPaths = Directory.GetFiles(folderDir + @"\Actors", "*.xml", SearchOption.TopDirectoryOnly);
                for (int index = 0; index < actorPaths.Length; ++index) {
                    FileParser.GetActors(actorPaths[index], ref Program.data.loadedActors, ref Program.data.loadedGroups, ref errorMessages);
                }
            }
            catch (Exception) {
                errorMessages.Add("[Actors] Encountered Issue when loading from '" + folderDir + @"\Actors'");
            }

            Program.screenWriter.Write(Output.GetSuccessfulLoadingScreen(errorMessages, true));
            return true;
        }

        public static void WriteToXML(XElement element, string path, out string errorMessage) {
            if(Path.GetExtension(path) != ".xml") {
                errorMessage = "[Saving] Bad Path Extension";
                return;
            }
            XDocument document;
            if (File.Exists(path)) {
                try {
                    document = XDocument.Load(path);
                }
                catch(Exception readEx) {
                    errorMessage = "[Saving] Encountered Issue when Loading: " + readEx.ToString();
                    return;
                }
                document.Root.Add(element);
            }
            else {
                document = new XDocument();
                document.Declaration = new XDeclaration("1.0", "utf-8", "yes");
                document.Add(new XElement("root", element));
                // Create new
            }

            try {
                document.Save(path);
            }
            catch (Exception saveEx) {
                errorMessage = "[Saving] Encountered Issue when Saving: " + saveEx.ToString();
                return;
            }

            errorMessage = null;
        }
    }
}