using System;
using System.Collections.Generic;

namespace InitiativeTracker
{
    public struct Option<T>
    {
        private readonly bool hasValue;
        private readonly T value;

        private Option(T value) {
            hasValue = true;
            this.value = value;
        }
        public static implicit operator Option<T>(T value) {
            return new Option<T>(value);
        }
        public static Option<T> Null { get { return new Option<T>(); } }

        public bool HasValue { get { return hasValue; } }
        public T Value { get { return value; } }
    }

    public struct Coloring
    {
        public ConsoleColor base_text;
        public ConsoleColor base_bg;
        public ConsoleColor active_text;
        public ConsoleColor active_bg;

        public Coloring(ConsoleColor base_text, ConsoleColor base_bg, ConsoleColor active_text, ConsoleColor active_bg) {
            this.base_text = base_text;
            this.base_bg = base_bg;
            this.active_text = active_text;
            this.active_bg = active_bg;
        }
    }
    public struct ColoringType
    {
        public string name;
        public ConsoleColor base_text;
        public ConsoleColor base_bg;
        public ConsoleColor active_text;
        public ConsoleColor active_bg;

        public ColoringType(string name, ConsoleColor base_text, ConsoleColor base_bg, ConsoleColor active_text, ConsoleColor active_bg) {
            this.name = name;
            this.base_text = base_text;
            this.base_bg = base_bg;
            this.active_text = active_text;
            this.active_bg = active_bg;
        }
    }


    public struct Scores
    {
        public int strength;
        public int strengthModifier;
        public int dexterity;
        public int dexterityModifier;
        public int constitution;
        public int constitutionModifier;
        public int intelligence;
        public int intelligenceModifier;
        public int wisdom;
        public int wisdomModifier;
        public int charisma;
        public int charismaModifier;

        public void CalculateModifiers() {
            strengthModifier = ObjectParser.Divide(strength - 10, 2);
            dexterityModifier = ObjectParser.Divide(dexterity - 10,  2);
            constitutionModifier = ObjectParser.Divide(constitution - 10, 2);
            intelligenceModifier = ObjectParser.Divide(intelligence - 10, 2);
            wisdomModifier = ObjectParser.Divide(wisdom - 10, 2);
            charismaModifier = ObjectParser.Divide(charisma - 10, 2);
        }
    }
    public struct AbstractScores {
        public List<Token> strength;
        public List<Token> dexterity;
        public List<Token> constitution;
        public List<Token> intelligence;
        public List<Token> wisdom;
        public List<Token> charisma;

        /// <summary>
        /// Returns Scores for this Object, or Error Message
        /// </summary>
        public Scores GetScores(out string errorMessage) {
            Scores scores = new Scores();

            var eval_strength = strength.Evaluate();
            if (!eval_strength.HasValue) {
                errorMessage = "[Scores] Unable to Compute: str";
                return default;
            }
            scores.strength = eval_strength.Value;
            var eval_dexterity = dexterity.Evaluate();
            if (!eval_dexterity.HasValue) {
                errorMessage = "[Scores] Unable to Compute: dex";
                return default;
            }
            scores.dexterity = eval_dexterity.Value;
            var eval_constitution = constitution.Evaluate();
            if (!eval_constitution.HasValue) {
                errorMessage = "[Scores] Unable to Compute: con";
                return default;
            }
            scores.constitution = eval_constitution.Value;
            var eval_intelligence = intelligence.Evaluate();
            if (!eval_intelligence.HasValue) {
                errorMessage = "[Scores] Unable to Compute: int";
                return default;
            }
            scores.intelligence = eval_intelligence.Value;
            var eval_wisdom = wisdom.Evaluate();
            if (!eval_wisdom.HasValue) {
                errorMessage = "[Scores] Unable to Compute: wis";
                return default;
            }
            scores.wisdom = eval_wisdom.Value;
            var eval_charisma = charisma.Evaluate();
            if (!eval_charisma.HasValue) {
                errorMessage = "[Scores] Unable to Compute: cha";
                return default;
            }
            scores.charisma = eval_charisma.Value;

            errorMessage = null;
            scores.CalculateModifiers();
            return scores;
        }
    }

    public struct Attack
    {
        public string name;
        public List<Token> attack;
        public List<Token> damage;

