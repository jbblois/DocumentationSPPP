using System;
using System.Collections.Generic;
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
using VersionS3P = PresentationSPPP.LIB.Version;

namespace PresentationSPPP.WPF
{
    /// <summary>
    /// Logique d'interaction pour Win_NewVersion.xaml
    /// </summary>
    public partial class Win_NewVersion : Window
    {
        private Win_NewVersion_View View;
        public Win_NewVersion(Document DocumentSelected)
        {
            DataContext = View = new Win_NewVersion_View(DocumentSelected);
            InitializeComponent();
        }

        private void Button_Add_Click(object sender, RoutedEventArgs e)
        {
            if (View.Button_Add_Click())
            {
                this.Close();
            }
        }
        private void Button_File_Click(object sender, RoutedEventArgs e)
        {
            View.Button_File_Click();
        }
    }
    public class Win_NewVersion_View : View
    {
        private Document DocumentSelected;
        public String NumeroVersion
        {
            get { return ""+(DocumentSelected.Versions.Max(V => V.Numero) + 1); }
        }

        public String DateVersion
        {
            get { return DateTime.Now.ToShortDateString(); }
        }

        private String _CheminFichier;
        public String CheminFichier
        {
            get { return _CheminFichier; }
            set
            {
                _CheminFichier = value;
                NotifyPropertyChanged();
            }
        }

        public String NomExtension { get; set; }

        public Win_NewVersion_View(Document DocumentSelected)
        {
            this.DocumentSelected = DocumentSelected;
        }

        public Boolean Button_File_Click()
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();

            if (dlg.ShowDialog() == true)
            {
                NomExtension = dlg.SafeFileName.Split('.')[1].ToLower();
                CheminFichier = dlg.FileName;
            }
            return true;
        }
        public Boolean Button_Add_Click()
        {
            if (NomExtension != "")
            {
                if (CheminFichier != "")
                {
                    return Win_NewVersion_View.AddVersion(DocumentSelected, NomExtension, CheminFichier);
                }
            }
            return false;
        }

        private static Boolean AddVersion(Document Document, String NomExtension, String CheminFichier)
        {
            if (Document.Versions.FirstOrDefault(V => V.Date.Date.CompareTo(DateTime.Now.Date) == 0) == null)
            {
                if (Document.GetType().Name == "Logigramm" && NomExtension != "png")
                {
                    MessageBox.Show("Les logigrammes sont des fichier de type .png");
                    return false; 
                }

                VersionS3P NouvelleVersion = new VersionS3P();

                NouvelleVersion.Numero = Document.Versions.Max(V => V.Numero) + 1; ;
                NouvelleVersion.Document = Document;
                NouvelleVersion.Date = DateTime.Now;

                Extension extension = Context.Extensions.FirstOrDefault(E => E.Nom == NomExtension);
                if (extension == null)
                {
                    extension = new Extension();
                    extension.Nom = NomExtension;
                }

                NouvelleVersion.Extension = extension;

                Context.Versions.Add(NouvelleVersion);
                View.Versions.Add(NouvelleVersion);

                try
                {
                    System.IO.Directory.CreateDirectory(Document.CheminRepertoire());
                    byte[] tabBytes = System.IO.File.ReadAllBytes(CheminFichier);
                    System.IO.File.WriteAllBytes(NouvelleVersion.CheminFichier, tabBytes);
                    Base.Save();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erreur :" + ex.Message);
                    return false;
                }
                return true;
                
            }
            else
            {
                MessageBox.Show("Une version de ce document a déjà été ajouté aujourd'huis");
                return false;
            }
        }

    }
}
