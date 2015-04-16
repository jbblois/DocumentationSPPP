using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using PresentationSPPP.LIB;

namespace PresentationSPPP.WPF
{
    /// <summary>
    /// Logique d'interaction pour Win_Home.xaml
    /// </summary>
    public partial class Win_Home : Window
    {
        private View_Home View;
        public Win_Home()
        {
            Base.Begin();
            DataContext = View = new View_Home();
            InitializeComponent();
            this.Title = "PresentationSPPP "+System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();

        }

        private void MenuItem_Categories_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            new Win_Categories().ShowDialog();
            this.Show();
        }

        //private void MenuItem_Extensions_Click(object sender, RoutedEventArgs e)
        //{
        //    this.Hide();
        //    new Win_Extensions().ShowDialog();
        //    this.Show();
        //}

        private void MenuItem_Sauvegarder_Click(object sender, RoutedEventArgs e)
        {
            View.MenuItem_Sauvegarder_Click();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            Base.End();
        }


        
    }

    public class View_Home
    {
        public static PresentationSPPPEntities Context { get { return Base.Context; } }
        public UC_Carrousels UC_Carrousels { get; set; }
        public UC_Documents UC_Documents { get; set; }
        public UC_Liens UC_Liens { get; set; }
        public UC_Versions UC_Versions { get; set; }

        public View_Home()
        {
            UC_Carrousels = new UC_Carrousels();
            UC_Documents = new UC_Documents();
            UC_Liens = new UC_Liens();
            UC_Versions = new UC_Versions();
        }


        public void MenuItem_Sauvegarder_Click()
        {
            try
            {
                List<Lien> LiensASupprimer = View.Liens.Where(L => L.Document == null 
                                                                || L.X1 >= L.X2 || L.Y1 >= L.Y2
                                                                || L.X1 < 0 || L.X1 > 800
                                                                || L.Y1 < 0 || L.Y1 > 600
                                                                || L.X2 < 0 || L.X2 > 800
                                                                || L.Y2 < 0 || L.Y2 > 600).ToList();
                Context.Liens.RemoveRange(LiensASupprimer);
                if (Base.Save())
                {
                    View.Synchroniser();
                    UC_Liens.ListBox_Liens.Items.Refresh();
                    MessageBox.Show("Modifications sauvegardées");
                }
                else
                {
                    MessageBox.Show("Erreur");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur survenue lors de la sauvegarde :\n" + ex.Message);
            }
        }
    }
}
