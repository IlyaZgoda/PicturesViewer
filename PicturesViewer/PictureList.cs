using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using System.IO;

namespace PicturesViewer
{
    public class PictureList
    {
        private List<Button> _buttons;
        private List<String> _fileNames;
        private PictureBox _pictureBox;
        public int N { get 
            {
                if (_buttons == null)
                    return 0;

                return _buttons.Count;
            } }

        public PictureList()
        {
            _buttons = new List<Button>();
            _fileNames = new List<String>();
        }

        public void SetPictureBox(PictureBox pictureBox) =>
            _pictureBox = pictureBox;

        public void AddPictures(string path)
        {
            string[] files = Directory.GetFiles(path);

            if(files == null ) 
                return;

            foreach (string x in files)
            {
                int i = 0;
                _buttons.Add(new Button());
                _buttons[i].Size = new Size(90, 90);
                _buttons[i].BackgroundImage = new Bitmap(x);
                _buttons[i].BackgroundImageLayout = ImageLayout.Stretch;

                _fileNames.Add(x);
                _buttons[i].Tag = $"{x}";
                _buttons[i].Click += OnButtonClick;
                MessageBox.Show(x);

                i++;
            }
        }
        // добавить метод который по заданному пути добавляет новый элемент
        // добавление одного файла, организовать листание 
        public void ShowImages(TableLayoutPanel tableLayoutPanel)
        {
            if (tableLayoutPanel == null) 
                return;

            tableLayoutPanel.Controls.Clear();

            if (N == 0)
                return;

            tableLayoutPanel.Size = new Size(100, 100 * N);
            tableLayoutPanel.RowCount = N;
            tableLayoutPanel.ColumnCount = 1;

            for (int i = 0; i < N; i++)
            {
                tableLayoutPanel.Controls.Add(_buttons[i], 0, i);
                MessageBox.Show(i.ToString());
            }


        }

        private void OnButtonClick(object sender, EventArgs e)
        {
            //получаем путь к файлу и отображаем в picture box 

            Button button = (Button)sender;
            string filePath = (string)button.Tag;
            _pictureBox.BackgroundImage = new Bitmap(filePath);
            _pictureBox.BackgroundImageLayout = ImageLayout.Stretch;
        }
    }
}
