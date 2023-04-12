using Microsoft.Xna.Framework;
using System;

namespace ShatteredSkies.Classes
{
    public class Chaos : Relic
    {
        //Global variables
        public bool[] Slots;
        public double[] Timers = new double[4] { 0, 0, 0, 0 };
        public int RelicOfChoice = 0;
        public string[] ChaosRelics = new string[4] { "", "", "", "" };
        //local variables
        private bool BlockOffAdding = false;
        public Chaos(int power, SceneManager sceneman, bool[] slots, Player play) : base(power, sceneman, play)
        {
            PowerLevel = power;
            SceneMan = sceneman;
            Slots = slots;
            if (PowerLevel > 2)//max power 
            {
                Slots[^1] = true;
            }
            ConnectedPlayer = play;

            ModifiesPlayerUpdate = true;
        }
        public override void ModPlayUpdate(Player play, GameTime GT)
        {
            for (int i = 0; i < 4; i++)
            {
                if (Slots[i])
                {
                    Timers[i] -= GT.ElapsedGameTime.TotalSeconds;
                    if (Timers[i] <= 0)
                    {
                        RelicOfChoice = SceneMan.rand.Next(0, 10);
                        if (i < 3)
                        {
                            ConnectedPlayer.CurrentRelics[i] = RelicOfChoice;
                        }
                        //gets rid of old stuff
                        switch (ChaosRelics[i])
                        {
                            case "Crimson":
                                CleanseRelic(typeof(Crimson));
                                break;
                            case "Magic":
                                CleanseRelic(typeof(Magic));
                                break;
                            case "Corruption":
                                CleanseRelic(typeof(Corruption));
                                break;
                            case "Living":
                                CleanseRelic(typeof(Living));
                                break;
                            case "Chronology":
                                CleanseRelic(typeof(Chronology));
                                break;
                            case "Soul":
                                CleanseRelic(typeof(Soul));
                                break;
                            case "Energy":
                                CleanseRelic(typeof(Energy));
                                break;
                            case "Future":
                                CleanseRelic(typeof(Future));
                                break;
                            case "Essence":
                                CleanseRelic(typeof(Essence));
                                break;
                        }
                        //adds new stuff
                        switch (RelicOfChoice)
                        {
                            case 0: //////////////// NOthing
                                ChaosRelics[i] = "";
                                break;
                            case 1: ////////////////// Crimson
                                foreach (Relic rel in SceneMan.ActiveRelics)
                                {
                                    if (rel is Crimson)
                                    {
                                        rel.PowerLevel += 1;
                                        ChaosRelics[i] = "Crimson";
                                        BlockOffAdding = true;
                                        break;
                                    }
                                }
                                if (!BlockOffAdding)
                                {
                                    SceneMan.ActiveRelics.Add(new Crimson(1, SceneMan, ConnectedPlayer));
                                    ChaosRelics[i] = "Crimson";
                                }
                                break;
                            case 2: ///////////// MAGIC
                                foreach (Relic rel in SceneMan.ActiveRelics)
                                {
                                    if (rel is Magic)
                                    {
                                        rel.PowerLevel += 1;
                                        ChaosRelics[i] = "Magic";
                                        BlockOffAdding = true;
                                        break;
                                    }
                                }
                                if (!BlockOffAdding)
                                {
                                    SceneMan.ActiveRelics.Add(new Magic(1, SceneMan, ConnectedPlayer));
                                    ChaosRelics[i] = "Magic";
                                }
                                break;
                            case 3:  ///////////// CORRUPTION
                                foreach (Relic rel in SceneMan.ActiveRelics)
                                {
                                    if (rel is Corruption)
                                    {
                                        rel.PowerLevel += 1;
                                        ChaosRelics[i] = "Corruption";
                                        BlockOffAdding = true;
                                        break;
                                    }
                                }
                                if (!BlockOffAdding)
                                {
                                    SceneMan.ActiveRelics.Add(new Corruption(1, SceneMan, ConnectedPlayer));
                                    ChaosRelics[i] = "Corruption";
                                }
                                break;
                            case 4:  ///////////// LIVING
                                foreach (Relic rel in SceneMan.ActiveRelics)
                                {
                                    if (rel is Living)
                                    {
                                        rel.PowerLevel += 1;
                                        ChaosRelics[i] = "Living";
                                        BlockOffAdding = true;
                                        break;
                                    }
                                }
                                if (!BlockOffAdding)
                                {
                                    SceneMan.ActiveRelics.Add(new Living(1, SceneMan, ConnectedPlayer));
                                    ChaosRelics[i] = "Living";
                                }
                                break;
                            case 5:  ///////////// CHRONOLOGY
                                foreach (Relic rel in SceneMan.ActiveRelics)
                                {
                                    if (rel is Chronology)
                                    {
                                        rel.PowerLevel += 1;
                                        ChaosRelics[i] = "Chronology";
                                        BlockOffAdding = true;
                                        break;
                                    }
                                }
                                if (!BlockOffAdding)
                                {
                                    SceneMan.ActiveRelics.Add(new Chronology(1, SceneMan, ConnectedPlayer));
                                    ChaosRelics[i] = "Chronology";
                                }
                                break;
                            case 6:  ///////////// Soul
                                foreach (Relic rel in SceneMan.ActiveRelics)
                                {
                                    if (rel is Soul)
                                    {
                                        rel.PowerLevel += 1;
                                        ChaosRelics[i] = "Soul";
                                        BlockOffAdding = true;
                                        break;
                                    }
                                }
                                if (!BlockOffAdding)
                                {
                                    SceneMan.ActiveRelics.Add(new Soul(1, SceneMan, ConnectedPlayer));
                                    ChaosRelics[i] = "Soul";
                                }
                                break;
                            case 7:  ///////////// ENERGY
                                foreach (Relic rel in SceneMan.ActiveRelics)
                                {
                                    if (rel is Energy)
                                    {
                                        rel.PowerLevel += 1;
                                        ChaosRelics[i] = "Energy";
                                        BlockOffAdding = true;
                                        break;
                                    }
                                }
                                if (!BlockOffAdding)
                                {
                                    SceneMan.ActiveRelics.Add(new Energy(1, SceneMan, ConnectedPlayer));
                                    ChaosRelics[i] = "Energy";
                                }
                                break;
                            case 8:  ///////////// FUTURE
                                foreach (Relic rel in SceneMan.ActiveRelics)
                                {
                                    if (rel is Future)
                                    {
                                        rel.PowerLevel += 1;
                                        ChaosRelics[i] = "Future";
                                        BlockOffAdding = true;
                                        break;
                                    }
                                }
                                if (!BlockOffAdding)
                                {
                                    SceneMan.ActiveRelics.Add(new Future(1, SceneMan, ConnectedPlayer));
                                    ChaosRelics[i] = "Future";
                                }
                                break;
                            case 9:  ///////////// ESSENCE
                                foreach (Relic rel in SceneMan.ActiveRelics)
                                {
                                    if (rel is Essence)
                                    {
                                        rel.PowerLevel += 1;
                                        ChaosRelics[i] = "Essence";
                                        BlockOffAdding = true;
                                        break;
                                    }
                                }
                                if (!BlockOffAdding)
                                {
                                    SceneMan.ActiveRelics.Add(new Essence(1, SceneMan, ConnectedPlayer));
                                    ChaosRelics[i] = "Essence";
                                }
                                break;
                        }
                        Timers[i] = SceneMan.rand.Next(50, 150) / 10;
                        BlockOffAdding = false;
                    }
                }
            }
        }

