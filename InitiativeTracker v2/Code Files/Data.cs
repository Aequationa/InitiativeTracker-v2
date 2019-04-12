using System.Collections.Generic;

namespace InitiativeTracker
{
    public struct Settings
    {
        public string defaultColoringType;
        public bool defaultRemove;
        public int defaultType;
        public int tabWidth;
    }

    public abstract class Change
    {
        public abstract void Undo();
    }

    public class HPChange : Change
    {
        readonly int id;
        readonly int currentHP;
        readonly int maximumHP;
        readonly int temporaryHP;

        public HPChange(Actor actor) {
            id = actor.id;
            currentHP = actor.currentHP;
            maximumHP = actor.maximumHP;
            temporaryHP = actor.temporaryHP;
        }

        public override void Undo() {
            var actor = Program.data.GetActor(id);
            if (actor != null) {
                actor.currentHP = currentHP;
                actor.maximumHP = maximumHP;
                actor.temporaryHP = temporaryHP;
            }
        }
    }
    public class RemoveActor : Change
    {
        readonly int id;
        readonly int pos;

        public RemoveActor(int id, int pos) {
            this.id = id;
            this.pos = pos;
        }

        public override void Undo() {
            Program.data.ResurrectActor_Silent(id, pos);
        }
    }
    public class AddActors : Change
    {
        public List<int> ids;

        public AddActors(Actor actor) {
            ids = new List<int> { actor.id };
        }
        public AddActors() {
            ids = new List<int>();
        }

        public override void Undo() {
            for (int index = 0; index < ids.Count; ++index) {
                Program.data.DeleteActor_Silent(ids[index]);
            }
        }
    }
    public class AddNote : Change
    {
        int id;
        int index;

        public AddNote(int id, int index) {
            this.id = id;
            this.index = index;
        }

        public override void Undo() {
            var actor = Program.data.GetActor(id);
            if(actor != null) {
                actor.RemoveNote_Silent(index);
            }
        }
    }
    public class RemoveNote : Change
    {
        public int id;
        public Note note;

        public RemoveNote(int id, Note note) {
            this.id = id;
            this.note = note;
        }

        public override void Undo() {
            var actor = Program.data.GetActor(id);
            if(actor != null) {
                actor.ReaddNote_Silent(note);
            }
        }
    }
    public class AddCondition : Change
    {
        public int id;
        public Condition condition;

        public AddCondition(int id, Condition condition) {
            this.id = id;
            this.condition = condition;
        }

        public override void Undo() {
            var actor = Program.data.GetActor(id);
            if(actor != null) {
                actor.RemoveCondition_Silent(condition);
            }
        }
    }
    public class RemoveCondition : Change
    {
        public int id;
        public Condition condition;

        public RemoveCondition(int id, Condition condition) {
            this.id = id;
            this.condition = condition;
        }

        public override void Undo() {
            var actor = Program.data.GetActor(id);
            if(actor != null) {
                actor.AddCondition_Silent(condition);
            }
        }
    }


    public class Data
    {
        // Note: This List is of all the alive Stuff and it is sorted at all times
        public List<int> idList;
        // Note: This list is id-sorted at all times
        private List<Actor> actors;

        public List<ColoringType> loadedColorings;
        public List<AbstractActor> loadedActors;
        public List<Group> loadedGroups;

        public Stack<Change> changes;

        int nextID;

        public Data() {
            idList = new List<int>();
            actors = new List<Actor>();
            loadedColorings = new List<ColoringType>();
            loadedActors = new List<AbstractActor>();
            loadedGroups = new List<Group>();

            changes = new Stack<Change>();

            nextID = 0;
        }

