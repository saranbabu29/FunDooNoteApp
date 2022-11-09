using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLayer.Model
{
    public class AddNoteModel
    {
        public string Title { get; set; }
        public string Description { get; set; }

        public DateTime Reminder { get; set; }
        public string Colour { get; set; }
        public string Image { get; set; }
        public bool Archieve { get; set; }
        public bool Pin { get; set; }
        public bool Trash { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }

    }
}
