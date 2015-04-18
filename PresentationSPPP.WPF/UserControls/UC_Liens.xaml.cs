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
using System.Windows.Navigation;
using System.Windows.Shapes;
using PresentationSPPP.LIB;
using VersionS3P = PresentationSPPP.LIB.Version;

namespace PresentationSPPP.WPF
{
    /// <summary>
    /// Logique d'interaction pour UC_Liens.xaml
    /// </summary>
    public partial class UC_Liens : UserControl
    {
        private UC_Liens_View View;
        public UC_Liens()
        {
            DataContext = View = new UC_Liens_View();
            InitializeComponent();
        }

        private void Button_AddLien_Click(object sender, RoutedEventArgs e)
        {
            if (UC_Liens_View.Button_AddLien_Click(ListBox_Logigrams.SelectedItem as Logigram))
            {
                ListBox_Liens.Items.Refresh();
            }
        }

        private void Button_DeleteLien_Click(object sender, RoutedEventArgs e)
        {
            if(UC_Liens_View.Button_DeleteLien_Click(ListBox_Liens.SelectedItem as Lien))
            {
                ListBox_Liens.Items.Refresh();
            }
        }

        private void TextBoxX_LostFocus(object sender, RoutedEventArgs e)
        {
            UC_Liens_View.TextBox_LostFocus(sender, e, 800);
        }
        private void TextBoxY_LostFocus(object sender, RoutedEventArgs e)
        {
            UC_Liens_View.TextBox_LostFocus(sender, e, 600);
        }
    }

    public class UC_Liens_View : View
    {
        public UC_Liens_View()
        {

        }

        public static Boolean Button_AddLien_Click(Logigram LogigramSelected)
        {
            if (LogigramSelected != null)
            {
                Lien NouveauLien = new Lien();
                NouveauLien.Logigram = LogigramSelected;
                Context.Liens.Add(NouveauLien);
                View.Liens.Add(NouveauLien);
                return true;
            }
            else
            {
                MessageBox.Show(@"Erreur: Veuillez selectionner un logigramme !");
                return false;
            }
        }

        public static Boolean Button_DeleteLien_Click(Lien LienSelected)
        {
            if (LienSelected != null)
            {
                Context.Liens.Remove(LienSelected);
                View.Liens.Remove(LienSelected);
                return true;
            }
            else
            {
                MessageBox.Show(@"Erreur: Veuillez selectionner un lien à supprimer !");
                return false;
            }
        }

        public static void TextBox_LostFocus(object sender, RoutedEventArgs e, int Max)
        {
            TextBox textBox = sender as TextBox;
            String text = textBox.Text;
            int value = 0;
            if (Int32.TryParse(text, out value))
            {
                if (value < 0 || value > Max)
                {
                    textBox.Text = "0";
                    MessageBox.Show("Veuillez saisir un entier compris entre 0 et "+Max);
                }
            }
            else
            {
                MessageBox.Show("Veuillez saisir un entier");
            }
        }
    }
}
