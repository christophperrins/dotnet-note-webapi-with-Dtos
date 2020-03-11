using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace src.Model.Entity
{
    public class Note
    {
        public Note()
        {

        }

        public Note(int id, string text)
        {
            Id = id;
            Text = text;
        }

        public int Id { get; set; }
        public string Text { get; set; }
    }
}
