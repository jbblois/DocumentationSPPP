//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré à partir d'un modèle.
//
//     Des modifications manuelles apportées à ce fichier peuvent conduire à un comportement inattendu de votre application.
//     Les modifications manuelles apportées à ce fichier sont remplacées si le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PresentationSPPP.LIB
{
    using System;
    using System.Collections.Generic;
    
    public partial class Document
    {
        public Document()
        {
            this.Versions = new HashSet<Version>();
            this.References = new HashSet<Lien>();
        }
    
        public long ID { get; set; }
        public long FID_Categorie { get; set; }
        public string Nom { get; set; }
        public string Auteur { get; set; }
    
        public virtual Categorie Categorie { get; set; }
        public virtual ICollection<Version> Versions { get; set; }
        public virtual ICollection<Lien> References { get; set; }
    }
}
