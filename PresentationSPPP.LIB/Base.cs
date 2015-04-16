using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresentationSPPP.LIB
{
    public static class Base
    {   
        public static PresentationSPPPEntities Context { get; private set; }
        public static String FileBase_Path { get { return @"D:\Documents\NetBeansProjects\PresentationSPPP\FileBase"; } }
        
        public static Boolean Begin()
        {
            Context = new PresentationSPPPEntities();
            return true;
        }
        public static Boolean Save()
        {
            try
            {
                Context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }
        public static Boolean Cancel()
        {
            Context.Dispose();
            Context = new PresentationSPPPEntities();
            return true;
        }
        public static Boolean End()
        {
            Context.Dispose();
            Context = null;
            return true;
        }

        public static String DateToString(DateTime Date)
        {
            return Date.Year + "-" + Date.Month + "-" + Date.Day;
        }


    }
}
