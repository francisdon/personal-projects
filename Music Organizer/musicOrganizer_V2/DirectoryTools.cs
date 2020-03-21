using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;

namespace musicOrganizer_V2
{
   public static class DirectoryTools
    {

        public static string Loc()
        {
            string directLoc = "";


            FolderBrowserDialog fdb = new FolderBrowserDialog();

            if (fdb.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                directLoc = fdb.SelectedPath;
            }

            return directLoc;
        }

             
        }


    }

