using Castle.Components.DictionaryAdapter.Xml;
using Library.Common;
using Library.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Library.Dtos
{
    public class OptionsDto 
    {
        public ListTypes ListType { get; set; }
        public string[] FilterValues { get; set; }
    }
}
