using System;
using System.Windows.Forms;


namespace Match3
{
    public partial class Menu : Form
    {
        System.Media.SoundPlayer MenuSoundtrack; // музыка, которая будет играть в меню
        public Menu()   // сама менюшка
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None; // убираем границы, чтоб не мешали
            this.WindowState = FormWindowState.Maximized; // полный экран делаем
            this.TopMost = true;
            MenuSoundtrack = new System.Media.SoundPlayer(SummerPracticMatch3.Properties.Resources.menu);
            MenuSoundtrack.Load();
            MenuSoundtrack.PlayLooping(); // подрубаем музыку
        }


        private void ExitButton_Click(object sender, EventArgs e) // кнопка выхода; из-за того, что я сделал полный экран, без неё выходить будет сложновато
        {
            MenuSoundtrack.Stop();
            ActiveForm.Hide();
            Close(); // закрыть меню
            Environment.Exit(0); // выйти из приложения
        }

        private void PlayButton_Click(object sender, EventArgs e) // кнопка начала новой игры
        {
            MenuSoundtrack.Stop();
            ActiveForm.Hide();
            Game MyGame = new Game(); // начать новую игру
            MyGame.ShowDialog();
            Close(); // закрыть меню
        }


    }
}