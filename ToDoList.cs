using System;
using System.IO;
using System.Windows.Forms;

namespace ToDoWinForms
{
    public partial class ToDoList : System.Windows.Forms.Form
    {
        public ToDoList()
        {
            InitializeComponent();
        }


        private void Button2_Click_1(object sender, EventArgs e)
        {
            // Проверяем, что в listBox есть выбранная задача
            if (listBox.SelectedIndex >= 0)
            {
                // Удаляем выбранную задачу
                listBox.Items.RemoveAt(listBox.SelectedIndex);
            }
            else
            {
                // Если задача не выбрана, показываем сообщение об ошибке
                MessageBox.Show("Выберите задачу для удаления!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Form_Load(object sender, EventArgs e)
        {
            LoadTasksFromFile();  // Загружаем задачи при запуске
        }

        private void Form_Closing(object sender, FormClosingEventArgs e)
        {
            SaveTasksToFile();  // Сохраняем задачи перед закрытием
        }

        private void SaveTasksToFile()
        {
            using (StreamWriter writer = new StreamWriter("tasks.txt"))
            {
                foreach (var item in listBox.Items)
                {
                    writer.WriteLine(item);
                }
            }
        }

        private void LoadTasksFromFile()
        {
            if (File.Exists("tasks.txt"))
            {
                using (StreamReader reader = new StreamReader("tasks.txt"))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        listBox.Items.Add(line);
                    }
                }
            }
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            // Проверяем, что в textBox есть текст
            if (!string.IsNullOrWhiteSpace(textBox.Text))
            {
                // Получаем текущий номер задачи, это просто количество элементов в listBox + 1
                int taskNumber = listBox.Items.Count + 1;

                // Создаем строку задачи с номером, описанием и датой
                string task = $"{taskNumber}. {textBox.Text} - Срок: {dateTimePicker1.Value.ToShortDateString()}";

                // Добавляем задачу в список
                listBox.Items.Add(task);

                // Очищаем текстовое поле после добавления
                textBox.Clear();
            }
            else
            {
                // Если поле пустое, показываем сообщение об ошибке
                MessageBox.Show("Введите описание задачи!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_exit_Click(object sender, EventArgs e)
        {
            // Закрывает форму
    this.Close();
        }

        // Для хранения позиции мыши
        private bool isMouseDown = false;
        private int mouseX = 0;
        private int mouseY = 0;

        // Метод для начала перетаскивания
        private void Form_MouseDown(object sender, MouseEventArgs e)
        {
            isMouseDown = true;
            mouseX = e.X;
            mouseY = e.Y;
        }

        // Метод для окончания перетаскивания
        private void Form_MouseUp(object sender, MouseEventArgs e)
        {
            isMouseDown = false;
        }

        // Метод для перетаскивания окна
        private void Form_MouseMove(object sender, MouseEventArgs e)
        {
            if (isMouseDown)
            {
                this.Left += e.X - mouseX;
                this.Top += e.Y - mouseY;
            }
        }
    }
}