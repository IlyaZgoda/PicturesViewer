using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PicturesViewer
{
    public partial class Form1 : Form
    {
        private PictureList _pictureList;
        public Form1()
        {
            InitializeComponent();

            _pictureList = new PictureList();
            _pictureList.SetPictureBox(pictureBox);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void LoadFromFolderButton_Click(object sender, EventArgs e)
        {
            folderBrowserDialog.ShowDialog();
            string path = folderBrowserDialog.SelectedPath;

            _pictureList.AddPictures(path);
            _pictureList.ShowImages(tableLayoutPanel);
        }
    }
}
