using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Windows.Forms;

namespace zdsimScanner
{
    public partial class Form2 : Form
    {
        // сюда переносим поля/методы по теме файла

        //============================================================================
        // Гарантировать длину (для индексации): только нарастить до нужной длины значениями "0"
        //============================================================================
        private static void EnsureSizeAtLeastFillZeros(System.Collections.Specialized.StringCollection sc, int size)
        {
            if (sc == null) return;
            while (sc.Count < size) sc.Add("0");
        }

        //============================================================================
        // Аналогично для null (звук)
        //============================================================================
        private static void EnsureSizeAtLeastFillNulls(System.Collections.Specialized.StringCollection sc, int size)
        {
            if (sc == null) return;
            while (sc.Count < size) sc.Add(null);
        }










    }
}