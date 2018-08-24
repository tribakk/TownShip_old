using System;

namespace TownShip_Form.Kernal
{
    public class ProductTagString
    {
        public ProductTag m_Tag;
        public String m_Name;
        public FactoryType m_FactoryType;
        public ProductTagString(ProductTag tag, String name, FactoryType type /*= FactoryType.ftAnimal*/)
        {
            m_Tag = tag;
            m_Name = name;
            m_FactoryType = type;
        }
    };
}