using System.Collections.Generic;
using System.Text;
using System;
/************************************************************
 * Assignment 3
 * Programmers: Robert Tyler Trotter z1802019
 *              Mitchell Trafton     z1831076
 ***********************************************************/

namespace Assignment3
{
    public class Player : IComparable
    {
        ///constructors
        public Player()
        {
            /**************************************************
             * Default constructor. 
             * Sets all private attributes to 0, "", or null.
             *************************************************/

            id = 0;
            name = "";
            race = null;
            level = 0;
            exp = 0;
            guildID = null;
            class_ = null;
            role = null;
        }

        public Player(uint ID = 0, string Name = "", Race? Race_ = null, uint Level = 0, uint Exp = 0,
            uint? GuildID = null, uint[] Gear = null, uint[] Inventory = null, Class? setClass = null, Role? Role_ = null)
        {
            /*******************************************************************
             * Alternate constructor.
             * Allows caller to define class attributes when class is created.
             * All undefined attributes are set to 0, "", or null.
             *****************************************************************/


            id = ID;
            name = Name;
            race = Race_;
            level = Level;
            exp = Exp;
            guildID = GuildID;
            if (Gear == null) gear = new uint[Constants.GEAR_SLOTS];
            else gear = Gear;
            if (Inventory != null) inventory = new List<uint>(Inventory);
            else inventory = null;
            class_ = setClass;
            role = Role_;
        }

        ///prvate attributes
        //player data
        private readonly uint id; //player's ID
        private readonly string name; //player's name
        private readonly Race? race; //player's race
        private uint level; //player's current level
        private uint exp; //current experience amount
        private uint? guildID; //ID of player's current guild
        private Class? class_; //player's class
        private Role? role; //player's role
        private uint[] gear = new uint[Constants.GEAR_SLOTS];
        private List<uint> inventory = new List<uint>();
        //other data
        private bool equipRingFirstSlot = true;//true when next ring should be equiped into the first ring slot, false for second slot
        private bool equipTrinketFirstSlot = true;//true when next trinket should be equiped into the first trinket slot, false for second slot
        private uint invFullSlots = 0;//number of slots in inventory that are full

        ///public attributes
        public uint ID
        {
            get { return id; }
        }

        public string Name
        {
            get { return name; }
        }

        public Race? Race
        {
            get { return race; }
        }

        public uint Level
        {
            get { return level; }
            set
            {
                //set level as requested so long as number does not exceed MAX_LEVEL
                if (value <= Constants.MAX_LEVEL)
                {
                    level = value;
                }
                else
                {
                    Console.WriteLine("ERROR: Level value " + value + " is out of range (max " + Constants.MAX_LEVEL + ").\n" +
                        "Level not changed.");
                }
            }
        }

        public uint Exp
        {
            get { return exp; }
            set
            {
                //whenever Exp is set equal to a value, that value is added to exp
                exp += value;

                LevelUp();//call LevelUp() function to increase the current level if appropriate
            }
        }

        public uint? GuildID
        {
            get { return guildID; }
            set { guildID = value; }
        }

        public Class? Class_
        {
            get { return class_; }
            set
            {
                if (role == null) class_ = value;
                //if current role is not an allowed role for the new class (or new class null), simply set the role to null
                else if (value == null || !Constants.allowedRolls[(Class)class_].Contains((Role)role))
                {
                    role = null;
                    class_ = value;
                }
            }
        }

        public Role? Role
        {
            get { return role; }
            set
            {
                if (class_ == null) throw new Exception("Error: Can't assign a role without a class assigned.");//don't assign a new role without a class_ having a value first
                else if (value == null) role = value;
                else
                {
                    if (Constants.allowedRolls[(Class)class_].Contains((Role)value)) role = value;
                    else throw new Exception("Error: Players of class " + Enum.GetName(typeof(Class), class_) + " cannot have the role " + Enum.GetName(typeof(Role), value) + ".");
                }
            }
        }

        public uint this[int index]
        {
            get { return gear[index]; }
            set { gear[index] = value; }
        }

        ///functions/interfaces

        private void LevelUp()
        {
            /***********************************************************************
             * Levels up the player if there is sufficient exp.
             * 
             * If there is enough exp to level up (>= current level * 1000), 
             * subtract that amount from current exp, increase current level, 
             * and call function again to see if further leveling up should occur.
             **********************************************************************/
            if (exp >= (level * 1000))
            {
                exp -= level * 1000;
                level++;
                LevelUp();
            }
        }

        public int CompareTo(object obj)
        {
            /****************************************
             * CompareTo function for IComparable interface. 
             * 
             * Compares this Player object to another using
             * the 'name' attribute.
             ****************************************/

            if (obj == null || name == null) throw new ArgumentNullException();

            Player Obj = obj as Player;

            if (Obj.Name == null) throw new ArgumentNullException();
            else return name.CompareTo(Obj.Name);
        }

