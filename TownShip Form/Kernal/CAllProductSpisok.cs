using System;
using System.Collections.Generic;
using System.Linq;

namespace TownShip_Form.Kernal
{
    public class CAllProductSpisok
    {
        List<ProductTagString> m_Array = new List<ProductTagString>();
        public String GetName(ProductTag tag)
        {
            String name = "";
            int count = m_Array.Count;
            for (int i = 0; i < count; i++)
            {
                if (m_Array[i].m_Tag == tag)
                {
                    name = m_Array[i].m_Name;
                    break;
                }
            }
            return name;
        }
        public FactoryType GetFactoryType(ProductTag tag)
        {
            FactoryType type = FactoryType.ftField;
            int count = m_Array.Count;
            for (int i = 0; i < count; i++)
            {
                if (m_Array[i].m_Tag == tag)
                {
                    type = m_Array[i].m_FactoryType;
                    break;
                }
            }
            return type;
        }
        public ProductTag GetTag(String Name)
        {
            ProductTag tag = ProductTag.ptNotFound;
            if (Name == null || Name.Length == 0)
                return tag;
            int count = m_Array.Count();
            for (int i = 0; i < count; i++)
            {
                //if (i<10 && false) 
                if (m_Array[i].m_Name == Name)
                {
                    tag = m_Array[i].m_Tag;
                    break;
                }
            }
            if (Name != "" && tag == ProductTag.ptNotFound)
            {
                System.Diagnostics.Debug.Assert(false, Name);
                //OutputDebugString(("BIG ERROR: ") + Name + ("\n\r\r"));
            }

            return tag;
        }

        public int GetCount()
        {
            return m_Array.Count;
        }
        public ProductTagString GetProductTagString(int i)
        {
            return m_Array[i];
        }

