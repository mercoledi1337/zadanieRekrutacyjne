using RealWorldApp.Core.Exceptions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace RealWorldApp.Core.Tags
{

    public class Tag
    {
        public int Count { get; set; }

        [Key]
        public string Name { get; set; }

        private Tag(string name, int count)
        {
            Count = count;
            Name = name;
        }

        public static Tag Create(string name, int count)
        {
            if (count < 0)
                throw new TagCountException();
            return new Tag(name,count);
        }
        private Tag()
        {

        }
    }
}
