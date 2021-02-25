using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Assignment3
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //populate ServerClassType_class_cbx
            foreach (Class c in Enum.GetValues(typeof(Class)))
                ServerClassType_class_cbx.Items.Add(Enum.GetName(typeof(Class), c));

            //populate the comboboxes needing server names
            foreach (Guild g in Globals.guilds.Values)
                if (!ServerClassType_server_cbx.Items.Contains(g.Server))
                {
                    ServerClassType_server_cbx.Items.Add(g.Server);
                    RolePercentage_server_cbx.Items.Add(g.Server);
                    LvlRange_server_cbx.Items.Add(g.Server);
                }

            //populate LvlRange_role_cbx
            foreach (Role r in Enum.GetValues(typeof(Role)))
                LvlRange_role_cbx.Items.Add(Enum.GetName(typeof(Role), r));

            //set limits for max and min NumericUpDown items
            LvlRange_max_nud.Maximum = Constants.MAX_LEVEL;
            LvlRange_min_nud.Maximum = Constants.MAX_LEVEL;

            //populate GuildType_type_cbx
            foreach (GuildType t in Enum.GetValues(typeof(GuildType)))
            {
                if (t == GuildType.MythicPls) GuildType_type_cbx.Items.Add("Mythic+");//if t is of type MythicPls, use 'Mythic+' instead
                else GuildType_type_cbx.Items.Add(Enum.GetName(typeof(GuildType), t));
            }
                
        }
    }
}