        public CAllProductSpisok()
        {
            m_Array.Add(new ProductTagString(ProductTag.ptPhenica, "пшеница", FactoryType.ftField));
            m_Array.Add(new ProductTagString(ProductTag.ptKukuruza, "кукуруза", FactoryType.ftField));
            m_Array.Add(new ProductTagString(ProductTag.ptMorkov, "морковь", FactoryType.ftField));
            m_Array.Add(new ProductTagString(ProductTag.ptSaharniTrosnik, "сахарный тросник", FactoryType.ftField));
            m_Array.Add(new ProductTagString(ProductTag.ptHlopok, "хлопок", FactoryType.ftField));
            m_Array.Add(new ProductTagString(ProductTag.ptKlubnika, "клубника", FactoryType.ftField));
            m_Array.Add(new ProductTagString(ProductTag.ptTomat, "томат", FactoryType.ftField));
            m_Array.Add(new ProductTagString(ProductTag.ptSosna, "сосна", FactoryType.ftField));
            m_Array.Add(new ProductTagString(ProductTag.ptKartofel, "картофель", FactoryType.ftField));
            m_Array.Add(new ProductTagString(ProductTag.ptKakao, "какао", FactoryType.ftField));
            m_Array.Add(new ProductTagString(ProductTag.ptKauchuk, "каучук", FactoryType.ftField));
            m_Array.Add(new ProductTagString(ProductTag.ptShelk, "шелк", FactoryType.ftField));
            m_Array.Add(new ProductTagString(ProductTag.ptPerec, "перец", FactoryType.ftField));
            m_Array.Add(new ProductTagString(ProductTag.ptMoloko, "молоко", FactoryType.ftAnimal));
            m_Array.Add(new ProductTagString(ProductTag.ptYaco, "яйцо", FactoryType.ftAnimal));
            m_Array.Add(new ProductTagString(ProductTag.ptSherst, "шерсть", FactoryType.ftAnimal));
            m_Array.Add(new ProductTagString(ProductTag.ptHleb, "хлеб", FactoryType.ftBakery));
            m_Array.Add(new ProductTagString(ProductTag.ptPechenie, "печенье", FactoryType.ftBakery));
            m_Array.Add(new ProductTagString(ProductTag.ptBublic, "бублик", FactoryType.ftBakery));
            m_Array.Add(new ProductTagString(ProductTag.ptPizza, "пицца", FactoryType.ftBakery));
            m_Array.Add(new ProductTagString(ProductTag.ptKartofelHleb, "картофельный хлеб", FactoryType.ftBakery));
            m_Array.Add(new ProductTagString(ProductTag.ptBananaHleb, "банановый хлеб", FactoryType.ftBakery));//ftBakery
            m_Array.Add(new ProductTagString(ProductTag.ptClivki, "сливки", FactoryType.ftMilk));
            m_Array.Add(new ProductTagString(ProductTag.ptSyr, "сыр", FactoryType.ftMilk));
            m_Array.Add(new ProductTagString(ProductTag.ptMaslo, "масло", FactoryType.ftMilk));
            m_Array.Add(new ProductTagString(ProductTag.ptIogury, "йогурт", FactoryType.ftMilk));
            m_Array.Add(new ProductTagString(ProductTag.ptPersikIogurt, "персиковый йогурт", FactoryType.ftFuture)); //ftMilk
            m_Array.Add(new ProductTagString(ProductTag.ptHlopokTkan, "хлопковая ткань", FactoryType.ftWeaving));
            m_Array.Add(new ProductTagString(ProductTag.ptPryaja, "пряжа", FactoryType.ftWeaving));
            m_Array.Add(new ProductTagString(ProductTag.ptShelkNit, "шелковая ткань", FactoryType.ftWeaving));
            m_Array.Add(new ProductTagString(ProductTag.ptRubashka, "рубашка", FactoryType.ftSewing));
            m_Array.Add(new ProductTagString(ProductTag.ptSviter, "свитер", FactoryType.ftSewing));
            m_Array.Add(new ProductTagString(ProductTag.ptPalto, "пальто", FactoryType.ftSewing));
            m_Array.Add(new ProductTagString(ProductTag.ptShlapa, "шляпа", FactoryType.ftSewing));
            m_Array.Add(new ProductTagString(ProductTag.ptPlatie, "платье", FactoryType.ftSewing));
            m_Array.Add(new ProductTagString(ProductTag.ptKostum, "костюм", FactoryType.ftSewing));
            m_Array.Add(new ProductTagString(ProductTag.ptPopkorn, "попкорн", FactoryType.ftSnack));
            m_Array.Add(new ProductTagString(ProductTag.ptKukuruzaChips, "кукурузные чипсы", FactoryType.ftSnack));
            m_Array.Add(new ProductTagString(ProductTag.ptGranola, "гранола", FactoryType.ftSnack));
            m_Array.Add(new ProductTagString(ProductTag.ptChips, "чипсы", FactoryType.ftSnack));
            m_Array.Add(new ProductTagString(ProductTag.ptkanope, "канопе", FactoryType.ftSnack));//ftSnack
            m_Array.Add(new ProductTagString(ProductTag.ptMilkSheik, "милкшейк", FactoryType.ftFastFood));
            m_Array.Add(new ProductTagString(ProductTag.ptChisburger, "чизбургер", FactoryType.ftFastFood));
            m_Array.Add(new ProductTagString(ProductTag.ptSendvich, "сэндвич", FactoryType.ftFastFood));
            m_Array.Add(new ProductTagString(ProductTag.ptKartoshkaFree, "картошка фри", FactoryType.ftFastFood));
            m_Array.Add(new ProductTagString(ProductTag.ptPecheneiKartofel, "печеный картофель", FactoryType.ftFastFood));//ftFastFood
            m_Array.Add(new ProductTagString(ProductTag.ptRezina, "резина", FactoryType.ftRubber));
            m_Array.Add(new ProductTagString(ProductTag.ptPlastik, "пластик", FactoryType.ftRubber));
            m_Array.Add(new ProductTagString(ProductTag.ptKley, "клей", FactoryType.ftRubber));
            m_Array.Add(new ProductTagString(ProductTag.ptSahar, "сахар", FactoryType.ftSugar));
            m_Array.Add(new ProductTagString(ProductTag.ptSirop, "сироп", FactoryType.ftSugar));
            m_Array.Add(new ProductTagString(ProductTag.ptKaramel, "карамель", FactoryType.ftSugar));
            m_Array.Add(new ProductTagString(ProductTag.ptMedovayaKaramel, "медовая карамель", FactoryType.ftSugar));
            m_Array.Add(new ProductTagString(ProductTag.ptBumaga, "бумага", FactoryType.ftPaper));
            m_Array.Add(new ProductTagString(ProductTag.ptBumagaPolotence, "бумажные полотенца", FactoryType.ftPaper));
            m_Array.Add(new ProductTagString(ProductTag.ptOboi, "обои", FactoryType.ftPaper));
            m_Array.Add(new ProductTagString(ProductTag.ptKniga, "книга", FactoryType.ftFuture));//ftPaper
            m_Array.Add(new ProductTagString(ProductTag.ptMorogenoe, "мороженое", FactoryType.ftIceCream));
            m_Array.Add(new ProductTagString(ProductTag.ptFruktLed, "фруктовый лед", FactoryType.ftIceCream));
            m_Array.Add(new ProductTagString(ProductTag.ptZamarojeniIogurt, "замороженный йогурт", FactoryType.ftIceCream));
            m_Array.Add(new ProductTagString(ProductTag.ptEscimo, "эскимо", FactoryType.ftIceCream));
            m_Array.Add(new ProductTagString(ProductTag.ptAnanasSorbet, "ананасовый сорбет", FactoryType.ftFuture));//ftIceCream
            m_Array.Add(new ProductTagString(ProductTag.ptKeks, "кекс", FactoryType.ftConfectionery));
            m_Array.Add(new ProductTagString(ProductTag.ptShokoladPirog, "шоколадный пирог", FactoryType.ftConfectionery));
            m_Array.Add(new ProductTagString(ProductTag.ptPirojenoe, "пироженое", FactoryType.ftConfectionery));
            m_Array.Add(new ProductTagString(ProductTag.ptPoncik, "пончик", FactoryType.ftConfectionery));
            m_Array.Add(new ProductTagString(ProductTag.ptChizkeyk, "чизкейк", FactoryType.ftConfectionery));

            m_Array.Add(new ProductTagString(ProductTag.ptKormKorova, "корм для коров", FactoryType.ftKorm));
            m_Array.Add(new ProductTagString(ProductTag.prKormKurica, "корм для куриц", FactoryType.ftKorm));
            m_Array.Add(new ProductTagString(ProductTag.ptKormOvca, "корм для овец", FactoryType.ftKorm));
            m_Array.Add(new ProductTagString(ProductTag.prKormPchela, "корм для пчел", FactoryType.ftKorm));

            m_Array.Add(new ProductTagString(ProductTag.ptKlubnikaVarenie, "клубничное варенье", FactoryType.ftJam));
            m_Array.Add(new ProductTagString(ProductTag.ptPersikVarenie, "персиковый конфитюр", FactoryType.ftJam));
            m_Array.Add(new ProductTagString(ProductTag.ptArbuzVarenie, "арбузный джем", FactoryType.ftJam));
            m_Array.Add(new ProductTagString(ProductTag.ptSlivaVarenie, "сливовое повидло", FactoryType.ftJam));

            m_Array.Add(new ProductTagString(ProductTag.ptPersik, "персик", FactoryType.ftIsland));
            m_Array.Add(new ProductTagString(ProductTag.ptArbuz, "арбуз", FactoryType.ftIsland));
            m_Array.Add(new ProductTagString(ProductTag.ptSliva, "слива", FactoryType.ftIsland));

            m_Array.Add(new ProductTagString(ProductTag.ptSotiMed, "соты с медом", FactoryType.ftAnimal));

            m_Array.Add(new ProductTagString(ProductTag.ptPlasticButilka, "пластиковая бутылка", FactoryType.ftPlastic));
            m_Array.Add(new ProductTagString(ProductTag.ptToy, "игрушка", FactoryType.ftPlastic));

            m_Array.Add(new ProductTagString(ProductTag.ptMedPrynic, "медовый пряник", FactoryType.ftConfectionery));
            m_Array.Add(new ProductTagString(ProductTag.ptMyach, "мяч", FactoryType.ftPlastic));

            m_Array.Add(new ProductTagString(ProductTag.ptVinograd, "виноград", FactoryType.ftIsland));
            m_Array.Add(new ProductTagString(ProductTag.ptOlivki, "оливки", FactoryType.ftIsland));
            m_Array.Add(new ProductTagString(ProductTag.ptLaym, "лайм", FactoryType.ftIsland));
            m_Array.Add(new ProductTagString(ProductTag.ptBanan, "банан", FactoryType.ftIsland));
            m_Array.Add(new ProductTagString(ProductTag.ptKokos, "кокос", FactoryType.ftIsland));
            m_Array.Add(new ProductTagString(ProductTag.ptVinogradJele, "виноградное желе", FactoryType.ftJam));
            m_Array.Add(new ProductTagString(ProductTag.ptNaduvnayaLodka, "надувная лодка", FactoryType.ftPlastic));

            m_Array.Add(new ProductTagString(ProductTag.ptDraje, "драже", FactoryType.ftCandy));
            m_Array.Add(new ProductTagString(ProductTag.ptIriska, "ириски", FactoryType.ftCandy));
            m_Array.Add(new ProductTagString(ProductTag.ptBecon, "бекон", FactoryType.ftAnimal));
            m_Array.Add(new ProductTagString(ProductTag.ptBecon, "корм для свиней", FactoryType.ftKorm));
            m_Array.Add(new ProductTagString(ProductTag.ptGlazirBecon, "глазированный бекон", FactoryType.ftSnack));
            m_Array.Add(new ProductTagString(ProductTag.ptSousChili, "соус чили", FactoryType.ftMexican));
            m_Array.Add(new ProductTagString(ProductTag.ptBurrito, "буррито", FactoryType.ftMexican));
            m_Array.Add(new ProductTagString(ProductTag.ptNachos, "начос", FactoryType.ftMexican));
            m_Array.Add(new ProductTagString(ProductTag.ptLaymPirog, "лаймовый пирог", FactoryType.ftConfectionery));
            m_Array.Add(new ProductTagString(ProductTag.ptKokosMakaruna, "кокосовые макаруны", FactoryType.ftConfectionery));
            m_Array.Add(new ProductTagString(ProductTag.ptKaramelPalochka, "карамельная палочка", FactoryType.ftCandy));
            m_Array.Add(new ProductTagString(ProductTag.ptChocolade, "шоколад", FactoryType.ftCandy));
            m_Array.Add(new ProductTagString(ProductTag.ptLedenec, "леденец", FactoryType.ftCandy));



        }
    };
}