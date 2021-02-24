using System;
using System.Collections.Generic;
using System.Text;
/************************************************************
 * Assignment 2
 * Programmers: Robert Tyler Trotter z1802019
 *              Mitchell Trafton     z1831076
 ***********************************************************/

namespace Assignment3
{
    static class Globals
    {
        //global variables are stored here
        public static Dictionary<uint, Item> items = new Dictionary<uint, Item>();
        public static Dictionary<uint, Guild> guilds = new Dictionary<uint, Guild>();
        public static Dictionary<uint, Player> characters = new Dictionary<uint, Player>();

        public static GameFile game = new GameFile();
    }
}
