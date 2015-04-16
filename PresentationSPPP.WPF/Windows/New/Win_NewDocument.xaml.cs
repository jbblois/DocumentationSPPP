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
using System.DirectoryServices.AccountManagement;
using PresentationSPPP.LIB;
using VersionS3P = PresentationSPPP.LIB.Version;


namespace PresentationSPPP.WPF
{
    /// <summary>
    /// Logique d'interaction pour Win_NewDocument.xaml
    /// </summary>
    public partial class Win_NewDocument : Window
    {
        private Win_NewDocument_View View;
        public Win_NewDocument()
        {
            DataContext = View = new Win_NewDocument_View();
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
    public class Win_NewDocument_View : View
    {
        private String _NomFichier;
        public String NomFichier
        {
            get { return _NomFichier; }
            set 
            {
                _NomFichier = value;
                NotifyPropertyChanged();
            }
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

        public Categorie CategorieSelectionnee { get; set; }
        public Boolean IsLogigram { get; set; }

        public Boolean Button_File_Click()
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();

            if (dlg.ShowDialog() == true)
            {
                NomFichier = dlg.SafeFileName.Split('.')[0];
                NomExtension = dlg.SafeFileName.Split('.')[1].ToLower();
                CheminFichier = dlg.FileName;
            }
            return true;
        }
        public Boolean Button_Add_Click()
        {
            if (NomFichier != "")
            {
                if (NomExtension != "")
                {
                    if (CheminFichier != "")
                    {
                        if (CategorieSelectionnee != null)
                        {
                            if (IsLogigram)
                            {
                               return AddLogigram();
                            }
                            else
                            {
                               return AddDocument();
                            }
                        }
                    }
                }
            }
            return false;
        }

        public Boolean AddDocument()
        {
            if (Context.Documents.FirstOrDefault(Document => Document.Nom == NomFichier) == null)
            {
                Document NouveauDocument = new Document();
                NouveauDocument.Categorie = CategorieSelectionnee;
                NouveauDocument.Nom = NomFichier;
                NouveauDocument.Auteur = UserPrincipal.Current.DisplayName;
                if (FirstVersion(NouveauDocument))
                {
                    Context.Documents.Add(NouveauDocument);
                    View.Documents.Add(NouveauDocument); 
                    Base.Save();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                MessageBox.Show("Un document existe déjà avec ce nom");
                return false;
            }
            
        }
        public Boolean AddLogigram()
        {
            if (Context.Documents.FirstOrDefault(Document => Document.Nom == NomFichier) == null)
            {
                if (NomExtension == "png")
                {
                    Logigram NouveauLogigram = new Logigram();
                    NouveauLogigram.Categorie = CategorieSelectionnee;
                    NouveauLogigram.Nom = NomFichier;
                    NouveauLogigram.Auteur = UserPrincipal.Current.DisplayName;
                    if (FirstVersion(NouveauLogigram as Document))
                    {
                        Context.Documents.Add(NouveauLogigram);
                        View.Documents.Add(NouveauLogigram);
                        Base.Save();
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    MessageBox.Show("Le logigramme doit être un png");
                    return false;
                }
                

            }
            else
            {
                MessageBox.Show("Un logigramme existe déjà avec ce nom");
                return false;
            }
            
        }
        private Boolean FirstVersion(Document Document)
        {
            VersionS3P NouvelleVersion = new VersionS3P();
            
            NouvelleVersion.Numero = 1;
            NouvelleVersion.Document = Document;
            NouvelleVersion.Date = DateTime.Now;
            
            Extension extension = Context.Extensions.FirstOrDefault(E => E.Nom == NomExtension);
            if (extension == null)
            {
                extension = new Extension();
                extension.Nom = NomExtension;
            }
            
            NouvelleVersion.Extension = extension;

            Document.Versions.Add(NouvelleVersion);

            try
            {
                System.IO.Directory.CreateDirectory(Document.CheminRepertoire());
                byte[] tabBytes = System.IO.File.ReadAllBytes(CheminFichier);
                System.IO.File.WriteAllBytes(NouvelleVersion.CheminFichier, tabBytes);
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }
        
    }
}
