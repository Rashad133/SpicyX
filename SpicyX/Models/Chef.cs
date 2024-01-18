﻿namespace SpicyX.Models
{
    public class Chef
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Image {  get; set; }

        public string? FaceLink { get; set; }
        public string? TwitLink { get; set; }
        public string? GoogleLink { get; set; }
        public string? LinkedLink { get; set; }

        public int PositionId { get; set; }
        public Position? Position { get; set; }
    }
}
