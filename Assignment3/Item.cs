using System;
/************************************************************
 * Assignment 3
 * Programmers: Robert Tyler Trotter z1802019
 *              Mitchell Trafton     z1831076
 ***********************************************************/
namespace Assignment3
{
    class Item : IComparable
    {
        // private attributes
        private uint id; //identifer for the item 
        private string name; // item name
        private itemType type; //where the item can be placed
        private uint ilvl;//
        private uint primary;
        private uint stamina;
        private uint requirement;
        private string flavor; // flavor text
        // public properties
        public uint Id
        {
            get { return id; }

            private set { id = value; }
        }
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        public itemType Type
        {
            get { return type; }
            set
            {
                if ((int)value < 0 || (int)value > 12) throw new ArgumentOutOfRangeException($"{nameof(value)} must be between 0 and 12."); // chck to see if data is within bounds otherwise throw exception
                type = value;
            }
        }
        public uint Ilvl
        {
            get { return ilvl; }
            set
            {
                if (value > Constants.MAX_ILVL) throw new ArgumentOutOfRangeException($"{nameof(value)} must be between 0 and {Constants.MAX_ILVL}.");
                ilvl = value;
            }
        }
        public uint Primary
        {
            get { return primary; }
            set
            {
                if (value > Constants.MAX_PRIMARY) throw new ArgumentOutOfRangeException($"{nameof(value)} must be between 0 and {Constants.MAX_PRIMARY}.");
                primary = value;
            }
        }
        public uint Stamina
        {
            get { return stamina; }
            set
            {
                if (value > Constants.MAX_STAMINA) throw new ArgumentOutOfRangeException($"{nameof(value)} must be between 0 and {Constants.MAX_STAMINA}.");
                stamina = value;
            }
        }
        public uint Requirement
        {
            get { return requirement; }
            set
            {
                if (value > Constants.MAX_LEVEL) throw new ArgumentOutOfRangeException($"{nameof(value)} must be between 0 and {Constants.MAX_LEVEL}.");
                requirement = value;
            }
        }

        public string Flavor
        {
            get { return flavor; }
            set { flavor = value; }
        }
        public Item()
        {
            id = 0;
            name = "N/A";
            type = itemType.None;
            ilvl = 0;
            primary = 0;
            stamina = 0;
            requirement = 0;
            flavor = "N/A";
        }
        public Item(uint tid, string tname, int ttype, uint tlvl, uint tprimary, uint tstamina, uint trequirement, string tflavor)
        {
            /****************************************
             * Default constructor.
             * 
             * Assigns values to class's attributes.
             ****************************************/

            id = tid;
            name = tname;
            type = (itemType)ttype;
            ilvl = tlvl;
            primary = tprimary;
            stamina = tstamina;
            requirement = trequirement;
            flavor = tflavor;
        }

        int IComparable.CompareTo(object obj)
        {
            /***************************************************
            * IComparable interface, compareTo
            * 
            * Parameters:
            * @input = Item object
            * 
            * Output:
            * An int value representing the order of the 
            * items sorting by their string name variable
            ****************************************************/

            Item other = (Item)obj;

            return String.Compare(name, other.name);
        }

        public override string ToString()
        {
            /***************************************************
             * override ToString 
             * Prints out the item type, name, level requirement
             * and the flavor text on a second line.
             * 
             **************************************************/
            return "(" + type + ") " + name + "|" + ilvl + "|" + "\n" + "   " + flavor;
        }

    }
}