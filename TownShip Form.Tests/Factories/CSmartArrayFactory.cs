using TownShip_Form.Kernal;
// <copyright file="CSmartArrayFactory.cs" company="Microsoft">Copyright © Microsoft 2018</copyright>

using System;
using Microsoft.Pex.Framework;

namespace TownShip_Form.Kernal
{
    /// <summary>A factory for TownShip_Form.Kernal.CSmartArray instances</summary>
    public static partial class CSmartArrayFactory
    {
        /// <summary>A factory for TownShip_Form.Kernal.CSmartArray instances</summary>
        [PexFactoryMethod(typeof(CSmartArray))]
        public static CSmartArray Create()
        {
            CSmartArray cSmartArray = new CSmartArray();
            return cSmartArray;

            // TODO: Edit factory method of CSmartArray
            // This method should be able to configure the object in all possible ways.
            // Add as many parameters as needed,
            // and assign their values to each field by using the API.
        }
    }

    public static partial class CProductCalcFactory
    {
        public static CProductCalc Create()
        {
            CProductCalc cProductCalc = new CProductCalc();
            return cProductCalc;
        }
    }

    //public enum FactType
    //{
    //    ftBackary = 1,
    //    ftMilk,
    //    ftWeaving,
    //    ftSewing,
    //    ftSnack,
    //    ftFastFood,
    //    ftRubber,
    //    ftSugar,
    //    ftPaper,
    //    ftIceCream,
    //    ftConfectionery,
    //    ftAnimal,
    //    ftKorm,
    //    ftPlastic,
    //    ftJamFactory,
    //    ftCandyFactory
    //}
    public static partial class FactoryFactory
    {
        public static CFactory CreateFactory(Kernal.FactoryType type)
        {
            CFactory pFact = null;
            switch (type)
            {
                case FactoryType.ftAnimal:
                    pFact = new CAnimalFactory();
                    break;
                case FactoryType.ftBakery:
                    pFact = new CBakery();
                    break;
                case FactoryType.ftConfectionery:
                    pFact = new CСonfectioneryFactory();
                    break;
                case FactoryType.ftFastFood:
                    pFact = new CFastFoodFactory();
                    break;
                case FactoryType.ftIceCream:
                    pFact = new CIceCreamFactory();
                    break;
                case FactoryType.ftJam:
                    pFact = new CJamFactory();
                    break;
                case FactoryType.ftKorm:
                    pFact = new CKormFactory();
                    break;
                case FactoryType.ftMilk:
                    pFact = new CMilkFactory();
                    break;
                case FactoryType.ftPaper:
                    pFact = new CPaperFactory();
                    break;
                case FactoryType.ftPlastic:
                    pFact = new CPlasticFactory();
                    break;
                case FactoryType.ftSewing:
                    pFact = new CSewingFactory();
                    break;
                case FactoryType.ftSnack:
                    pFact = new CSnackFactory();
                    break;
                case FactoryType.ftSugar:
                    pFact = new CSugarFactory();
                    break;
                case FactoryType.ftWeaving:
                    pFact = new CWeavingFactory();
                    break;
                case FactoryType.ftRubber:
                    pFact = new CRubberFactory();
                    break;
                case FactoryType.ftCandy:
                    pFact = new CCandyFactory();
                    break;
                case FactoryType.ftMexican:
                    pFact = new CMexicanRestaurant();
                    break;

            }
            return pFact;
        }
    }
}
