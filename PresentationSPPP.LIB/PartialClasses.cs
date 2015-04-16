using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresentationSPPP.LIB
{
    public partial class Categorie
    {
        public static Categorie Document { get { return Base.Context.Categories.First(C => C.Nom == "Document"); } }
        public static Categorie Logigram { get { return Base.Context.Categories.First(C => C.Nom == "Logigram"); } }

        public List<Logigram> Logigrammes 
        { 
            get 
            {
                return Base.Context.Documents.OfType<Logigram>().Where(L => L.FID_Categorie == ID).ToList();
            } 
        }

        public int NbsDocuments { get { return Documents.Count; } }
    }
    public partial class Document
    {
        public String CheminRepertoire()
        {
            return Base.FileBase_Path + '\\' + this.Categorie.Nom+ '\\' + this.Nom;
        }
    }
    public partial class Logigram
    {
        public Logigram Previous
        {
            get { return Base.Context.Documents.OfType<Logigram>().FirstOrDefault(L => L.FID_Next == ID); }
        }
    }
    public partial class Version
    {
        public String Nom 
        {
            get { return this.Numero + " " + Document.Nom + " " + Base.DateToString(Date);  }
        }
        public String NomFichier
        {
           get { return Document.Nom + "_" + Base.DateToString(Date) + '.' + Extension.Nom; }  
        }
        public String CheminFichier
        {
            get { return Document.CheminRepertoire() + '\\' + NomFichier; }
        }
        
    }
    public partial class Lien
    {
        public String Nom
        {
            get
            {
                if (Document != null)
                {
                    return Document.Nom;
                }
                else
                {
                    return "Nouveau lien";
                }
            }
        }
    }
}
