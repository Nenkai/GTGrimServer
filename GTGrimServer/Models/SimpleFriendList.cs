﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.Xml.Serialization;

namespace GTGrimServer.Models
{
    [XmlRoot(ElementName = "friends")]
    public class SimpleFriendList
    {
        public List<SimpleFriend> Items { get; set; }
    }
}