        public bool HasColoringType(string coloringType) {
            for(int index = 0; index < loadedColorings.Count; ++index) {
                if(loadedColorings[index].name == coloringType) {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Returns (dead or alive) Actor
        /// </summary>
        public Actor GetActor(int id) {
            // Search
            int N = 0;
            while (actors.Count > (1 << N)) {
                ++N;
            }
            int pos = 0;
            for (int n = N - 1; n >= 0; --n) {
                int next = pos + (1 << n);
                if (next < actors.Count && actors[next].id <= id) {
                    pos = next;
                }
            }
            if(pos < actors.Count && actors[pos].id == id) {
                return actors[pos];
            }
            else {
                return null;
            }
        }

        public void AddActor(AbstractActor actor) {
            var instance = actor.CreateInstance(loadedColorings, out string errorMessage);
            if(errorMessage == null) {
                AddActor_Silent(instance);
                changes.Push(new AddActors(instance));
            }
        }
        public void AddActors(List<AbstractActor> actors) {
            var change = new AddActors();
            for(int index = 0; index < actors.Count; ++index) {
                var instance = actors[index].CreateInstance(loadedColorings, out string errorMessage);
                if (errorMessage == null) {
                    AddActor_Silent(instance);
                    change.ids.Add(instance.id);
                }
            }
            changes.Push(change);
        }

        public void AddActor_Silent(Actor actor) {
            // Set ID
            actor.id = nextID;
            ++nextID;
            actors.Add(actor);

            // Insert into List
            int N = 0;
            while (idList.Count + 1 > 1 << N) {
                ++N;
            }
            int pos = 0;
            for (int n = N - 1; n >= 0; --n) {
                int next = pos + (1 << n);
                if (next - 1 < idList.Count && GetActor(idList[next - 1]).initiative >= actor.initiative) {
                    pos = next;
                }
            }

            if (idList.Count == 0) {
                idList.Insert(pos, actor.id);
                Program.outputData.Info_SetActive(0, -1);
                Program.outputData.Info_SetSelected(0, -1);
            }
            else {
                int selectedID = idList[Program.outputData.Info_GetSelected()];
                idList.Insert(pos, actor.id);
                Program.outputData.Info_SetActive(Program.outputData.Info_GetActive() < pos ? Program.outputData.Info_GetActive() : Program.outputData.Info_GetActive() + 1, selectedID);
                Program.outputData.Info_SetSelected(Program.outputData.Info_GetSelected() < pos ? Program.outputData.Info_GetSelected() : Program.outputData.Info_GetSelected() + 1, selectedID);
            }
        }
        public void AddActor(Actor actor) {
            AddActor_Silent(actor);
            changes.Push(new AddActors(actor));
        }
        public void ResurrectActor_Silent(int id, int pos) {
            if(idList.Count == 0) {
                idList.Insert(pos, id);
                Program.outputData.Info_SetActive(0, -1);
                Program.outputData.Info_SetSelected(0, -1);
            }
            else { 
                int selectedID = idList[Program.outputData.Info_GetSelected()];
                idList.Insert(pos, id);
                Program.outputData.Info_SetActive(Program.outputData.Info_GetActive() < pos ? Program.outputData.Info_GetActive() : Program.outputData.Info_GetActive() + 1, selectedID);
                Program.outputData.Info_SetSelected(Program.outputData.Info_GetSelected() < pos ? Program.outputData.Info_GetSelected() : Program.outputData.Info_GetSelected() + 1, selectedID);
            }
        }
        public void RemoveActor(int id) {
            for (int pos = 0; pos < idList.Count; ++pos) {
                if (idList[pos] == id) {
                    var selectedID = idList[Program.outputData.Info_GetSelected()];
                    idList.RemoveAt(pos);
                    changes.Push(new RemoveActor(id, pos));

                    if (idList.Count == 0) {
                        Program.outputData.Info_SetActive(0, selectedID);
                        Program.outputData.Info_SetSelected(0, selectedID);
                    }
                    else {
                        if(Program.outputData.Info_GetActive() == idList.Count) {
                            Program.outputData.Info_SetActive(idList.Count - 1, selectedID);
                        }
                        else {
                            Program.outputData.Info_SetActive(Program.outputData.Info_GetActive() > pos ? Program.outputData.Info_GetActive() - 1 : Program.outputData.Info_GetActive(), selectedID);
                        }
                        if(Program.outputData.Info_GetSelected() == idList.Count) {
                            Program.outputData.Info_SetSelected(idList.Count - 1, selectedID);
                        }
                        else {
                            Program.outputData.Info_SetSelected(Program.outputData.Info_GetSelected() > pos ? Program.outputData.Info_GetSelected() - 1 : Program.outputData.Info_GetSelected(), selectedID);
                        }
                    }
                    break;
                }
            }
        }
        public void RemoveActor_Silent(int id) {
            for (int pos = 0; pos < idList.Count; ++pos) {
                if (idList[pos] == id) {
                    var selectedID = idList[Program.outputData.Info_GetSelected()];
                    idList.RemoveAt(pos);

                    if (idList.Count == 0) {
                        Program.outputData.Info_SetActive(0, selectedID);
                        Program.outputData.Info_SetSelected(0, selectedID);
                    }
                    else {
                        if (Program.outputData.Info_GetActive() == idList.Count) {
                            Program.outputData.Info_SetActive(idList.Count - 1, selectedID);
                        }
                        else {
                            Program.outputData.Info_SetActive(Program.outputData.Info_GetActive() > pos ? Program.outputData.Info_GetActive() - 1 : Program.outputData.Info_GetActive(), selectedID);
                        }
                        if (Program.outputData.Info_GetSelected() == idList.Count) {
                            Program.outputData.Info_SetSelected(idList.Count - 1, selectedID);
                        }
                        else {
                            Program.outputData.Info_SetSelected(Program.outputData.Info_GetSelected() > pos ? Program.outputData.Info_GetSelected() - 1 : Program.outputData.Info_GetSelected(), selectedID);
                        }
                    }
                    break;
                }
            }
        }

        public void DeleteActor_Silent(int id) {
            RemoveActor_Silent(id);
            // Search
            int N = 0;
            while (actors.Count > (1 << N)) {
                ++N;
            }
            int pos = 0;
            for (int n = N - 1; n >= 0; --n) {
                int next = pos + (1 << n);
                if (next < actors.Count && actors[next].id <= id) {
                    pos = next;
                }
            }
            if (pos < actors.Count && actors[pos].id == id) {
                actors.RemoveAt(pos);
            }


        }

        // TODO!
        public void GainTemporary(int id, int amount) {
            var actor = GetActor(id);
            if (actor != null) {
                changes.Push(new HPChange(actor));
                if (amount > 0) {
                    actor.temporaryHP += amount;
                }
            }
        }
        public void GainHealth(int id, int amount) {
            var actor = GetActor(id);
            if (actor != null) {
                changes.Push(new HPChange(actor));
                if (amount > 0) {
                    actor.currentHP += amount;
                    if (actor.currentHP > actor.maximumHP) {
                        actor.currentHP = actor.maximumHP;
                    }
                }
            }
        }
        public void LoseHealth(int id, int amount) {
            var actor = GetActor(id);
            if (actor != null) {
                changes.Push(new HPChange(actor));
                if (amount > 0) {
                    if (amount <= actor.temporaryHP) {
                        actor.temporaryHP -= amount;
                    }
                    else {
                        actor.currentHP -= amount - actor.temporaryHP;
                        actor.temporaryHP = 0;
                    }
                    if (actor.currentHP <= 0 && actor.remove) {
                        RemoveActor(id);
                    }
                }
            }
        }
    }
}