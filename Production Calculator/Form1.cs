using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace Production_Calculator
{
    public partial class Form1 : System.Windows.Forms.Form
    {
        private int[] population = new int[(int)Population.Length];
        private float[] needs = new float[(int)Needs.Length];
        private Timer timer = new Timer();
        public Form1()
        {
            InitializeComponent();
            UpdateProcesses();
            timer.Tick += buttonCalculate_Click;
        }
        private void UpdateProcesses()
        {
            selectProcess.Items.Clear();
            foreach (Process process in Process.GetProcesses())
            {
                if (process.MainWindowHandle != IntPtr.Zero && !selectProcess.Items.Contains(process.ProcessName))
                {
                    selectProcess.Items.Add(process.ProcessName);
                }
            }
        }

        private void buttonCalculate_Click(object sender, EventArgs e)
        {
            Process[] processes = Process.GetProcessesByName((string)selectProcess.SelectedItem);
            if (processes.Length == 0)
            {
                MessageBox.Show("Invalid proccess name");
                return;
            }

            long address, offset;
            try
            {
                address = Convert.ToInt64(textAddress.Text, 16);
                offset = Convert.ToInt64(textOffset.Text, 16);
            }
            catch (Exception ex)
            {
                address = -1;
                offset = -1;
            }
            if (address < 0 || offset < 0)
            {
                MessageBox.Show($"Invalid address or offset");
                return;
            }
            population = Reader.ReadPopulation(processes[0], address, offset, checkHistory.Checked);
            float[] _needs = Calculator.CalculateNeeds(population);

            labelBeggars.Text = population[(int)Population.Beggars].ToString();
            labelPeasants.Text = population[(int)Population.Peasants].ToString();
            labelCitizens.Text = population[(int)Population.Citizens].ToString();
            labelPatricians.Text = population[(int)Population.Patricians].ToString();
            labelNoblemen.Text = population[(int)Population.Noblemen].ToString();
            labelNomads.Text = population[(int)Population.Nomads].ToString();
            labelEnvoys.Text = population[(int)Population.Envoys].ToString();

            UpdateNeeds(labelFish, Needs.Fish, _needs);
            UpdateNeeds(labelCider, Needs.Cider, _needs);
            UpdateNeeds(labelLinenGarments, Needs.LinenGarments, _needs);
            UpdateNeeds(labelSpices, Needs.Spices, _needs);
            UpdateNeeds(labelBread, Needs.Bread, _needs);
            UpdateNeeds(labelBeer, Needs.Beer, _needs);
            UpdateNeeds(labelLeatherJerkins, Needs.LeatherJerkins, _needs);
            UpdateNeeds(labelBooks, Needs.Books, _needs);
            UpdateNeeds(labelCandlesticks, Needs.Candlesticks, _needs);
            UpdateNeeds(labelMeat, Needs.Meat, _needs);
            UpdateNeeds(labelWine, Needs.Wine, _needs);
            UpdateNeeds(labelGlasses, Needs.Glasses, _needs);
            UpdateNeeds(labelFurCoats, Needs.FurCoats, _needs);
            UpdateNeeds(labelBrocadeRobes, Needs.BrocadeRobes, _needs);
            UpdateNeeds(labelDates, Needs.Dates, _needs);
            UpdateNeeds(labelMilk, Needs.Milk, _needs);
            UpdateNeeds(labelCarpets, Needs.Carpets, _needs);
            UpdateNeeds(labelCoffee, Needs.Coffee, _needs);
            UpdateNeeds(labelPearlNecklaces, Needs.PearlNecklaces, _needs);
            UpdateNeeds(labelParfum, Needs.Parfum, _needs);
            UpdateNeeds(labelMarzipan, Needs.Marzipan, _needs);
        }

        private void UpdateNeeds(Label label, Needs index, float[] buffer)
        {
            int idx = (int)index;
            float value = buffer[idx];

            label.ForeColor = (int)value == (int)needs[idx] ? Color.Black : Color.Red;
            label.Text = value.ToString("F2");

            needs[idx] = value;
        }

        private void UpdateTimer()
        {
            timer.Stop();
            timer.Interval = (int)rangeInterval.Value * 1000;
            timer.Start();
        }

        private void ProductionChainDialog(string title, Dictionary<Image, float> items)
        {
            const int offset = 6;
            const int spacing = 3;
            const int groupTitleHeight = 13;
            const int iconSize = 46;
            const int labelHeight = 23;

            Form form = new Form();
            form.Width = 0;
            form.Height = 0;
            form.AutoSize = true;
            form.Icon = Icon;
            form.MaximizeBox = false;
            form.BackColor = Color.White;

            GroupBox group = new GroupBox();
            group.Location = new Point(offset, offset);
            group.Width = 0;
            group.Height = 0;
            group.AutoSize = true;
            group.Text = title;

            int i = 0;
            foreach (KeyValuePair<Image, float> item in items)
            {
                PictureBox icon = new PictureBox();
                icon.Left = offset + (iconSize + offset) * i;
                icon.Top = groupTitleHeight + offset;
                icon.Size = new Size(iconSize, iconSize);
                icon.Image = item.Key;

                Label label = new Label();
                label.Left = icon.Left;
                label.Top = icon.Top + icon.Height + spacing;
                label.Width = icon.Width;
                label.Height = labelHeight;
                label.Text = item.Value.ToString("F2");
                label.TextAlign = ContentAlignment.MiddleCenter;

                group.Controls.Add(icon);
                group.Controls.Add(label);

                i++;
            }

            form.Controls.Add(group);
            form.ShowDialog();
        }

        private void selectProcess_Enter(object sender, EventArgs e)
        {
            UpdateProcesses();
        }

        private void checkTop_CheckedChanged(object sender, EventArgs e)
        {
            TopMost = checkTop.Checked;
        }

        private void checkOverwrite_CheckedChanged(object sender, EventArgs e)
        {
            textAddress.Enabled = checkOverwrite.Checked;
            textOffset.Enabled = checkOverwrite.Checked;
            checkHistory.Enabled = checkOverwrite.Checked;
        }

        private void checkTimer_CheckedChanged(object sender, EventArgs e)
        {
            rangeInterval.Enabled = checkTimer.Checked;
            if (checkTimer.Checked)
            {
                UpdateTimer();
            }
            else
            {
                timer.Stop();
            }
        }

        private void selectVersion_SelectedIndexChanged(object sender, EventArgs e)
        {
            long address = Constants.BaseAddresses[selectVersion.SelectedIndex];
            textAddress.Text = address < 0 ? "null" : "0x" + address.ToString("X").ToLower();

            long offset = Constants.AddressOffsets[selectVersion.SelectedIndex];
            textOffset.Text = offset < 0 ? "null" : "0x" + offset.ToString("X").ToLower();

            checkHistory.Checked = selectVersion.SelectedIndex > 3;
        }

        private void rangeInterval_ValueChanged(object sender, EventArgs e)
        {
            UpdateTimer();
        }

        private void iconFish_Click(object sender, EventArgs e)
        {
            Dictionary<Image, float> dict = new Dictionary<Image, float>();
            float need = needs == null ? 1 : needs[(int)Needs.Fish];
            dict.Add(Properties.Resources.Fish, need);
            ProductionChainDialog("Fish", dict);
        }

        private void iconCider_Click(object sender, EventArgs e)
        {
            Dictionary<Image, float> dict = new Dictionary<Image, float>();
            float need = needs == null ? 1 : needs[(int)Needs.Cider];
            dict.Add(Properties.Resources.Cider, need);
            ProductionChainDialog("Cider", dict);
        }

        private void iconLinenGarments_Click(object sender, EventArgs e)
        {
            Dictionary<Image, float> dict = new Dictionary<Image, float>();
            float need = needs == null ? 1 : needs[(int)Needs.LinenGarments];
            dict.Add(Properties.Resources.LinenGarments, need);
            dict.Add(Properties.Resources.Hemp, need * 2.0f);
            ProductionChainDialog("Linen Garments", dict);
        }

        private void iconSpices_Click(object sender, EventArgs e)
        {
            Dictionary<Image, float> dict = new Dictionary<Image, float>();
            float need = needs == null ? 1 : needs[(int)Needs.Spices];
            dict.Add(Properties.Resources.Spices, need);
            ProductionChainDialog("Spices", dict);
        }

        private void iconBread_Click(object sender, EventArgs e)
        {
            Dictionary<Image, float> dict = new Dictionary<Image, float>();
            float need = needs == null ? 1 : needs[(int)Needs.Bread];
            dict.Add(Properties.Resources.Bread, need);
            dict.Add(Properties.Resources.Flour, need);
            dict.Add(Properties.Resources.Wheat, need * 2.0f);
            ProductionChainDialog("Bread", dict);
        }

        private void iconBeer_Click(object sender, EventArgs e)
        {
            Dictionary<Image, float> dict = new Dictionary<Image, float>();
            float need = needs == null ? 1 : needs[(int)Needs.Beer];
            dict.Add(Properties.Resources.Beer, need);
            dict.Add(Properties.Resources.Wheat, need);
            dict.Add(Properties.Resources.Herbs, need);
            ProductionChainDialog("Beer", dict);
        }

        private void iconLeatherJerkins_Click(object sender, EventArgs e)
        {
            Dictionary<Image, float> dict = new Dictionary<Image, float>();
            float need = needs == null ? 1 : needs[(int)Needs.LeatherJerkins];
            dict.Add(Properties.Resources.LeatherJerkins, need);
            dict.Add(Properties.Resources.AnimalHides, need * 2.0f);
            dict.Add(Properties.Resources.Brine, need * 0.5f);
            dict.Add(Properties.Resources.Salt, need * 0.5f);
            dict.Add(Properties.Resources.Coal, need * 0.5f);
            ProductionChainDialog("Leather Jerkins", dict);
        }

        private void iconBooks_Click(object sender, EventArgs e)
        {
            Dictionary<Image, float> dict = new Dictionary<Image, float>();
            float need = needs == null ? 1 : needs[(int)Needs.Books];
            dict.Add(Properties.Resources.Books, need);
            dict.Add(Properties.Resources.Indigo, need * 2.0f);
            dict.Add(Properties.Resources.Paper, need * 0.5f);
            dict.Add(Properties.Resources.Wood, need);
            ProductionChainDialog("Books", dict);
        }

        private void iconCandlesticks_Click(object sender, EventArgs e)
        {
            Dictionary<Image, float> dict = new Dictionary<Image, float>();
            float need = needs == null ? 1 : needs[(int)Needs.Candlesticks];
            dict.Add(Properties.Resources.Candlesticks, need);
            dict.Add(Properties.Resources.Candles, need * 1.5f);
            dict.Add(Properties.Resources.Beeswax, need * 3.0f);
            dict.Add(Properties.Resources.Hemp, need * 1.5f);
            dict.Add(Properties.Resources.Brass, need * 0.75f);
            dict.Add(Properties.Resources.CopperOre, need * 0.75f);
            dict.Add(Properties.Resources.Coal, need * 0.5f);
            ProductionChainDialog("Candlesticks", dict);
        }

        private void iconMeat_Click(object sender, EventArgs e)
        {
            Dictionary<Image, float> dict = new Dictionary<Image, float>();
            float need = needs == null ? 1 : needs[(int)Needs.Meat];
            dict.Add(Properties.Resources.Meat, need);
            dict.Add(Properties.Resources.Cattle, need * 2.0f);
            dict.Add(Properties.Resources.Brine, need * 0.5f);
            dict.Add(Properties.Resources.Salt, need * 0.5f);
            dict.Add(Properties.Resources.Coal, need * 0.5f);
            ProductionChainDialog("Meat", dict);
        }

        private void iconWine_Click(object sender, EventArgs e)
        {
            Dictionary<Image, float> dict = new Dictionary<Image, float>();
            float need = needs == null ? 1 : needs[(int)Needs.Wine];
            dict.Add(Properties.Resources.Wine, need);
            dict.Add(Properties.Resources.Grapes, need * 3.0f);
            dict.Add(Properties.Resources.Barrels, need);
            dict.Add(Properties.Resources.Wood, need * 2.0f / 3.0f);
            dict.Add(Properties.Resources.Iron, need * 0.5f);
            dict.Add(Properties.Resources.IronOre, need * 0.5f);
            dict.Add(Properties.Resources.Coal, need * 0.5f);
            ProductionChainDialog("Wine", dict);
        }

        private void iconGlasses_Click(object sender, EventArgs e)
        {
            Dictionary<Image, float> dict = new Dictionary<Image, float>();
            float need = needs == null ? 1 : needs[(int)Needs.Glasses];
            dict.Add(Properties.Resources.Glasses, need);
            dict.Add(Properties.Resources.Quartz, need * 0.75f);
            dict.Add(Properties.Resources.Brass, need * 0.75f);
            dict.Add(Properties.Resources.CopperOre, need * 0.75f);
            dict.Add(Properties.Resources.Coal, need * 0.5f);
            ProductionChainDialog("Glasses", dict);
        }

        private void iconFurCoats_Click(object sender, EventArgs e)
        {
            Dictionary<Image, float> dict = new Dictionary<Image, float>();
            float need = needs == null ? 1 : needs[(int)Needs.FurCoats];
            dict.Add(Properties.Resources.FurCoats, need);
            dict.Add(Properties.Resources.Furs, need);
            dict.Add(Properties.Resources.Brine, need * 0.33125f);
            dict.Add(Properties.Resources.Salt, need * 0.33125f);
            dict.Add(Properties.Resources.Coal, need * 0.33125f);
            ProductionChainDialog("Fur Coats", dict);
        }

        private void iconBrocadeRobes_Click(object sender, EventArgs e)
        {
            Dictionary<Image, float> dict = new Dictionary<Image, float>();
            float need = needs == null ? 1 : needs[(int)Needs.BrocadeRobes];
            dict.Add(Properties.Resources.BrocadeRobes, need);
            dict.Add(Properties.Resources.Silk, need * 2.0f);
            dict.Add(Properties.Resources.Gold, need);
            dict.Add(Properties.Resources.GoldOre, need);
            dict.Add(Properties.Resources.Coal, need * 0.75f);
            ProductionChainDialog("Brocade Robes", dict);
        }

        private void iconDates_Click(object sender, EventArgs e)
        {
            Dictionary<Image, float> dict = new Dictionary<Image, float>();
            float need = needs == null ? 1 : needs[(int)Needs.Dates];
            dict.Add(Properties.Resources.Dates, need);
            ProductionChainDialog("Dates", dict);
        }

        private void iconMilk_Click(object sender, EventArgs e)
        {
            Dictionary<Image, float> dict = new Dictionary<Image, float>();
            float need = needs == null ? 1 : needs[(int)Needs.Milk];
            dict.Add(Properties.Resources.Milk, need);
            ProductionChainDialog("Milk", dict);
        }

        private void iconCarpets_Click(object sender, EventArgs e)
        {
            Dictionary<Image, float> dict = new Dictionary<Image, float>();
            float need = needs == null ? 1 : needs[(int)Needs.Carpets];
            dict.Add(Properties.Resources.Carpets, need);
            dict.Add(Properties.Resources.Silk, need);
            dict.Add(Properties.Resources.Indigo, need);
            ProductionChainDialog("Carpets", dict);
        }

        private void iconCoffee_Click(object sender, EventArgs e)
        {
            Dictionary<Image, float> dict = new Dictionary<Image, float>();
            float need = needs == null ? 1 : needs[(int)Needs.Coffee];
            dict.Add(Properties.Resources.Coffee, need);
            dict.Add(Properties.Resources.CoffeeBeans, need * 2.0f);
            ProductionChainDialog("Coffee", dict);
        }

        private void iconPearlNecklaces_Click(object sender, EventArgs e)
        {
            Dictionary<Image, float> dict = new Dictionary<Image, float>();
            float need = needs == null ? 1 : needs[(int)Needs.PearlNecklaces];
            dict.Add(Properties.Resources.PearlNecklaces, need);
            dict.Add(Properties.Resources.Pearls, need);
            ProductionChainDialog("Pearl Necklaces", dict);
        }

        private void iconParfum_Click(object sender, EventArgs e)
        {
            Dictionary<Image, float> dict = new Dictionary<Image, float>();
            float need = needs == null ? 1 : needs[(int)Needs.Parfum];
            dict.Add(Properties.Resources.Parfum, need);
            dict.Add(Properties.Resources.RoseOil, need * 3.0f);
            ProductionChainDialog("Parfum", dict);
        }

        private void iconMarzipan_Click(object sender, EventArgs e)
        {
            Dictionary<Image, float> dict = new Dictionary<Image, float>();
            float need = needs == null ? 1 : needs[(int)Needs.Marzipan];
            dict.Add(Properties.Resources.Marzipan, need);
            dict.Add(Properties.Resources.Almonds, need * 2.0f);
            dict.Add(Properties.Resources.Sugar, need);
            dict.Add(Properties.Resources.SugarCane, need * 2.0f);
            ProductionChainDialog("Marzipan", dict);
        }

        private void label31_Click(object sender, EventArgs e)
        {

        }
    }
}
