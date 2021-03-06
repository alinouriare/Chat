﻿using System;

namespace Chat.Models
{
    public class Messages
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Text { get; set; }

        public DateTime Timestap { get; set; }

        public int ChatId { get; set; }

        public Chat Chat { get; set; }

    }
}
