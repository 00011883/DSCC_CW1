using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GamesStore_11883_Client.Models
{
    public class Game
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ShortDescription { get; set; }
        public decimal Price { get; set; }
        public string PosterUrl { get; set; }
        public string ImgUrl { get; set; }
        public string Logo { get; set; }
        public Author Author { get; set; }
    }
}