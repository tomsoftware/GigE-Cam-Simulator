using System.Xml;


namespace GigE_Cam_Simulator
{
    public class PropertyItem
    {
        public string RegisterName { get; }
        public RegisterTypes Register => RegisterTypeHelper.RegisterTypeByName(this.RegisterName);
        public int RegisterAddress => RegisterTypeHelper.RegisterByType(this.Register).Address;

        public string StringValue { get; set; }
        public bool IsString { get; set; }

        public int IntValue { get; set; }
        public bool IsInt { get; set; }

        public int[] Bits { get; set; }
        public bool IsBits { get; set; }


        public PropertyItem(string registerName)
        {
            this.RegisterName = registerName;
        }

    }

    class RegisterConfig
    {
        public List<PropertyItem> Properties { get; }

        public RegisterConfig(string fileName) 
        {
            this.Properties = ReadConfigFile(fileName);
        }

        private static List<PropertyItem> ReadConfigFile(string filePath)
        {
            var properties = new List<PropertyItem>();

            var xmlDoc = new XmlDocument();
            xmlDoc.Load(filePath);

            var propertyNodes = xmlDoc.SelectNodes("//property");
            foreach (XmlNode propertyNode in propertyNodes)
            {
                XmlNode registerNode = propertyNode.SelectSingleNode("register");
                if (registerNode == null)
                {
                    continue;
                }

                var property = new PropertyItem(registerNode.InnerText);

                // read string values
                var stringNode = propertyNode.SelectSingleNode("string");
                if (stringNode != null)
                {
                    property.StringValue = stringNode.InnerText;
                    property.IsString = true;
                }

                // read bit values
                var bitNodes = propertyNode.SelectNodes("bit");
                if (bitNodes != null && bitNodes.Count > 0)
                {
                    int[] bits = new int[bitNodes.Count];
                    for (int i = 0; i < bitNodes.Count; i++)
                    {
                        bits[i] = int.Parse(bitNodes[i].InnerText);
                    }

                    property.Bits = bits;
                    property.IsBits = true;
                }

                // read bit values
                var intNodes = propertyNode.SelectSingleNode("int");
                if (intNodes != null )
                {
                    property.IntValue = int.Parse(intNodes.InnerText);
                    property.IsInt = true;
                }

                properties.Add(property);
            }

            return properties;
        }

    }
}