        public Attack(string name, List<Token> attack, List<Token> damage) {
            this.name = name;
            this.attack = attack;
            this.damage = damage;
        }
    }

    public enum Condition : int
    {
        Blinded, Charmed, Deafened, Frightened, Grappled, Incapacitated, Invisible, Paralyzed,
        Petrified, Poisoned, Prone, Restrained, Stunned, Unconscious
    }
    public static class ConditionExt
    {
        private static readonly string[] ConditionNames = new string[] {
            "Blinded", "Charmed", "Deafened", "Frightened", "Grappled", "Incapacitated", "Invisible", 
            "Paralyzed", "Petrified", "Poisoned", "Prone", "Restrained", "Stunned", "Unconscious"
        };
        private const int ConditionCount = 14;

        private static readonly List<string>[] ConditionDescriptions = new List<string>[] {
            new List<string>{"A blinded creature can’t see and automatically fails any ability check that requires sight.",
                "Attack rolls against the creature have advantage, and the creature’s Attack rolls have disadvantage." },
            new List<string>{"A charmed creature can’t Attack the charmer or target the charmer with harmful Abilities or magical Effects.",
                "The charmer has advantage on any ability check to interact socially with the creature." },
            new List<string>{"A deafened creature can’t hear and automatically fails any ability check that requires hearing." },
            new List<string>{"A frightened creature has disadvantage on Ability Checks and Attack rolls while the source of its fear is within line of sight.",
                "The creature can’t willingly move closer to the source of its fear." },
            new List<string>{"A grappled creature’s speed becomes 0, and it can’t benefit from any bonus to its speed.",
                "The condition ends if the Grappler is incapacitated (see the condition).",
                "The condition also ends if an effect removes the grappled creature from the reach of the Grappler or Grappling effect, such as when a creature is hurled away by the Thunderwave spell." },
            new List<string>{"An incapacitated creature can’t take Actions or reactions." },
            new List<string>{"An invisible creature is impossible to see without the aid of magic or a Special sense. For the purpose of Hiding, the creature is heavily obscured. The creature’s location can be detected by any noise it makes or any tracks it leaves.",
                "Attack rolls against the creature have disadvantage, and the creature’s Attack rolls have advantage." },
            new List<string>{"A paralyzed creature is incapacitated (see the condition) and can’t move or speak.",
                "The creature automatically fails Strength and Dexterity Saving Throws.",
                "Attack rolls against the creature have advantage.",
                "Any Attack that hits the creature is a critical hit if the attacker is within 5 feet of the creature." },
            new List<string>{"A petrified creature is transformed, along with any nonmagical object it is wearing or carrying, into a solid inanimate substance (usually stone). Its weight increases by a factor of ten, and it ceases aging.",
                "The creature is incapacitated (see the condition), can’t move or speak, and is unaware of its surroundings.",
                "Attack rolls against the creature have advantage.",
                "The creature automatically fails Strength and Dexterity Saving Throws.",
                "The creature has Resistance to all damage.",
                "The creature is immune to poison and disease, although a poison or disease already in its system is suspended, not neutralized." },
            new List<string>{"A poisoned creature has disadvantage on Attack rolls and Ability Checks." },
            new List<string>{"A prone creature’s only Movement option is to crawl, unless it stands up and thereby ends the condition.",
                "The creature has disadvantage on Attack rolls.",
                "An Attack roll against the creature has advantage if the attacker is within 5 feet of the creature. Otherwise, the Attack roll has disadvantage." },
            new List<string>{"A restrained creature’s speed becomes 0, and it can’t benefit from any bonus to its speed.",
                "Attack rolls against the creature have advantage, and the creature’s Attack rolls have disadvantage.",
                "The creature has disadvantage on Dexterity Saving Throws." },
            new List<string>{"A stunned creature is incapacitated (see the condition), can’t move, and can speak only falteringly.",
                "The creature automatically fails Strength and Dexterity Saving Throws.",
                "Attack rolls against the creature have advantage." },
            new List<string>{"An unconscious creature is incapacitated (see the condition), can’t move or speak, and is unaware of its surroundings.",
                "The creature drops whatever it’s holding and falls prone.",
                "The creature automatically fails Strength and Dexterity Saving Throws.",
                "Attack rolls against the creature have advantage.",
                "Any Attack that hits the creature is a critical hit if the attacker is within 5 feet of the creature." }
        };

