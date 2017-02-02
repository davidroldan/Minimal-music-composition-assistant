using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicaMinimalista.Objects.Utils
{
    public class StringConstants
    {
        public static string TEMP_ABC = System.IO.Directory.GetCurrentDirectory() + @"\temp\temp.abc";
        public static string TEMP_MIDI = System.IO.Directory.GetCurrentDirectory() + @"\temp\temp.midi";
        public static string TEMP_SVG_WRITE = System.IO.Directory.GetCurrentDirectory() + @"\temp\temp.svg";
        public static string TEMP_SVG_READ = System.IO.Directory.GetCurrentDirectory() + @"\temp\temp001.svg";
        public static string TEMP_EPS_WRITE = System.IO.Directory.GetCurrentDirectory() + @"\temp\temp.eps";
        public static string TEMP_EPS_READ = System.IO.Directory.GetCurrentDirectory() + @"\temp\temp001.eps";
        public static string ABC2MIDI = System.IO.Directory.GetCurrentDirectory() + @"\bin\abc2midi.exe";
        public static string ABCM2PS = System.IO.Directory.GetCurrentDirectory() + @"\bin\abcm2ps.exe";
        public static string EPSTOPDF = System.IO.Directory.GetCurrentDirectory() + @"\bin\epstopdf.exe";

        public static string MIDI_DIR = System.IO.Directory.GetCurrentDirectory() + @"\midi";
        public static string ABC_DIR = System.IO.Directory.GetCurrentDirectory() + @"\files";
        public static string TEMP_DIR = System.IO.Directory.GetCurrentDirectory() + @"\temp";
        public static string PDF_DIR = System.IO.Directory.GetCurrentDirectory() + @"\pdf";
        public static string PROJECT_DIR = System.IO.Directory.GetCurrentDirectory() + @"\projects";
    }
}
