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
        Role? role_rbtn_selection = null; //currently selected role from the three radio buttons; null if none selected

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
                    RacePercentage_server_cbx.Items.Add(g.Server);
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

        private void Role_Selected(object sender, EventArgs e)
        {
            /*****************************************************************
             * CheckedChanged handler for Form1's radio buttons.
             * 
             * Changes the value of role_rbtn_selection based on which role
             * was selected.
             *****************************************************************/
            role_rbtn_selection = (Role)((RadioButton)sender).TabIndex;
        }

        private void RacePercentage_submit_btn_Click(object sender, EventArgs e)
        {
            /****************************************************************
             * onClick handler for RacePercentage_submit_btn.
             * 
             * Checks for a server selection from RacePercentage_Server_cbx, 
             * and displays data in query_text based on the output from 
             * GameFile.racePercent() based on that selection.
             ****************************************************************/

            if (RacePercentage_server_cbx.SelectedIndex == -1)
            {//if there is no server selected, display an appropriate error popup and return
                System.Windows.Forms.MessageBox.Show("Please select a server.");
                return;
            }

            foreach (string outLine in Globals.game.racePercent(RacePercentage_server_cbx.SelectedItem.ToString()))
                query_txt.AppendText(outLine + Environment.NewLine);
        }
    }
}