        public static string GetName(this Condition condition) {
            return ConditionNames[(int)condition];
        }

        public static List<string> GetDescription(this Condition condition) {
            return ConditionDescriptions[(int)condition];
        }

        /// <summary>
        /// Returns the Condition that most closely matches text, if any
        /// </summary>
        public static Option<Condition> GetCondition(this string text) {
            var result = Option<Condition>.Null;
            for(int index = 0; index < ConditionCount; ++index) {
                if(GetName((Condition)index).StartsWith(text, StringComparison.InvariantCultureIgnoreCase)) {
                    if (result.HasValue) {
                        return Option<Condition>.Null;
                    }
                    else {
                        result = (Condition)index;
                    }
                }
            }
            return result;
        }
    }

    public class Conditions
    {
        private List<Condition> conditions;

        public Conditions() {
            conditions = new List<Condition>();
        }
        public Conditions(int capacity) {
            conditions = new List<Condition>(capacity);
        }
        private Conditions(List<Condition> conditions) {
            this.conditions = conditions;
        }

        public Conditions Clone() {
            return new Conditions(new List<Condition>(conditions));
        }
        public bool IsEmpty() {
            return conditions.Count == 0;
        }
        public bool Contains(Condition condition) {
            // Find Best Position
            int N = 0;
            while (conditions.Count > 1 << N) {
                ++N;
            }
            int pos = 0;
            for (int n = N - 1; n >= 0; --n) {
                int next = pos + (1 << n);
                if (next < conditions.Count && (int)conditions[next] <= (int)condition) {
                    pos = next;
                }
            }
            // Check if Item already exists
            if (pos < conditions.Count && conditions[pos] == condition) {
                return true;
            }
            else {
                return false;
            }
        }
        public IEnumerator<Condition> GetEnumerator() {
            return conditions.GetEnumerator();
        }

        public bool Add(Condition condition) {
            // Find Insert Position
            int N = 0;
            while (conditions.Count + 1 > 1 << N) {
                ++N;
            }
            int pos = 0;
            for (int n = N - 1; n >= 0; --n) {
                int next = pos + (1 << n);
                if (next - 1 < conditions.Count && (int)conditions[next - 1] <= (int)condition) {
                    pos = next;
                }
            }
            // Check if Item already exists
            if (pos - 1 >= 0 && conditions[pos - 1] == condition) {
                return false;
            }
            else {
                conditions.Insert(pos, condition);
                return true;
            }
        }
        public bool Remove() {
            if(conditions.Count != 0) {
                conditions.RemoveAt(conditions.Count - 1);
                return true;
            }
            return false;
        }
        public bool Remove(Condition condition) {
            // Find Best Position
            int N = 0;
            while (conditions.Count > 1 << N) {
                ++N;
            }
            int pos = 0;
            for (int n = N - 1; n >= 0; --n) {
                int next = pos + (1 << n);
                if (next < conditions.Count && (int)conditions[next] <= (int)condition) {
                    pos = next;
                }
            }
            // Check if Item already exists
            if (pos < conditions.Count && conditions[pos] == condition) {
                conditions.RemoveAt(pos);
                return true;
            }
            else {
                return false;
            }
        }
        public void Clear() {
            conditions.Clear();
        }
    }

    public struct Note
    {
        public int index;
        public string value;

        public Note(int index, string value) {
            this.index = index;
            this.value = value;
        }
    }


    /// <summary>
    /// Represents an Actor in Initiative List
    /// </summary>
    public class Actor
    {
        public int id = int.MinValue;
        public string name = null;
        public int initiative = int.MinValue;
        public int currentHP = int.MinValue;
        public int maximumHP = int.MinValue;
        public int temporaryHP = int.MinValue;
        public ConsoleColor base_text;
        public ConsoleColor base_bg;
        public ConsoleColor active_text;
        public ConsoleColor active_bg;
        public Option<int> armorClass = Option<int>.Null;
        public Option<string> speed = Option<string>.Null;
        public Option<Scores> scores = Option<Scores>.Null;
        public List<Attack> attacks = new List<Attack>();
        public Conditions conditions = null;
        public int nextNoteIndex = 1;
        public List<Note> notes = new List<Note>();
        public Option<List<string>> description = Option<List<string>>.Null;
        public bool remove = Program.settings.defaultRemove;
        
