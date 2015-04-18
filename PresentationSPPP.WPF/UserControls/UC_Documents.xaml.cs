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
    /// Logique d'interaction pour UC_Documents.xaml
    /// </summary>
    public partial class UC_Documents : UserControl
    {
        private UC_Documents_View View;
        public UC_Documents()
        {
            DataContext = View = new UC_Documents_View();
            InitializeComponent();
        }

        private void Button_AddDocument_Click(object sender, RoutedEventArgs e)
        {
            new Win_NewDocument().ShowDialog();
        }
        private void Button_DeleteDocument_Click(object sender, RoutedEventArgs e)
        {
            UC_Documents_View.Button_DeleteDocument_Click(ListBox_Documents.SelectedItem as Document);
        }
    }
    public class UC_Documents_View : View
    {
        public static Boolean Button_DeleteDocument_Click(Document Document)
        {
            if (Document != null)
            {
                MessageBoxResult MBR = MessageBox.Show(@"Etes-vous sur de vouloir supprimer le document :" + Document.Nom, "Supprimer un document", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (MBR == MessageBoxResult.Yes)
                {
                    try
                    {
                        Context.Liens.ToList().RemoveAll(L => L.FID_Document == Document.ID);
                        Context.Versions.ToList().RemoveAll(V => V.FID_Document == Document.ID);
                    
                        Context.Documents.Remove(Document);
                        View.Documents.Remove(Document);
                    
                        System.IO.Directory.Delete(Document.CheminRepertoire(), true);
                    
                        Context.SaveChanges();
                        return true;
                    }
                    catch (Exception ex)
                    {
                        UC_Documents_View.Cancel();
                        MessageBox.Show("Erreur: "+ex.Message);
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
                MessageBox.Show(@"Erreur: Veuillez selectionner un document à supprimer !");
                return false;
            }
        }
    }
}