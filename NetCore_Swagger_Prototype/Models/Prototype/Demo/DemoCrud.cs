﻿using System.ComponentModel;

namespace NetCore_Swagger_Prototype.Models.Prototype.Demo
{
    public class DemoCrud
    {
        public long Id { get; set; }
        public string Name { get; set; }

        [DefaultValue(false)]
        public bool IsComplete { get; set; }
        public string Secret { get; set; }
    }
}