        /// <summary>
        /// Return whether Condition was added
        /// </summary>
        public bool AddCondition_Silent(Condition condition) {
            return conditions.Add(condition);
        }
        /// <summary>
        /// Return whether Condition was added
        /// </summary>
        public bool AddCondition(Condition condition) {
            if (conditions.Add(condition)) {
                Program.data.changes.Push(new AddCondition(id, condition));
                return true;
            }
            else {
                return false;
            }
        }
        /// <summary>
        /// Return whether Condition was removed
        /// </summary>
        public bool RemoveCondition_Silent(Condition condition) {
            return conditions.Remove(condition);
        }
        /// <summary>
        /// Return whether Condition was removed
        /// </summary>
        public bool RemoveCondition(Condition condition) {
            if (conditions.Remove(condition)) {
                Program.data.changes.Push(new RemoveCondition(id, condition));
                return true;
            }
            else {
                return false;
            }
        }

        public void AddNote(string text) {
            notes.Add(new Note(nextNoteIndex, text));
            Program.data.changes.Push(new AddNote(id, nextNoteIndex));
            ++nextNoteIndex;
        }
        public void ReaddNote_Silent(Note note) {
            // Find Position
            int N = 0;
            while(notes.Count + 1 > 1 << N) {
                ++N;
            }
            int pos = 0;
            for(int n = N - 1; n >= 0; --n) {
                int next = pos + (1 << n);
                if(next - 1 < notes.Count && notes[next - 1].index <= note.index) {
                    pos = next;
                }
            }
            notes.Insert(pos, note);
        }
        public bool RemoveNote_Silent(int index) {
            // Find Position
            int N = 0;
            while (notes.Count > 1 << N) {
                ++N;
            }
            int pos = 0;
            for (int n = N - 1; n >= 0; --n) {
                int next = pos + (1 << n);
                if (next < notes.Count && notes[next].index <= index) {
                    pos = next;
                }
            }
            if(pos < notes.Count && notes[pos].index == index) {
                notes.RemoveAt(pos);
                return true;
            }
            else {
                return false;
            }
        }
        public bool RemoveNote(int index) {
            // Find Position
            int N = 0;
            while (notes.Count > 1 << N) {
                ++N;
            }
            int pos = 0;
            for (int n = N - 1; n >= 0; --n) {
                int next = pos + (1 << n);
                if (next < notes.Count && notes[next].index <= index) {
                    pos = next;
                }
            }
            if (pos < notes.Count && notes[pos].index == index) {
                Program.data.changes.Push(new RemoveNote(id, notes[pos]));
                notes.RemoveAt(pos);
                return true;
            }
            else {
                return false;
            }
        }
        public bool HasNote(int index) {
            // Find Position
            int N = 0;
            while (notes.Count > 1 << N) {
                ++N;
            }
            int pos = 0;
            for (int n = N - 1; n >= 0; --n) {
                int next = pos + (1 << n);
                if (next < notes.Count && notes[next].index <= index) {
                    pos = next;
                }
            }
            return pos < notes.Count && notes[pos].index == index;
        }
    }

    /// <summary>
    /// Represents a valid Actor outside of Initiative List
    /// </summary>
    public class AbstractActor
    {
        public string name = null;
        public Option<List<Token>> initiative = Option<List<Token>>.Null;
        public List<Token> HP = null;
        public Option<List<Token>> temporary = Option<List<Token>>.Null;
        public Option<string> coloringType = Option<string>.Null;
        public Option<Coloring> coloring = Option<Coloring>.Null;
        public Option<List<Token>> armorClass = Option<List<Token>>.Null;
        public Option<string> speed = new Option<string>();
        public Option<AbstractScores> scores = Option<AbstractScores>.Null;
        public List<Attack> attacks = new List<Attack>();
        public Conditions conditions = null;
        public List<string> notes = new List<string>();
        public Option<List<string>> description = Option<List<string>>.Null;
        public bool remove = Program.settings.defaultRemove;

