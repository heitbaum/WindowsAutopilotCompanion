﻿namespace CompanionApp.Model
{
    public class Group
    {
        public string DisplayName { get; set; }
        public string Id { get; set; }
        public override string ToString()
        {
            return DisplayName;
        }
    }
}
