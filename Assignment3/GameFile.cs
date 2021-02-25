using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
/************************************************************
* Assignment 3
* Programmers: Robert Tyler Trotter z1802019
*              Mitchell Trafton     z1831076
***********************************************************/

namespace Assignment3

{
    public class GameFile
    {
        //constructor
        public GameFile()
        {

            LoadIn();

        }

        /***************************************************************
		 * LoadIn()
		 * purpose: initialize our dictionaries by reading the files in 
		 * bin/init, for the player and item classes, as well as our guilds
		 * 
		 ***************************************************************/
        private void LoadIn()
        {
            try
            {
                //loading in the guilds into our Dictionary for use
                using (StreamReader inGuild = new StreamReader("init/guilds.txt"))
                {
                    string line;
                    uint id;
                    uint gtype;
                    string name;
                    //we go line by line and begin creating our guilds from the file
                    while ((line = inGuild.ReadLine()) != null)
                    {
                        string[] subs = line.Split('\t');
                        id = UInt32.Parse(subs[0]);
                        gtype = UInt32.Parse(subs[1]);
                        name = subs[2];
                        Globals.guilds.Add(id, new Guild(id, (GuildType)gtype, name));
                    }

                }
            }
            catch (Exception e)
            {
                Console.WriteLine("guilds.txt could not be read");
                Console.WriteLine(e.Message);
            }

            try
            {
                //loading in the items into our Dictionary for use
                using (StreamReader inItems = new StreamReader("init/equipment.txt"))
                {
                    string line;
                    uint id;
                    string name;
                    int type;
                    uint ilvl;
                    uint primary;
                    uint stamina;
                    uint requirement;
                    string flavor;
                    //this goes line by line to split up the variables so we can assign them properly within our items objects
                    while ((line = inItems.ReadLine()) != null)
                    {
                        string[] subs = line.Split('\t');
                        id = UInt32.Parse(subs[0]);
                        name = subs[1];
                        type = Int32.Parse(subs[2]);
                        ilvl = UInt32.Parse(subs[3]);
                        primary = UInt32.Parse(subs[4]);
                        stamina = UInt32.Parse(subs[5]);
                        requirement = UInt32.Parse(subs[6]);
                        flavor = subs[7];
                        Globals.items.Add(id, new Item(id, name, type, ilvl, primary, stamina, requirement, flavor));
                    }
                    Globals.items.Add(0, new Item(0, "N/A", 13, 0, 0, 0, 0, "N/A")); // handles no item available
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("equipment.txt could not be read");
                Console.WriteLine(e.Message);
            }
            try
            {
                //importing Players now
                using (StreamReader inCharacter = new StreamReader("init/players.txt"))
                {
                    string line;
                    uint id;
                    string name;
                    int race;
                    uint role;
                    int cclass;
                    uint level;
                    uint exp;
                    uint? guildID;
                    uint[] gearSlots;
                    uint[] inventory = new uint[Constants.MAX_INVENTORY_SIZE];

                    while ((line = inCharacter.ReadLine()) != null)
                    {
                        string[] subs = line.Split('\t');
                        id = UInt32.Parse(subs[0]);
                        name = subs[1];
                        race = Int32.Parse(subs[2]);
                        role = UInt32.Parse(subs[3]);
                        cclass = Int32.Parse(subs[4]);
                        level = UInt32.Parse(subs[5]);
                        exp = UInt32.Parse(subs[6]);
                        if (subs[7] == "") guildID = null;
                        else guildID = UInt32.Parse(subs[7]);

                        gearSlots = new uint[subs.Length - 8];
                        /*
						for(int i = 7; i<subs.Length-1; i++)//the rest of the file is inventory, this will record the IDs of player inventory and store them
                        {
							gearSlots[i-7] = UInt32.Parse(subs[i]);
                        }
						*/
                        Globals.characters.Add(id, new Player(id, name, (Race?)race, level, exp, guildID, gearSlots, inventory, (Class)cclass, (Role)role));
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Players.txt could not be read");
                Console.WriteLine(e.Message);
            }

        }
        /*****************************************************************
		 * List<string> PrintItems()
		 * this will loop through the items dictionary and place their 
		 * ToString() in a list of strings to use
		 ****************************************************************/
        public List<string> PrintItems()
        {
            List<string> itemList = new List<string>();//list of items; return variable

            foreach (KeyValuePair<uint, Item> item in Globals.items)
            {
                itemList.Add(item.ToString());
            }
            itemList.Add("End of items");

            return itemList;
        }
        /****************************************************************
		* List<string> PrintGuild() 
		* This will loop through each guild entryusing the ToString override 
		* within that class, and place it in a list of strings for use.
		*****************************************************************/
        public List<string> PrintGuild()
        {
            List<string> guildList = new List<string>();//list of guilds; return variable

            foreach (KeyValuePair<uint, Guild> guild in Globals.guilds)
            {
                guildList.Add(guild.Value.Name);
            }
            guildList.Add("End of Guilds");

            return guildList;
        }
        /****************************************************************
		 * List<string> PrintPlayer()
		 * this will loop through each player entry in the dictionary
		 * using the ToString override within that class, and place it
		 * in a list of strings for use.
		 ****************************************************************/
        public List<string> PrintPlayer()
        {
            List<string> playerList = new List<string>(); //list of strings containing player information; return variable

            foreach (KeyValuePair<uint, Player> character in Globals.characters)
            {
                playerList.Add(character.Value.ToString());
            }
            playerList.Add("End of players");

            return playerList;
        }
        /***************************************************************************
		 * public int SelectPlayer()
		 * returns: int version of a Dictionary Player ID, or -1 for invalid entry
		 * This method lists out all the available characters within the dictionary
		 * for a user to choose from, and then passes that choice back to menu, where
		 * it can then pass it further to another method for use. 
		 * 
		 * 
		 *************************************************************************/
        public int SelectPlayer()
        {
            int selection = 0;//used for user selection
            string userIn;//used to catch user input
            Dictionary<int, uint> translate = new Dictionary<int, uint>();//used to match user input to the key of the character for selection
            foreach (KeyValuePair<uint, Player> character in Globals.characters)
            {
                Console.WriteLine(selection + " " + character.Value.Name);
                translate.Add(selection, character.Key);// match the character key with a shorthand user selection
                selection++;
            }
            Console.WriteLine("Please select the Player: ");
            userIn = Console.ReadLine();
            //try block for input scrubbing, if we catch an exception then we kick back out to menu with no changes done
            try
            {
                selection = Int32.Parse(userIn);
                if (selection < 0 || selection >= Globals.characters.Count)
                {
                    Console.WriteLine("Invalid entry, returning to Menu with no changes");
                }
                else
                {
                    return (int)translate[selection];
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Invalid entry, returning to Menu with no changes");
            }
            return -1;
        }
        /***********************************************************************
		 * public int SelectGuild
		 * input: none
		 * returns: -1 on failure or the ID of a guild cast as an int
		 * The user is given numerical options to select which guild they'd like
		 * to use in whatever function is called. this is passed back.
		 * 
		 * 
		 **********************************************************************/
        public int SelectGuild()
        {
            int selection = 0;//used for user selection
            string userIn;//used to catch user input
            Dictionary<int, uint> translate = new Dictionary<int, uint>();//used to match user input to the key of the character for selection
            foreach (KeyValuePair<uint, Guild> guild in Globals.guilds)
            {
                Console.WriteLine(selection + " " + guild.Value);
                translate.Add(selection, guild.Key);// match the character key with a shorthand user selection
                selection++;
            }
            Console.WriteLine("Please select the guild");
            userIn = Console.ReadLine();
            //try block for input scrubbing, if we catch an exception then we kick back out to menu with no changes done
            try
            {
                selection = Int32.Parse(userIn);
                if (selection < 0 || selection >= Globals.guilds.Count)
                {
                    Console.WriteLine("Invalid entry, returning to Menu with no changes");
                }
                else
                {
                    return (int)translate[selection];
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Invalid entry, returning to Menu with no changes");
            }
            return -1;
        }
        /*************************************************************************
		 * public int selectGear
		 * input: none
		 * returns: -1 on failure or GearID cast to an int
		 * this method lists all the gear available for the user to select from using 
		 * a menu choice and then returns the selected gear's id to menu
		 * 
		 * 
		 *************************************************************************/
        public int SelectGear()
        {
            int selection = 0;//used for user selection
            string userIn;//used to catch user input
            Dictionary<int, uint> translate = new Dictionary<int, uint>();//used to match user input to the key of the character for selection
            foreach (KeyValuePair<uint, Item> item in Globals.items)
            {
                Console.WriteLine(selection + " " + item.Value.Name);
                translate.Add(selection, item.Key);// match the character key with a shorthand user selection
                selection++;
            }
            Console.WriteLine("Please select the Gear");
            userIn = Console.ReadLine();
            //try block for input scrubbing, if we catch an exception then we kick back out to menu with no changes done
            try
            {
                selection = Int32.Parse(userIn);
                if (selection < 0 || selection >= Globals.items.Count)
                {
                    Console.WriteLine("Invalid entry, returning to Menu with no changes");
                }
                else
                {
                    return (int)translate[selection];
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Invalid entry, returning to Menu with no changes");
            }
            return -1;
        }

        /************************************************************************
		* public void LeaveGuild
		* input: uint player id in the character dictionary
		* this code sets the guild ID to zero, effectively removing a player 
		* from a guild.
		* *********************************************************************/
        public void LeaveGuild(uint pid)
        {
            Globals.characters[pid].GuildID = 0;
            if (UpdateFiles(player: true))
                Console.WriteLine(Globals.characters[pid].Name + " Is no longer in a guild.");
        }

        /************************************************************************
		 * public void JoinGuild
		 * input: uint pid(player ID), gid(guild ID)
		 * This assigns the character's guildID field to the one the user specified
		 * and prints a confirmation message if it can be successfully be written to file
		 * *********************************************************************/
        public void JoinGuild(uint pid, uint gid)
        {
            Globals.characters[pid].GuildID = gid;

            if (UpdateFiles(player: true))
                Console.WriteLine(Globals.characters[pid].Name + " is now a member of " + Globals.guilds[gid].Name);
        }
        /**********************************************************************
		 * public void AddExp
		 * input uint pid (Player ID)
		 * this method prompts the user for a value of experience to assign
		 * to a player, and then returns a confirmation of the player's level
		 * and current experience
		 * *********************************************************************/
        public void AddExp(uint pid)
        {
            int xp;
            Console.WriteLine("How much experience would you like to award?");
            try
            {
                xp = Int32.Parse(Console.ReadLine());// read what the user wrote
                if (xp > 0)
                {
                    Globals.characters[pid].Exp = (uint)xp;
                    Console.WriteLine(Globals.characters[pid].Name + " is level: " + Globals.characters[pid].Level + " And has: " + Globals.characters[pid].Exp + "Experience");
                }
            }
            catch (Exception e)
            {

            }
        }
        /******************************************************************
		 * bool Disband guild
		 * input: uint gid, the guild id
		 * this will first identify if the gid is valid, then will go through
		 * the player dictionary and identify members of the guild, setting their
		 * guild ID to 0 before printing a success message, and then removing 
		 * the guild from the guild dictionary
		 * 
		 * returns true/false depending on success
		 ******************************************************************/
        public bool DisbandGuild(uint gid)
        {
            if (Globals.guilds.ContainsKey(gid))
            {
                foreach (KeyValuePair<uint, Player> character in Globals.characters)
                {
                    if (character.Value.GuildID == gid) character.Value.GuildID = 0;
                }
                Globals.guilds.Remove(gid);

                if (UpdateFiles(true, true))
                {
                    Console.WriteLine("Guild Successfully removed");
                    return true;
                }
                else return false;
            }
            else
            {
                Console.WriteLine("Error: guild with ID: " + gid + "Not found");
                return false;
            }
        }
        /**********************************************************************
		 * Public Void Sorting()
		 * no inputs
		 * This function places both items and players into array lists, sorts them
		 * using the overloaded Icomparables that are written, and prints them out
		 * 
		 **********************************************************************/
        public void Sorting()
        {
            ArrayList itemList = new ArrayList();
            ArrayList playerList = new ArrayList();
            foreach (KeyValuePair<uint, Item> item in Globals.items)
            {
                itemList.Add(item.Value);
            }
            foreach (KeyValuePair<uint, Player> player in Globals.characters)
            {
                playerList.Add(player.Value);

            }
            itemList.Sort();
            playerList.Sort();

            foreach (Item item in itemList)
            {
                Console.WriteLine(item.ToString());
            }
            foreach (Player player in playerList)
            {
                Console.WriteLine(player.ToString());
            }
        }


        public bool AddPlayer(Player p)
        {
            /******************************************************************
			 * Public bool AddPlayer()
			 * 
			 * Adds player to players.cs along with global characters list.
			 * 
			 * Params:
			 * @p = Player object to add.
			 * 
			 * Returns true upon successful write.
			 *****************************************************************/
            try
            {
                string newPlayerEntry = "";

                newPlayerEntry += p.ID.ToString() + '\t';
                newPlayerEntry += p.Name + '\t';
                newPlayerEntry += ((int)p.Race).ToString() + '\t';
                newPlayerEntry += ((int)p.Class_).ToString() + '\t';
                newPlayerEntry += p.Level.ToString() + '\t';
                newPlayerEntry += p.Exp.ToString() + '\t';
                newPlayerEntry += p.GuildID.ToString();

                File.AppendAllText(@"init/players.txt", Environment.NewLine + newPlayerEntry);

                Globals.characters.Add(p.ID, p);

                return true;
            }
            catch (Exception e)
            {//if player was unable to be created, return false
                return false;
            }
        }


        public bool AddGuild(Guild g)
        {
            /******************************************************************
			 * Public bool AddGuild()
			 * 
			 * Adds guild to guilds.cs along with global guilds list.
			 * 
			 * Params:
			 * @g = Guild object to add.
			 * 
			 * Returns true upon successful write.
			 *****************************************************************/

            try
            {
                string newGuildEntry = "";

                newGuildEntry += g.GID.ToString() + '\t';
                newGuildEntry += g.Name + '-';
                newGuildEntry += g.Server;

                File.AppendAllText(@"init/guilds.txt", Environment.NewLine + newGuildEntry);

                Globals.guilds.Add(g.GID, g);

                return true;
            }
            catch (Exception e)
            {//if new guild was unable to be created, return false
                return false;
            }
        }

        public bool UpdateFiles(bool player = false, bool guild = false)
        {
            /************************************************************************************
			 * Public bool UpdateFiles()
			 * 
			 * Updates the players.txt and/or guilds.txt files with information in their corresponding
			 * global dictionaries.
			 * 
			 * Params:
			 * @player = True if players.txt should be updated.
			 * @guild  = True if guilds.txt should be updated.
			 * 
			 * Returns true upon successful write(s).
			 ************************************************************************************/

            bool success = true;//true if all files were updated successfully; return variable

            if (player)
            {
                try
                {
                    using (File.Create(@"init/players_tmp.txt")) { } //create temp file for storing new player entries

                    foreach (Player p in Globals.characters.Values)
                    {
                        string newPlayerEntry = "";

                        newPlayerEntry += p.ID.ToString() + '\t';
                        newPlayerEntry += p.Name + '\t';
                        newPlayerEntry += ((int)p.Race).ToString() + '\t';
                        newPlayerEntry += ((int)p.Class_).ToString() + '\t';
                        newPlayerEntry += p.Level.ToString() + '\t';
                        newPlayerEntry += p.Exp.ToString() + '\t';
                        newPlayerEntry += p.GuildID.ToString();

                        if (new FileInfo(@"init/players_tmp.txt").Length == 0) File.AppendAllText(@"init/players_tmp.txt", newPlayerEntry);
                        else File.AppendAllText(@"init/players_tmp.txt", Environment.NewLine + newPlayerEntry);
                    }

                    File.Copy(@"init/players_tmp.txt", @"init/players.txt", true);
                    File.Delete(@"init/players_tmp.txt");
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error: Unable to overwrite existing player data.");
                    success = false;
                }
            }

            if (guild)
            {
                try
                {
                    using (File.Create(@"init/guilds_tmp.txt")) { } //create temp file for storing new player entries

                    foreach (Guild g in Globals.guilds.Values)
                    {
                        string newGuildEntry = "";

                        newGuildEntry += g.GID.ToString() + '\t';
                        newGuildEntry += g.Name + '-';
                        newGuildEntry += g.Server;

                        if (new FileInfo(@"init/guilds_tmp.txt").Length == 0) File.AppendAllText(@"init/guilds_tmp.txt", newGuildEntry);
                        else File.AppendAllText(@"init/guilds_tmp.txt", Environment.NewLine + newGuildEntry);
                    }

                    File.Copy(@"init/guilds_tmp.txt", @"init/guilds.txt", true);
                    File.Delete(@"init/guilds_tmp.txt");
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error: Unable to overwrite existing guild data.");
                    success = false;
                }
            }

            return success;
        }


        /************************************************************************
        *************************************************************************
        **********************ASSIGNMENT 3 METHODS*******************************
        *************************************************************************
        *************************************************************************/

        /************************************************************************
         * racePercentage
         * input: string server name
         * output: arraylist of strings
         * description: This method uses Linq to first query our Guilds Dictionary
         * to find all the ones on the same server, then querys our player dictionary
         * to find all the players of those guilds. it then groups and counts those
         * players by race
         ************************************************************************/
        public ArrayList racePercent(string server)
        {
            ArrayList guildList = new ArrayList();
            double totalPlayers = 0;
            Dictionary<string, int> raceTally = new Dictionary<string, int>();
            //First we need to locate all the guilds in the server that the query is asking for
            var guildQuery =
                from S in Globals.guilds
                where S.Value.Server.CompareTo(server) == 0
                select S.Key;// we store the names of those guilds in the servers in the serverQuery Var
            //Now we store our values in the guildlist for later use when querying players
            foreach(uint gId in guildQuery)
            {
                var playerQuery =
                    from S in Globals.characters
                    where S.Value.GuildID == gId
                    group S by S.Value.Race into raceCount //Here we group the names and races to be used and counted
                    select raceCount;
                // and now we begin counting up our races
                foreach(var tRace in playerQuery)
                {
                    if(raceTally.ContainsKey(tRace.Key.ToString()))
                    {
                        raceTally[tRace.Key.ToString()] += tRace.Count(); // if the race is added, add the tally
                    }
                    else
                    {
                        raceTally.Add(tRace.Key.ToString(), tRace.Count());// if we don't have the race added yet, add it
                    }
                    totalPlayers += tRace.Count();
                }
            }
            //Now that our Querying is done we need to change it into an array of strings and pass it off
            guildList.Add("percentage of races for" + server + " server \n");
            foreach(KeyValuePair<string,int> tally in raceTally)
            {
                double percentage = tally.Value / totalPlayers;
                guildList.Add(tally.Key + "\t" + percentage);
            }
            return guildList;

        }
    }
}