        public void EquipGear(uint newGearID)
        {
            /*****************************************************************************
             * Equips a piece of gear into the appropriate gear slot. 
             * 
             * Will throw an exception if the gear's ID is invalid, 
             * the player's level isn't high enough, or the gear's type is invalid.
             * 
             * Parameters:
             * @newGearID = ID of gear that can be found in the global items dictionary.
             ***************************************************************************/
            if (Globals.items.ContainsKey(newGearID))
            {
                if (level >= Globals.items[newGearID].Requirement)
                {
                    switch (Globals.items[newGearID].Type)
                    {
                        case itemType.Helmet:
                            gear[0] = Globals.items[newGearID].Id;
                            break;

                        case itemType.Neck:
                            gear[1] = Globals.items[newGearID].Id;
                            break;

                        case itemType.Shoulders:
                            gear[2] = Globals.items[newGearID].Id;
                            break;

                        case itemType.Back:
                            gear[3] = Globals.items[newGearID].Id;
                            break;

                        case itemType.Chest:
                            gear[4] = Globals.items[newGearID].Id;
                            break;

                        case itemType.Wrist:
                            gear[5] = Globals.items[newGearID].Id;
                            break;

                        case itemType.Gloves:
                            gear[6] = Globals.items[newGearID].Id;
                            break;

                        case itemType.Belt:
                            gear[7] = Globals.items[newGearID].Id;
                            break;

                        case itemType.Pants:
                            gear[8] = Globals.items[newGearID].Id;
                            break;

                        case itemType.Boots:
                            gear[9] = Globals.items[newGearID].Id;
                            break;

                        case itemType.Ring:
                            if (equipRingFirstSlot) gear[10] = Globals.items[newGearID].Id;
                            else gear[11] = Globals.items[newGearID].Id;
                            equipRingFirstSlot = !equipRingFirstSlot;
                            break;

                        case itemType.Trinket:
                            if (equipTrinketFirstSlot) gear[12] = Globals.items[newGearID].Id;
                            else gear[13] = Globals.items[newGearID].Id;
                            equipTrinketFirstSlot = !equipTrinketFirstSlot;
                            break;

                        default:
                            throw new Exception("Gear has an invalid/missing type.");
                    }
                }
                else throw new Exception("This trinket requires level " + Globals.items[newGearID].Requirement + "to equip. " +
                    "Player's current level is " + level + ".");
            }
            else throw new Exception("Gear ID could not be found.");
        }

        public void UnequipGear(int gearSlot)
        {
            /***************************************************************************
             * Unequips the gear at the desired slot and moves it into the inventory.
             * 
             * If the inventory is at capacity, throw an exception.
             * 
             * Parameters:
             * @gearSlot = Desired gear slot to move to inventory.
             **************************************************************************/

            if (invFullSlots >= Constants.MAX_INVENTORY_SIZE) throw new Exception("Can't unequip gear. All inventory slots are full.");
            else
            {
                if (gear[gearSlot] == 0) return;//don't do anything if gear slot is empty

                inventory.Add(gear[gearSlot]);
                gear[gearSlot] = 0;//clear gear slot when finished

                invFullSlots++;
            }
        }

        public void PrintGearList()
        {
            /****************************************************************
             * Prints player info along with the contents of the gear slots.
             ****************************************************************/

            //print player info
            Console.WriteLine(ToString());

            //print info on gear slots
            itemType printGear = itemType.Helmet; //stores item type to print for each slot
            bool ringPrinted = false, trinketPrinted = false; //true when first ring/trinked slot has been printed
            for (int gearSlot = 0; gearSlot < Constants.GEAR_SLOTS; gearSlot++)
            {
                if (gear[gearSlot] == 0)
                {
                    Console.WriteLine(gearSlot + " | " + Enum.GetName(typeof(itemType), printGear) + ": empty");
                }
                else
                {
                    Console.WriteLine(gearSlot + " | (" + Enum.GetName(typeof(itemType), printGear) + ") " +
                        Globals.items[gear[gearSlot]].Name + " |" + Globals.items[gear[gearSlot]].Stamina +
                        "| --" + Globals.items[gear[gearSlot]].Requirement + "--");

                    Console.WriteLine("\t\"" + Globals.items[gear[gearSlot]].Flavor + "\"");
                }

                //don't increase printGear for first time printing ring/gear slot
                if (printGear == itemType.Ring && !ringPrinted) { ringPrinted = true; continue; }
                if (printGear == itemType.Trinket && !trinketPrinted) { trinketPrinted = true; continue; }

                printGear++;
            }
        }

        public override string ToString()
        {
            /************************************************************************************
             * Returns a string containing player's name, race, level, and guild (if applicable).
             ************************************************************************************/

            string returnInfo;//information to return
            string raceOut; //output for race

            if (race != null) raceOut = Enum.GetName(typeof(Race), race);//only retrive value for race if it is not null
            else raceOut = "n/a"; //if race is null, just print n/a

            returnInfo = "\nName: " + name.PadRight(20, ' ') + "\tRace: " + raceOut.PadRight(20, ' ') + "\tLevel: " + level + "\t\tGuild: ";

            if (guildID != null && guildID != 0)//only print guild if there is a valid ID availible 
                returnInfo += Globals.guilds[(uint)guildID].Name;
            else if (guildID != null && !Globals.guilds.ContainsKey((uint)guildID)) returnInfo += "[error: guild ID not recognized]";
            else returnInfo += "n/a";

            return returnInfo;
        }

        public string ToStringBasic()
        {
            /*************************************************************************************
             * Returns a string containing a player's name, class, and level without any labels.
             *************************************************************************************/
            string classOut; //output for class

            if (class_ != null) classOut = Enum.GetName(typeof(Class), class_);//only retrive value for class if it is not null
            else classOut = "n/a"; //if class is null, just print n/a

            return name.PadRight(15, ' ') + '\t' + classOut.PadRight(7, ' ') + "\t           " + level;
        }
    }
}