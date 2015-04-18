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
    /// Logique d'interaction pour UC_Versions.xaml
    /// </summary>
    public partial class UC_Versions : UserControl
    {
        private UC_Versions_View View;
        public UC_Versions()
        {
            DataContext = View = new UC_Versions_View();
            InitializeComponent();
        }

        private void Button_AddVersion_Click(object sender, RoutedEventArgs e)
        {
            UC_Versions_View.Button_AddVersion_Click(ListBox_Documents.SelectedItem as Document);
        }

        private void Button_DownloadFile_Click(object sender, RoutedEventArgs e)
        {
            UC_Versions_View.Button_DownloadFile_Click(ListBox_Versions.SelectedItem as VersionS3P);
        }

        private void Button_DeleteVersion_Click(object sender, RoutedEventArgs e)
        {
            UC_Versions_View.Button_DeleteVersion_Click(ListBox_Versions.SelectedItem as VersionS3P);
        }
    }
    public class UC_Versions_View : View
    {

        public static Boolean Button_AddVersion_Click(Document DocumentSelected)
        {
            if (DocumentSelected != null)
            {
                new Win_NewVersion(DocumentSelected).ShowDialog();
                return true;
            }
            else
            {
                MessageBox.Show(@"Erreur: Veuillez selectionner un document !");
                return false;
            }
        }
        public static Boolean Button_DownloadFile_Click(VersionS3P VersionSelected)
        {
            if (VersionSelected != null)
            {
                String folderSelected;
                System.Windows.Forms.FolderBrowserDialog fbd = new System.Windows.Forms.FolderBrowserDialog();
                if (fbd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    folderSelected = fbd.SelectedPath;
                    try
                    {
                        Byte[] file = System.IO.File.ReadAllBytes(VersionSelected.CheminFichier);
                        System.IO.File.WriteAllBytes(folderSelected + '\\' + VersionSelected.NomFichier, file);
                        MessageBox.Show("OK");
                        return true;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Erreur:\n"+ex.Message);
                        return false;
                    }
                }
                else
                {
                    return true;
                }
            }
            else
            {
                MessageBox.Show(@"Erreur: Veuillez selectionner une version à télécharger !");
                return false;
            }
        }
        public static Boolean Button_DeleteVersion_Click(VersionS3P VersionSelected)
        {
            if (VersionSelected != null)
            {
                MessageBoxResult MBR = MessageBox.Show(@"Etes-vous sur de vouloir supprimer la version : " + VersionSelected.NomFichier, "Supprimer une version", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (MBR == MessageBoxResult.Yes)
                {
                    try
                    {
                        System.IO.File.Delete(VersionSelected.CheminFichier);
                        
                        Context.Versions.Remove(VersionSelected);
                        View.Versions.Remove(VersionSelected);
                    
                        Context.SaveChanges();
                        return true;
                    }
                    catch (Exception ex)
                    {
                        UC_Documents_View.Cancel();
                        MessageBox.Show("Erreur:\n" + ex.Message);
                        return false;
                    }
                }
                else
                {
                    return true;
                }
            }
            else
            {
                MessageBox.Show(@"Erreur: Veuillez selectionner une version à supprimer !");
                return false;
            }
        }
    }
}
