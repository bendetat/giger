﻿using System.Xml;

namespace Chvart.Giger
{
    public class Paper : Element<Paper>
    {
        public Paper() : base(0, 0, 0, 0)
        {
        }

        protected override XmlNode GetXmlNode(XmlDocument doc)
        {
            throw new System.NotImplementedException();
        }
    }
}