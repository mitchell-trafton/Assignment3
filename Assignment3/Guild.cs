using System;
/************************************************************
 * Assignment 3
 * Programmers: Robert Tyler Trotter z1802019
 *              Mitchell Trafton     z1831076
 ***********************************************************/
namespace Assignment3
{

	/****************************************
	 * Class Guilds
	 * Purpose:
	 * creates a guild object to hold the
	 * information about various guilds in 
	 * world of conflictcraft
	 ****************************************/
	public class Guild : IComparable
	{
		private uint gid;
		private GuildType type;
		private string name;
		private string server;

		public Guild()
		{
			/********************************************
			 * Null constructor
			 * No inputs
			 * this creates a blank guild
			 ********************************************/
			gid = 0;
			type = (GuildType)0;
			name = "N/A";
			server = "N/A";
		}

		public Guild(uint tid = 0, GuildType ttype = (GuildType)0, string tname = "")
		{
			/******************************************************************
			 * guild constructor
			 * inputs uint id, guildType, string name
			 * 
			 * This constructor is for pulling the guild information from the file
			 * server name is parsed from the guild name that is read in.
			 * 
			 ******************************************************************/
			gid = tid;
			type = ttype;
			string[] subs = tname.Split('-');
			name = subs[0];
			server = subs[1];

		}

		public Guild(uint tid = 0, GuildType ttype = (GuildType)0, string tname = "", string tserver = "")
		{
			/*****************************************************************
			 * guild constructor the second
			 * input: uint id, guildType, string name, string server
			 * 
			 * This constructor is for the user inputed guild, where we don't 
			 * need to parse the servername from the guild name, it's done for us
			 *****************************************************************/
			//checks to see if we were given a server name, since loading from file is deliniated by a dash, we will handle it in this class
			gid = tid;
			type = ttype;
			if (String.IsNullOrEmpty(tserver))
			{
				string[] subs = tname.Split('-');
				name = subs[0];
				server = subs[1];
			}
			else
			{
				name = tname;
				server = tserver;

			}

		}
		//properties
		public uint GID
		{
			get { return gid; }
			set { gid = value; }
		}
		public GuildType Type
		{
			get { return type; }
			set { type = value; }
		}
		public string Name
		{
			get { return name; }
			set { name = value; }
		}
		public string Server
		{
			get { return server; }
			set { server = value; }
		}


		int IComparable.CompareTo(object obj)
		{
			/************************************************
			 * IComparable implementation
			 * This allows us to compare two guild objects
			 * we will compare by their names.
			 ***********************************************/
			Guild other = (Guild)obj;
			return String.Compare(name, other.name);
		}

		public override string ToString()
		{
			/****************************************************
			 * ToString() override
			 * 
			 * This prints out the name, guild type, and server it resides in
			 ****************************************************/
			return name + "\t" + type + "\t" + server;
		}

		public string ToStringBasic()
		{
			/*************************************************************************************
             * Returns a string containing a guild's name with it's server in brackets.
             *************************************************************************************/
			return name.PadRight(30, ' ') + "\t[" + server + "]";
		}
	}
}