        private void CleanseRelic(Type type)
        {
            for (int x = 0; x < SceneMan.ActiveRelics.Count; x++) // Main relics
            {
                if (SceneMan.ActiveRelics[x].GetType() == type)
                {
                    if (SceneMan.ActiveRelics[x].PowerLevel > 1)
                    {
                        SceneMan.ActiveRelics[x].PowerLevel -= 1;
                        break;
                    }
                    else
                    {
                        SceneMan.ActiveRelics.RemoveAt(x);
                        for (int y = 0; y < ConnectedPlayer.LocalRelics.Count; y++) // Player Relics
                        {
                            if (ConnectedPlayer.LocalRelics[y].GetType() == type)
                            {
                                ConnectedPlayer.LocalRelics.RemoveAt(y);
                            }
                        }
                        for (int q = 0; q < SceneMan.Allies.Count; q++) // Ally Relics
                        {
                            for (int y = 0; y < SceneMan.Allies[q].LocalRelics.Count; y++) // Ally Relics
                            {
                                if (SceneMan.Allies[q].LocalRelics[y].GetType() == type)
                                {
                                    SceneMan.Allies[q].LocalRelics.RemoveAt(y);
                                }
                            }
                        }
                        break;
                    }
                }
            }
            for (int x = 0; x < SceneMan.Allies.Count; x++) // Ally Relics
            {
                for (int y = 0; y < SceneMan.Allies[x].LocalRelics.Count; y++) // Ally Relics
                {
                    if (SceneMan.Allies[x].LocalRelics[y].GetType() == type)
                    {
                        if (SceneMan.Allies[x].LocalRelics[y].PowerLevel > 1)
                        {
                            SceneMan.Allies[x].LocalRelics[y].PowerLevel -= 1;
                            break;
                        }
                        else
                        {
                            SceneMan.Allies[x].LocalRelics.RemoveAt(x);
                            break;
                        }
                    }
                }
            }
        }
    }
}