        public Actor CreateInstance(List<ColoringType> coloringTypes, out string errorMessage) {
            Actor actor = new Actor();
            // Get Name
            actor.name = name;
            // Get HP Values
            var eval_HP = HP.Evaluate();
            if(!eval_HP.HasValue){
                errorMessage = "[Actor, name='" + name + "'] Unable to Compute: hp";
                return actor;
            }
            actor.currentHP = eval_HP.Value;
            actor.maximumHP = eval_HP.Value;

            if (temporary.HasValue) {
                var eval_temporary = temporary.Value.Evaluate();
                if (!eval_temporary.HasValue) {
                    errorMessage = "[Actor, name='" + name + "'] Unable to Compute: temp";
                    return actor;
                }
                actor.temporaryHP = eval_temporary.Value;
            }
            // Get Color Values
            if (coloring.HasValue) {
                if (coloringType.HasValue) {
                    errorMessage = "[Actor, name='" + name + "'] Ambiguity: Coloring is defined twice";
                    return actor;
                }
                else {
                    actor.base_text = coloring.Value.base_text;
                    actor.base_bg = coloring.Value.base_bg;
                    actor.active_text = coloring.Value.active_text;
                    actor.active_bg = coloring.Value.active_bg;
                }
            }
            else {
                string targetColoringType;
                if (coloringType.HasValue) {
                    targetColoringType = coloringType.Value;
                }
                else {
                    targetColoringType = Program.settings.defaultColoringType;
                }

                bool found = false;
                for (int index = 0; index < coloringTypes.Count; ++index) {
                    if(coloringTypes[index].name == targetColoringType) {
                        actor.base_text = coloringTypes[index].base_text;
                        actor.base_bg = coloringTypes[index].base_bg;
                        actor.active_text = coloringTypes[index].active_text;
                        actor.active_bg = coloringTypes[index].active_bg;
                        found = true;
                        break;
                    }
                }
                if (!found) {
                    errorMessage = "[Actor, name='" + name + "'] Coloring not found: " + targetColoringType;
                    return actor;
                }
            }
            // Get Armor Class
            if (armorClass.HasValue) {
                var eval_armorClass = armorClass.Value.Evaluate();
                if (!eval_armorClass.HasValue) {
                    errorMessage = "[Actor, name='" + name + "'] Unable to Compute: ac";
                    return actor;
                }
                actor.armorClass = eval_armorClass.Value;
            }
            // Get Speed
            actor.speed = speed;
            // Get Scores
            if (scores.HasValue) {
                var eval_scores = scores.Value.GetScores(out string scoresMessage);
                if(scoresMessage != null) {
                    errorMessage = "[Actor, name='" + name + "']" + scoresMessage;
                    return actor;
                }
                actor.scores = eval_scores;
            }
            // Get Intiative
            if (initiative.HasValue) {
                var eval_initiative = initiative.Value.Evaluate();
                if (!eval_initiative.HasValue) {
                    errorMessage = "[Actor, name='" + name + "'] Unable to Compute: ini";
                    return actor;
                }
                actor.initiative = eval_initiative.Value;
            }
            else {
                if (actor.scores.HasValue) {
                    actor.initiative = ObjectParser.Roll(1, 20) + actor.scores.Value.dexterityModifier;
                }
                else {
                    errorMessage = "[Actor, name='" + name + "'] Unable to Determine Value: ini";
                    return actor;
                }
            }
            // Get Attacks, Description, Conditions 
            actor.attacks = attacks;
            actor.description = description;
            if(conditions == null) {
                actor.conditions = new Conditions();
            }
            else {
                actor.conditions = conditions.Clone();
            }
            // Get Notes
            for(int index = 0; index < notes.Count; ++index) {
                actor.notes.Add(new Note(actor.nextNoteIndex, notes[index]));
                ++actor.nextNoteIndex;
            }
            // Get Remove
            actor.remove = remove;
            
            errorMessage = null;
            return actor;
        }
    }

    public class Group
    {
        public string name;
        public List<Tuple<string, List<Token>>> actors;

        public Group() {
            name = null;
            actors = new List<Tuple<string, List<Token>>>();
        }
    }
}