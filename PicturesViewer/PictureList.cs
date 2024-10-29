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
        private int _currentImageIndex;
        public int N 
        { 
            get 
            {
                if (_buttons == null)
                    return 0;

                return _buttons.Count;
            } 
        }

        public PictureList()
        {
            _buttons = new List<Button>();
            _fileNames = new List<String>();
        }

        public void SetPictureBox(PictureBox pictureBox) =>
            _pictureBox = pictureBox;

        public void AddPictures(string path)
        {
            string[] patterns = { "*.png", "*.jpg", "*.jpeg", "*.bmp", "*.gif" };
            List<string> files = new List<string>();

            foreach (string pattern in patterns)
                files.AddRange(Directory.GetFiles(path, pattern, SearchOption.TopDirectoryOnly));

            if (files == null || files.Count == 0)
                return;

            foreach (string x in files)
                AddPicture(x);
        }

        public void AddPicture(string filePath)
        {
            if (File.Exists(filePath))
            {
                Button button = new Button();
                button.Size = new Size(90, 90);
                button.BackgroundImage = new Bitmap(filePath);
                button.BackgroundImageLayout = ImageLayout.Stretch;

                _fileNames.Add(filePath);
                button.Tag = filePath;
                button.Click += OnButtonClick;

                _buttons.Add(button);
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
                tableLayoutPanel.Controls.Add(_buttons[i], 0, i);   
        }

        public void ShowCurrentImage()
        {
            if (_currentImageIndex >= 0 && _currentImageIndex < _fileNames.Count)
            {
                string filePath = _fileNames[_currentImageIndex];

                UpdateImage(filePath);
            }
        }

        private void UpdateImage(string filePath)
        {
            _pictureBox.BackgroundImage = new Bitmap(filePath);
            _pictureBox.BackgroundImageLayout = ImageLayout.Stretch;
        }

        public void SlideUp()
        {
            if (_currentImageIndex == 0)
                return;

            _currentImageIndex--;
            ShowCurrentImage();
        }

        public void SlideDown()
        {
            if( _currentImageIndex == _fileNames.Count - 1)
                return;

            _currentImageIndex++;
            ShowCurrentImage();
        }

        private void OnButtonClick(object sender, EventArgs e)
        {
            //получаем путь к файлу и отображаем в picture box 

            Button button = (Button)sender;
            string filePath = (string)button.Tag;

            UpdateImage(filePath);

            _currentImageIndex = _fileNames.IndexOf(filePath);
        }
    }
}
